using System;
using System.Collections.Generic;
using eCartModels;


namespace eCartInterfaces
{
    public interface iCartMgr
    {
        void setDbLayer(iCartDb cartdblayer);
        int getDefaultPickupPointId(int storeId);
        StorePickupPoint GetStorePickup(int id);
        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<CartDetail> getShopperCarts(int userId);
        List<PaymentReceiver> GetPaymentRecievers();
        List<CartHistory> getCartHistory(int id);
        List<CartActivity> getCartDeliveryActivities(int id);
        UserDetail GetUserDetails(string userId);
        CartDetail GetCartDetail(int id);
        StoreItem GetStoreItem(int id);

        List<CartDetail> AddItemToCart(int id, int qty, decimal price, List<CartDetail> cartSession,string userId);
        bool addCartHistory(int cartId, int statusId, string userId);
        bool AddDeliveryDetails(int id, DateTime date, string address, int riderId, string remarks);

        bool UpdateCartPickupPoint(int storeId, int pickupPoint, List<CartDetail> cart);
        bool UpdateCartAsDelivery(int cartId, List<CartDetail> cart);
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

        bool AddStoreKiosk(StoreKiosk storeKiosk);
        bool AddStoreKioskOrder(StoreKioskOrder kioskOrder);
        bool UpdateStoreKiosk(StoreKiosk storeKiosk);
        bool UpdateStoreKioskOrder(StoreKioskOrder kioskOrder);
        StoreKiosk GetStoreKiosk(int id);
        StoreKioskOrder GetStoreKioskOrder(int id);
    }
}
