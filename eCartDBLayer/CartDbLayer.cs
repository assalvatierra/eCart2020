﻿using eCartModels;
using eCartInterfaces;
using System.Linq;

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
                //db.SaveChanges();

                return true;
            }
            catch 
            {
                return false;

            }
        }

        public bool AddCartHistory(CartHistory cartHistory)
        {
            try
            {

                db.CartHistories.Add(cartHistory);
                //db.SaveChanges();

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
    }
}