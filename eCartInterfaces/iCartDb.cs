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

        CartDetail GetCartDetail(int id);
        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<PaymentReceiver> GetPaymentRecievers();

        bool EditCartDetails(CartDetail cartDetail);
        bool EditCartItem(CartItem cartItem);
        bool EditCartDelivery(CartDelivery cartDelivery);

        bool RemoveCartDelivery(CartDelivery cartDelivery);

        bool Save();
    }
}
