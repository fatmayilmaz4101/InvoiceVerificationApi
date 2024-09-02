using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Request;
using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using InvoiceVerificationApi.dtos;
using Microsoft.AspNetCore.JsonPatch;
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
            var query = context.ArticleLists.AsNoTracking().OrderBy(c => c.Id).AsQueryable();
            if (articleNo is not null)
            {
                query = query.Where(c => EF.Functions.Like(c.ArticleNo, $"%{articleNo}%"));
            }
            query = query.Skip((page - 1) * 10).Take(10);
            var articleLists = await query.ToListAsync();
            var articleList = articleLists.Select(article => new ArticleListDto
            {
                Id = article.Id,
                ArticleNo = article.ArticleNo,
                ArticleName = article.ArticleName,
                Unit = article.Unit.ToString(),  // Enum'ı string olarak dönüştürüyoruz
                MinPrice = article.MinPrice,
                MaxPrice = article.MaxPrice,
                Cost = article.Cost,
                Description = article.Description,
                CreatedDate = article.CreatedDate
            }).ToList();

            var response = new GetArticleListResponse()
            {
                TotalCount = totalCount,
                ArticleLists = articleList
            };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetArticleListResponse>> Get(int id)
        {
            var article = await context.ArticleLists.FindAsync(id);
            return Ok(article);
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
                    MinPrice = article.MinPrice,
                    MaxPrice = article.MaxPrice,
                    Cost = article.Cost,
                    Description = article.Description,
                    CreatedDate = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
                return Ok("Succesfull");
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ArticleListEntity> jsonPatchDoc)
        {
            var articleList = await context.ArticleLists.FindAsync(id);
            if (articleList is null)
            {
                return NotFound();
            }
            jsonPatchDoc.ApplyTo(articleList);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}