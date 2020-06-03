using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iDBRefDBLayer
    {
        IQueryable<MasterCity> GetMasterCities();
        IQueryable<MasterArea> GetMasterAreas();
        IQueryable<StoreStatus> GetStoreStatus();
        IQueryable<ItemMaster> GetItemMaster();
        IQueryable<StoreDetail> GetStoreDetails();
        IQueryable<ItemCategory> GetItemCategories();
        IQueryable<ItemCatGroup> GetItemCatGroups();
        IQueryable<ItemMasterCategory> GetItemMasterCategories();
        IQueryable<PaymentReceiver> GetPaymentReceivers();
        IQueryable<PaymentParty> GetPaymentParties();
        IQueryable<PaymentStatus> GetPaymentStatus();
        IQueryable<PaymentDetail> GetPaymentDetails();
        IQueryable<StorePaymentType> GetStorePaymentTypes();
        IQueryable<StorePaymentStatus> GetStorePaymentStatus();
        IQueryable<StoreCategory> GetStoreCategories();
        IQueryable<UserStatus> GetUserStatusList();
        IQueryable<RiderStatus> GetRiderStatus();
        IQueryable<RiderCashParty> GetRiderCashParties();
        IQueryable<UserDetail> GetUserDetails();

    }
}
