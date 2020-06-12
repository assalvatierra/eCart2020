using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using eCartModels;

namespace eCartDbLayer
{
    public class StoreContext: DbContext
    {

        public StoreContext()
             : base("name=ecartdbContainer")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<StoreDetail> StoreDetails { get; set; }
        public virtual DbSet<StoreStatus> StoreStatus { get; set; }
        public virtual DbSet<StoreCategory> StoreCategories { get; set; }
        public virtual DbSet<StoreItem> StoreItems { get; set; }
        public virtual DbSet<StoreItem> Store { get; set; }
        public virtual DbSet<StorePickupPoint> StorePickupPoints { get; set; }
        public virtual DbSet<StorePickupPartner> StorePickupPartners { get; set; }
        public virtual DbSet<StorePickupStatus> StorePickupStatus { get; set; }

        public System.Data.Entity.DbSet<MasterCity> MasterCities { get; set; }

        public System.Data.Entity.DbSet<MasterArea> MasterAreas { get; set; }

        public System.Data.Entity.DbSet<CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<CartStatus> CartStatus { get; set; }

        public System.Data.Entity.DbSet<UserDetail> UserDetails { get; set; }

        public System.Data.Entity.DbSet<ItemMaster> ItemMasters { get; set; }

        public System.Data.Entity.DbSet<ItemMasterCategory> ItemMasterCategories { get; set; }

        public System.Data.Entity.DbSet<ItemCategory> ItemCategories { get; set; }

        public System.Data.Entity.DbSet<ItemCatGroup> ItemCatGroups { get; set; }

        public System.Data.Entity.DbSet<PaymentDetail> PaymentDetails { get; set; }

        public System.Data.Entity.DbSet<PaymentParty> PaymentParties { get; set; }

        public System.Data.Entity.DbSet<PaymentReceiver> PaymentReceivers { get; set; }

        public System.Data.Entity.DbSet<PaymentStatus> PaymentStatus { get; set; }

        public System.Data.Entity.DbSet<StorePayment> StorePayments { get; set; }

        public System.Data.Entity.DbSet<StorePaymentStatus> StorePaymentStatus { get; set; }

        public System.Data.Entity.DbSet<StorePaymentType> StorePaymentTypes { get; set; }

        public System.Data.Entity.DbSet<StoreImage> StoreImages { get; set; }

        public System.Data.Entity.DbSet<StoreImgType> StoreImgTypes { get; set; }

        public System.Data.Entity.DbSet<CartDelivery> CartDeliveries { get; set; }

        public System.Data.Entity.DbSet<RiderDetail> RiderDetails { get; set; }

        public System.Data.Entity.DbSet<CartHistory> CartHistories { get; set; }

        public System.Data.Entity.DbSet<ItemImage> ItemImages { get; set; }

        public System.Data.Entity.DbSet<StoreKiosk> storeKiosks { get; set; }

        public System.Data.Entity.DbSet<StoreKioskOrder> storeKioskOrders { get; set; }

        public System.Data.Entity.DbSet<StoreUserType> StoreUserTypes { get; set; }

        public System.Data.Entity.DbSet<StoreUser> StoreUsers { get; set; }
    }
}