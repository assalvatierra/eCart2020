using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iRiderDb
    {
        bool DbDispose();
        IQueryable<RiderDetail> GetRiderDetails();
        bool AddRiderDetails(RiderDetail riderDetail);
        bool EditRiderDetails(RiderDetail riderDetail);
        bool RemoveRiderDetails(RiderDetail riderDetail);

        IQueryable<CartDelivery> GetCartDeliveries();
        IQueryable<RiderCashDetail> GetRiderCashDetails();
    }
}
