using InvoiceVerificationApi.BusinessLogic.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CompanyListEntity> CompanyLists { get; set; }
        public DbSet<CompanyPriceListEntity> CompanyPriceLists { get; set; }
        public DbSet<ArticleListEntity> ArticleLists { get; set; }
        public DbSet<PriceListMappingEntity> PriceListMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.CompanyList)
            .HasForeignKey<PriceListMappingEntity>(x => x.CompanyListId);

            modelBuilder.Entity<CompanyPriceListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.CompanyPriceList)
            .HasForeignKey<PriceListMappingEntity>(x => x.CompanyPriceListId);

            modelBuilder.Entity<ArticleListEntity>()
            .HasOne(x => x.PriceListMapping)
            .WithOne(x => x.ArticleList)
            .HasForeignKey<PriceListMappingEntity>(x => x.ArticleListId);

            base.OnModelCreating(modelBuilder);
        }

    }
}