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
    //primary constructor 
    //IEnumerable yerine liste tercih et perf farkı var(geri dönüs değeri)
    //AsNoTracking
    public class StockIdentificationController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetStockIdentificationResponse>> Get()
        {
            var stockIdentifications = await context.StockIdentifications.AsNoTracking().ToListAsync();
            return Ok(new GetStockIdentificationResponse(stockIdentifications));
        }
        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Failed to load file");
            }
            var stockIdentifications = new List<StockIdentificationEntity>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var stockIdentification = new StockIdentificationEntity()
                        {
                            StockCode = string.Empty,
                            StockName = string.Empty
                        };

                        if (worksheet.Cells[row, 1].Value is string stockCode)
                        {
                            stockIdentification.StockCode = stockCode;
                        }

                        if (worksheet.Cells[row, 2].Value is string stockName)
                        {
                            stockIdentification.StockName = stockName;
                        }

                        if (worksheet.Cells[row, 3].Value is string strUnit && Enum.Parse<Unit>(strUnit) is Unit unit)
                        {
                            stockIdentification.Unit = unit;
                        }

                        if (worksheet.Cells[row, 4].Value is string description)
                        {
                            stockIdentification.Description = description;
                        }

                        stockIdentifications.Add(stockIdentification);
                    }
                }
            }
            context.StockIdentifications.AddRange(stockIdentifications);
            await context.SaveChangesAsync();
            return Ok(new { Message = "The file was successfully uploaded and saved to the database" });
        }

    }
}