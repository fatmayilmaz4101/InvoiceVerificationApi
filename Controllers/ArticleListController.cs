using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task<ActionResult<GetArticleListResponse>> Get()
        {
            var articleLists = await context.ArticleLists.AsNoTracking().ToListAsync();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(articleLists, options);
            return Ok(json);
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