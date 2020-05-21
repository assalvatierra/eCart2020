using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartModels;

namespace eCartInterfaces
{
    public interface iStoreDb
    {
        StoreDetail GetStoreDetails(int id);
        StoreDetail GetStoreByUserId(string id);
        bool SaveChanges();

        //Store Registration
        bool CreateStoreDetail(StoreDetail storeDetail);
        bool EditStoreDetail(StoreDetail storeDetail);
        bool IsStoreImgExist(int storeId);
        bool CreateStoreImg(StoreImage storeImage);
        StoreImage GetStoreImg(int storeId, int imgTypeId);

        //Store Items
        bool AddStoreItem(StoreItem storeItem);
        bool AddItemMaster(ItemMaster itemMaster);
        bool AddItemImage(ItemImage itemImage);
        bool EditStoreItem(StoreImage storeImage);
        StoreItem GetStoreItem(int id);
        ItemMaster GetItemMaster(int id);
        IQueryable<ItemCatGroup> GetItemCatGroups();
        IQueryable<ItemCategory> GetItemCategories();
        bool EditStoreItem(StoreItem storeItem);
        bool EditStoreItemImg(ItemImage itemImage);
        ItemImage GetItemImage(int itemMasterId);
        bool RemoveStoreItem(StoreItem storeItem);
    }
}
