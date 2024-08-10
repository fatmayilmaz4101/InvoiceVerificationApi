using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using InvoiceVerificationApi.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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
        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Failed to load file");
            }
            var companyDefinitions = new List<CompanyDefinitionEntity>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var companyDefinition = new CompanyDefinitionEntity()
                        {
                            CompanyAccountCode = string.Empty,
                            CompanyAccountName = string.Empty
                        };

                        if (worksheet.Cells[row, 1].Value is string companyAccountCode)
                        {
                            companyDefinition.CompanyAccountCode = companyAccountCode;
                        }

                        if (worksheet.Cells[row, 2].Value is string companyAccountName)
                        {
                            companyDefinition.CompanyAccountName = companyAccountName;
                        }

                        if (worksheet.Cells[row, 3].Value is int paymentTerm)
                        {
                            companyDefinition.PaymentTerm = paymentTerm;
                        }
                        if (worksheet.Cells[row, 4].Value is string strUnit && Enum.Parse<InvoiceUnit>(strUnit) is InvoiceUnit invoiceUnit)
                        {
                            companyDefinition.InvoiceUnit = invoiceUnit;
                        }
                        if (worksheet.Cells[row, 5].Value is string description)
                        {
                            companyDefinition.Description = description;
                        }



                        companyDefinitions.Add(companyDefinition);
                    }
                }
            }
            context.CompanyDefinitions.AddRange(companyDefinitions);
            await context.SaveChangesAsync();
            return Ok(new { Message = "The file was successfully uploaded and saved to the database" });
        }


    }
}