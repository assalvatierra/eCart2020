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
            var userId = GetUserId();
            if (userId != null && GetCartSession() == null)
            {
                CartManager cartManager = new CartManager();
                Session["CARTDETAILS"] = cartManager;

                //assign user to session
                Session["USERID"] = GetUserId();
            }

            var cartMgr = GetCartSession();
            if (userId == null || cartMgr.GetCartList() == null)
            {
                return PartialView(new List<CartDetail>());
            }


            var cartList = GetCartSession().GetCartList();
          
            var cartDetails = ConvertCartDetails(cartList);
            return PartialView(cartDetails);
        }

        public List<CartDetail> ConvertCartDetails(List<mvCartDetail> cartList)
        {
            List<CartDetail> cartDetails = new List<CartDetail>();
            foreach (var cart in cartList)
            {

                //cartItems 
                List<CartItem> cartItems = new List<CartItem>();

                foreach (var item in cart.itemList)
                {
                    cartItems.Add(new CartItem()
                    {
                        StoreItemId = item.StoreItemId,
                        StoreItem = store.CartMgr.GetStoreItem(item.StoreItemId),
                        ItemQty = item.StoreId,
                        CartItemStatusId = 1,
                        Remarks1 = "",
                        Remarks2 = "",
                        ItemOrder = "1",
                    });
                }
                
                cartDetails.Add(new CartDetail()
                {
                    StoreDetailId = cart.StoreId,
                    CartStatusId = cart.StatusId,
                    CartItems = cartItems,
                    DeliveryType = "Pickup",
                    DtPickup = DateTime.Now,
                    StorePickupPointId = 1,
                });

            }

            return cartDetails;
        }

        public string GetUserId()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return userId;
        }

        public CartManager GetCartDetails()
        {
           
            var cartSession = (CartManager)Session["CARTDETAILS"];
            ViewBag.cart = cartSession;
            return cartSession;
        }

        public CartManager GetCartSession()
        {

            return (CartManager)Session["CARTDETAILS"];
        }

        public void UpdateCartDetails(List<CartDetail> cart)
        {

            if (cart != null)
            {
                Session["CARTDETAILS"] = cart;
            }

        }

        public void UpdateCartSession(CartManager cart)
        {

            if (cart != null)
            {
                Session["CARTDETAILS"] = cart;
            }

        }

        [HttpPost]
        public bool AddToCart(int id, int qty, int storeId)
        {
            try
            {
                var UserId = GetUserId();
                var cartMgr = GetCartSession();
                cartMgr.SetCartList(cartMgr.GetCartList());
                //if (cartSession == null)
                //{
                //    cartSession = new List<CartDetail>();
                //}
                //var cart = store.CartMgr.AddItemToCart(id, qty, itemPrice, cartSession, UserId);
                //UpdateCartDetails(cart);

                var response = cartMgr.AddItem(storeId, id, qty);
                if (response == 1)
                {
                    var carlist = cartMgr.GetCartList();
                    UpdateCartSession(cartMgr);
                    return true;
                }
                return false;
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

                //if (store.CartMgr.RemoveCartItem(cart, id))
                //{
                //    return true;
                //}

                //var response = cartManager.RemoveItem(id);
                //if (response == 1)
                //{
                //    return true;
                //}

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
            var cartSession = GetCartSession();
            return Json(cartSession, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CartCheckout()
        {
            var cartMgr = store.CartMgr;

            if(HttpContext.User.Identity.IsAuthenticated)
            {
                //var cartDetails = GetCartDetails();

                var cartList = GetCartSession().GetCartList();
                var cartDetails = ConvertCartDetails(cartList);

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
                //return store.CartMgr.UpdateCartPickupPoint(cartId, pickupPointId, cartSession);
                return false;
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
                //return store.CartMgr.UpdateCartAsDelivery(cartId, cartSession);
                return false;
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
                //return store.CartMgr.SetCartPaymentReceiver(cartId, receiverId, cartSession);
                return false;
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
            //return store.CartMgr.SetCartPickupDate(cartId, date, cartSession);
            return false;

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

                //if (store.CartMgr.SaveOrder(cartSession, userId, id)) {  //save to db
                  
                //    return true;
                 
                //}

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
                //return store.CartMgr.saveOrder(cartSession, userId); //save to db

                return false;
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