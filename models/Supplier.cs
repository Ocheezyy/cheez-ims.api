using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cheez_ims_api.models
{
    [Table("suppliers")]
    public class Supplier
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("name")]
        [MaxLength(60)]
        public required string Name { get; set; }
        
        [Column("contact_email")]
        [MaxLength(50)]
        public required string ContactEmail { get; set; }
        
        [Column("phone")]
        [MaxLength(35)]
        public required string Phone { get; set; }
        
        [Column("address")]
        [MaxLength(150)]
        public required string Address { get; set; }
        
        [Column("rating", TypeName = "numeric(3,1)")]
        public required decimal Rating { get; set; }
        
        [Column("status")]
        public required Enums.SupplierStatus Status { get; set; }

        [JsonIgnore]
        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Supplier → Products
    }
}
