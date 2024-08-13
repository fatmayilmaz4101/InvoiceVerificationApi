using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //primary constructor 
    //IEnumerable yerine liste tercih et perf farkı var(geri dönüs değeri)
    //AsNoTracking
    public class ArticleListController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetArticleListResponse>> Get()
        {
            var articleLists = await context.ArticleLists.AsNoTracking().ToListAsync();
            return Ok(new GetArticleListResponse(articleLists));
        }

    }
}