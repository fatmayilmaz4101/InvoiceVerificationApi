using System.Formats.Asn1;
using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Request;
using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using InvoiceVerificationApi.dtos;
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
        public async Task<IActionResult> Patch(int id, [FromBody] PutArticleListRequest updatedArticle)
        {
            var article = await context.ArticleLists.FindAsync(id);
            if (updatedArticle is not null && article is not null)
            {
                if (updatedArticle.ArticleNo is not null && updatedArticle.ArticleNo != "" && updatedArticle.ArticleNo != "string")
                { article.ArticleNo = updatedArticle.ArticleNo; }
                if (updatedArticle.ArticleName is not null && updatedArticle.ArticleName != "" && updatedArticle.ArticleName != "string")
                { article.ArticleName = updatedArticle.ArticleName; }
                if (updatedArticle.Unit is not null && updatedArticle.Unit != 0)
                { article.Unit = (Enums.Unit)updatedArticle.Unit; }
                if (updatedArticle.MinPrice.HasValue && updatedArticle.MinPrice != 0)
                { article.MinPrice = updatedArticle.MinPrice.Value; }
                if (updatedArticle.MaxPrice.HasValue && updatedArticle.MaxPrice != 0)
                { article.MaxPrice = updatedArticle.MaxPrice.Value; }
                if (updatedArticle.Cost.HasValue && updatedArticle.Cost != 0)
                { article.Cost = updatedArticle.Cost.Value; }
                if (updatedArticle.Description is not null && updatedArticle.Description != "" && updatedArticle.Description != "string")
                { article.Description = updatedArticle.Description; }
            }
            else
            {
                return BadRequest("Invalid data or article not found.");
            }
            await context.SaveChangesAsync();
            return Ok("Update successful.");
        }
    }
}