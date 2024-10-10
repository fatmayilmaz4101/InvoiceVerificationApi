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
                return BadRequest("No file uploaded");
            }
            if (!file.FileName.EndsWith(".xlsx"))
            {
                return BadRequest("Invalid file format. Only .xlsx files are allowed.");
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
                                CompanyCode = string.Empty,
                                CompanyName = string.Empty,
                                CreatedDate = DateTime.UtcNow
                            },
                            ArticleList = new ArticleListEntity()
                            {
                                ArticleNo = string.Empty,
                                ArticleName = string.Empty,
                                CreatedDate = DateTime.UtcNow
                            },
                            CompanyPriceList = new CompanyPriceListEntity()
                            {
                                CreatedDate = DateTime.UtcNow
                            }
                        };
                        if (worksheet.Cells[row, 1].Value is string companyCode)
                        {
                            priceListMapping.CompanyList.CompanyCode = companyCode;
                        }

                        if (worksheet.Cells[row, 2].Value is string companyName)
                        {
                            priceListMapping.CompanyList.CompanyName = companyName;
                        }
                        if (worksheet.Cells[row, 3].Value is string articleNo)
                        {
                            priceListMapping.ArticleList.ArticleNo = articleNo;
                        }
                        if (worksheet.Cells[row, 4].Value is string articleName)
                        {
                            priceListMapping.ArticleList.ArticleName = articleName;
                        }
                        if (worksheet.Cells[row, 5].Value is double unitPrice)
                        {
                            priceListMapping.CompanyPriceList.UnitPrice = unitPrice;
                        }
                        if (worksheet.Cells[row, 6].Value is string strUnit && Enum.Parse<Unit>(strUnit) is Unit unit)
                        {
                            priceListMapping.ArticleList.Unit = unit;
                        }
                        if (worksheet.Cells[row, 7].Value is string strCurrency && Enum.Parse<Currency>(strCurrency) is Currency currency)
                        {
                            priceListMapping.CompanyPriceList.Currency = currency;
                        }
                        if (worksheet.Cells[row, 8].Value is int paymentTerm)
                        {
                            priceListMapping.CompanyList.PaymentTerm = paymentTerm;
                        }
                        if (worksheet.Cells[row, 9].Value is string strInvoiceCurrency && Enum.Parse<InvoiceCurrency>(strInvoiceCurrency) is InvoiceCurrency invoiceCurrency)
                        {
                            priceListMapping.CompanyList.InvoiceCurrency = invoiceCurrency;
                        }
                        if (worksheet.Cells[row, 10].Value is double minPrice)
                        {
                            priceListMapping.ArticleList.MinPrice = minPrice;
                        }
                        if (worksheet.Cells[row, 11].Value is double maxPrice)
                        {
                            priceListMapping.ArticleList.MaxPrice = maxPrice;
                        }
                        if (worksheet.Cells[row, 12].Value is double cost)
                        {
                            priceListMapping.ArticleList.Cost = cost;
                        }
                        if (worksheet.Cells[row, 13].Value is string description)
                        {
                            priceListMapping.CompanyPriceList.Description = description;
                        }

                        var company = await context.CompanyLists.FirstOrDefaultAsync(x => x.CompanyCode == priceListMapping.CompanyList.CompanyCode);
                        if (company is not null)
                        {
                            priceListMapping.CompanyList = null!;
                            priceListMapping.CompanyListId = company.Id;
                        };
                        var article = await context.ArticleLists.FirstOrDefaultAsync(x => x.ArticleNo == priceListMapping.ArticleList.ArticleNo);
                        if (article is not null)
                        {
                            priceListMapping.ArticleList = null!;
                            priceListMapping.ArticleListId = article.Id;
                        }
                        var companyPriceList = await context.PriceListMappings.FirstOrDefaultAsync(x => x.ArticleListId == priceListMapping.ArticleListId && x.CompanyListId == priceListMapping.CompanyListId);
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