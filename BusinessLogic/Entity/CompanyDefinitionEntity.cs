using System.ComponentModel.DataAnnotations.Schema;
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.BusinessLogic.Entity
{
    [Table("company_definition")]
    public class CompanyDefinitionEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("company_account_code")]
        public required string CompanyAccountCode { get; set; }
        [Column("company_account_name")]
        public required string CompanyAccountName { get; set; }
        [Column("payment_term")]
        public int PaymentTerm { get; set; }
        [Column("invoice_unit")]
        public InvoiceUnit InvoiceUnit { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}