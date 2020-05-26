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

            throw new NotImplementedException();
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
                StorePickupPointId = storePickupPointId,
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
                CartItemStatusId = 1,
                ItemOrder = storeItem.Id.ToString(),
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
                cartDetail.StorePickupPointId = pickupPointId;
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
                var ACTIVE = 2;
                var isValid = false;

                foreach (var cart in cartDetails)
                {
                    var storeID = cart.StoreDetailId;
                    //check if cart is active, then change status to submitted 
                    //and save to the db
                    if (cart.CartStatusId == 1 )
                    {
                        var tempCartId = AddCartDetails(cart);
                        if (tempCartId > 0)
                        {
                            //add cart items
                            var AddCartItemsRes = AddCartItems(cart.CartItems.ToList(), tempCartId);
                            if (!AddCartItemsRes)
                            {
                                return false;
                            }

                            //addPaymentDetails
                            if (cart.PaymentDetails != null)
                            {
                                var AddCartPayments = AddPaymentDetails(cart.PaymentDetails.ToList(), tempCartId);
                                if (!AddCartPayments)
                                {
                                    return false;
                                }

                            }
                            //add cart history
                            var addHistory = addCartHistory(tempCartId, ACTIVE, userID);
                            if (addHistory)
                            {
                                isValid = true;
                            }

                        }
                    }
                }

                if (isValid)
                {
                    cartDetails.RemoveAll(c => c.CartItems.Count() > 0);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveOrder(List<CartDetail> cartDetails, string userId, int cartId)
        {
            try
            {
                var cart = cartDetails.Find(s => s.Id == cartId);

                var userID = userId;
                var ACTIVE = 2;
                var storeID = cart.StoreDetailId;
                //check if cart is active, then change status to submitted 
                //and save to the db
                if (cart.CartStatusId == 1)
                {
                    var tempCartId = AddCartDetails(cart);
                    if (tempCartId > 0)
                    {
                        //add cart items
                        var AddCartItemsRes = AddCartItems(cart.CartItems.ToList(), tempCartId);
                        if (!AddCartItemsRes)
                        {
                            return false;
                        }

                        //addPaymentDetails
                        if (cart.PaymentDetails != null )
                        {
                            var AddCartPayments = AddPaymentDetails(cart.PaymentDetails.ToList(), tempCartId);
                            if (!AddCartPayments)
                            {
                                return false;
                            }

                        }

                        //add cart history
                        var addHistory = addCartHistory(tempCartId, ACTIVE, userID);
                        if (addHistory)
                        {
                            //remove cart from session
                            var remResult = RemoveCartSession(cart, cartDetails);
                            if (remResult)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public int AddCartDetails(CartDetail cart)
        {
            CartDetail tempCart = new CartDetail()
            {
                Id = cart.Id,
                CartStatusId = 2,  //Submitted
                DeliveryType = cart.DeliveryType,
                DtPickup = cart.DtPickup,
                StoreDetailId = cart.StoreDetailId,
                StorePickupPointId = cart.StorePickupPointId,
                UserDetailId = cart.UserDetailId
            };

            var addResult = cartdb.AddCartDetails(tempCart);
            if (addResult)
            {
                return tempCart.Id;
            }

            return 0;
        }

        public bool AddCartItems(List<CartItem> cartItems, int cartId)
        {
            //add cart items
            foreach (var item in cartItems)
            {
                CartItem tempItem = new CartItem()
                {
                    CartDetailId = cartId,
                    CartItemStatusId = item.CartItemStatusId,
                    ItemOrder = item.ItemOrder,
                    ItemQty = item.ItemQty,
                    Remarks1 = item.Remarks1,
                    Remarks2 = item.Remarks2,
                    StoreItemId = item.StoreItemId
                };

                var result = cartdb.AddCartItem(tempItem);
                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool AddPaymentDetails(List<PaymentDetail> paymentDetails, int cartId)
        {
            //add cart items
            foreach (var item in paymentDetails)
            {
                PaymentDetail tempPayment = new PaymentDetail()
                {
                    CartDetailId = cartId,
                    Amount = item.Amount,
                    dtPayment = item.dtPayment,
                    PaymentPartyId = item.PaymentPartyId,
                    PaymentReceiverId = item.PaymentReceiverId,
                    PartyInfo = item.PartyInfo,
                    PaymentStatusId = item.PaymentStatusId,
                    ReceiverInfo = item.ReceiverInfo
                };

                var result = cartdb.AddPaymentDetails(tempPayment);
                if (result == false)
                {
                    return false;
                }
            }

            return true;
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


        public bool RemoveCartSession(int storeId, List<CartDetail> cartList)
        {
            try
            {
                var cartDetail = cartList.Find(c=>c.StoreDetailId == storeId);

                cartList.Remove(cartDetail);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool RemoveCartSession(CartDetail cart, List<CartDetail> cartList)
        {
            try
            {
                cartList.Remove(cart);
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
            var cartList = db.CartDetails.Where(s => s.UserDetailId == userId).OrderByDescending(s => s.Id).ToList();
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


        public bool addCartHistory(int cartId , int statusId, string userId)
        {
            try
            {
                var cartHistory = new CartHistory
                {
                    CartDetailId = cartId,
                    CartStatusId = statusId,
                    dtStatus = DateTime.Now,
                    UserId = GetUserDetails(userId).Id.ToString(), //TODO : increase UserId max length to 40

                };

                cartdb.AddCartHistory(cartHistory);
               

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool SetDBCartStatus(int cartId, int cartStatusId, string userId)
        {
            try
            {
                CartDetail cart = cartdb.GetCartDetail(cartId);
                cart.CartStatusId = cartStatusId;
                if (cartdb.EditCartDetails(cart))
                {
                    if (addCartHistory(cartId, cartStatusId, userId))
                    {
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

        public string setCartStatusCancelled(int cartId, string userId)
        {

            var cartstatusId = cartdb.GetCartDetail(cartId).CartStatusId;
            if (cartstatusId < 3) 
            {
                // if cart is still on Pending or Active, 
                // user is allowed to cancel the cart


                if (SetDBCartStatus(cartId, 6, userId))
                {
                    return "Order Cancelled";
                }
              
            }
            return "Order is now being processed";
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

        public CartDetail GetCartDetail(int id)
        {
            try
            {
                return cartdb.GetCartDetail(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}