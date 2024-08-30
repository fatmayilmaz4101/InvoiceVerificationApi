
namespace InvoiceVerificationApi.dtos
{
    public class CompanyPriceUpdateDto
    {
        public int Id { get; set; }
        public required string ArticleNo { get; set; }
        public required string CompanyCode { get; set; }
        public double UnitPrice { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
    }
}