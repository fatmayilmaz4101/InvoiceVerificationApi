using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    [Table("price_list_mapping")]

    public class PriceListMappingEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("company_price_list_id")]
        public int CompanyPriceListId { get; set; }
        [Column("article_list_id")]
        public int ArticleListId { get; set; }
        [Column("company_list_id")]
        public int CompanyListId { get; set; }
        [Column("date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ArticleListEntity ArticleList { get; set; } = null!;
        public CompanyListEntity CompanyList { get; set; } = null!;
        public CompanyPriceListEntity CompanyPriceList { get; set; } = null!;
    }
}