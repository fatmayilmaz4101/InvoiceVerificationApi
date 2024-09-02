namespace InvoiceVerificationApi.dtos
{
    public class CompanyPriceDto
    {
        public int Id { get; set; }
        public required ArticleListDto ArticleList { get; set; }
        public required CompanyListDto CompanyList { get; set; }
        public required CompanyPriceListDto CompanyPriceList { get; set; }

    }
}