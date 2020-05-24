using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using eCartInterfaces;
using eCartModels;
using eCartDbLayer;

namespace eCartServices
{
    public class CartMgr: iCartMgr
    {
        protected ecartdbContainer db = new ecartdbContainer();
        private iCartDb cartdb = new CartDbLayer();

        public void setDbLayer(iCartDb cartdblayer)
        {
            this.cartdb = cartdblayer;
        }

        public int getDefaultPickupPointId(int storeId)
        {
            var pickupPoint = cartdb.GetStorePickupPoints(storeId).FirstOrDefault();

            return pickupPoint.Id;
        }

        public bool addItemToCart(int id, int qty, decimal price)
        {
            //try
            //{
            //    //create cartItem
            //    var newItem = CreateCartItem(id, qty);

            //    //create cartDetails
            //    var newCart = CreateCart(newItem);

            //    //get current cart from session
            //    var cartList = getCartDetails();
            //    var isAssigned = false;

            //    if (cartList != null)
            //    {
            //        return false;
            //    }

            //    foreach (var cart in cartList)
            //        {
            //            //if (cart.StoreId == newItem.StoreId)
            //            //{
            //            //    if (cart.CartStatus == 1)
            //            //    {
            //            //        //add new item to the current active cart
            //            //        newCart.Id = cartList.LastOrDefault().Id;
            //            //        cart.cartItems.Add(newItem);
            //            //        isAssigned = true;
            //            //    }
            //            //    else
            //            //    {
            //            //        newCart.Id = cartList.LastOrDefault().Id + 1;
            //            //        cartList.Add(newCart);
            //            //        isAssigned = true;
            //            //    }
            //            //}
            //        }

            //    if (isAssigned == false)
            //    {
            //        newCart.Id = cartList.Count() + 1;
            //        cartList.Add(newCart);
            //        isAssigned = true;
            //    }

            //    return isAssigned;

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //    return false;
            //}

            return false;
        }

        public List<CartDetail> addItemToCart(int id, int qty, decimal price, List<CartDetail> cartSession, string userId)
        {
            try
            {
                //create cartItem
                var newItem = CreateCartItem(id, qty);

                //create cartDetails
                var newCart = CreateCart(newItem, userId);

                //get current cart from session
                var cartList = cartSession;
                var isAssigned = false;

                if (cartList != null)
                {
                    //return false;
                }

                foreach (var cart in cartList)
                {
                    if (cart.StoreDetailId == newItem.StoreItem.StoreDetailId)
                    {
                        if (cart.CartStatusId == 1)
                        {
                            //add new item to the current active cart
                            newCart.Id = cartList.LastOrDefault().Id;
                            cart.CartItems.Add(newItem);
                            isAssigned = true;
                        }
                        else
                        {
                            newCart.Id = cartList.LastOrDefault().Id + 1;
                            cartList.Add(newCart);
                            isAssigned = true;
                        }
                    }
                }

                if (isAssigned == false)
                {
                    newCart.Id = cartList.Count() + 1;
                    cartList.Add(newCart);
                    isAssigned = true;
                }

                if (isAssigned)
                {
                    return cartList;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        //create default cart
        private CartDetail CreateCart( CartItem cartItem,string userId)
        {
            var storeItem = db.StoreItems.Find(cartItem.StoreItemId);
            var store = db.StoreDetails.Find(cartItem.StoreItem.StoreDetailId);
            var storePickupPointId = getDefaultPickupPointId(store.Id);
            return new CartDetail
            {
                StoreDetailId = store.Id,
                StoreDetail = store,
                CartStatusId = 1,
                StorePickupPoint = GetStorePickup(storePickupPointId),
                DeliveryType = "Pickup",
                DtPickup = DateTime.Now.AddHours(4),
                CartItems = new List<CartItem> { cartItem },
                PaymentDetails = null,
                UserDetailId = cartdb.GetUserDetails(userId).Id
            };
        }

        //create default cart item
        private CartItem CreateCartItem(int id, int qty)
        {
            var storeItem = db.StoreItems.Find(id);

            return new CartItem
            {
                StoreItem = storeItem,
                ItemQty = qty,
                StoreItemId = id,
                CartItemStatusId = 1
            };
        }

        public void addCartItemToDb(CartItem cartItem)
        {
            try
            {
                db.CartItems.Add(cartItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool addCartDetailToDb(CartDetail cartDetail)
        {
            try
            {
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //throw ex;
                return false;
            }
        }

        public int getCartInfo(int id)
        {
            throw new NotImplementedException();
        }

        public List<cCart> getCartItems()
        {
            var cartDetails = getCartDetails();
            var cartItems = new List<cCart>();

            if (cartDetails == null)
            {
                return null;
            }

            foreach(var cart in cartDetails)
            {
                foreach (var item in cart.cartItems)
                {
                    cartItems.Add(item);
                }
            }


            return cartItems;
        }


        public List<cCartDetails> getCartDetails()
        {
            return null;
            //move this code to web
            //return (List<cCartDetails>)HttpContext.Current.Session["CARTDETAILS"] ?? null;
        }


        public List<CartItem> getCartSummary()
        {
            var carts = getCartItems();

            //transfer cCart to cartItems 
            List<CartItem> cartItems = new List<CartItem>();

            var order = 1;
            foreach (var item in carts)
            {
                var storeItem = db.StoreItems.Find(item.Id);

                cartItems.Add(new CartItem
                {
                    CartDetailId = 1,
                    StoreItemId = item.Id,
                    StoreItem = storeItem,
                    ItemQty = item.Qty,
                    ItemOrder = order.ToString(),
                    CartItemStatusId = 1,
                    Remarks1 = "",
                    Remarks2 = ""
                });

                order++;
            }

            return cartItems;
        }

        public List<CartDetail> getCartDetailsSummary()
        {
            List<CartDetail> cartDetail = new List<CartDetail>();

            var cartList = getCartDetails();

            if(cartList == null)
            {
                return null;
            }

            foreach (var cart in cartList)
            {
                //create CartDetails for current group
                var tempCartDetail = CreateCartDetail(cart); //KEY = StoredDetail.Id
                cartDetail.Add(tempCartDetail);

            }

            return cartDetail;
        }


        public List<PaymentReceiver> GetPaymentRecievers()
        {
            try
            {
                return cartdb.GetPaymentRecievers();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //create a new cart per store
        public CartDetail CreateCartDetail(cCartDetails cart)
        {
            //StoreDetail tempStore = db.StoreDetails.Find(cart.StoreId);
            //StorePickupPoint pickup = db.StorePickupPoints.Find(cart.PickupPointId);
            CartDetail cartDetails = new CartDetail
            {
                Id = cart.Id,
                UserDetailId = getUserId(),  
                StoreDetailId = cart.StoreId,
                StoreDetail = cartdb.GetStoreDetail(cart.StoreId),
                CartStatusId = cart.CartStatus,               //default: active
                CartStatu = cartdb.GetCartStatus(cart.CartStatus),
                StorePickupPoint = cartdb.GetStorePickupPoint(cart.PickupPointId),
                StorePickupPointId = cart.PickupPointId,
                DeliveryType = cart.DeliveryType,
                DtPickup = cart.DtPickup,
                CartItems = getCartItems(cart),
                PaymentDetails = getPaymentDetails(cart)
            };

            return cartDetails;
        }

        public int getUserId()
        {
            return -1;
            //move to web part            
            //return HttpContext.Current.Session["USERID"] != null ? (int)HttpContext.Current.Session["USERID"] : 0;
        }

        public string getUserAccID()
        {

            var UserDetailID = getUserId();
            var userDetail = db.UserDetails.Find(UserDetailID);
            return userDetail.UserId;
        }


        //transfer to db cartItem object
        public List<CartItem> getCartItems(cCartDetails cart)
        {
            var items = new List<CartItem>();
            var cItems = cart.cartItems;
            int order = 1;

            foreach (var item in cart.cartItems)
            {
                items.Add(new CartItem
                {
                    CartDetailId = cart.Id,
                    CartItemStatusId = cart.CartStatus,
                    ItemOrder = order.ToString(),
                    ItemQty = item.Qty,
                    StoreItemId = item.Id,
                    StoreItem = cartdb.GetStoreItem(item.Id),
                    Remarks1 = item.remarks1,
                    Remarks2 = item.remarks2
                });
                order++;
            }

            return items;
        }


        //transfer to db cartItem object
        public List<PaymentDetail> getPaymentDetails(cCartDetails cart)
        {

            if (cart.cartPayments != null)
            {

                var paymentDetails = new List<PaymentDetail>();

                foreach (var item in cart.cartPayments)
                {
                    paymentDetails.Add(new PaymentDetail
                    {
                      Amount = 0,
                      CartDetailId = cart.Id,
                      dtPayment = DateTime.Now,
                      PaymentPartyId = item.PaymentPartyId,
                      PartyInfo = "",
                      PaymentReceiverId = item.PaymentRecieverId,
                      PaymentStatusId = 1,  //pending
                      ReceiverInfo = ""
                    });
                }
                return paymentDetails;
            }
            return null;
        }

        public bool UpdateCartPickupPoint(int cartId, int pickupPointId, List<CartDetail> cart)
        {
            try
            {
                var cartDetail = cart.Find(s => s.Id == cartId);
                cartDetail.StorePickupPoint = cartdb.GetStorePickupPoint(pickupPointId);
                cartDetail.DeliveryType = "Pickup";
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public bool UpdateCartAsDelivery(int cartId, List<CartDetail> cart)
        {
            try
            {
                var cartDetail = cart.Find(s => s.Id == cartId);
                var defaultPickupPointId = getDefaultPickupPointId(cartDetail.StoreDetailId);
                cartDetail.StorePickupPoint = cartdb.GetStorePickupPoint(defaultPickupPointId);
                cartDetail.DeliveryType = "Delivery";
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool RemoveCartItem(List<CartDetail> carts, int id)
        {
            try
            {
                var cartList = carts;

                foreach(var cart in cartList)
                {
                    foreach (var item in cart.CartItems)
                    {
                        if (item.StoreItemId == id)
                        {
                            //find the item and remove from the cart list
                            cart.CartItems.Remove(item);

                            //check if cart is empty, delete cart
                            if (cart.CartItems.Count == 0)
                            {
                                cartList.Remove(cart);
                            }

                            return true;
                        }
                    }
                }
                
                //no item found
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public bool saveOrder(List<CartDetail> cartDetails, string userId)
        {
            try
            {
                var userID = userId;
                var isValid = false;

                foreach (var cart in cartDetails)
                {
                    var storeID = cart.StoreDetailId;
                    //check if cart is active, then change status to submitted 
                    //and save to the db
                    if (cart.CartStatusId == 1 )
                    {
                        //update cart status to Submitted
                        //var cartStatus = db.CartStatus.Find(2);  //Submitted
                        cart.CartStatu = cartdb.GetCartStatus(2);

                        var addResult = cartdb.AddCartDetails(cart);
                        if (addResult) {

                            if (addCartHistory(cart, 2, userID))
                            {
                                if (removeCartSession(storeID))
                                {
                                    isValid = true;
                                }
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (isValid)
                {
                    if (cartdb.Save())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public bool SaveOrder(CartDetail cart, string userId)
        {
            try
            {
                var userID = userId;
                var ACTIVE = 2;
                var storeID = cart.StoreDetailId;
                //check if cart is active, then change status to submitted 
                //and save to the db
                if (cart.CartStatusId == 1)
                {
                    cart.CartStatusId = 2;  //Submitted
                    cart.StoreDetailId = cart.StoreDetailId;
                    cart.StoreDetail = null;
                    cart.StorePickupPointId = cart.StorePickupPoint.Id;
                    cart.StorePickupPoint = null;
                    foreach (var item in cart.CartItems)
                    {
                        item.CartDetail = cart;
                        item.CartDetailId = cart.Id;
                        item.ItemOrder = "1";

                    }


                    var addRes = cartdb.AddCartDetails(cart);
                    if (addRes)
                    {
                        //add cart history
                        var addHistory = addCartHistory(cart, ACTIVE, userID);
                        if (addHistory)
                        {
                            //remove cart from session
                            var remResult = removeCartSession(storeID);
                            if (remResult)
                            {
                                if (cartdb.Save())
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public void updateCartDetailsStatus(int cartId, string status)
        {
            var cart = getCartDetails().Find(s=> s.Id == cartId);
            int statusId = db.CartStatus.Where(s=>s.Name.ToLower() == status.ToLower()).FirstOrDefault() != null ?
                db.CartStatus.Where(s => s.Name.ToLower() == status.ToLower()).FirstOrDefault().Id : 1;
            cart.CartStatus = statusId;

        }


        public bool SetCartPaymentReceiver(int cartId, int recieverId, List<CartDetail> cart)
        {
            try
            {
                var cartList = cart;
                var cartpayment = new PaymentDetail()
                {
                    Id = 1,
                    PaymentReceiverId = recieverId,
                    Amount = 0,
                    dtPayment = DateTime.Now,
                    PaymentStatusId = 1, //pending
                    PartyInfo = "",
                    PaymentPartyId = 1,  //shopper
                    ReceiverInfo = ""
                };

                cartList.ForEach((c) => {
                    if (c.Id == cartId)
                    {
                        //c.pay = db.PaymentReceivers.Find(recieverId).Description;

                        if (c.PaymentDetails == null )
                        {
                            c.PaymentDetails = new List<PaymentDetail>();

                            //create new cartPayment
                            c.PaymentDetails.Add(cartpayment);
                        }
                        else
                        {
                            //add to the current list
                            c.PaymentDetails.Add(cartpayment);
                        }
                    }
                });

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool SetCartPickupDate(int cartId, DateTime pickupdate, List<CartDetail> cart)
        {
            try
            {
              
                //get session cart
                var cartDetails = cart.Find(c=>c.Id == cartId);
                cartDetails.DtPickup = pickupdate;
                return true;
            }
            catch
            {
                return false;
            }

        }


        public bool removeCartSession(int storeId)
        {
            try
            {
                var cartlist = getCartDetails();
                var cart = cartlist.Find(c=>c.StoreId == storeId);
            
                cartlist.Remove(cart);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<StorePickupPoint> GetStorePickupPoints(int storeId)
        {
            try
            {
                return cartdb.GetStorePickupPoints(storeId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public StorePickupPoint GetStorePickup(int id)
        {
            try
            {
                return db.StorePickupPoints.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartDetail> getShopperCarts(int userId)
        {
            var cartList = db.CartDetails.Where(s => s.UserDetailId == userId).ToList();
            return cartList;
        }

        public List<CartHistory> getCartHistory(int id)
        {
            return db.CartHistories.Where(s => s.CartDetailId == id).ToList();
        }

        public List<CartActivity> getCartDeliveryActivities(int id)
        {
            return db.CartActivities.Where(c => c.CartDeliveryId == id).OrderByDescending(s=>s.Id).ToList();
        }


        public bool addCartHistory(CartDetail cart , int statusId, string userId)
        {
            try
            {
                var cartHistory = new CartHistory
                {
                    CartDetail = cart,
                    CartStatusId = statusId,
                    dtStatus = DateTime.Now,
                    UserId = userId,
                };

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string setDBCartStatus(int cartId, int cartStatusId, string userId)
        {
            try
            {
                CartDetail cart = db.CartDetails.Find(cartId);
                CartStatus cartStatus = db.CartStatus.Find(cartStatusId);

                var userID = getUserAccID();

                cart.CartStatu = cartStatus;
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();

                addCartHistory(cart, cartStatus.Id, userID);
                return "cart status updated";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string setCartStatusCancelled(int cartId, string userId)
        {

            var cartstatusId = db.CartDetails.Find(cartId).CartStatusId;
            if (cartstatusId < 3) 
            {
                // if cart is still on Pending or Active, 
                // user is allowed to cancel the cart
                setDBCartStatus(cartId, 6, userId);
                return "Order Cancelled";
            }
            else
            {
                return "Order is now being processed";
            }

        }

        public void removeDBCartItem(int id, int statusId)
        {

            try
            {
                CartItem cartItem = db.CartItems.Find(id);
                cartItem.CartItemStatusId = 2;

                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void addDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks)
        {
            try
            {
                var deliveryDetails = new CartDelivery
                {
                    CartDetailId = id,
                    dtDelivery = date,
                    Address = address,
                    RiderDetailId = riderId,
                    Remarks = remarks
                };

                db.CartDeliveries.Add(deliveryDetails);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateCartDelivery(CartDelivery cartDelivery)
        {
            try
            {
                var deliveryDetails = db.CartDeliveries.Find(cartDelivery.Id);
                deliveryDetails.RiderDetailId = cartDelivery.RiderDetailId;
                deliveryDetails.dtDelivery = cartDelivery.dtDelivery;
                deliveryDetails.Address = cartDelivery.Address;
                deliveryDetails.Remarks = cartDelivery.Remarks;

                db.Entry(deliveryDetails).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void removeCartdelivery(int id)
        {
            try
            {
            var deliveryDetails = db.CartDeliveries.Find(id);

            db.CartDeliveries.Remove(deliveryDetails);
            db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public UserDetail GetUserDetails(string userId)
        {
            try
            {
               var user = cartdb.GetUserDetails(userId);
               return user;
            }
            catch
            {
                return null;
            }
        }
    }
}