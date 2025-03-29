using System.Globalization;
using cheez_ims_api.Data;
using cheez_ims_api.Dtos;
using cheez_ims_api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace cheez_ims_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    
    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("overview")]
    [SwaggerOperation(OperationId = "GetReportsOverview", Summary = "Get Reports Overview", Tags = new[] { "Reports" })]
    public async Task<ReportsOverviewDto> GetReportsOverview()
    {
        var inventoryStatus = new ReportOverviewInventoryStatus
        {
            InStock = await _context.Products.CountAsync(p => p.Status == Enums.ProductStatus.InStock),
            LowStock = await _context.Products.CountAsync(p => p.Status == Enums.ProductStatus.LowStock),
            OutOfStock = await _context.Products.CountAsync(p => p.Status == Enums.ProductStatus.OutOfStock),
            Discontinued = await _context.Products.CountAsync(p => p.Status == Enums.ProductStatus.Discontinued)
        };

        var salesTrend = await _context.Orders
            .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
            .Select(g => new
            {
                Year = g.Key.Year,
                MonthNumber = g.Key.Month,
                Sales = g.Sum(o => o.TotalAmount),
                Orders = g.Count()
            })
            .ToListAsync(); // Fetch from DB first

        var formattedSalesTrend = salesTrend
            .Select(g => new ReportOverviewSalesTrend
            {
                Month = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.MonthNumber)} {g.Year}",
                Sales = g.Sales,
                Orders = g.Orders
            })
            .OrderBy(st => salesTrend.FirstOrDefault(x => x.MonthNumber == DateTime.ParseExact(st.Month.Split(' ')[0], "MMMM", CultureInfo.CurrentCulture).Month)?.Year)
            .ThenBy(st => DateTime.ParseExact(st.Month.Split(' ')[0], "MMMM", CultureInfo.CurrentCulture).Month)
            .ToArray();

        var topProducts = await _context.OrderItems
            .GroupBy(oi => oi.Product.Name)
            .OrderByDescending(g => g.Sum(oi => oi.Quantity * oi.UnitPrice))
            .Select(g => new ReportOverviewTopProducts
            {
                ProductName = g.Key,
                Sales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
            })
            .Take(5)
            .ToArrayAsync();

        var inventoryValues = await _context.Products
            .GroupBy(p => p.Category.Name)
            .Select(g => new ReportOverviewInventoryValueByCategory
            {
                Category = g.Key,
                Value = g.Sum(p => p.StockQuantity * p.Price)
            })
            .OrderByDescending(iv => iv.Value)
            .ToArrayAsync();

        return new ReportsOverviewDto
        {
            InventoryStatus = inventoryStatus,
            SalesTrend = formattedSalesTrend,
            TopProducts = topProducts,
            InventoryValue = inventoryValues,
        };
    }
    
    
    
}