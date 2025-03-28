using Bogus;
using cheez_ims_api.models;

namespace cheez_ims_api.Data
{
    public static class DatabaseSeeder
    {
        private static Enums.ProductStatus GetProductStatus(int stockQuantity, int reorderLevel, Faker f)
        {
            if (stockQuantity == 0)
            {
                var stockZeroOptions = new[] { Enums.ProductStatus.OutOfStock, Enums.ProductStatus.Discontinued };
                return f.PickRandom(stockZeroOptions);
            }
            if (stockQuantity < reorderLevel)
                return Enums.ProductStatus.LowStock;
            
            return Enums.ProductStatus.InStock;
        }

        private static string GetActivityMessage(Enums.ActivityType activityType, Product product, Order order, Supplier supplier, Faker f)
        {
            if (activityType == Enums.ActivityType.RestockProduct)
            {
                var random = new Random();
                return $"Added {random.Next(1, 35)} units of {product.Name}";
            }

            if (activityType == Enums.ActivityType.CreateOrder)
            {
                return $"Created order #{order.OrderNumber}";
            }

            if (activityType == Enums.ActivityType.ShippedOrder)
            {
                return $"Shipped order #{order.OrderNumber}";
            }

            if (activityType == Enums.ActivityType.CreateSupplier)
            {
                return $"Added supplier {supplier.Name}";
            }

            if (activityType == Enums.ActivityType.CreateProduct)
            {
                return $"Created new product {product.Name}";
            }

            if (activityType == Enums.ActivityType.LowStockProduct)
            {
                return $"Marked {product.Name} as low stock";
            }
            
            return f.Lorem.Sentence();
        }
        
        
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            if (!context.Orders.Any())
            {
                var faker = new Faker();
                
                var categories = new Faker<Category>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                    .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                    .Generate(110);
                context.Categories.AddRange(categories);
                context.SaveChanges();
                
                var suppliers = new Faker<Supplier>()
                    .RuleFor(s => s.Id, f => Guid.NewGuid())
                    .RuleFor(s => s.Name, f => f.Company.CompanyName())
                    .RuleFor(s => s.ContactEmail, f => f.Internet.Email())
                    .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(s => s.Address, f => f.Address.StreetAddress())
                    .Generate(90);
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
                
                var products = new Faker<Product>()
                    .RuleFor(p => p.Id, f => Guid.NewGuid())
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.SKU, f => f.Commerce.Ean13())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(5, 500))
                    .RuleFor(p => p.StockQuantity, f => f.Random.Int(10, 500))
                    .RuleFor(p => p.ReorderLevel, f => f.Random.Int(1, 20))
                    .RuleFor(p => p.Status, (f, p) => GetProductStatus(p.StockQuantity, p.ReorderLevel, f))
                    .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id)
                    .RuleFor(p => p.SupplierId, f => f.PickRandom(suppliers).Id)
                    .Generate(400);
                var random = new Random();
                for (int i = 0; i < random.Next(5, 30); i++)
                {
                    var product = products[i];
                    product.StockQuantity = product.ReorderLevel - random.Next(1, product.ReorderLevel - 2);
                    product.Status = GetProductStatus(product.StockQuantity, product.ReorderLevel, new Faker());
                }
                context.Products.AddRange(products);
                context.SaveChanges();
                
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
                
                var orders = new Faker<Order>()
                    .RuleFor(o => o.Id, f => Guid.NewGuid())
                    .RuleFor(o => o.OrderNumber, f => f.Random.Int())
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
                
                var orderItems = new Faker<OrderItem>()
                    .RuleFor(oi => oi.Id, f => Guid.NewGuid())
                    .RuleFor(oi => oi.OrderId, f => f.PickRandom(orders).Id)
                    .RuleFor(oi => oi.ProductId, f => f.PickRandom(products).Id)
                    .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 10))
                    .RuleFor(oi => oi.UnitPrice, (f, oi) => products.First(p => p.Id == oi.ProductId).Price)
                    .Generate(5000);
                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();

                var activities = new Faker<Activity>()
                    .RuleFor(a => a.Id, f => Guid.NewGuid())
                    .RuleFor(a => a.ActivityType, f => f.PickRandom<Enums.ActivityType>())
                    .RuleFor(a => a.Message, (f, a) => 
                        GetActivityMessage(
                            a.ActivityType, 
                            f.PickRandom(products), 
                            f.PickRandom(orders), 
                            f.PickRandom(suppliers), f)
                        )
                    .RuleFor(o => o.Timestamp, f => f.Date.Past(1).ToUniversalTime())
                    .RuleFor(a => a.UserId, f => f.PickRandom(users).Id)
                    .Generate(400);
                context.Activities.AddRange(activities);
                context.SaveChanges();
            }
        }
    }
}