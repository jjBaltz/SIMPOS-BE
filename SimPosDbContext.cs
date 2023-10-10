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
            new Item { ItemId = 1, Type = "Drink", Name = "Water", Price = 0},
            new Item { ItemId = 2, Type = "Drink", Name = "Soda", Price = 3 },
            new Item { ItemId = 3, Type = "Drink", Name = "Iced Tea", Price = 3 },
            new Item { ItemId = 4, Type = "Drink", Name = "Lemonade", Price = 4 },
            new Item { ItemId = 5, Type = "Pizza", Name = "8in", Price = 12 },
            new Item { ItemId = 6, Type = "Pizza", Name = "12in", Price = 14 },
            new Item { ItemId = 7, Type = "Pizza", Name = "16in", Price = 18 },
            new Item { ItemId = 8, Type = "Pizza", Name = "Cheese", Price = 0.50M },
            new Item { ItemId = 9, Type = "Pizza", Name = "Pepperoni", Price = 0.50M },
            new Item { ItemId = 10, Type = "Pizza", Name = "Sausage", Price = 0.50M },
            new Item { ItemId = 11, Type = "Pizza", Name = "Bacon", Price = 0.75M },
            new Item { ItemId = 12, Type = "Pizza", Name = "Mushroom", Price = 0.50M },
            new Item { ItemId = 13, Type = "Pizza", Name = "Red & Green Peppers", Price = 0.50M },
            new Item { ItemId = 14, Type = "Pizza", Name = "Banana Pepper", Price = 0.75M },
            new Item { ItemId = 15, Type = "Pizza", Name = "Onion", Price = 0.50M },
            new Item { ItemId = 16, Type = "Wings", Name = "8ct", Price = 8 },
            new Item { ItemId = 17, Type = "Wings", Name = "12ct", Price = 12 },
            new Item { ItemId = 18, Type = "Wings", Name = "16ct", Price = 16 },
            new Item { ItemId = 19, Type = "Wings", Name = "Sweet BBQ", Price = 0.50M },
            new Item { ItemId = 20, Type = "Wings", Name = "Mild", Price = 0.50M },
            new Item { ItemId = 21, Type = "Wings", Name = "Medium", Price = 0.50M },
            new Item { ItemId = 22, Type = "Wings", Name = "Hot", Price = 0.50M },
            new Item { ItemId = 23, Type = "Wings", Name = "Spicy Garlic", Price = 0.50M }
        });
    }
}