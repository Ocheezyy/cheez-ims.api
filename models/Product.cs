using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("name")]
        [MaxLength(100)]
        public required string Name { get; set; }
        
        [Column("description")]
        [MaxLength(200)]
        public required string Description { get; set; }
        
        [Column("sku")]
        [MaxLength(40)]
        public required string SKU { get; set; }
        
        [Column("price", TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }
        
        [Column("stock_quantity")]
        public int StockQuantity { get; set; }
        
        [Column("reorder_level")]
        public int ReorderLevel { get; set; }
        
        [Column("status")]
        public Enums.ProductStatus Status { get; set; }

        // Foreign Keys & Navigation Properties
        [Column("category_id")]
        public required Guid CategoryId { get; set; }
        
        public required Category Category { get; set; }
        
        [Column("supplier_id")]
        public required Guid SupplierId { get; set; }
        public required Supplier Supplier { get; set; }

        public List<OrderItem>? OrderItems { get; set; }  // Orders containing this product
    }
}
