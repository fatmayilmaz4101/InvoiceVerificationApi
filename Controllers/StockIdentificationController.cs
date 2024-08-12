using InvoiceVerificationApi.Contract.Response;
using InvoiceVerificationApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    }
}