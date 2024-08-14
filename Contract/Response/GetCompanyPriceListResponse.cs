using InvoiceVerificationApi.dtos;

namespace InvoiceVerificationApi.Contract.Response
{
public class GetCompanyPriceListResponse
{
    public required ArticleListDto ArticleList { get; set; }
    public required CompanyListDto CompanyList { get; set; }
    public required CompanyPriceListDto CompanyPriceList { get; set; }
}
}