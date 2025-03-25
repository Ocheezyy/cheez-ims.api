using cheez_ims_api.Data;
using cheez_ims_api.models;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    }));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
