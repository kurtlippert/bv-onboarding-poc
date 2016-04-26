using BroadvineOnboard.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BroadvineOnboard.DAL
{
    public class BroadvineOnboardContext : DbContext
    {
        public BroadvineOnboardContext() : base("BroadvineOnboardContext") { }

        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.AccountType> AccountTypes { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.AccountSubType> AccountSubTypes { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.DriverType> DriverTypes { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.DepartmentGroup> DepartmentGroups { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.Area> Areas { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.RevenueSegmentGroup> RevenueSegmentGroups { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.RevenueSegment> RevenueSegments { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.WagePTEB> WagePTEBs { get; set; }

        public System.Data.Entity.DbSet<BroadvineOnboard.Models.COA> COAs { get; set; }
    }
}