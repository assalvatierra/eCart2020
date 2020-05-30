using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartModels;

namespace eCartDBLayer
{
    public class AdminDBLayer : iAdminDb
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

        #region Store 
        public bool AddStoreDetails(StoreDetail storeDetail)
        {
            try
            {
                db.StoreDetails.Add(storeDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditStoreDetails(StoreDetail storeDetail)
        {
            try
            {
                db.Entry(storeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }


        public IQueryable<StoreDetail> GetStoreDetails()
        {
            return db.StoreDetails;
        }

        public bool RemoveStoreDetails(StoreDetail storeDetail)
        {
            try
            {
                db.StoreDetails.Remove(storeDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region User 
        public bool AddUserDetails(UserDetail userDetail)
        {
            try
            {
                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool EditUserDetails(UserDetail userDetail)
        {
            try
            {
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public IQueryable<UserDetail> GetUserDetails()
        {
            return db.UserDetails;
        }

        public bool RemoveUserDetails(UserDetail userDetail)
        {
            try
            {
                db.UserDetails.Remove(userDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion

        #region MasterCities 
        public bool AddMasterCity(MasterCity masterCity)
        {
            try
            {
                db.MasterCities.Add(masterCity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool EditMasterCity(MasterCity masterCity)
        {
            try
            {
                db.Entry(masterCity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<MasterCity> GetMasterCities()
        {
            return db.MasterCities;
        }

        public bool RemoveMasterCity(MasterCity masterCity)
        {
            try
            {
                db.MasterCities.Remove(masterCity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion


        #region MasterCities 
        public bool AddMasterArea(MasterArea masterArea)
        {
            try
            {
                db.MasterAreas.Add(masterArea);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool EditMasterArea(MasterArea masterArea)
        {
            try
            {
                db.Entry(masterArea).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<MasterArea> GetMasterAreas()
        {
            return db.MasterAreas;
        }

        public bool RemoveMasterArea(MasterArea masterArea)
        {
            try
            {
                db.MasterAreas.Remove(masterArea);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion

    }
}
