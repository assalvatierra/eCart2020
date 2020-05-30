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

        public List<CartDetail> AddItemToCart(int id, int qty, decimal price, List<CartDetail> cartSession, string userId)
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
            var storeItem = cartdb.GetStoreItems().Where(c => c.Id == cartItem.StoreItemId).FirstOrDefault();
            var store = cartdb.GetStoreDetails().Where(s=>s.Id == cartItem.StoreItem.StoreDetailId).FirstOrDefault();
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
            var storeItem = cartdb.GetStoreItems().Where(c => c.Id == id).FirstOrDefault();

            return new CartItem
            {
                StoreItem = storeItem,
                ItemQty = qty,
                StoreItemId = id,
                CartItemStatusId = 1,
                ItemOrder = storeItem.Id.ToString(),
            };
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
                    //and save to the database
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
                //and save to the database
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
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public int SaveKioskOrder(List<CartDetail> cartDetails, string userId, int cartId)
        {
            try
            {
                var cart = cartDetails.Find(s => s.Id == cartId);
                var OrderId = 0;
                var userID = userId;
                var ACTIVE = 2;
                var storeID = cart.StoreDetailId;
                //check if cart is active, then change status to submitted 
                //and save to the database
                if (cart.CartStatusId == 1)
                {
                    var tempCartId = AddCartDetails(cart);
                    if (tempCartId > 0)
                    {
                        //add cart items
                        var AddCartItemsRes = AddCartItems(cart.CartItems.ToList(), tempCartId);
                        if (!AddCartItemsRes)
                        {
                            return OrderId;
                        }

                        //addPaymentDetails
                        if (cart.PaymentDetails != null)
                        {
                            var AddCartPayments = AddPaymentDetails(cart.PaymentDetails.ToList(), tempCartId);
                            if (!AddCartPayments)
                            {
                                return OrderId;
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
                                //return true;
                                OrderId = tempCartId;
                            }
                        }
                    }
                }
                return OrderId;
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }


        public int AddCartDetails(CartDetail cart)
        {
            CartDetail tempCart = new CartDetail()
            {
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
                return cartdb.GetStorePickupPoint(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartDetail> getShopperCarts(int userId)
        {
            var cartList =  cartdb.GetCartDetails().Where(s => s.UserDetailId == userId).OrderByDescending(s => s.Id).ToList();
            return cartList;
        }

        public List<CartHistory> getCartHistory(int id)
        {
            return cartdb.GetCartHistories().Where(s => s.CartDetailId == id).ToList();
        }

        public List<CartActivity> getCartDeliveryActivities(int id)
        {
            return cartdb.GetCartActivities().Where(c => c.CartDeliveryId == id).OrderByDescending(s=>s.Id).ToList();
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
                    UserId = userId, 

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

                var editResult = cartdb.EditCartDetails(cart);
                if (editResult)
                {
                    var addRecord = addCartHistory(cartId, cartStatusId, userId);
                    if (addRecord)
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
                CartItem cartItem = cartdb.GetCartItems().Where(c=>c.Id == id).FirstOrDefault();
                cartItem.CartItemStatusId = 2;
                cartdb.EditCartItem(cartItem);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks)
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

                return cartdb.AddCartDelivery(deliveryDetails);
             
            }
            catch
            {
                return false;
            }
        }

        public void updateCartDelivery(CartDelivery cartDelivery)
        {
            try
            {
                var deliveryDetails = cartdb.GetCartDeliveries().Where(c => c.Id == cartDelivery.Id).FirstOrDefault();
                deliveryDetails.RiderDetailId = cartDelivery.RiderDetailId;
                deliveryDetails.dtDelivery = cartDelivery.dtDelivery;
                deliveryDetails.Address = cartDelivery.Address;
                deliveryDetails.Remarks = cartDelivery.Remarks;

               
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
                var deliveryDetails = cartdb.GetCartDeliveries().Where(c => c.Id == id).FirstOrDefault();
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

        public StoreItem GetStoreItem(int id)
        {
            try
            {
                return cartdb.GetStoreItem(id);
            }catch{
                return null;
            }
        }


        public List<PaymentDetail> GetCartPaymentDetails(int id)
        {
            try
            {
                return cartdb.GetCartDetail(id).PaymentDetails.ToList();
            }
            catch
            {
                return null;
            }
        }


        public List<CartDelivery> GetCartDeliveries(int id)
        {
            try
            {
                return cartdb.GetCartDetail(id).CartDeliveries.ToList();
            }
            catch
            {
                return null;
            }
        }

        #region  Store Kiosk

        public bool AddStoreKiosk(StoreKiosk storeKiosk)
        {
            return cartdb.AddStoreKiosk(storeKiosk);
        }

        public bool AddStoreKioskOrder(StoreKioskOrder kioskOrder)
        {
            return cartdb.AddStoreKioskOrder(kioskOrder);
        }

        public bool UpdateStoreKiosk(StoreKiosk storeKiosk)
        {
            return cartdb.EditStoreKiosk(storeKiosk);
        }

        public bool UpdateStoreKioskOrder(StoreKioskOrder kioskOrder)
        {
            return cartdb.EditStoreKioskOrder(kioskOrder);
        }

        public StoreKiosk GetStoreKiosk(int id)
        {
            return cartdb.GetStoreKiosks().Where(s => s.Id == id).FirstOrDefault();
        }

        public StoreKioskOrder GetStoreKioskOrder(int id)
        {
            return cartdb.GetStoreKioskOrders().Where(s => s.Id == id).FirstOrDefault();
        }

        public List<StoreKioskOrder> GetStoreKioskOrderList(int storeId)
        {
            return cartdb.GetStoreKioskOrders().Where(s => s.StoreKiosk.StoreDetailId == storeId).ToList();
        }

        public List<StoreKiosk> GetStoreKioskList(int id)
        {
            return cartdb.GetStoreKiosks().Where(s => s.StoreDetailId == id).ToList();
        }

        #endregion
    }
}