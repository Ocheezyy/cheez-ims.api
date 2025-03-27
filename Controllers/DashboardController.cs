using cheez_ims_api.Data;
using cheez_ims_api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace cheez_ims_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/dashboard/overview
        [HttpGet("overview")]
        [SwaggerOperation(OperationId = "GetDashboardOverview", Summary = "Get Dashboard Overview", Tags = new[] { "Dashboard" })]
        public async Task<ActionResult<IEnumerable<DashboardOverviewDto>>> GetDashboardOverview()
        {
            FormattableString query = $"SELECT to_char(order_date, 'MM YY') AS Month, DATE_TRUNC('month', order_date) AS SortDate, SUM(total_amount) AS TotalAmount FROM orders WHERE order_date >= NOW() - INTERVAL '12 months' GROUP BY Month, SortDate ORDER BY SortDate;";
        
            return await _context
                       .Database
                       .SqlQuery<DashboardOverviewDto>(query)
                       .ToListAsync();
        }
        
    }
    
}