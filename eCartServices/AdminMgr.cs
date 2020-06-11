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


        public bool IsUserEmailExist(string email)
        {
            try
            {
                var result = adminDb.GetUserDetails().Where(u => u.Email.ToLower() == email.ToLower()).Count();
                if (result > 0)
                    return true;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public bool IsUserMobileExist(string mobile)
        {
            try
            {
                var result = adminDb.GetUserDetails().Where(u => u.Mobile == mobile).Count();
                if (result > 0)
                    return true;
                return false;
            }
            catch
            {
                return true;
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

        #region Item Categories
        public bool AddItemCategory(ItemCategory itemCategory)
        {
            try
            {
                return adminDb.AddItemCategory(itemCategory);
            }
            catch
            {
                return false;
            }
        }

        public bool EditItemCategory(ItemCategory itemCategory)
        {
            try
            {
                return adminDb.EditItemCategory(itemCategory);
            }
            catch
            {
                return false;
            }
        }

        public ItemCategory GetItemCategory(int id)
        {
            try
            {
                return adminDb.GetItemCategories().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveItemCategory(ItemCategory itemCategory)
        {
            try
            {
                return adminDb.RemoveItemCategory(itemCategory);
            }
            catch
            {
                return false;
            }
        }

        public List<ItemCategory> GetItemCategoriesList()
        {
            try
            {
                return adminDb.GetItemCategories().ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Item Categories Group
        public bool AddItemCatGroup(ItemCatGroup itemCatGroup)
        {
            try
            {
                return adminDb.AddItemCatGroup(itemCatGroup);
            }
            catch
            {
                return false;
            }
        }

        public bool EditItemCatGroup(ItemCatGroup itemCatGroup)
        {
            try
            {
                return adminDb.EditItemCatGroup(itemCatGroup);
            }
            catch
            {
                return false;
            }
        }

        public ItemCatGroup GetItemCatGroup(int id)
        {
            try
            {
                return adminDb.GetItemCatGroups().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveItemCatGroup(ItemCatGroup itemCatGroup)
        {
            try
            {
                return adminDb.RemoveItemCatGroup(itemCatGroup);
            }
            catch
            {
                return false;
            }
        }

        public List<ItemCatGroup> GetItemCatGroupList()
        {
            try
            {
                return adminDb.GetItemCatGroups().ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Item Masters
        public bool AddItemMaster(ItemMaster itemMaster)
        {
            try
            {
                return adminDb.AddItemMaster(itemMaster);
            }
            catch
            {
                return false;
            }
        }

        public bool EditItemMaster(ItemMaster itemMaster)
        {
            try
            {
                return adminDb.EditItemMaster(itemMaster);
            }
            catch
            {
                return false;
            }
        }

        public ItemMaster GetItemMaster(int id)
        {
            try
            {
                return adminDb.GetItemMasters().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveItemMaster(ItemMaster itemMaster)
        {
            try
            {
                return adminDb.RemoveItemMaster(itemMaster);
            }
            catch
            {
                return false;
            }
        }

        public List<ItemMaster> GetItemMasterList()
        {
            try
            {
                return adminDb.GetItemMasters().ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Store Payments
        public bool AddStorePayment(StorePayment storePayment)
        {
            try
            {
                return adminDb.AddStorePayment(storePayment);
            }
            catch
            {
                return false;
            }
        }

        public bool EditStorePayment(StorePayment storePayment)
        {
            try
            {
                return adminDb.EditStorePayment(storePayment);
            }
            catch
            {
                return false;
            }
        }

        public StorePayment GetStorePayment(int id)
        {
            try
            {
                return adminDb.GetStorePayments().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveStorePayment(StorePayment storePayment)
        {
            try
            {
                return adminDb.RemoveStorePayment(storePayment);
            }
            catch
            {
                return false;
            }
        }

        public List<StorePayment> GetStorePaymentList()
        {
            try
            {
                return adminDb.GetStorePayments().ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Rider Details
        public bool AddRiderDetails(RiderDetail riderDetail)
        {
            try
            {
                return adminDb.AddRiderDetails(riderDetail);
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
                return adminDb.EditRiderDetails(riderDetail);
            }
            catch
            {
                return false;
            }
        }

        public RiderDetail GetRiderDetails(int id)
        {
            try
            {
                return adminDb.GetRiderDetails().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveRiderDetails(RiderDetail riderDetail)
        {
            try
            {
                return adminDb.RemoveRiderDetails(riderDetail);
            }
            catch
            {
                return false;
            }
        }

        public List<RiderDetail> GetRiderDetailsList()
        {
            try
            {
                return adminDb.GetRiderDetails().ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<PaymentDetail> GetPaymentDetails(int cartId)
        {
            return adminDb.GetPaymentDetails().Where(s => s.CartDetailId == cartId).ToList();
        }
        #endregion

        #region Item Master Category
        public bool AddItemMasterCategory(ItemMasterCategory itemMasterCategory)
        {
            try
            {
                return adminDb.AddItemMasterCategory(itemMasterCategory);
            }
            catch
            {
                return false;
            }
        }

        public ItemMasterCategory GetItemMasterCategory(int id)
        {
            try
            {
                return adminDb.GetItemMasterCategories().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveItemMasterCategory(ItemMasterCategory itemMasterCategory)
        {
            try
            {
                return adminDb.RemoveItemMasterCategory(itemMasterCategory);
            }
            catch
            {
                return false;
            }
        }

        public List<ItemMasterCategory> GetItemMasterCategoryList()
        {
            try
            {
                return adminDb.GetItemMasterCategories().ToList();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Item Image
        public bool AddItemImage(ItemImage itemImage)
        {
            try
            {
                return adminDb.AddItemImage(itemImage);
            }
            catch
            {
                return false;
            }
        }

        public ItemImage GetItemImage(int id)
        {
            try
            {
                return adminDb.GetItemImages().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }


        public ItemImage GetItemImageByItemId(int id)
        {
            try
            {
                return adminDb.GetItemImages().Where(s => s.ItemMasterId == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool EditItemImage(ItemImage itemImage)
        {
            try
            {
                return adminDb.EditItemImage(itemImage);
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveItemImage(ItemImage itemImage)
        {
            return adminDb.RemoveItemImage(itemImage);
        }

        #endregion

        #region Store Kiosk
        public bool AddStoreKiosk(StoreKiosk storeKiosk)
        {
            try
            {
                return adminDb.AddStoreKiosk(storeKiosk);
            }
            catch
            {
                return false;
            }
        }

        public StoreKiosk GetStoreKiosk(int id)
        {
            try
            {
                return adminDb.GetStoreKiosks().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<StoreKiosk> GetStoreKioskList()
        {
            try
            {
                return adminDb.GetStoreKiosks().ToList();
            }
            catch
            {
                return null;
            }
        }


        public bool EditStoreKiosk(StoreKiosk storeKiosk)
        {
            try
            {
                return adminDb.EditStoreKiosk(storeKiosk);
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveStoreKiosk(StoreKiosk storeKiosk)
        {
            return adminDb.RemoveStoreKiosk(storeKiosk);
        }

        #endregion

        #region Store Kiosk
        public bool AddUserApplication(UserApplication userApplication)
        {
            try
            {
                return adminDb.AddUserApplication(userApplication);
            }
            catch
            {
                return false;
            }
        }

        public UserApplication GetUserApplication(int id)
        {
            try
            {
                return adminDb.GetUserApplications().Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<UserApplication> GetUserApplicationList()
        {
            try
            {
                return adminDb.GetUserApplications().ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool EditUserApplication(UserApplication userApplication)
        {
            try
            {
                return adminDb.EditUserApplication(userApplication);
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveUserApplication(UserApplication userApplication)
        {
            return adminDb.RemoveUserApplication(userApplication);
        }

        #endregion






    }
}
