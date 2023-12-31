using SIMPOS.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var DefaultCors = "_DefaultCors";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DefaultCors,

        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7252")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
        });
});

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
app.UseCors(DefaultCors);

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

app.MapPost("api/orderitem/{orderId}/{itemId}", (SIMPOSDbContext db, int orderId, int itemId) =>
{
    OrderItem orderItem = new OrderItem()
    {
        OrderId = orderId,
        ItemId = itemId,
    };

    try
    {
        db.OrderItems.Add(orderItem);
        db.SaveChanges();
        return Results.Ok(orderItem);
    }
    catch (DbUpdateException)
    {
        return Results.NotFound();
    }
});

app.MapDelete("api/orderitem/{id}", (SIMPOSDbContext db, int id) =>
{
    OrderItem orderItem = db.OrderItems.SingleOrDefault(oi => oi.OrderItemId == id);
    if (orderItem == null)
    {
        return Results.NotFound();
    }
    db.OrderItems.Remove(orderItem);
    db.SaveChanges();
    return Results.NoContent();
});

//ORDER APIs
app.MapGet("api/orders", (SIMPOSDbContext db) =>
{
    return db.Orders.ToList();
});

app.MapGet("api/orders/{id}", (SIMPOSDbContext db, int id) =>
{
    return db.Orders
        .Include(order => order.Customer)
        .Single(order => order.OrderId == id);
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

//CUSTOMER APIs
app.MapGet("api/customers", (SIMPOSDbContext db) =>
{
    return db.Customers.ToList();
});

app.MapGet("api/customers/{id}", (SIMPOSDbContext db, int id) =>
{
    return db.Customers
        .Include(customer => customer.Orders)
        .Single(customer => customer.CustomerId == id);
});

app.MapPost("/api/customers", (SIMPOSDbContext db, Customer customer) =>
{
    try
    {
        db.Customers.Add(customer);
        db.SaveChanges();
        return Results.Created($"/api/customers/{customer.CustomerId}", customer);
    }
    catch (DbUpdateException)
    {
        return Results.NotFound();
    }
});

app.MapPut("/api/customers/{id}", (SIMPOSDbContext db, int id, Customer customer) =>
{
    Customer customerToUpdate = db.Customers.SingleOrDefault(customer => customer.CustomerId == id);
    if (customerToUpdate == null)
    {
        return Results.NotFound();
    }
    customerToUpdate.FirstName = customer.FirstName;
    customerToUpdate.LastName = customer.LastName;
    customerToUpdate.Email = customer.Email;
    customerToUpdate.PhoneNumber = customer.PhoneNumber;

    db.Update(customerToUpdate);
    db.SaveChanges();
    return Results.Ok(customerToUpdate);
});

app.MapDelete("/api/customers/{id}", (SIMPOSDbContext db, int id) =>
{
    Customer customer = db.Customers.SingleOrDefault(customer => customer.CustomerId == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    db.Customers.Remove(customer);
    db.SaveChanges();
    return Results.NoContent();
});

//USER APIs
app.MapGet("/api/checkuser/{uid}", (SIMPOSDbContext db, string uid) =>
{
    var userExist = db.Users.Where(user => user.UID == uid).ToList();
    if (userExist == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(userExist);
});

app.MapPost("/api/register", (SIMPOSDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/user/user.Id", user);
});

app.MapGet("/api/users", (SIMPOSDbContext db) =>
{
    return db.Users.ToList();
});

app.Run();