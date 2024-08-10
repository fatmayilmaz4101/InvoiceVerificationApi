using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace InvoiceVerificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyPriceListController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetCompanyPriceListResponse>> Get()
        {
            var companyPriceLists = await context.CompanyPriceLists.AsNoTracking().ToListAsync();
            return Ok(new GetCompanyPriceListResponse(companyPriceLists));
        }
        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file is null || file.Length is 0)
            {
                return BadRequest("Failed to load file");
            }
            var companyPriceLists = new List<CompanyPriceListEntity>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var companyPriceList = new CompanyPriceListEntity()
                        {
                            CurrencyType = string.Empty
                        };
                        if (worksheet.Cells[row, 1].Value is double unitPrice)
                        {
                            companyPriceList.UnitPrice = unitPrice;
                        }
                        if (worksheet.Cells[row, 2].Value is string currencyType)
                        {
                            companyPriceList.CurrencyType = currencyType;
                        }
                        if (worksheet.Cells[row, 3].Value is string description)
                        {
                            companyPriceList.Description = description;
                        }
                        companyPriceLists.Add(companyPriceList);
                    }
                }
            }
            context.CompanyPriceLists.AddRange(companyPriceLists);
            await context.SaveChangesAsync();
            return Ok(new { Message = "The file was successfully uploaded and saved to the database" });
        }
    }
}