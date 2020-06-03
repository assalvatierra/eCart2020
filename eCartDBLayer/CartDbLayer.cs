using eCartModels;
using eCartInterfaces;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Data.Entity;

namespace eCartDbLayer
{

    public class CartDbLayer : iCartDb
    {
        ecartdbContainer db = new ecartdbContainer();

        public bool AddCartDetails(CartDetail cartDetail)
        {
            try
            {
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
                return false;

            }
        }

        public bool AddCartHistory(CartHistory cartHistory)
        {
            try
            {

                db.CartHistories.Add(cartHistory);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool AddCartItem(CartItem cartItem)
        {
            try
            {
                db.CartItems.Add(cartItem);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;

            }
        }


        public bool AddPaymentDetails(PaymentDetail paymentDetail)
        {
            try
            {
                db.PaymentDetails.Add(paymentDetail);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;

            }
        }

        public StoreDetail GetStoreDetail(int id)
        {
            try
            {
                return db.StoreDetails.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public StoreItem GetStoreItem(int id)
        {
            try
            {
                return db.StoreItems.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public CartStatus GetCartStatus(int id)
        {
            try
            {
                return db.CartStatus.Find(id);
            }
            catch
            {
                return null;
            }
            
        }

        public StorePickupPoint GetStorePickupPoint(int id)
        {
            try
            {
                return db.StorePickupPoints.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public bool Save()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserDetail GetUserDetails(string userId)
        {
            try
            {
                return db.UserDetails.Where(u => u.UserId == userId).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<StorePickupPoint> GetStorePickupPoints(int storeId)
        {
            try
            {
                return db.StorePickupPoints.Where(s=>s.StoreDetailId == storeId).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<PaymentReceiver> GetPaymentRecievers()
        {
            try
            {
                return db.PaymentReceivers.ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool EditCartDetails(CartDetail cartDetail)
        {
            try
            {

                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public CartDetail GetCartDetail(int id)
        {
            return db.CartDetails.Find(id);
        }

        public IQueryable<StoreItem> GetStoreItems()
        {
            return db.StoreItems;
        }

        public IQueryable<StoreDetail> GetStoreDetails()
        {
            return db.StoreDetails;
        }

        public IQueryable<CartDetail> GetCartDetails()
        {
            return db.CartDetails;
        }

        public IQueryable<CartHistory> GetCartHistories()
        {
            return db.CartHistories;
        }

        public IQueryable<CartActivity> GetCartActivities()
        {
            return db.CartActivities;
        }

        public IQueryable<CartItem> GetCartItems()
        {
            return db.CartItems;
        }

        public bool EditCartItem(CartItem cartItem)
        {
            try
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddCartDelivery(CartDelivery cartDelivery)
        {
            try
            {
                db.CartDeliveries.Add(cartDelivery);
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

        public bool EditCartDelivery(CartDelivery cartDelivery)
        {

            try
            {
                db.Entry(cartDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool RemoveCartDelivery(CartDelivery cartDelivery)
        {
            try
            {
                db.CartDeliveries.Remove(cartDelivery);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddStoreKiosk(StoreKiosk storeKiosk)
        {
            try
            {
                db.StoreKiosks.Add(storeKiosk);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool AddStoreKioskOrder(StoreKioskOrder kioskOrder)
        {
            try
            {

                db.StoreKioskOrders.Add(kioskOrder);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool EditStoreKiosk(StoreKiosk storeKiosk)
        {
            try
            {
                db.Entry(storeKiosk).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool EditStoreKioskOrder(StoreKioskOrder kioskOrder)
        {
            try
            {
                db.Entry(kioskOrder).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public IQueryable<StoreKiosk> GetStoreKiosks()
        {
            return db.StoreKiosks;
        }

        public IQueryable<StoreKioskOrder> GetStoreKioskOrders()
        {
            return db.StoreKioskOrders;
        }

        public IQueryable<CartRelease> GetCartReleases()
        {
            return db.CartReleases;
        }

        public bool AddCartRelease(CartRelease cartRelease)
        {
            try
            {
                db.CartReleases.Add(cartRelease);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditCartRelease(CartRelease cartRelease)
        {
            try
            {
                db.Entry(cartRelease).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveCartRelease(CartRelease cartRelease)
        {
            try
            {
                db.CartReleases.Remove(cartRelease);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}