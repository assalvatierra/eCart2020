using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCartModels;
using eCartServices;

namespace eCart
{
    public interface iCartManager
    {
        int AddItem(int storeid, int storeitemid, decimal qty);
        int RemoveItem(int storeid, int storeitemid);

        List<mvCartDetail> GetCartList();
        List<CartDetail> ConvertCartDetails();
    }


    public class CartManager : iCartManager
    {
        List<mvCartDetail> cartlist;

        public CartManager()
        {
            this.cartlist = new List<mvCartDetail>();

        }

        public void SetCartList(List<mvCartDetail> cartDetails)
        {
            this.cartlist = cartDetails;

        }


        public int AddItem(int storeid, int storeitemid, decimal Qty)
        {
            int iret = 0;

            try
            {
                //find cart of the store
                mvCartDetail cart = this.cartlist.Find(c => c.StoreId == storeid);
                if (cart == null)
                {
                    //create cart for the store
                    cart = new mvCartDetail();
                    cart.itemList = new List<mvCartItem>();
                    cart.StoreId = storeid;

                }

                mvCartItem item = cart.itemList.Find(c => c.StoreItemId == storeitemid);
                if (item != null)
                {
                    item.ItemQuantity = item.ItemQuantity + Qty;
                    iret = 1;
                }
                else
                {
                    item = new mvCartItem
                    {
                        StoreId = storeid,
                        StoreItemId = storeitemid,
                        ItemQuantity = Qty
                    };

                    cart.itemList.Add(item);
                    
                    iret = 1;

                    cartlist.Add(cart);
                }
            }
            catch(Exception e)
            {
                iret = -1;
            }

            return iret;
        }

        public List<mvCartDetail> GetCartList()
        {
            return this.cartlist;
        }

        public int RemoveItem(int storeId, int storeitemid)
        {
            int iret = 0;
            try
            {
                mvCartDetail cart = cartlist.Find(c => c.StoreId == storeId);
                if (cart != null)
                {
                    mvCartItem item = cart.itemList.Find(c => c.StoreItemId == storeitemid);
                    if (item != null)
                    {
                        cart.itemList.Remove(item);
                        iret = 1;
                    }
                }

            }
            catch (Exception e)
            {
                iret = -1;
            }

            return iret;

        }


        public List<CartDetail> ConvertCartDetails()
        {
            //helper classes
            StoreFactory store = new StoreFactory();

            List<CartDetail> cartDetails = new List<CartDetail>();
            foreach (var cart in this.cartlist)
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
                    StoreDetail = store.StoreMgr.getStoreDetails(cart.StoreId),
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



    }


    public class mvCartItem
    {
        public int Id;
        public int StoreId;
        public int StoreItemId;
        public Decimal ItemQuantity;
    }

    public class mvCartDetail
    {
        public int Id;
        public int StoreId;
        public int StatusId;
        public List<mvCartItem> itemList; 
    }
}



/*
 
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

    
        public CartManager GetCartSession()
        {

            return (CartManager)Session["CARTDETAILS"];
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


 */
