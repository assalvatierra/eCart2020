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
            var cart = GetCartDetails();
            if (GetUserId() != null && GetCartDetails() == null)
            {
                 List<CartDetail> cartDetails = new List<CartDetail>();
                 Session["CARTDETAILS"] = cartDetails;
                 //assign user to session
                 Session["USERID"] = GetUserId();
            }

            var cartItems = GetCartDetails();
            ViewBag.IsUserLogged = GetUserId() != null ? true : false;
            return PartialView(cartItems);
        }

        public string GetUserId()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return userId;
        }

        public List<CartDetail> GetCartDetails()
        {
           
            var cartSession = (List<CartDetail>)Session["CARTDETAILS"];
            ViewBag.cart = cartSession;
            return cartSession;
        }


        public void UpdateCartDetails(List<CartDetail> cart)
        {

            if (cart != null)
            {
                Session["CARTDETAILS"] = cart;
            }

        }

        [HttpPost]
        public bool AddToCart(int id, int qty)
        {
            try
            {
                var UserId = GetUserId();
               
                var cartSession = GetCartDetails();
                if (cartSession == null)
                {
                    cartSession = new List<CartDetail>();
                }
                var cart = store.CartMgr.AddItemToCart(id, qty, cartSession, UserId);

                UpdateCartDetails(cart);
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
                var cart = GetCartDetails();
                if (store.CartMgr.RemoveCartItem(cart,id))
                {
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

        public ActionResult CartCheckout()
        {
            var cartMgr = store.CartMgr;

            if(HttpContext.User.Identity.IsAuthenticated)
            { 
                var cartDetails = GetCartDetails();
                ViewBag.PaymentParties = cartMgr.GetPaymentRecievers();
                    string userId = HttpContext.User.Identity.GetUserId();
                ViewBag.UserDetails = cartMgr.GetUserDetails(userId);

                return View(cartDetails);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

        }

        [HttpGet]
        public JsonResult GetStorePickupPoints(int storeId)
        {
            var locations = store.CartMgr.GetStorePickupPoints(storeId).Select(s => new { s.Id, s.Address });

            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStorePickupAddress(int id)
        {
            var location = store.CartMgr.GetStorePickup(id).Address;

            return Json(location, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPaymentRecievers()
        {
            var paymentOptions = store.CartMgr.GetPaymentRecievers().Select(s => new { s.Id, s.Description });
            return Json(paymentOptions, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public bool UpdatePickupPoint(int cartId, int pickupPointId)
        {
            try
            {
                var cartSession = GetCartDetails();
                return store.CartMgr.UpdateCartPickupPoint(cartId, pickupPointId, cartSession);

            }
            catch 
            {
                return false;
            }
        }

        public bool UpdateCartAsDelivery(int cartId)
        {
            try
            {
                var cartSession = GetCartDetails();
                return store.CartMgr.UpdateCartAsDelivery(cartId, cartSession);
            }
            catch 
            {
                return false;
            }

        }

        [HttpPost]
        public bool SetPaymentReceiver(int cartId, int receiverId)
        {
            try
            {
                var cartSession = GetCartDetails();
                receiverId = receiverId != 0 ? receiverId : 1;
                return store.CartMgr.SetCartPaymentReceiver(cartId, receiverId, cartSession);
            }
            catch
            {
                return false;
            }
        }


        [HttpPost]
        public bool SetCartPickupDate(int cartId, DateTime date)
        {
            var cartSession = GetCartDetails();
            return store.CartMgr.SetCartPickupDate(cartId, date, cartSession);

        }


        public bool SubmitOrder(int id)
        {
            try
            {
                var cartSession = GetCartDetails();

                if (cartSession == null)
                {
                    return false;
                }

                var userId = GetUserId();

                if (store.CartMgr.SaveOrder(cartSession, userId, id)) {  //save to db
                  
                    return true;
                 
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool SubmitAllOrder()
        {
            try
            {
                var userId = GetUserId();
                var cartSession = GetCartDetails();
                return store.CartMgr.saveOrder(cartSession, userId); //save to db

            }
            catch
            {
                return false;
            }

        }


        public ActionResult PendingCarts()
        {
            var userId = GetUserId();
            var userDetails = store.CartMgr.GetUserDetails(userId);
            var myCarts = store.CartMgr.getShopperCarts(userDetails.Id);

            ViewBag.UserDetails = userDetails;

            return View(myCarts);
        }

        [HttpGet]
        public string CancelCart(int cartId)
        {
            var userId = GetUserId();
            var result = store.CartMgr.setCartStatusCancelled(cartId, userId.ToString());
            return result;
        }

        public ActionResult CancelCartStatus(int cartId)
        {
            var userId = GetUserId();
            
            return RedirectToAction("PendingCarts", "CartDetails" , new { area = "Shopper" });
        }

    }
}