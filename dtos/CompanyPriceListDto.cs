
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.dtos
{
    public class CompanyPriceListDto
    {
        public double UnitPrice { get; set; }
        public Currency Currency { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}