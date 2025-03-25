using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string SKU { get; set; }  // Stock Keeping Unit
        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }  // Min stock before alert

        // Foreign Keys & Navigation Properties
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }

        public Guid? SupplierId { get; set; }  // Nullable if not all products have suppliers
        public required Supplier Supplier { get; set; }

        public List<OrderItem>? OrderItems { get; set; }  // Orders containing this product
    }
}
