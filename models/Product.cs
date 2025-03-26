using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class Product
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(200)]
        public required string Description { get; set; }
        [MaxLength(40)]
        public required string SKU { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }  // Min stock before alert

        // Foreign Keys & Navigation Properties
        public required Guid CategoryId { get; set; }
        public required Category Category { get; set; }

        public required Guid SupplierId { get; set; }
        public required Supplier Supplier { get; set; }

        public List<OrderItem>? OrderItems { get; set; }  // Orders containing this product
    }
}
