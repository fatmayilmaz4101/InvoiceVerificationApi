using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.dtos;

namespace InvoiceVerificationApi.Contract.Response
{
    public class GetCompanyListResponse
    {
        public int TotalCount { get; set; }
        public List<CompanyListDto> CompanyLists { get; set; }
    }
}