using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    //değer bazı işlem record. Kopyalanan nesne üstünde işlem yapar.
    public record GetStockIdentificationResponse(List<StockIdentificationEntity> StockIdentifications);
}