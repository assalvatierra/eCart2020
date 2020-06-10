using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iAdminMgr
    {
        bool DbDispose();

        bool AddStoreDetails(StoreDetail storeDetail);
        bool EditStoreDetails(StoreDetail storeDetail);
        bool RemovestoreDetails(StoreDetail storeDetail);
        StoreDetail GetStoreDetail(int id);

        bool AddUserDetails(UserDetail userDetail);
        bool EditUserDetails(UserDetail userDetail);
        bool RemoveUserDetails(UserDetail userDetail);
        bool IsUserEmailExist(string email);
        bool IsUserMobileExist(string mobile);
        UserDetail GetUserDetail(int id);
        List<UserDetail> GetUserDetailList();

        bool AddMasterCity(MasterCity masterCity);
        bool EditMasterCity(MasterCity masterCity);
        bool RemoveMasterCity(MasterCity masterCity);
        MasterCity GetMasterCity(int id);
        List<MasterCity> GetMasterCitiesList();

        bool AddMasterArea(MasterArea masterArea);
        bool EditMasterArea(MasterArea masterArea);
        bool RemoveMasterArea(MasterArea masterArea);
        MasterArea GetMasterArea(int id);
        List<MasterArea> GetMasterAreaList();

        bool AddItemCategory(ItemCategory itemCategory);
        bool EditItemCategory(ItemCategory itemCategory);
        bool RemoveItemCategory(ItemCategory itemCategory);
        ItemCategory GetItemCategory(int id);
        List<ItemCategory> GetItemCategoriesList();

        bool AddItemCatGroup(ItemCatGroup itemCatGroup);
        bool EditItemCatGroup(ItemCatGroup itemCatGroup);
        bool RemoveItemCatGroup(ItemCatGroup itemCatGroup);
        ItemCatGroup GetItemCatGroup(int id);
        List<ItemCatGroup> GetItemCatGroupList();

        bool AddItemMaster(ItemMaster itemMaster);
        bool EditItemMaster(ItemMaster itemMaster);
        bool RemoveItemMaster(ItemMaster itemMaster);
        ItemMaster GetItemMaster(int id);
        List<ItemMaster> GetItemMasterList();

        bool AddStorePayment(StorePayment storePayment);
        bool EditStorePayment(StorePayment storePayment);
        bool RemoveStorePayment(StorePayment storePayment);
        StorePayment GetStorePayment(int id);
        List<StorePayment> GetStorePaymentList();

        bool AddRiderDetails(RiderDetail rider);
        bool EditRiderDetails(RiderDetail rider);
        bool RemoveRiderDetails(RiderDetail rider);
        RiderDetail GetRiderDetails(int id);
        List<RiderDetail> GetRiderDetailsList();
        List<PaymentDetail> GetPaymentDetails(int cartId);

        bool AddItemMasterCategory(ItemMasterCategory itemMasterCategory);
        bool RemoveItemMasterCategory(ItemMasterCategory itemMasterCategory);
        ItemMasterCategory GetItemMasterCategory(int id);
        List<ItemMasterCategory> GetItemMasterCategoryList();


        bool AddItemImage(ItemImage itemImage);
        bool EditItemImage(ItemImage itemImage);
        bool RemoveItemImage(ItemImage itemImage);
        ItemImage GetItemImage(int id);
        ItemImage GetItemImageByItemId(int id);

        bool AddStoreKiosk(StoreKiosk storeKiosk);
        bool EditStoreKiosk(StoreKiosk storeKiosk);
        bool RemoveStoreKiosk(StoreKiosk storeKiosk);
        StoreKiosk GetStoreKiosk(int id);
        List<StoreKiosk> GetStoreKioskList();

        bool AddUserApplication(UserApplication userApplication);
        bool EditUserApplication(UserApplication userApplication);
        bool RemoveUserApplication(UserApplication userApplication);
        UserApplication GetUserApplication(int id);
        List<UserApplication> GetUserApplicationList();
    }
}
