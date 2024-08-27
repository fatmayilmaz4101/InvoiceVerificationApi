using InvoiceVerificationApi.dtos;

namespace InvoiceVerificationApi.Contract.Response
{
    public class GetCompanyPriceListResponse
    {
        public int TotalCount { get; set; }
        public required List<CompanyPriceDto> CompanyPriceLists { get; set; }

    }
    public class CompanyPriceDto
    {
        public required ArticleListDto ArticleList { get; set; }
        public required CompanyListDto CompanyList { get; set; }
        public required CompanyPriceListDto CompanyPriceList { get; set; }

    }
}