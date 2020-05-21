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

        StoreItem GetStoreItem(int id);
        StoreDetail GetStoreDetail(int id);
        CartStatus GetCartStatus(int id);
        StorePickupPoint GetStorePickupPoint(int id);

        bool Save();
    }
}
