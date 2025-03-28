using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cheez_ims_api.Data;
using cheez_ims_api.models;
using Swashbuckle.AspNetCore.Annotations;

namespace cheez_ims_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        [SwaggerOperation(OperationId = "GetOrders", Summary = "Get Orders", Tags = new[] { "Orders" })]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] string? include = null)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(include))
            {
                if (include.Contains("user"))
                {
                    query = query.Include(o => o.User);
                }

                if (include.Contains("order_items"))
                {
                    query = query.Include(o => o.OrderItems);
                }
            }

            return await query.ToListAsync();
        }

        // GET: api/Orders/uuid
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetOrderById", Summary = "Get Order By Id", Tags = new[] { "Orders" })]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "UpdateOrder", Summary = "Update", Tags = new[] { "Orders" })]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateOrder", Summary = "Create", Tags = new[] { "Orders" })]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")][SwaggerOperation(OperationId = "DeleteOrder", Summary = "Delete", Tags = new[] { "Orders" })]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
