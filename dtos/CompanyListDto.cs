
namespace InvoiceVerificationApi.dtos
{
    public class CompanyListDto
    {
        public int Id { get; set; }
        public required string CompanyCode { get; set; }
        public required string CompanyName { get; set; }
        public int PaymentTerm { get; set; }
        public string? InvoiceCurrency { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;


    }
}