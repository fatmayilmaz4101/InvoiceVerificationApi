using InvoiceVerificationApi.BusinessLogic.Entity;
using InvoiceVerificationApi.DataAccess;
using InvoiceVerificationApi.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace InvoiceVerificationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceListMappingController(AppDbContext context) : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (file is null || file.Length is 0)
            {
                return BadRequest("Failed to load file");
            }
            var priceListMappings = new List<PriceListMappingEntity>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var priceListMapping = new PriceListMappingEntity()
                        {
                            CompanyList = new CompanyListEntity()
                            {
                                CompanyAccountCode = string.Empty,
                                CompanyAccountName = string.Empty,
                            },
                            StockList = new StockListEntity()
                            {
                                StockCode = string.Empty,
                                StockName = string.Empty
                            },
                            CompanyPriceList = new CompanyPriceListEntity()
                        };
                        if (worksheet.Cells[row, 1].Value is string companyAccountCode)
                        {
                            priceListMapping.CompanyList.CompanyAccountCode = companyAccountCode;
                        }

                        if (worksheet.Cells[row, 2].Value is string companyAccountName)
                        {
                            priceListMapping.CompanyList.CompanyAccountName = companyAccountName;
                        }
                        if (worksheet.Cells[row, 3].Value is string stockCode)
                        {
                            priceListMapping.StockList.StockCode = stockCode;
                        }
                        if (worksheet.Cells[row, 4].Value is string stockName)
                        {
                            priceListMapping.StockList.StockName = stockName;
                        }
                        if (worksheet.Cells[row, 5].Value is double unitPrice)
                        {
                            priceListMapping.CompanyPriceList.UnitPrice = unitPrice;
                        }
                        if (worksheet.Cells[row, 6].Value is string strUnit && Enum.Parse<Unit>(strUnit) is Unit unit)
                        {
                            priceListMapping.StockList.Unit = unit;
                        }
                        if (worksheet.Cells[row, 7].Value is string strCurrencyType && Enum.Parse<CurrencyType>(strCurrencyType) is CurrencyType currencyType)
                        {
                            priceListMapping.CompanyPriceList.CurrencyType = currencyType;
                        }
                        if (worksheet.Cells[row, 8].Value is int paymentTerm)
                        {
                            priceListMapping.CompanyList.PaymentTerm = paymentTerm;
                        }
                        if (worksheet.Cells[row, 9].Value is string strInvoiceUnit && Enum.Parse<InvoiceUnit>(strInvoiceUnit) is InvoiceUnit invoiceUnit)
                        {
                            priceListMapping.CompanyList.InvoiceUnit = invoiceUnit;
                        }
                        if (worksheet.Cells[row, 10].Value is string description)
                        {
                            priceListMapping.CompanyList.Description = description;
                        }

                        var company = await context.CompanyLists.FirstOrDefaultAsync(x => x.CompanyAccountCode == priceListMapping.CompanyList.CompanyAccountCode);
                        if (company is not null)
                        {
                            priceListMapping.CompanyList = null!;
                            priceListMapping.CompanyDefinitionId = company.Id;
                        };
                        var stock = await context.StockLists.FirstOrDefaultAsync(x => x.StockCode == priceListMapping.StockList.StockCode);
                        if (stock is not null)
                        {
                            priceListMapping.StockList = null!;
                            priceListMapping.StockIdentificationId = stock.Id;
                        }
                        var companyPriceList = await context.PriceListMappings.FirstOrDefaultAsync(x => x.StockIdentificationId == priceListMapping.StockIdentificationId && x.CompanyDefinitionId == priceListMapping.CompanyDefinitionId);
                        if (companyPriceList is not null)
                        {
                            continue;
                        }
                        priceListMappings.Add(priceListMapping);
                    }
                }
            }
            // var stockCodes = priceListMappings.Select(x => x.StockIdentification.StockCode).Distinct().ToList();
            // var existStockIdentification = await context.StockIdentifications.Where(x => stockCodes.Contains(x.StockCode)).AsNoTracking().ToListAsync();

            // var companyAccountCodes = priceListMappings.Select(x => x.CompanyDefinition.CompanyAccountCode).Distinct().ToList();
            // var existCompanyIdentification = await context.CompanyDefinitions.Where(x => companyAccountCodes.Contains(x.CompanyAccountCode)).AsNoTracking().ToListAsync();

            // var list = priceListMappings.RemoveAll(x =>
            // existStockIdentification.Any(e => e.StockCode == x.StockIdentification.StockCode) ||
            // existCompanyIdentification.Any(e => e.CompanyAccountCode == x.CompanyDefinition.CompanyAccountCode));

            context.PriceListMappings.AddRange(priceListMappings);
            await context.SaveChangesAsync();
            return Ok(new { Message = "The file was successfully uploaded and saved to the database" });

        }
    }
}