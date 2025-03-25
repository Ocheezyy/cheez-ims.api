using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class Order
    {
        public Guid Id { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "timestamp with time zone")]
        public DateTime? DeliveryDate { get; set; }  // Nullable until delivered
        public string Status { get; set; } = "Pending";  // e.g., "Shipped", "Delivered"

        // Foreign Key & Navigation Properties
        public Guid SupplierId { get; set; }
        public required Supplier Supplier { get; set; }

        public required List<OrderItem> OrderItems { get; set; }  // One-to-Many: Order → OrderItems
    }
}
