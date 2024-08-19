using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    public class GetCompanyListResponse
    {
        public int TotalCount { get; set; }
        public List<CompanyListEntity> CompanyLists { get; set; }
    }
}