using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyListController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCompanyListResponse>> Get()
        {
            var companyLists = await context.CompanyLists.AsNoTracking().ToListAsync();
            return Ok(new GetCompanyListResponse(companyLists));
        }
    }
}