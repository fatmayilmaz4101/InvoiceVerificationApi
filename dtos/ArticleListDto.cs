using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.dtos
{
    public class ArticleListDto
    {
        public required string ArticleNo { get; set; }
        public required string ArticleName { get; set; }
        public Unit Unit { get; set; }

    }
}