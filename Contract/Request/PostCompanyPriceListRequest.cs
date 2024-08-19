using InvoiceVerificationApi.dtos;
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.Contract.Request
{
    public class PostCompanyPriceListRequest
    {
        public int CompanyId { get; set; }
        public int ArticleId { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string ArticleNo { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public Currency Currency { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public CompanyListDto? CompanyList { get; set; }
        public ArticleListDto? ArticleList { get; set; }
    }
}