using System.ComponentModel.DataAnnotations.Schema;

namespace cheez_ims_api.models
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal UnitPrice { get; set; } // Price at time of sale

        // Foreign Keys & Navigation Properties
        public Guid SaleId { get; set; }
        public required Sale Sale { get; set; }

        public Guid ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
