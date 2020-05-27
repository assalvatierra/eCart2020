using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCartServices;
using eCartModels;
using Microsoft.AspNet.Identity;

namespace eCart.Areas.Shopper.Controllers
{
    public class KioskController : Controller
    {
        StoreFactory store = new StoreFactory();
        // GET: Shopper/Kiosk
        public ActionResult Index()
        {
            var storeMgr = store.StoreMgr;
            var userid = HttpContext.User.Identity.GetUserId();
            var storedetail = storeMgr.GetStoreDetailByLoginId(userid);
            if (store == null)
                return RedirectToAction("PageError");



            int storeId = (int)storedetail.Id;
            Session["STOREID"] = storeId;

            var storeKiosks = storeMgr.GetStoreKiosks(storeId);
            var storeItems = storeMgr.getStoreItems(storeId);

            //store details to show
            var storeDetails = storeMgr.getStoreDetails(storeId);
            var defaultImg = "/img/placeholders/placeholder-product.png";
            ViewBag.StoreName = storeDetails.Name;
            ViewBag.StoreAddress = storeDetails.Address;
            ViewBag.StoreImg = storeDetails.StoreImages.FirstOrDefault() != null ? storeDetails.StoreImages.FirstOrDefault().ImageUrl : defaultImg;

            return View(storeItems);
        }

        [HttpPost]
        public bool AddToCart(int id, int qty, decimal itemPrice)
        {
            try
            {

                iCartManager cart = (iCartManager)Session["USERCART"];
                if (cart == null)
                    cart = (iCartManager)(new CartManager());

                int currentstoreid = (int)Session["STOREID"];
                cart.AddItem(currentstoreid, id, qty);
                Session["USERCART"] = cart;

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public PartialViewResult _CartSummary()
        {

            iCartManager cart = (iCartManager)Session["USERCART"];
            if (cart == null)
            {
                cart = (iCartManager)(new CartManager());
                Session["USERCART"] = cart;
            }

            var cartItems = cart.ConvertCartDetails();

            return PartialView(cartItems);
        }
    }
}