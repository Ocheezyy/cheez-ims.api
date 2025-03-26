using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cheez_ims_api.models
{
    public class Category
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(150)]
        public required string Description { get; set; }

        [JsonIgnore]
        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Category → Products
    }
}
