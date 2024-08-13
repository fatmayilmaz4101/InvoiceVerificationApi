using System.Text.Json;
using System.Text.Json.Serialization;
using InvoiceVerificationApi.BusinessLogic.Entity;
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
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(companyLists, options);
            return Ok(json);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyListEntity company)
        {
            if (company is null)
            {
                return BadRequest("Invalid data.");
            }
            await context.CompanyLists.AddAsync(new CompanyListEntity()
            {
                CompanyCode = company.CompanyCode,
                CompanyName = company.CompanyName,
                PaymentTerm = company.PaymentTerm,
                InvoiceCurrency = company.InvoiceCurrency,
                Description = company.Description,
                CreatedDate = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
            return Ok("Succesfull");
        }
    }
}