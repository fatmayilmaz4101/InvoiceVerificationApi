using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
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
            .Include(x => x.CompanyDefinition)
            .Include(x => x.StockIdentification)
            .Include(x => x.CompanyPriceList)
            .AsNoTracking()
            .ToListAsync();
            return Ok(new GetCompanyPriceListResponse(companyPriceLists));
        }
    }
}