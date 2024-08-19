using System.Text.Json;
using System.Text.Json.Serialization;
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
                ArticleList = new ArticleListDto
                {
                    ArticleNo = x.ArticleList.ArticleNo,
                    ArticleName = x.ArticleList.ArticleName,
                    Unit = x.ArticleList.Unit
                },
                CompanyList = new CompanyListDto
                {
                    CompanyCode = x.CompanyList.CompanyCode,
                    CompanyName = x.CompanyList.CompanyName
                },
                CompanyPriceList = new CompanyPriceListDto
                {
                    UnitPrice = x.CompanyPriceList.UnitPrice,
                    Currency = x.CompanyPriceList.Currency,
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
    }
}