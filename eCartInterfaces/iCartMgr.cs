using System;
using System.Collections.Generic;
using eCartModels;

namespace eCartInterfaces
{
    public interface iCartMgr
    {
        void setDbLayer(iCartDb cartdblayer);
        int getUserId();
        int getCartInfo(int id);
        int getDefaultPickupPointId(int storeId);
        StorePickupPoint GetStorePickup(int id);
        List<cCart> getCartItems();
        List<CartItem> getCartSummary();
        List<CartDetail> getCartDetailsSummary();
        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<CartDetail> getShopperCarts(int userId);
        List<PaymentReceiver> GetPaymentRecievers();
        List<CartHistory> getCartHistory(int id);
        List<CartActivity> getCartDeliveryActivities(int id);
        UserDetail GetUserDetails(string userId);
        CartDetail GetCartDetail(int id);

        bool addItemToCart(int id, int qty, decimal price);
        List<CartDetail> addItemToCart(int id, int qty, decimal price, List<CartDetail> cartSession,string userId);
        void addCartItemToDb(CartItem cartItem);
        bool addCartDetailToDb(CartDetail cartDetail);
        bool addCartHistory(int cartId, int statusId, string userId);
        void addDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks);

        bool UpdateCartPickupPoint(int storeId, int pickupPoint, List<CartDetail> cart);
        bool UpdateCartAsDelivery(int cartId, List<CartDetail> cart);
        void updateCartDetailsStatus(int cartId, string status);
        void updateCartDelivery(CartDelivery cartDelivery);
        bool SetCartPickupDate(int cartId, DateTime pickupdate, List<CartDetail> cart);
        bool SetCartPaymentReceiver(int cartId, int recieverId, List<CartDetail> cart);
        bool SetDBCartStatus(int cartId, int cartStatusId, string userId);
        string setCartStatusCancelled(int cartId, string userId);

        bool RemoveCartItem(List<CartDetail> carts,int id);
        void removeDBCartItem(int id, int statusId);
        void removeCartdelivery(int id);
        bool RemoveCartSession(int storeId, List<CartDetail> cart);
        bool RemoveCartSession(CartDetail cat, List<CartDetail> cartList);

        bool saveOrder(List<CartDetail> cartDetails, string userId);
        bool SaveOrder(List<CartDetail> cartDetails, string userId, int cartId);

    }
}
