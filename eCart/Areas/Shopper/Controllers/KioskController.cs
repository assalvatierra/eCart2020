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
        public ActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("PageError");

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account", new { area = "" }); 

            var storeMgr = store.StoreMgr;
            var userid = HttpContext.User.Identity.GetUserId();
            var storedetail = storeMgr.GetStoreDetailByLoginId(userid);
            if (store == null)
                return RedirectToAction("PageError");

            int storeId = (int)storedetail.Id;
            Session["STOREID"] = storeId;

            if (storeId == 0)
                return RedirectToAction("PageError");

            var storeKiosks = storeMgr.GetStoreKiosks(storeId);

            //Implement modal window to select kiosk if store has multiple kiosks
            //if(storeKiosks!=null)
            //{
            //    StoreKiosk kiosk = storeKiosks.First();
            //    Session["KIOSK"] = kiosk;

            //}

            var storeItems = storeMgr.getStoreItems(storeId);

            //store details to show
            var storeDetails = storeMgr.getStoreDetails(storeId);
            var defaultImg = "/img/placeholders/placeholder-product.png";
            ViewBag.StoreName = storeDetails.Name;
            ViewBag.StoreAddress = storeDetails.Address;
            ViewBag.StoreImg = storeDetails.StoreImages.FirstOrDefault() != null ? storeDetails.StoreImages.FirstOrDefault().ImageUrl : defaultImg;
            ViewBag.KioskId = (int)id;

            return View(storeItems);
        }

        [HttpPost]
        public bool AddToCart(int id, int qty)
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
            catch 
            {
                return false;
            }
        }

        [HttpPost]
        public bool RemoveCartItem(int id)
        {
            try
            {

                iCartManager cart = (iCartManager)Session["USERCART"];
                if (cart == null)
                    return false;

                int currentstoreid = (int)Session["STOREID"];
                cart.RemoveItem(currentstoreid,id);
                Session["USERCART"] = cart;

                return true;
            }
            catch (Exception e)
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

        public ActionResult CartCheckout(int kioskId, string customer)
        {
            iCartManager cart = (iCartManager)Session["USERCART"];
            if (cart == null)
            {
                cart = (iCartManager)(new CartManager());
                Session["USERCART"] = cart;
            }

            var cartItems = cart.ConvertCartDetails();

            ViewBag.Name = customer;
            ViewBag.KioskId = kioskId;

            return View(cartItems);

        }

        public ActionResult ClearCart()
        {
            Session["USERCART"] = (iCartManager)(new CartManager());
            return RedirectToAction("index");
        }

        public ActionResult SelectKiosk()
        {
            var storeMgr = store.StoreMgr;

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account", new { area = "" });

            var userid = HttpContext.User.Identity.GetUserId();
            var storedetail = storeMgr.GetStoreDetailByLoginId(userid);
            if (storedetail == null)
                return RedirectToAction("PageError");

            var kioskList = store.CartMgr.GetStoreKioskList(storedetail.Id);

            return View(kioskList.ToList());
        }


        public string GetUserId()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return userId;
        }


        public bool SubmitOrder(int id, int kioskId, string customer)
        {
            try
            {
                iCartManager cart = (iCartManager)Session["USERCART"];
                if (cart == null)
                {
                    cart = (iCartManager)(new CartManager());
                    Session["USERCART"] = cart;
                }

                var cartSession = cart.ConvertCartDetails();
               
                if (cartSession == null)
                {
                    return false;
                }

                var userId = GetUserId();
                cartSession.FirstOrDefault().StoreDetail = null;
                cartSession.FirstOrDefault().UserDetailId = store.CartMgr.GetUserDetails(userId).Id;
                var cartId = store.CartMgr.SaveKioskOrder(cartSession, userId, id);
                if (cartId > 0)
                {  //save to db

                    var kioskOrder = new StoreKioskOrder()
                    {
                        CartDetailId = cartId,
                        StoreKioskId = kioskId,
                        DtOrder = DateTime.Now,
                        Customer = customer.Trim()
                    };

                    //save storeKioskOrder
                    var kioskOrderRes = store.CartMgr.AddStoreKioskOrder(kioskOrder);
                    if (kioskOrderRes)
                    {
                        ClearCart();
                        return true;
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult PageError()
        {
            return View();
        }
    }
}