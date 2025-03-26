using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class Order
    {
        public Guid Id { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal TotalAmount { get; set; }
        public required Enums.PaymentMethod PaymentMethod { get; set; }
        public Enums.OrderStatus Status { get; set; } = Enums.OrderStatus.Pending;
        public Enums.PaymentStatus PaymentStatus { get; set; } = Enums.PaymentStatus.Pending;

        // Foreign Keys & Navigation Properties
        [MaxLength(75)]
        public required string UserId { get; set; }
        public required User User { get; set; }

        public List<OrderItem>? OrderItems { get; set; } // Items in the sale
    }
}
