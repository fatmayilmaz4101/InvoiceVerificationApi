using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    public record GetCompanyListResponse(List<CompanyListEntity> CompanyLists);
};