
using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.Contract.Request
{
    //DataAnnotations
    public class PostArticleListRequest
    {
        public required string ArticleNo { get; set; }
        public required string ArticleName { get; set; }
        public Unit Unit { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
    };
}