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

        #region MasterAreas
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

        #region Item Categories
        public bool AddItemCategory(ItemCategory itemCategory)
        {
            try
            {
                db.ItemCategories.Add(itemCategory);
                db.SaveChanges();
                return true;
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
                db.Entry(itemCategory).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<ItemCategory> GetItemCategories()
        {
            return db.ItemCategories;
        }

        public bool RemoveItemCategory(ItemCategory itemCategory)
        {
            try
            {
                db.ItemCategories.Remove(itemCategory);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion

        #region Item Categories Group
        public bool AddItemCatGroup(ItemCatGroup itemCatGroup)
        {
            try
            {
                db.ItemCatGroups.Add(itemCatGroup);
                db.SaveChanges();
                return true;
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
                db.Entry(itemCatGroup).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<ItemCatGroup> GetItemCatGroups()
        {
            return db.ItemCatGroups;
        }

        public bool RemoveItemCatGroup(ItemCatGroup itemCatGroup)
        {
            try
            {
                db.ItemCatGroups.Remove(itemCatGroup);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion

        #region Item Masters
        public bool AddItemMaster(ItemMaster itemMaster)
        {
            try
            {
                db.ItemMasters.Add(itemMaster);
                db.SaveChanges();
                return true;
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
                db.Entry(itemMaster).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<ItemMaster> GetItemMasters()
        {
            return db.ItemMasters;
        }

        public bool RemoveItemMaster(ItemMaster itemMaster)
        {
            try
            {
                db.ItemMasters.Remove(itemMaster);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        #endregion

        #region Store Payments
        public bool AddStorePayment(StorePayment storePayment)
        {
            try
            {
                db.StorePayments.Add(storePayment);
                db.SaveChanges();
                return true;
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
                db.Entry(storePayment).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<StorePayment> GetStorePayments()
        {
            return db.StorePayments;
        }

        public bool RemoveStorePayment(StorePayment storePayment)
        {
            try
            {
                db.StorePayments.Remove(storePayment);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region Rider Details
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

        public IQueryable<PaymentDetail> GetPaymentDetails()
        {
            return db.PaymentDetails;
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

        #endregion

        #region Rider Details
        public bool AddItemMasterCategory(ItemMasterCategory itemMasterCategory)
        {
            try
            {
                db.ItemMasterCategories.Add(itemMasterCategory);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public IQueryable<ItemMasterCategory> GetItemMasterCategories()
        {
            return db.ItemMasterCategories;
        }

        public bool RemoveItemMasterCategory(ItemMasterCategory itemMasterCategory)
        {
            try
            {
                db.ItemMasterCategories.Remove(itemMasterCategory);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region Rider Details
        public bool AddItemImage(ItemImage itemImage)
        {
            try
            {
                db.ItemImages.Add(itemImage);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }



        public IQueryable<ItemImage> GetItemImages()
        {
            return db.ItemImages;
        }

        public bool EditItemImage(ItemImage itemImage)
        {
            try
            {
                db.Entry(itemImage).State = EntityState.Modified;
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
