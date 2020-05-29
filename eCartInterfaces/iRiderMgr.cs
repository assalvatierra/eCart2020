using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartModels;

namespace eCartInterfaces
{
    public interface iRiderMgr
    {

        void AddCartPayment(RiderCashDetail cashDetail);
        void AddDeliveryActivity(int id, int statusId);
        void AddCartHistory(int id, int statusId);

        string getLastestActivity(int id);
        List<RiderDetail> GetActiveRiders();

        void setCartStatusDelivered(int id);
    }
}
