using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class Sale
    {
        public Guid Id { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "numeric(18,2)")]
        public decimal TotalAmount { get; set; }
        public required string PaymentMethod { get; set; } // e.g., "Cash", "Credit Card"
        public string Status { get; set; } = "Completed"; // e.g., "Pending", "Refunded"

        // Foreign Keys & Navigation Properties
        public string? UserId { get; set; } // Nullable if sales aren't always user-linked
        public User? User { get; set; }    // Who processed the sale

        public required List<SaleItem> SaleItems { get; set; } // Items in the sale
    }
}
