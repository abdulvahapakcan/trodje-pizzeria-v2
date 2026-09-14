using Microsoft.EntityFrameworkCore;
using PizzeriaOrder.Api.Data;
using PizzeriaOrder.Api.Services.Email;
using PizzeriaOrder.Api.Services.Menu;
using PizzeriaOrder.Api.Services.Orders;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is missing.");
}

builder.Services.AddDbContext<PizzeriaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Controllers
builder.Services.AddControllers();

// Services
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, BrevoEmailService>();

// HttpClient för Brevo
builder.Services.AddHttpClient();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://trodjepizzeria.com",
                "https://www.trodjepizzeria.com",
                "https://trodje-pizzeria.pages.dev",
                "http://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();