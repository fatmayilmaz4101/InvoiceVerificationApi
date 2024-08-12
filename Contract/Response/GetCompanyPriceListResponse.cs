using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    public record GetCompanyPriceListResponse(List<PriceListMappingEntity> CompanyPriceLists);
}