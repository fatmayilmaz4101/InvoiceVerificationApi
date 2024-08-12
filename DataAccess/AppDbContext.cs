using InvoiceVerificationApi.BusinessLogic.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CompanyDefinitionEntity> CompanyDefinitions { get; set; }
        public DbSet<CompanyPriceListEntity> CompanyPriceLists { get; set; }
        public DbSet<StockIdentificationEntity> StockIdentifications { get; set; }
        public DbSet<PriceListMappingEntity> PriceListMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyDefinitionEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.CompanyDefinition)
            .HasForeignKey<PriceListMappingEntity>(x => x.CompanyDefinitionId);

            modelBuilder.Entity<CompanyPriceListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.CompanyPriceList)
            .HasForeignKey<PriceListMappingEntity>(x => x.CompanyPriceListId);

            modelBuilder.Entity<StockIdentificationEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.StockIdentification)
            .HasForeignKey<PriceListMappingEntity>(x => x.StockIdentificationId);

            base.OnModelCreating(modelBuilder);
        }

    }
}