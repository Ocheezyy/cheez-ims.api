using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cheez_ims_api.models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        [MaxLength(60)]
        public required string Name { get; set; }
        [MaxLength(50)]
        public required string ContactEmail { get; set; }
        [MaxLength(35)]
        public required string Phone { get; set; }
        [MaxLength(100)]
        public required string Address { get; set; }

        [JsonIgnore]
        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Supplier → Products
    }
}
