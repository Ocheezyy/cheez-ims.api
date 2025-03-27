using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cheez_ims_api.models
{
    [Table("categories")]
    public class Category
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("name")]
        [MaxLength(50)]
        public required string Name { get; set; }
        
        [Column("description")]
        [MaxLength(150)]
        public required string Description { get; set; }

        [JsonIgnore]
        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Category → Products
    }
}
