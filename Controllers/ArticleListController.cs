using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Request;
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
        public async Task<ActionResult<GetArticleListResponse>> Get(int page, string? articleNo)
        {
            var totalCount = await context.ArticleLists.AsNoTracking().CountAsync();
            var query = context.ArticleLists.AsNoTracking().AsQueryable();
            if (articleNo is not null)
            {
                query = query.Where(c => EF.Functions.Like(c.ArticleNo, $"%{articleNo}%"));
            }
            query = query.Skip((page - 1) * 10).Take(10);
            var articleList = await query.ToListAsync();
            var response = new GetArticleListResponse()
            {
                TotalCount = totalCount,
                ArticleLists = articleList
            };
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostArticleListRequest article)
        {
            if (article is null)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                await context.ArticleLists.AddAsync(new ArticleListEntity()
                {
                    ArticleNo = article.ArticleNo,
                    ArticleName = article.ArticleName,
                    Unit = article.Unit,
                    Description = article.Description,
                    CreatedDate = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
                return Ok("Succesfull");
            }
        }
    }
}