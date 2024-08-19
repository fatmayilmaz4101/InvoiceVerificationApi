using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.dtos;

namespace InvoiceVerificationApi.Contract.Response
{
    //değer bazı işlem record. Kopyalanan nesne üstünde işlem yapar.
    public class GetArticleListResponse
    {
        public int TotalCount { get; set; }
        public List<ArticleListDto> ArticleLists { get; set; }
    };
}