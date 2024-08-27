namespace InvoiceVerificationApi.dtos
{
    public class ArticleListDto
    {
        public int Id { get; set; }
        public required string ArticleNo { get; set; }
        public required string ArticleName { get; set; }
        public string? Unit { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double Cost { get; set; }
        public string? Description { get; set; }  // Yeni alan
        public DateTime? CreatedDate { get; set; }  // Yeni alan


    }
}