namespace cheez_ims_api.models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string ContactEmail { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }

        // Navigation Property
        public List<Product>? Products { get; set; }  // One-to-Many: Supplier → Products
    }
}
