using System.ComponentModel.DataAnnotations.Schema;
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    [Table("stock_identification")]
    public class StockIdentificationEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("stock_code")]
        public required string StockCode { get; set; }
        [Column("stock_name")]
        public required string StockName { get; set; }
        [Column("unit")]
        public Unit Unit { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


    }
}