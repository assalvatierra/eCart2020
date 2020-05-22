using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCartServices;
using eCartModels;

namespace eCart.Areas.Shopper.Controllers
{
    public class CartDetailsController : Controller
    {
        StoreFactory store = new StoreFactory();

        // GET: Shopper/Cart
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult _CartSummary()
        {
            var cartItems = GetCartDetails();

            return PartialView(cartItems);
        }


        public List<CartDetail> GetCartDetails()
        {
           
            var cartSession = (List<CartDetail>)Session["CARTDETAILS"];
            ViewBag.cart = cartSession;
            return cartSession;
        }


        public void SetCartDetails(List<CartDetail> cart)
        {

            if (cart != null)
            {
                //var cartSession = new List<CartDetail>();

                //cart.ForEach(c => {
                //    cartSession.Add(c);
                //});

                Session["CARTDETAILS"] = cart;
            }

        }

        [HttpPost]
        public bool AddToCart(int id, int qty, decimal itemPrice)
        {
            try
            {
                var cartSession = GetCartDetails();
                if (cartSession != null )
                {
                    var cart = store.CartMgr.addItemToCart(id, qty, itemPrice, cartSession);

                    SetCartDetails(cart);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet]
        public JsonResult getSession()
        {
            var cartSession = GetCartDetails();
            return Json(cartSession, JsonRequestBehavior.AllowGet);
        }

    }
}