using InvoiceVerificationApi.dtos;

namespace InvoiceVerificationApi.Contract.Response
{
    public class GetCompanyPriceListResponse
    {
        public int TotalCount { get; set; }
        public required List<CompanyPriceDto> CompanyPriceLists { get; set; }

    }
}