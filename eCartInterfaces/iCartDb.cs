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

        StoreItem GetStoreItem(int id);
        StoreDetail GetStoreDetail(int id);
        CartStatus GetCartStatus(int id);
        StorePickupPoint GetStorePickupPoint(int id);
        UserDetail GetUserDetails(string userId);
        CartDetail GetCartDetail(int id);

        List<StorePickupPoint> GetStorePickupPoints(int storeId);
        List<PaymentReceiver> GetPaymentRecievers();

        bool EditCartDetails(CartDetail cartDetail);

        bool Save();
    }
}
