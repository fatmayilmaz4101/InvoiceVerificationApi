using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Request;
using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using InvoiceVerificationApi.dtos;
using Microsoft.AspNetCore.JsonPatch;
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
            var query = context.CompanyLists.AsNoTracking().OrderBy(c => c.Id).AsQueryable();
            if (companyCode is not null)
            {
                query = query.Where(c => EF.Functions.Like(c.CompanyCode, $"%{companyCode}%"));
            }
            query = query.Skip((page - 1) * 10).Take(10);
            var companyLists = await query.ToListAsync();
            var companyList = companyLists.Select(company => new CompanyListDto
            {
                Id = company.Id,
                CompanyCode = company.CompanyCode,
                CompanyName = company.CompanyName,
                PaymentTerm = company.PaymentTerm,
                InvoiceCurrency = company.InvoiceCurrency.ToEnumString(),
                Description = company.Description,
                CreatedDate = company.CreatedDate
            }).ToList();
            var response = new GetCompanyListResponse()
            {
                TotalCount = totalCount,
                CompanyLists = companyList
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCompanyListResponse>> Get(int id)
        {
            var company = await context.CompanyLists.FindAsync(id);
            return Ok(company);
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
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<CompanyListEntity> jsonPatchDoc)
        {
            var companyList = await context.CompanyLists.FindAsync(id);
            if (companyList is null)
            {
                return NotFound();
            }
            jsonPatchDoc.ApplyTo(companyList);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}