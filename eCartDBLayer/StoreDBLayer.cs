using System;
using System.Linq;
using System.Data.Entity;
using eCartModels;
using eCartInterfaces;

namespace eCartDbLayer
{
    public class StoreDBLayer : iStoreDb
    {
        StoreContext sdb = new StoreContext();

        public bool SaveChanges()
        {
            try
            {
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Store Registration
        public StoreDetail GetStoreDetails(int id)
        {
            var store = sdb.StoreDetails.Find(id);
            return store;
        }

        public bool CreateStoreDetail(StoreDetail storeDetail)
        {
            try
            {
                sdb.StoreDetails.Add(storeDetail);
                sdb.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public bool EditStoreDetail(StoreDetail storeDetail)
        {
            try
            {
                sdb.Entry(storeDetail).State = EntityState.Modified;
                sdb.SaveChanges();

                return true;
            }
            catch
            {
                //throw ex;
                return false;
            }
        }

        public StoreDetail GetStoreByUserId(string id)
        {
                if (sdb.StoreDetails.Any(s => s.LoginId == id))
                {
                    return sdb.StoreDetails.Where(s => s.LoginId == id).FirstOrDefault();
                }
                return null;
        }

        #endregion
        
        #region Store Edit

        public bool AddStoreImage(StoreImage storeImage)
        {
            try
            {
                sdb.StoreImages.Add(storeImage);
                sdb.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<StoreImage> GetStoreImages()
        {
            return sdb.StoreImages;
        }

        public bool EditStoreImage(StoreImage storeImage)
        {
            try
            {
                sdb.Entry(storeImage).State = EntityState.Modified;
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Store Item

        public bool AddStoreItem(StoreItem storeItem)
        {
            try
            {
                sdb.StoreItems.Add(storeItem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddItemMaster(ItemMaster itemMaster)
        {
            try
            {
                sdb.ItemMasters.Add(itemMaster);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddItemImage(ItemImage itemImage)
        {
            try
            {
                sdb.ItemImages.Add(itemImage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditStoreItem(StoreImage storeImage)
        {
            throw new NotImplementedException();
        }

        public StoreItem GetStoreItem(int id)
        {
            try
            {
                return sdb.StoreItems.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public ItemMaster GetItemMaster(int id)
        {
            try
            {
                return sdb.ItemMasters.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ItemCatGroup> GetItemCatGroups()
        {
            try
            {
                return sdb.ItemCatGroups;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ItemCategory> GetItemCategories()
        {
            try
            {
                return sdb.ItemCategories;
            }
            catch
            {
                return null;
            }
        }

        public bool EditStoreItem(StoreItem storeItem)
        {
            try
            {
                sdb.Entry(storeItem).State = EntityState.Modified;
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditStoreItemImg(ItemImage itemImage)
        {
            try
            {
                sdb.Entry(itemImage).State = EntityState.Modified;
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ItemImage GetItemImage(int itemMasterId)
        {
            try
            {
                return sdb.ItemImages.Where(s => s.ItemMasterId == itemMasterId).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveStoreItem(StoreItem storeItem)
        {
            try
            {
                sdb.StoreItems.Remove(storeItem);
                sdb.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<StorePickupPoint> GetStorePickupPoints()
        {
            return sdb.StorePickupPoints;
        }

        public IQueryable<StorePickupPartner> GetStorePickupPartners()
        {
            return sdb.StorePickupPartners;
        }

        public bool AddStorePickupPartner(StorePickupPartner pickupPartner)
        {
            try
            {
                sdb.StorePickupPartners.Add(pickupPartner);
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveStorePickupPartner(StorePickupPartner pickupPartner)
        {
            try
            {
                sdb.StorePickupPartners.Remove(pickupPartner);
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        #endregion

        #region kiosk

        public IQueryable<StoreKiosk> GetStoreKiosks()
        {
            return sdb.storeKiosks;
        }

        public IQueryable<StoreKioskOrder> GetStoreKioskOrders()
        {
            return sdb.storeKioskOrders;
        }

        #endregion


        #region Store User
        public IQueryable<StoreUser> GetStoreUsers()
        {
            return sdb.StoreUsers;
        }

        public bool AddStoreUser(StoreUser storeUser)
        {
            try
            {
                sdb.StoreUsers.Add(storeUser);
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditStoreUser(StoreUser storeUser)
        {
            try
            {
                sdb.Entry(storeUser).State = EntityState.Modified;
                sdb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveStoreUser(StoreUser storeUser)
        {
            try
            {
                sdb.StoreUsers.Remove(storeUser);
                sdb.SaveChanges();
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