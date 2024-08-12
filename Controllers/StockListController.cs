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
    public class StockListController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetStockListResponse>> Get()
        {
            var stockLists = await context.StockLists.AsNoTracking().ToListAsync();
            return Ok(new GetStockListResponse(stockLists));
        }

    }
}