using System.ComponentModel.DataAnnotations.Schema;
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    [Table("article_list")]
    public class ArticleListEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("article_no")]
        public required string ArticleNo { get; set; }
        [Column("article_name")]
        public required string ArticleName { get; set; }
        [Column("unit")]
        public Unit Unit { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public PriceListMappingEntity PriceListMapping { get; set; } = null!;
    }
}