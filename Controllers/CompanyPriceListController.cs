using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task<ActionResult<GetCompanyPriceListResponse>> Get()
        {
            var companyPriceLists = await context.PriceListMappings
            .Include(x => x.CompanyList)
            .Include(x => x.ArticleList)
            .Include(x => x.CompanyPriceList)

            .AsNoTracking()
            .ToListAsync();

            var response = companyPriceLists.Select(x => new GetCompanyPriceListResponse
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
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(response, options);

            return Ok(json);
        }
    }
}