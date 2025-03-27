using Microsoft.EntityFrameworkCore;
using Bogus;
using cheez_ims_api.models;

namespace cheez_ims_api.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            if (!context.Orders.Any())
            {
                var faker = new Faker();

                // Seed Categories
                var categories = new Faker<Category>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                    .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                    .Generate(110);
                context.Categories.AddRange(categories);
                context.SaveChanges();

                // Seed Suppliers
                var suppliers = new Faker<Supplier>()
                    .RuleFor(s => s.Id, f => Guid.NewGuid())
                    .RuleFor(s => s.Name, f => f.Company.CompanyName())
                    .RuleFor(s => s.ContactEmail, f => f.Internet.Email())
                    .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(s => s.Address, f => f.Address.StreetAddress())
                    .Generate(90);
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();

                // Seed Products
                var products = new Faker<Product>()
                    .RuleFor(p => p.Id, f => Guid.NewGuid())
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.SKU, f => f.Commerce.Ean13())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(5, 500))
                    .RuleFor(p => p.StockQuantity, f => f.Random.Int(10, 500))
                    .RuleFor(p => p.ReorderLevel, f => f.Random.Int(1, 20))
                    .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id)
                    .RuleFor(p => p.SupplierId, f => f.PickRandom(suppliers).Id)
                    .Generate(400);
                context.Products.AddRange(products);
                context.SaveChanges();

                // Seed Users
                var users = new Faker<User>()
                    .RuleFor(u => u.Id, f => Guid.NewGuid())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.UserName, f => f.Internet.UserName())
                    .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                    .Generate(500);
                context.Users.AddRange(users);
                context.SaveChanges();

                // Seed Orders
                var orders = new Faker<Order>()
                    .RuleFor(o => o.Id, f => Guid.NewGuid())
                    .RuleFor(o => o.OrderDate, f => f.Date.Past(1).ToUniversalTime())
                    .RuleFor(o => o.DeliveryDate, (f, o) => f.Random.Bool(0.7f) ? f.Date.Soon(30, o.OrderDate).ToUniversalTime() : (DateTime?)null)
                    .RuleFor(o => o.TotalAmount, f => f.Random.Decimal(20, 2000))
                    .RuleFor(o => o.PaymentMethod, f => f.PickRandom<Enums.PaymentMethod>())
                    .RuleFor(o => o.Status, f => f.PickRandom<Enums.OrderStatus>())
                    .RuleFor(o => o.PaymentStatus, f => f.PickRandom<Enums.PaymentStatus>())
                    .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
                    .Generate(1000);
                context.Orders.AddRange(orders);
                context.SaveChanges();

                // Seed OrderItems
                var orderItems = new Faker<OrderItem>()
                    .RuleFor(oi => oi.Id, f => Guid.NewGuid())
                    .RuleFor(oi => oi.OrderId, f => f.PickRandom(orders).Id)
                    .RuleFor(oi => oi.ProductId, f => f.PickRandom(products).Id)
                    .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 10))
                    .RuleFor(oi => oi.UnitPrice, (f, oi) => products.First(p => p.Id == oi.ProductId).Price)
                    .Generate(5000);
                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();
            }
        }
    }
}