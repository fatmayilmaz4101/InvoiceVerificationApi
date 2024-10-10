using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.Contract.Request
{
    public class PostCompanyListRequest
    {
        public required string CompanyCode { get; set; }
        public required string CompanyName { get; set; }
        public int PaymentTerm { get; set; }
        public InvoiceCurrency InvoiceCurrency { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

    }
}