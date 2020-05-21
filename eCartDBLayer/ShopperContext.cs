using System.Data.Entity;
using eCartModels;

namespace eCart.Areas.Shopper
{
    public class ShopperContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ShopperContext() : base("name=ecartdbContainer")
        {
        }

        public System.Data.Entity.DbSet<PaymentDetail> PaymentDetails { get; set; }

        public System.Data.Entity.DbSet<CartDetail> CartDetails { get; set; }

        public System.Data.Entity.DbSet<PaymentParty> PaymentParties { get; set; }

        public System.Data.Entity.DbSet<PaymentReceiver> PaymentReceivers { get; set; }

        public System.Data.Entity.DbSet<PaymentStatus> PaymentStatus { get; set; }

        public System.Data.Entity.DbSet<UserDetail> UserDetails { get; set; }

        public System.Data.Entity.DbSet<UserStatus> UserStatuses { get; set; }

        public System.Data.Entity.DbSet<MasterCity> MasterCities { get; set; }

        public System.Data.Entity.DbSet<MasterArea> MasterAreas  { get; set; }

        //public System.Data.Entity.DbSet<eCart.Models.UserRolesMapping> userRolesMappings { get; set; }
    }
}
