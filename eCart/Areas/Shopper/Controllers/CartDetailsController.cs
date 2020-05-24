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
            var cartItems = GetCartDetails();

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
        public bool AddToCart(int id, int qty, decimal itemPrice)
        {
            try
            {
                var cartSession = GetCartDetails();
                if (cartSession == null)
                {
                    //prepare new cart
                    cartSession = new List<CartDetail>();
                }

                var cart = store.CartMgr.addItemToCart(id, qty, itemPrice, cartSession);
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var cartMgr = store.CartMgr;
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

            //return RedirectToAction("Index", "Home", new { area = "" });
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCartAsDelivery(int cartId)
        {
            try
            {
                var cartSession = GetCartDetails();
                return store.CartMgr.UpdateCartAsDelivery(cartId, cartSession);
            }
            catch (Exception ex)
            {
                throw ex;
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
                var cartSession = GetCartDetails().Find(s => s.Id == id);
                var userId = GetUserId();

                if (cartSession == null)
                {
                    return false;
                }

                return store.CartMgr.SaveOrder(cartSession, userId);  //save to db

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
                var cartSession = GetCartDetails();
                return store.CartMgr.saveOrder(cartSession); //save to db

            }
            catch
            {
                return false;
            }

        }
    }
}