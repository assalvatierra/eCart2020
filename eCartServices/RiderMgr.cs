using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCartInterfaces;
using eCartModels;

namespace eCartServices
{
    public class RiderMgr : iRiderMgr
    {
        private ecartdbContainer db = new ecartdbContainer();

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
    }
}