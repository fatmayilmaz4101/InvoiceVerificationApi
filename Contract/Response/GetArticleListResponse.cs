using InvoiceVerificationApi.BusinessLogic.Entity;

namespace InvoiceVerificationApi.Contract.Response
{
    //değer bazı işlem record. Kopyalanan nesne üstünde işlem yapar.
    public class GetArticleListResponse
    {
        public int TotalCount { get; set; }
        public List<ArticleListEntity> ArticleLists { get; set; }
    };
}