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
        bool DbDispose();

        bool AddRiderDetails(RiderDetail riderDetail);
        bool EditRiderDetails(RiderDetail riderDetail);
        bool RemoveRiderDetails(RiderDetail riderDetail);
        RiderDetail GetRiderDetails(int id);
        RiderDetail GetRiderDetailByLoginId(string loginId);
        List<RiderDetail> GetRiderDetailsList();

        void AddCartPayment(RiderCashDetail cashDetail);
        void AddDeliveryActivity(int id, int statusId);
        void AddCartHistory(int id, int statusId);
        CartDelivery GetCartDelivery(int id);
        RiderCashDetail GetRiderCashDetailsByCartDetailId(int id);
        RiderCashDetail GetRiderCashDetails(int id);

        string getLastestActivity(int id);
        List<RiderDetail> GetActiveRiders();

        void setCartStatusDelivered(int id);
    }
}
