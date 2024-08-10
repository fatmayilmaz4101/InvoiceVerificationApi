using InvoiceVerificationApi.BusinessLogic.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<CompanyDefinitionEntity> CompanyDefinitions { get; set; }
        public DbSet<CompanyPriceListEntity> CompanyPriceLists { get; set; }
        public DbSet<StockIdentificationEntity> StockIdentifications { get; set; }
        public DbSet<PriceListMappingEntity> PriceListMappings { get; set; }

    }
}