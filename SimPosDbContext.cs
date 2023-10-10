using Microsoft.EntityFrameworkCore;
using SIMPOS.Models;

public class SIMPOSDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Item> Items { get; set; }

    public SIMPOSDbContext(DbContextOptions<SIMPOSDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasData(new Item[]
        {
            new Item { ItemId = 1, Name = "Water", Price = 0},
            new Item { ItemId = 2, Name = "Soda", Price = 3 },
            new Item { ItemId = 3, Name = "Iced Tea", Price = 3 },
            new Item { ItemId = 4, Name = "Lemonade", Price = 4 },
            new Item { ItemId = 5, Name = "8in", Price = 12 },
            new Item { ItemId = 6, Name = "12in", Price = 14 },
            new Item { ItemId = 7, Name = "16in", Price = 18 },
            new Item { ItemId = 8, Name = "Cheese", Price = 0.50M },
            new Item { ItemId = 9, Name = "Pepperoni", Price = 0.50M },
            new Item { ItemId = 10, Name = "Sausage", Price = 0.50M },
            new Item { ItemId = 11, Name = "Bacon", Price = 0.75M },
            new Item { ItemId = 12, Name = "Mushroom", Price = 0.50M },
            new Item { ItemId = 13, Name = "Red & Green Peppers", Price = 0.50M },
            new Item { ItemId = 14, Name = "Banana Pepper", Price = 0.75M },
            new Item { ItemId = 15, Name = "Onion", Price = 0.50M },
            new Item { ItemId = 16, Name = "8ct", Price = 8 },
            new Item { ItemId = 17, Name = "12ct", Price = 12 },
            new Item { ItemId = 18, Name = "16ct", Price = 16 },
            new Item { ItemId = 19, Name = "Sweet BBQ", Price = 0.50M },
            new Item { ItemId = 20, Name = "Mild", Price = 0.50M },
            new Item { ItemId = 21, Name = "Medium", Price = 0.50M },
            new Item { ItemId = 22, Name = "Hot", Price = 0.50M },
            new Item { ItemId = 23, Name = "Spicy Garlic", Price = 0.50M }
        });
    }
}