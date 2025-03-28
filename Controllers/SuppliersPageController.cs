using cheez_ims_api.Data;
using cheez_ims_api.Dtos;
using cheez_ims_api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace cheez_ims_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersPageController : ControllerBase
{
    private readonly AppDbContext _context;
        
    public SuppliersPageController(AppDbContext context)
    {
        _context = context;
    }
    
    // GET: api/SuppliersPage/Products/{Guid}
    [HttpGet("Products/{id}")]
    [SwaggerOperation(OperationId = "GetSupplierProducts", Summary = "Get Supplier Products", Tags = new[] { "SuppliersPage" })]
    public async Task<ActionResult<IEnumerable<Product>>> GetSupplierProducts(Guid id)
    {
        return await _context.Products.Where(p => p.SupplierId == id).Include(p => p.Category).ToListAsync();
    }
    
    // GET: api/SuppliersPage/Orders/{Guid}
    [HttpGet("Orders/{id}")]
    [SwaggerOperation(OperationId = "GetSupplierOrders", Summary = "Get Supplier Orders", Tags = new[] { "SuppliersPage" })]
    public async Task<ActionResult<IEnumerable<SupplierOrdersDto>>> GetSupplierOrders(Guid id)
    {
        return await _context.Orders
            .Where(o => o.OrderItems.Any(oi => oi.Product.SupplierId == id))
            .Select(o => new SupplierOrdersDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                ShippingAddress = o.ShippingAddress,
                TotalAmount = o.TotalAmount,
                PaymentMethod = o.PaymentMethod,
                PaymentStatus = o.PaymentStatus,
                OrderStatus = o.Status,
                UserId = o.UserId,
                OrderItemCount = o.OrderItems.Count(oi => oi.Product.SupplierId == id)
            })
            .ToListAsync();
    }
}