using cheez_ims_api.Data;
using cheez_ims_api.models;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


const string corsOrigins = "ProdAndLocalhost";


builder.Services.AddCors(options =>
{
    options.AddPolicy(corsOrigins,
        policy =>
        {
            policy.WithOrigins("https://cheez-ims.ocheezy.dev")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cheez IMS API",  // Replace with your API name
        Version = "v1",           // Ensures OpenAPI versioning works correctly
        Description = "API for inventory management"
    });
    c.EnableAnnotations();
});

string ParsePostgresString(string pgConnectionString)
{
    // Example: postgresql://postgres:password@host:port/db_name
    var uri = new Uri(pgConnectionString.Replace("postgresql://", "http://"));

    var username = uri.UserInfo.Split(':')[0];
    var password = uri.UserInfo.Split(':')[1];
    var host = uri.Host;
    var port = uri.Port;
    var dbName = uri.AbsolutePath.TrimStart('/');

    // Format to EF Core connection string: Host=host;Port=port;Database=db_name;Username=username;Password=password;
    var efCoreConnectionString = $"Host={host};Port={port};Database={dbName};Username={username};Password={password};";

    return efCoreConnectionString;
}

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var connectionString = string.IsNullOrEmpty(databaseUrl) ? builder.Configuration.GetConnectionString("DefaultConnection")
    : ParsePostgresString(databaseUrl);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, o =>
    {
        o.MapEnum<Enums.PaymentMethod>("payment_method");
        o.MapEnum<Enums.OrderStatus>("order_status");
        o.MapEnum<Enums.PaymentStatus>("payment_status");
        o.MapEnum<Enums.ActivityType>("activity_type");
        o.MapEnum<Enums.ProductStatus>("product_status");
    }));




var app = builder.Build();

app.UseCors(corsOrigins);

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     dbContext.Database.Migrate();
//     DatabaseSeeder.Seed(scope.ServiceProvider);
// }

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     
// }

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cheez IMS API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
