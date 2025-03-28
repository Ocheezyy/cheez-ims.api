using cheez_ims_api.Data;
using cheez_ims_api.Dtos;
using cheez_ims_api.models;
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
            FormattableString query = $"""
                SELECT
                    to_char(order_date, 'Mon YY') AS Month,
                    DATE_TRUNC('month', order_date) AS SortDate,
                    SUM(total_amount) AS TotalAmount
                FROM orders
                WHERE order_date >= NOW() - INTERVAL '12 months'
                GROUP BY Month, SortDate
                ORDER BY SortDate;
                """;
        
            return await _context
                       .Database
                       .SqlQuery<DashboardOverviewDto>(query)
                       .ToListAsync();
        }

        [HttpGet("overview-stats")]
        [SwaggerOperation(OperationId = "GetOverviewStats", Summary = " Get Dashboard overview Card Stats",
            Tags = new[] { "Dashboard" })]
        public async Task<ActionResult<DashboardOverviewStatsDto>> GetDashboardOverviewStats()
        {
            var inventoryStats = await _context.Products
                .GroupBy(p => 1) // Grouping ensures a single row result
                .Select(g => new
                {
                    TotalInventory = g.Sum(p => p.StockQuantity),
                    LowStockInventory = g.Count(p => p.Status == Enums.ProductStatus.LowStock),
                    TotalValue = g.Sum(p => p.StockQuantity * p.Price)
                })
                .FirstOrDefaultAsync() ?? new { TotalInventory = 0, LowStockInventory = 0, TotalValue = new Decimal(0.0) };;
            var activeOrders = await _context.Orders.CountAsync(o => o.Status == Enums.OrderStatus.Pending || o.Status == Enums.OrderStatus.Shipped);

            var dashboardStats = new DashboardOverviewStatsDto
            {
                ActiveOrders = activeOrders,
                TotalValue = inventoryStats.TotalValue,
                TotalInventory = inventoryStats.TotalInventory,
                LowStockInventory = inventoryStats.LowStockInventory,
            };
            return dashboardStats;
        }

        [HttpGet("recent-activity")]
        [SwaggerOperation(OperationId = "GetRecentActivity", Summary = "Get Recent Activity",
            Tags = new[] { "Dashboard" })]
        public async Task<ActionResult<IEnumerable<Activity>>> GetRecentActivity()
        {
            return await _context.Activities
                .OrderByDescending(a => a.Timestamp)
                .Take(8)
                .Include(a => a.User)
                .ToListAsync();
        }
        
    }
    
}