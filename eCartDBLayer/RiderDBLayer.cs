using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eCartDBLayer
{
    public class RiderDBLayer : iRiderDb
    {
        ecartdbContainer db = new ecartdbContainer();

        public bool DbDispose()
        {
            try
            {
                db.Dispose();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool AddRiderDetails(RiderDetail riderDetail)
        {
            try
            {
                db.RiderDetails.Add(riderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditRiderDetails(RiderDetail riderDetail)
        {
            try
            {

                db.Entry(riderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<RiderDetail> GetRiderDetails()
        {
            return db.RiderDetails;
        }

        public bool RemoveRiderDetails(RiderDetail riderDetail)
        {
            try
            {
                db.RiderDetails.Remove(riderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<CartDelivery> GetCartDeliveries()
        {
            return db.CartDeliveries;
        }

        public IQueryable<RiderCashDetail> GetRiderCashDetails()
        {
            return db.RiderCashDetails;
        }
    }
}
