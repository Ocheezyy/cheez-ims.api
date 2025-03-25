using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal UnitPrice { get; set; }  // Price at time of order

        // Foreign Keys & Navigation Properties
        public required Guid OrderId { get; set; }
        public required Order Order { get; set; }

        public required Guid ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
