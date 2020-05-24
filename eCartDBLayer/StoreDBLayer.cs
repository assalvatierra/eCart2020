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
        ecartdbContainer db = new ecartdbContainer();

        public bool SaveChanges()
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
                db.StoreDetails.Add(storeDetail);
                db.SaveChanges();

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
        public bool IsStoreImgExist(int storeId)
        {
            return db.StoreImages.Any(s => s.StoreDetailId == storeId);
        }

        public StoreImage GetStoreImg(int storeId, int imgTypeId)
        {
            return db.StoreImages.Where(s => s.StoreDetailId == storeId && s.StoreImgTypeId == imgTypeId).FirstOrDefault();
        }

        public bool CreateStoreImg(StoreImage storeImage)
        {
            try
            {
                db.StoreImages.Add(storeImage);
                db.SaveChanges();

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
                db.StoreItems.Add(storeItem);
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
                db.ItemMasters.Add(itemMaster);
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
                db.ItemImages.Add(itemImage);
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
                return db.StoreItems.Find(id);
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
                return db.ItemMasters.Find(id);
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
                return db.ItemCatGroups;
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
                return db.ItemCategories;
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
                db.Entry(storeItem).State = EntityState.Modified;
                db.SaveChanges();
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
                db.Entry(itemImage).State = EntityState.Modified;
                db.SaveChanges();
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
                return db.ItemImages.Where(s => s.ItemMasterId == itemMasterId).FirstOrDefault();
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
                db.StoreItems.Remove(storeItem);
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