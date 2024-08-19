using InvoiceVerificationApi.Enums;

namespace InvoiceVerificationApi.dtos
{
    public class ArticleListDto
    {
        public required string ArticleNo { get; set; }
        public required string ArticleName { get; set; }
        public string? Unit { get; set; }
        public string? Description { get; set; }  // Yeni alan
        public DateTime? CreatedDate { get; set; }  // Yeni alan


    }
}