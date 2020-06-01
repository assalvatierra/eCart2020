using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using eCartModels;

namespace eCartDbLayer
{
    public class DBRefContext: DbContext
    {
        public DBRefContext() : base ("name=ecartdbContainer")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public virtual DbSet<MasterCity> MasterCities { get; set; }
        public virtual DbSet<MasterArea> MasterAreas{ get; set; }
        public virtual DbSet<StoreDetail> StoreDetails { get; set; }
        public virtual DbSet<StoreStatus> StoreStatuses { get; set; }
        public virtual DbSet<StoreStatus> StoreStatus { get; set; }
        public virtual DbSet<StoreCategory> StoreCategories { get; set; }
        public virtual DbSet<StorePaymentType> StorePaymentTypes { get; set; }
        public virtual DbSet<StorePaymentStatus> StorePaymentStatus { get; set; }

        public virtual DbSet<ItemMaster> ItemMasters { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ItemCatGroup> ItemCatGroups { get; set; }
        public virtual DbSet<ItemMasterCategory> ItemMasterCategories { get; set; }
        public virtual DbSet<PaymentReceiver> PaymentReceivers { get; set; }
        public virtual DbSet<PaymentParty> PaymentParties { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<RiderStatus> RiderStatus { get; set; }



    }
}
