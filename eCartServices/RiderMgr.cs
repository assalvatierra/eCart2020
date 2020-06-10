using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCartDBLayer;
using eCartInterfaces;
using eCartModels;

namespace eCartServices
{
    public class RiderMgr : iRiderMgr
    {
        private ecartdbContainer db = new ecartdbContainer();
        private iRiderDb rdb = new RiderDBLayer();

        public bool DbDispose()
        {
            return rdb.DbDispose();
        }

        public void AddCartPayment(RiderCashDetail cashDetail)
        {
            try
            {
                db.RiderCashDetails.Add(cashDetail);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddDeliveryActivity(int id, int statusId)
        {
            try
            {
                db.CartActivities.Add(new CartActivity { 
                    dtActivity = DateTime.Now,
                   CartActivityTypeId = statusId,
                   CartDeliveryId = id
                });
                db.SaveChanges();


            }catch(Exception ex)
            {
                throw ex;
            }

        }

        public void AddCartHistory(int id, int statusId)
        {
            try
            {
                db.CartHistories.Add(new CartHistory
                {
                    CartDetailId = id,
                    CartStatusId = statusId,
                    dtStatus = DateTime.Now,
                    UserId = "1", //TODO: change to rider Id
                    
                });

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public string getLastestActivity(int id)
        {
            try
            {
                if (db.CartActivities.Where(s => s.CartDelivery.CartDetailId == id) != null)
                {


                    var activity = db.CartActivities.Where(s=>s.CartDelivery.CartDetailId == id).OrderByDescending(s => s.Id).FirstOrDefault();
                    if(activity != null)
                    {
                        return activity.CartActivityType.Name.ToString();
                    }
                    else
                    {
                        return "Pending";
                    }

                }
                return "Pending";

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void setCartStatusDelivered(int id)
        {
            try
            {
                var cart = db.CartDetails.Find(id);
                cart.CartStatusId = 5;

                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RiderDetail> GetActiveRiders()
        {
            return rdb.GetRiderDetails().Where(r => r.RiderStatusId == 1).ToList();
        }

        public bool AddRiderDetails(RiderDetail riderDetail)
        {
            return rdb.AddRiderDetails(riderDetail);
        }

        public bool EditRiderDetails(RiderDetail riderDetail)
        {
            return rdb.EditRiderDetails(riderDetail);
        }

        public bool RemoveRiderDetails(RiderDetail riderDetail)
        {
            return rdb.RemoveRiderDetails(riderDetail);
        }

        public RiderDetail GetRiderDetails(int id)
        {
            return rdb.GetRiderDetails().Where(r => r.Id == id).FirstOrDefault();
        }

        public List<RiderDetail> GetRiderDetailsList()
        {
            return rdb.GetRiderDetails().ToList();
        }

        public CartDelivery GetCartDelivery(int id)
        {
            return rdb.GetCartDeliveries().Where(c => c.Id == id).FirstOrDefault();
        }

        public RiderCashDetail GetRiderCashDetailsByCartDetailId(int id)
        {
            return rdb.GetRiderCashDetails().Where(s => s.CartDetailId == id).OrderByDescending(s=>s.Id).FirstOrDefault();
        }

        public RiderCashDetail GetRiderCashDetails(int id)
        {
            return rdb.GetRiderCashDetails().Where(s => s.Id == id).OrderByDescending(s => s.Id).FirstOrDefault();
        }

        public RiderDetail GetRiderDetailByLoginId(string loginId)
        {
            return rdb.GetRiderDetails().Where(r=>r.UserId == loginId).FirstOrDefault();
        }

        public List<CartDelivery> GetActiveDeliveries(string userId)
        {
            try
            {
                var riderDetail = GetRiderDetailByLoginId(userId);
                var cartDeliveries = riderDetail.CartDeliveries.ToList();

                var deliveredList = new List<int>();
                cartDeliveries.ForEach(c => {
                    if (c.CartDetail.CartStatusId < 5)
                    {
                        deliveredList.Add(c.Id);
                    }
                });

                return cartDeliveries.Where(c => deliveredList.Contains(c.Id)).ToList();

            }
            catch
            {
                return null;
            }
        }

        public List<CartDelivery> GetDeliveredDeliveries(string userId)
        {
            try
            {
                var riderDetail = GetRiderDetailByLoginId(userId);
                var cartDeliveries = riderDetail.CartDeliveries.ToList();

                var deliveredList = new List<int>();
                cartDeliveries.ForEach(c => {
                    if (c.CartDetail.CartStatusId == 5)
                    {
                        deliveredList.Add(c.Id);
                    }
                });

                return cartDeliveries.Where(c => deliveredList.Contains(c.Id)).ToList();

            }
            catch
            {
                return null;
            }
        }
    }
}