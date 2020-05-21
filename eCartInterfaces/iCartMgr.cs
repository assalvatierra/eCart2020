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
        List<PaymentReceiver> getPaymentRecievers();
        List<CartHistory> getCartHistory(int id);
        List<CartActivity> getCartDeliveryActivities(int id);

        void addItemToCart(int id, int qty);
        bool addItemToCart(int id, int qty, decimal price);
        void addCartItemToDb(CartItem cartItem);
        bool addCartDetailToDb(CartDetail cartDetail);
        bool addCartHistory(CartDetail cart, int statusId, string userId);
        void addDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks);

        bool updateCartPickupPoint(int storeId, int pickupPoint);
        bool updateCartAsDelivery(int cartId);
        void updateCartDetailsStatus(int cartId, string status);
        void updateCartDelivery(CartDelivery cartDelivery);
        bool setCartPickupDate(int cartId, DateTime pickupdate);
        bool setCartPaymentReceiver(int cartId, int recieverId);
        string setDBCartStatus(int cartId, int cartStatusId, string userId);
        string setCartStatusCancelled(int cartId, string userId);

        bool removeCartItem(int id);
        void removeDBCartItem(int id, int statusId);
        void removeCartdelivery(int id);
        bool removeCartSession(int storeId);

        bool saveOrder(List<CartDetail> cartDetails);
        bool saveOrder(CartDetail cart);

    }
}
