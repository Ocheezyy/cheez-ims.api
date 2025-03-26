using cheez_ims_api.Data;
using cheez_ims_api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace cheez_ims_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        [SwaggerOperation(OperationId = "GetProducts", Summary = "Get Products", Tags = new[] { "Products" })]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? include = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(include))
            {
                var includes = include.Split(',');
                if (includes.Contains("category"))
                    query = query.Include(p => p.Category);   // Include Category
                if (includes.Contains("supplier"))
                    query = query.Include(p => p.Supplier);   // Include Supplier
            }
            return await query.ToListAsync();
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetProductById", Summary = "Get Product By Id", Tags = new[] { "Products" })]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/products
        [HttpPost]
        [SwaggerOperation(OperationId = "AddProduct", Summary = "Add Product", Tags = new[] { "Products" })]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Basic validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure Category and Supplier exist
            var category = await _context.Categories.FindAsync(product.CategoryId);
            var supplier = await _context.Suppliers.FindAsync(product.SupplierId);

            if (category == null || supplier == null)
            {
                return BadRequest("Invalid CategoryId or SupplierId");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "UpdateProduct", Summary = "Update Product", Tags = new[] { "Products" })]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if product exists
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update properties
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.SupplierId = product.SupplierId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteProduct", Summary = "Delete", Tags = new[] { "Products" })]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
