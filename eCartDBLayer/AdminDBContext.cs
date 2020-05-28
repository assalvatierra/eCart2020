using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using eCartModels;

namespace eCart.Areas.Shopper
{
    public class AdminDBContext : DbContext
    {
        public AdminDBContext() : base("name=ecartdbContainer")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<StoreDetail> StoreDetails { get; set; }
        public virtual DbSet<MasterCity> MasterCities { get; set; }
        public virtual DbSet<MasterArea> MasterAreas { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ItemCatGroup> ItemCatGroups { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StoreStatus> StoreStatus { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StoreCategory> StoreCategories { get; set; }

        public System.Data.Entity.DbSet<eCartModels.UserStatus> UserStatus { get; set; }

        public System.Data.Entity.DbSet<eCartModels.ItemMaster> ItemMasters { get; set; }

        public System.Data.Entity.DbSet<eCartModels.CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<eCartModels.CartStatus> CartStatus { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StorePickupPoint> StorePickupPoints { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StorePickupPartner> StorePickupPartners { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StorePayment> StorePayments { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StorePaymentStatus> StorePaymentStatus { get; set; }

        public System.Data.Entity.DbSet<eCartModels.StorePaymentType> StorePaymentTypes { get; set; }

        public System.Data.Entity.DbSet<eCartModels.RiderDetail> RiderDetails { get; set; }

        public System.Data.Entity.DbSet<eCartModels.RiderStatus> RiderStatus { get; set; }

        public System.Data.Entity.DbSet<eCartModels.PaymentDetail> PaymentDetails { get; set; }
    }
}
