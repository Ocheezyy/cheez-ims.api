using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cheez_ims_api.models
{
    [Table("order_items")]
    public class OrderItem
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("quantity")]
        public int Quantity { get; set; }
        
        [Column(TypeName = "numeric(18,2)")]
        public decimal UnitPrice { get; set; }  // Price at time of order

        // Foreign Keys & Navigation Properties
        [Column("order_id")]
        public required Guid OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }

        [Column("product_id")]
        public required Guid ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
