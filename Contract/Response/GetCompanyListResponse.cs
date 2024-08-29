using InvoiceVerificationApi.dtos;

namespace InvoiceVerificationApi.Contract.Response
{
    public class GetCompanyListResponse
    {
        public int TotalCount { get; set; }
        public required List<CompanyListDto> CompanyLists { get; set; }
    }
}