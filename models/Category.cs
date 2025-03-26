using System.ComponentModel.DataAnnotations;

namespace cheez_ims_api.models
{
    public class Category
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(150)]
        public required string Description { get; set; }

        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Category → Products
    }
}
