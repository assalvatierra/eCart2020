using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartDBLayer;
using eCartModels;
using System.Data.Entity;

namespace eCartServices
{
    class AdminMgr : iAdminMgr
    {
        private iAdminDb adminDb = new AdminDBLayer();

        public bool DbDispose()
        {
            return adminDb.DbDispose();
        }

        #region Store
        public bool AddStoreDetails(StoreDetail storeDetail)
        {
            try
            {
                return adminDb.AddStoreDetails(storeDetail);
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
                return adminDb.EditStoreDetails(storeDetail);
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
                return adminDb.GetStoreDetails().Where(s=>s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemovestoreDetails(StoreDetail storeDetail)
        {
            try
            {
                return adminDb.RemoveStoreDetails(storeDetail);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Users
        public bool AddUserDetails(UserDetail userDetail)
        {
            try
            {
                return adminDb.AddUserDetails(userDetail);
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
                return adminDb.EditUserDetails(userDetail);
            }
            catch
            {
                return false;
            }
        }

        public UserDetail GetUserDetail(int id)
        {
            try
            {
                return adminDb.GetUserDetails().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveUserDetails(UserDetail userDetail)
        {
            try
            {
                return adminDb.RemoveUserDetails(userDetail);
            }
            catch
            {
                return false;
            }
        }

        public List<UserDetail> GetUserDetailList()
        {
            try
            {
                return adminDb.GetUserDetails().Include(u => u.MasterArea).Include(u => u.MasterCity).Include(u => u.UserStatu).ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion


        #region Master City
        public bool AddMasterCity(MasterCity masterCity)
        {
            try
            {
                return adminDb.AddMasterCity(masterCity);
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
                return adminDb.EditMasterCity(masterCity);
            }
            catch
            {
                return false;
            }
        }

        public MasterCity GetMasterCity(int id)
        {
            try
            {
                return adminDb.GetMasterCities().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveMasterCity(MasterCity masterCity)
        {
            try
            {
                return adminDb.RemoveMasterCity(masterCity);
            }
            catch
            {
                return false;
            }
        }

        public List<MasterCity> GetMasterCitiesList()
        {
            try
            {
                return adminDb.GetMasterCities().ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Master Areas
        public bool AddMasterArea(MasterArea masterArea)
        {
            try
            {
                return adminDb.AddMasterArea(masterArea);
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
                return adminDb.EditMasterArea(masterArea);
            }
            catch
            {
                return false;
            }
        }

        public MasterArea GetMasterArea(int id)
        {
            try
            {
                return adminDb.GetMasterAreas().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveMasterArea(MasterArea masterArea)
        {
            try
            {
                return adminDb.RemoveMasterArea(masterArea);
            }
            catch
            {
                return false;
            }
        }

        public List<MasterArea> GetMasterAreaList()
        {
            try
            {
                return adminDb.GetMasterAreas().ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion


    }
}
