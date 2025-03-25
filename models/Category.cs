namespace cheez_ims_api.models
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Category → Products
    }
}
