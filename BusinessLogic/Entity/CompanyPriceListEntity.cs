using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    [Table("company_price_list")]

    public class CompanyPriceListEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("unit_price")]
        public double UnitPrice { get; set; }
        [Column("currency_type")]
        public required string CurrencyType { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

    }
}