using SIMPOS.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<SIMPOSDbContext>(builder.Configuration["SIMPOSDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//ITEMS APIs
app.MapGet("/api/items", (SIMPOSDbContext db) =>
{
    return db.Items.ToList();
});

app.MapGet("api/items/{type}", (SIMPOSDbContext db, string type) =>
{
    return db.Items
        .Where(i => i.Type == type)
        .ToList();
});

//ORDER APIs
app.MapGet("api/orders", (SIMPOSDbContext db) =>
{
    return db.Orders.ToList();
});

app.MapGet("api/orders/{id}", (SIMPOSDbContext db, int id) =>
{
    return db.Orders.Single(order => order.OrderId == id);
});

app.MapPost("api/orders", (SIMPOSDbContext db, Order order) =>
{
    try
    {
        db.Orders.Add(order);
        db.SaveChanges();
        return Results.Created($"api/orders/{order.OrderId}", order);
    }
    catch (DbUpdateException)
    {
        return Results.NotFound();
    }
});

app.MapPut("/api/orders/{id}", (SIMPOSDbContext db, int id, Order order) =>
{
    Order orderToUpdate = db.Orders.SingleOrDefault(order => order.OrderId == id);
    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }
    orderToUpdate.Status = order.Status;
    orderToUpdate.Type = order.Type;
    orderToUpdate.PaymentType = order.PaymentType;
    orderToUpdate.Total = order.Total;
    orderToUpdate.Rating = order.Rating;

    db.Update(orderToUpdate);
    db.SaveChanges();
    return Results.Ok(orderToUpdate);
});

app.MapDelete("/api/orders/{id}", (SIMPOSDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(order => order.OrderId == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();
