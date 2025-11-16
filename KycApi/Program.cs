using KycApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Temporary test output in the console
Console.WriteLine("==== Connection String Debug ====");
Console.WriteLine(connectionString ?? "Connection string is null!");
Console.WriteLine("================================");

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Test database connection at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        if (db.Database.CanConnect())
        {
            Console.WriteLine("Database connection successful!");
            db.Database.EnsureCreated();
        }
        else
        {
            Console.WriteLine("Cannot connect to database. Check server and connection string.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database connection failed: " + ex.Message);
    }
}

// Middleware
//app.UseSwagger();
//app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KycApi v1");
        c.RoutePrefix = string.Empty; // opens at root: https://localhost:7257/
    });
}


app.MapControllers();

// Temporary endpoint to check the connection string in browser
app.MapGet("/test-connection-string", () =>
{
    return connectionString ?? "Connection string is null!";
});

app.Run();
