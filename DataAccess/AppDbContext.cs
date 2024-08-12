using InvoiceVerificationApi.BusinessLogic.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CompanyListEntity> CompanyLists { get; set; }
        public DbSet<CompanyPriceListEntity> CompanyPriceLists { get; set; }
        public DbSet<StockListEntity> StockLists{ get; set; }
        public DbSet<PriceListMappingEntity> PriceListMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.CompanyList)
            .HasForeignKey<PriceListMappingEntity>(x => x.CompanyDefinitionId);

            modelBuilder.Entity<CompanyPriceListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.CompanyPriceList)
            .HasForeignKey<PriceListMappingEntity>(x => x.CompanyPriceListId);

            modelBuilder.Entity<StockListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.StockList)
            .HasForeignKey<PriceListMappingEntity>(x => x.StockIdentificationId);

            base.OnModelCreating(modelBuilder);
        }

    }
}