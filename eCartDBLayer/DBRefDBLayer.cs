using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eCartModels;
using eCartInterfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eCartDbLayer
{
    public class DBRefDBLayer: iDBRefDBLayer
    {
        ecartdbContainer edb = new ecartdbContainer();
        DBRefContext db = new DBRefContext();

        public IQueryable<MasterCity> GetMasterCities()
        {
            return db.MasterCities;
        }
        public IQueryable<MasterArea> GetMasterAreas()
        {
            return db.MasterAreas;
        }

        public IQueryable<StoreStatus> GetStoreStatus()
        {
            return db.StoreStatuses;
        }

        public IQueryable<ItemMaster> GetItemMaster()
        {
            return db.ItemMasters;
        }

        public IQueryable<StoreDetail> GetStoreDetails()
        {
            return db.StoreDetails;
        }

        public IQueryable<ItemCategory> GetItemCategories()
        {
            return db.ItemCategories;
        }

        public IQueryable<ItemMasterCategory> GetItemMasterCategories()
        {
            return db.ItemMasterCategories;
        }

        public IQueryable<PaymentReceiver> GetPaymentReceivers()
        {
            return db.PaymentReceivers;
        }

        public IQueryable<PaymentParty> GetPaymentParties()
        {
            return db.PaymentParties;
        }

        public IQueryable<PaymentStatus> GetPaymentStatus()
        {
            return db.PaymentStatus;
        }

        public IQueryable<StoreCategory> GetStoreCategories()
        {
            return db.StoreCategories;
        }

        public IQueryable<UserStatus> GetUserStatusList()
        {
            return db.UserStatuses;
        }

        public IQueryable<ItemCatGroup> GetItemCatGroups()
        {
            return db.ItemCatGroups;
        }

        public IQueryable<StorePaymentType> GetStorePaymentTypes()
        {
            return db.StorePaymentTypes;
        }

        public IQueryable<StorePaymentStatus> GetStorePaymentStatus()
        {
            return db.StorePaymentStatus;
        }

        public IQueryable<RiderStatus> GetRiderStatus()
        {
            return db.RiderStatus;
        }

        public IQueryable<PaymentDetail> GetPaymentDetails()
        {
            return db.PaymentDetails;
        }

        public IQueryable<RiderCashParty> GetRiderCashParties()
        {
            return db.RiderCashParties;
        }

        public IQueryable<UserDetail> GetUserDetails()
        {
            return db.UserDetails;
        }

        public IQueryable<UserApplicationType> GetUserApplicationTypes()
        {
            return db.UserApplicationTypes;
        }

        public IQueryable<UserApplicationStatus> GetUserApplicationStatus()
        {
            return db.UserApplicationStatus;
        }

        public List<IdentityUSer> GetAspNetUsers()
        {
            var context = new IdentityDbContext();
            var users = context.Users.Select(u => new { u.Id, u.UserName, u.Email });

            List<IdentityUSer> userList = new List<IdentityUSer>();
            foreach (var utmp in users)
            {
                userList.Add(new IdentityUSer()
                {
                    UserName = utmp.UserName,
                    LoginId = utmp.Id,
                    Email = utmp.Email
                });
            }
            return userList;

        }
    }

}
