using System.ComponentModel.DataAnnotations.Schema;
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    [Table("company_list")]
    public class CompanyListEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("company_code")]
        public required string CompanyCode { get; set; }
        [Column("company_name")]
        public required string CompanyName { get; set; }
        [Column("payment_term")]
        public int PaymentTerm { get; set; }
        [Column("invoice_currency")]
        public InvoiceCurrency InvoiceCurrency { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_date")]
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public PriceListMappingEntity PriceListMapping { get; set; } = null!;
    }
}