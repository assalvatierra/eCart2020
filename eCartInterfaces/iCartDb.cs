using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCartModels;

namespace eCartInterfaces
{
    public interface iCartDb
    {
        bool AddCartDetails(CartDetail cartDetail);
        bool AddCartHistory(CartHistory cartHistory);
        bool AddCartItem(CartItem cartItem);
        bool AddPaymentDetails(PaymentDetail paymentDetail);
        bool AddCartDelivery(CartDelivery cartDelivery);
        bool AddStoreKiosk(StoreKiosk storeKiosk);
        bool AddStoreKioskOrder(StoreKioskOrder kioskOrder);

        StoreItem GetStoreItem(int id);
        IQueryable<StoreItem> GetStoreItems();
        StoreDetail GetStoreDetail(int id);
        IQueryable<StoreDetail> GetStoreDetails();
        CartStatus GetCartStatus(int id);
        StorePickupPoint GetStorePickupPoint(int id);
        UserDetail GetUserDetails(string userId);
        IQueryable<CartDetail> GetCartDetails();
        IQueryable<CartHistory> GetCartHistories();
        IQueryable<CartActivity> GetCartActivities();
        IQueryable<CartItem> GetCartItems();
        IQueryable<CartDelivery> GetCartDeliveries();
        IQueryable<StoreKiosk> GetStoreKiosks();
        IQueryable<StoreKioskOrder> GetStoreKioskOrders();

        CartDetail GetCartDetail(int id);
        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<PaymentReceiver> GetPaymentRecievers();

        bool EditCartDetails(CartDetail cartDetail);
        bool EditCartItem(CartItem cartItem);
        bool EditCartDelivery(CartDelivery cartDelivery);
        bool EditStoreKiosk(StoreKiosk storeKiosk);
        bool EditStoreKioskOrder(StoreKioskOrder kioskOrder);

        bool RemoveCartDelivery(CartDelivery cartDelivery);

        bool Save();
    }
}
