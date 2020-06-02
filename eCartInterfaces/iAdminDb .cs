using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iAdminDb
    {
        bool DbDispose();

        IQueryable<StoreDetail> GetStoreDetails();
        bool AddStoreDetails(StoreDetail storeDetail);
        bool EditStoreDetails(StoreDetail storeDetail);
        bool RemoveStoreDetails(StoreDetail storeDetail);

        IQueryable<UserDetail> GetUserDetails();
        bool AddUserDetails(UserDetail userDetail);
        bool EditUserDetails(UserDetail userDetail);
        bool RemoveUserDetails(UserDetail userDetail);

        IQueryable<MasterCity> GetMasterCities();
        bool AddMasterCity(MasterCity masterCity);
        bool EditMasterCity(MasterCity masterCity);
        bool RemoveMasterCity(MasterCity masterCity);

        IQueryable<MasterArea> GetMasterAreas();
        bool AddMasterArea(MasterArea masterArea);
        bool EditMasterArea(MasterArea masterArea);
        bool RemoveMasterArea(MasterArea masterArea);

        IQueryable<ItemCategory> GetItemCategories();
        bool AddItemCategory(ItemCategory itemCategory);
        bool EditItemCategory(ItemCategory itemCategory);
        bool RemoveItemCategory(ItemCategory itemCategory);

        IQueryable<ItemCatGroup> GetItemCatGroups();
        bool AddItemCatGroup(ItemCatGroup itemCatGroup);
        bool EditItemCatGroup(ItemCatGroup itemCatGroup);
        bool RemoveItemCatGroup(ItemCatGroup itemCategory);

        IQueryable<ItemMaster> GetItemMasters();
        bool AddItemMaster(ItemMaster itemMaster);
        bool EditItemMaster(ItemMaster itemMaster);
        bool RemoveItemMaster(ItemMaster itemMaster);

        IQueryable<StorePayment> GetStorePayments();
        bool AddStorePayment(StorePayment storePayment);
        bool EditStorePayment(StorePayment storePayment);
        bool RemoveStorePayment(StorePayment storePayment);

        IQueryable<RiderDetail> GetRiderDetails();
        IQueryable<PaymentDetail> GetPaymentDetails();
        bool AddRiderDetails(RiderDetail riderDetail);
        bool EditRiderDetails(RiderDetail riderDetail);
        bool RemoveRiderDetails(RiderDetail riderDetail);

        IQueryable<ItemMasterCategory> GetItemMasterCategories();
        bool AddItemMasterCategory(ItemMasterCategory itemMasterCategory);
        bool RemoveItemMasterCategory(ItemMasterCategory itemMasterCategory);

        IQueryable<ItemImage> GetItemImages();
        bool AddItemImage(ItemImage itemImage);
        bool EditItemImage(ItemImage itemImage);
        bool RemoveItemImage(ItemImage itemImage);

        IQueryable<StoreKiosk> GetStoreKiosks();
        bool AddStoreKiosk(StoreKiosk storeKiosk);
        bool EditStoreKiosk(StoreKiosk storeKiosk);
        bool RemoveStoreKiosk(StoreKiosk storeKiosk);
    }
}
