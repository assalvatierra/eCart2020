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
        bool EditStoreImage(StoreImage storeImage);
        bool AddStoreImage(StoreImage storeImage);
        IQueryable<StoreImage> GetStoreImages();

        //Store Items
        bool AddStoreItem(StoreItem storeItem);
        bool AddItemMaster(ItemMaster itemMaster);
        bool AddItemImage(ItemImage itemImage);
        bool EditStoreItem(StoreImage storeImage);
        bool EditStoreItem(StoreItem storeItem);
        bool EditStoreItemImg(ItemImage itemImage);
        bool RemoveStoreItem(StoreItem storeItem);
        StoreItem GetStoreItem(int id);
        ItemMaster GetItemMaster(int id);
        ItemImage GetItemImage(int itemMasterId);
        IQueryable<ItemCatGroup> GetItemCatGroups();
        IQueryable<ItemCategory> GetItemCategories();
        IQueryable<StorePickupPoint> GetStorePickupPoints();
        IQueryable<StorePickupPartner> GetStorePickupPartners();
        bool AddStorePickupPartner(StorePickupPartner pickupPartner);
        bool RemoveStorePickupPartner(StorePickupPartner pickupPartner);
    }
}
