using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceVerificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyDefinitionController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCompanyDefinitionResponse>> Get()
        {
            var companyDefinitions = await context.CompanyDefinitions.AsNoTracking().ToListAsync();
            return Ok(new GetCompanyDefinitionResponse(companyDefinitions));
        }
    }
}