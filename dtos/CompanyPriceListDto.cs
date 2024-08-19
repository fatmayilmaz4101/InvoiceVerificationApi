

namespace InvoiceVerificationApi.dtos
{
    public class CompanyPriceListDto
    {
        public double UnitPrice { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}