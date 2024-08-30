using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Request;
using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using InvoiceVerificationApi.dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyPriceListController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCompanyPriceListResponse>> Get(int page)
        {
            var totalCount = await context.CompanyPriceLists.AsNoTracking().CountAsync();
            var companyPriceLists = await context.PriceListMappings
            .Include(x => x.CompanyList)
            .Include(x => x.ArticleList)
            .Include(x => x.CompanyPriceList)
                .AsNoTracking()
                .Skip((page - 1) * 10)
                .Take(10)
                .ToListAsync();

            var companyPrice = companyPriceLists.Select(x => new CompanyPriceDto
            {
                Id = x.CompanyPriceList.Id,

                ArticleList = new ArticleListDto
                {
                    ArticleNo = x.ArticleList.ArticleNo,
                    ArticleName = x.ArticleList.ArticleName,
                    Unit = x.ArticleList.Unit.ToEnumString(),
                    MinPrice = x.ArticleList.MinPrice,
                    MaxPrice = x.ArticleList.MaxPrice,
                    Cost = x.ArticleList.Cost
                },
                CompanyList = new CompanyListDto
                {
                    CompanyCode = x.CompanyList.CompanyCode,
                    CompanyName = x.CompanyList.CompanyName
                },
                CompanyPriceList = new CompanyPriceListDto
                {
                    UnitPrice = x.CompanyPriceList.UnitPrice,
                    Currency = x.CompanyPriceList.Currency.ToEnumString(),
                    Description = x.CompanyPriceList.Description
                }
            }).ToList();
            var response = new GetCompanyPriceListResponse
            {
                TotalCount = totalCount,
                CompanyPriceLists = companyPrice
            };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyPriceUpdateDto>> GetById(int id)
        {
            var companyPrice = await context.PriceListMappings
                .Include(x => x.ArticleList)
                .Include(x => x.CompanyList)
                .Include(x => x.CompanyPriceList)
                .AsNoTracking()
                .Where(x => x.CompanyPriceList.Id == id)
                .Select(x => new CompanyPriceUpdateDto
                {
                    Id = x.CompanyPriceList.Id,
                    ArticleNo = x.ArticleList.ArticleNo,
                    CompanyCode = x.CompanyList.CompanyCode,
                    UnitPrice = x.CompanyPriceList.UnitPrice,
                    Currency = x.CompanyPriceList.Currency.ToEnumString(),
                    Description = x.CompanyPriceList.Description
                })
                .FirstOrDefaultAsync();

            if (companyPrice == null)
            {
                return NotFound();
            }

            return Ok(companyPrice);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostCompanyPriceListRequest companyPriceRequest)
        {
            if (companyPriceRequest is null)
            {
                return BadRequest("Invalid data.");
            }
            var priceListMapping = new PriceListMappingEntity
            {
                ArticleListId = companyPriceRequest.ArticleId,
                CompanyListId = companyPriceRequest.CompanyId
            };
            // await context.PriceListMappings.AddAsync(priceListMapping);
            await context.CompanyPriceLists.AddAsync(new CompanyPriceListEntity()
            {
                UnitPrice = companyPriceRequest.UnitPrice,
                Currency = companyPriceRequest.Currency,
                Description = companyPriceRequest.Description,
                CreatedDate = DateTime.UtcNow,
                PriceListMapping = priceListMapping
            });
            await context.SaveChangesAsync();

            return Ok("Succesfull");
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument<CompanyPriceListEntity> jsonPatchDoc)
        {
            var companyPriceList = await context.CompanyPriceLists.FindAsync(id);
            if (companyPriceList is null)
            {
                return NotFound();
            }
            jsonPatchDoc.ApplyTo(companyPriceList);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}