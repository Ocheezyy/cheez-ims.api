using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace cheez_ims_api.models
{
    [Table("orders")]
    public class Order
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("order_date", TypeName = "timestamp with time zone")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        [Column("delivery_date", TypeName = "timestamp with time zone")]
        public DateTime? DeliveryDate { get; set; }
        
        [Column("total_amount", TypeName = "numeric(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [Column("payment_method")]
        public required Enums.PaymentMethod PaymentMethod { get; set; }
        
        [Column("order_status")]
        public Enums.OrderStatus Status { get; set; } = Enums.OrderStatus.Pending;
        
        [Column("payment_status")]
        public Enums.PaymentStatus PaymentStatus { get; set; } = Enums.PaymentStatus.Pending;

        // Foreign Keys & Navigation Properties
        [Column("user_id")]
        public required Guid UserId { get; set; }
        public required User User { get; set; }

        public List<OrderItem>? OrderItems { get; set; } // Items in the sale
    }
}
