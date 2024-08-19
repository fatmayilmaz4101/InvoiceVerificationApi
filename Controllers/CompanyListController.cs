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
    public class CompanyListController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCompanyListResponse>> Get(int page, string? companyCode)
        {
            var totalCount = await context.CompanyLists.AsNoTracking().CountAsync();
            var query = context.CompanyLists.AsNoTracking().AsQueryable();
            if (companyCode is not null)
            {
                query = query.Where(c => EF.Functions.Like(c.CompanyCode, $"%{companyCode}%"));
            }
            query = query.Skip((page - 1) * 10).Take(10);
            var companyList = await query.ToListAsync();
            var response = new GetCompanyListResponse()
            {
                TotalCount = totalCount,
                CompanyLists = companyList
            };

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCompanyListRequest company)
        {
            if (company is null)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
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
}