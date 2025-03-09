// MarketContext.cs
// Represents in memory representation of database

using CoreBusiness;

using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQL;

public class MarketContext : DbContext
{
    public MarketContext(DbContextOptions<MarketContext> options) : base(options)
    {
        // ...
    }
    // Database tables
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    // Overriding OnModelCreating method to define relationships
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1 to Many relationship between Categories and Products table; One category can have may products
        modelBuilder.Entity<Category>()
            .HasMany(category => category.Products) // One category has many products
            .WithOne(product => product.Category) // One product has only one category
            .HasForeignKey(product => product.CategoryId); // CategoryId inside Products table

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics", Description = "Electronic devices" },
            new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion" },
            new Category { Id = 3, Name = "Groceries", Description = "Food and groceries" },
            new Category { Id = 4, Name = "Furniture", Description = "Home furniture" },
            new Category { Id = 5, Name = "Books", Description = "Books and literature" },
            new Category { Id = 6, Name = "Toys", Description = "Toys for kids" },
            new Category { Id = 7, Name = "Sports", Description = "Sports equipment" },
            new Category { Id = 8, Name = "Beauty", Description = "Beauty products" },
            new Category { Id = 9, Name = "Automotive", Description = "Automotive accessories" },
            new Category { Id = 10, Name = "Jewelry", Description = "Jewelry and accessories" }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Smartphone", CategoryId = 1, Price = 799.99, Quantity = 50 },
            new Product { Id = 2, Name = "Laptop", CategoryId = 1, Price = 1299.99, Quantity = 30 },
            new Product { Id = 3, Name = "Shirt", CategoryId = 2, Price = 29.99, Quantity = 100 },
            new Product { Id = 4, Name = "Jeans", CategoryId = 2, Price = 49.99, Quantity = 60 },
            new Product { Id = 5, Name = "Apple", CategoryId = 3, Price = 1.99, Quantity = 500 },
            new Product { Id = 6, Name = "Banana", CategoryId = 3, Price = 0.99, Quantity = 300 },
            new Product { Id = 7, Name = "Sofa", CategoryId = 4, Price = 499.99, Quantity = 10 },
            new Product { Id = 8, Name = "Coffee Table", CategoryId = 4, Price = 129.99, Quantity = 25 },
            new Product { Id = 9, Name = "Novel", CategoryId = 5, Price = 19.99, Quantity = 200 },
            new Product { Id = 10, Name = "Toy Car", CategoryId = 6, Price = 9.99, Quantity = 100 },
            new Product { Id = 11, Name = "Football", CategoryId = 7, Price = 25.99, Quantity = 50 },
            new Product { Id = 12, Name = "Soccer Shoes", CategoryId = 7, Price = 49.99, Quantity = 35 },
            new Product { Id = 13, Name = "Lipstick", CategoryId = 8, Price = 15.99, Quantity = 150 },
            new Product { Id = 14, Name = "Shampoo", CategoryId = 8, Price = 9.99, Quantity = 250 },
            new Product { Id = 15, Name = "Car Battery", CategoryId = 9, Price = 79.99, Quantity = 40 },
            new Product { Id = 16, Name = "Car Wash Kit", CategoryId = 9, Price = 19.99, Quantity = 75 },
            new Product { Id = 17, Name = "Ring", CategoryId = 10, Price = 299.99, Quantity = 15 },
            new Product { Id = 18, Name = "Necklace", CategoryId = 10, Price = 199.99, Quantity = 20 },
            new Product { Id = 19, Name = "Smartwatch", CategoryId = 1, Price = 149.99, Quantity = 100 },
            new Product { Id = 20, Name = "Headphones", CategoryId = 1, Price = 79.99, Quantity = 150 },
            new Product { Id = 21, Name = "Sweater", CategoryId = 2, Price = 39.99, Quantity = 80 },
            new Product { Id = 22, Name = "Jacket", CategoryId = 2, Price = 89.99, Quantity = 40 },
            new Product { Id = 23, Name = "Water Bottle", CategoryId = 3, Price = 5.99, Quantity = 200 },
            new Product { Id = 24, Name = "Soda", CategoryId = 3, Price = 2.99, Quantity = 400 },
            new Product { Id = 25, Name = "Washing Machine", CategoryId = 4, Price = 399.99, Quantity = 20 }
        );

        // Seed Transactions
        modelBuilder.Entity<Transaction>().HasData(
        new Transaction { Id = 1, TimeStamp = DateTime.Now, ProductId = 1, ProductName = "Smartphone", Price = 799.99, BeforeQty = 50, SoldQty = 5, CashierName = "Alice" },
            new Transaction { Id = 2, TimeStamp = DateTime.Now, ProductId = 2, ProductName = "Laptop", Price = 1299.99, BeforeQty = 30, SoldQty = 3, CashierName = "Bob" },
            new Transaction { Id = 3, TimeStamp = DateTime.Now, ProductId = 3, ProductName = "Shirt", Price = 29.99, BeforeQty = 100, SoldQty = 10, CashierName = "Charlie" },
            new Transaction { Id = 4, TimeStamp = DateTime.Now, ProductId = 4, ProductName = "Jeans", Price = 49.99, BeforeQty = 60, SoldQty = 8, CashierName = "Diana" },
            new Transaction { Id = 5, TimeStamp = DateTime.Now, ProductId = 5, ProductName = "Apple", Price = 1.99, BeforeQty = 500, SoldQty = 50, CashierName = "Eve" },
            new Transaction { Id = 6, TimeStamp = DateTime.Now, ProductId = 6, ProductName = "Banana", Price = 0.99, BeforeQty = 300, SoldQty = 40, CashierName = "Frank" },
            new Transaction { Id = 7, TimeStamp = DateTime.Now, ProductId = 7, ProductName = "Sofa", Price = 499.99, BeforeQty = 10, SoldQty = 1, CashierName = "Grace" },
            new Transaction { Id = 8, TimeStamp = DateTime.Now, ProductId = 8, ProductName = "Coffee Table", Price = 129.99, BeforeQty = 25, SoldQty = 3, CashierName = "Hannah" },
            new Transaction { Id = 9, TimeStamp = DateTime.Now, ProductId = 9, ProductName = "Novel", Price = 19.99, BeforeQty = 200, SoldQty = 20, CashierName = "Isaac" },
            new Transaction { Id = 10, TimeStamp = DateTime.Now, ProductId = 10, ProductName = "Toy Car", Price = 9.99, BeforeQty = 100, SoldQty = 15, CashierName = "Jack" },
            new Transaction { Id = 11, TimeStamp = DateTime.Now, ProductId = 11, ProductName = "Football", Price = 25.99, BeforeQty = 50, SoldQty = 8, CashierName = "Katherine" },
            new Transaction { Id = 12, TimeStamp = DateTime.Now, ProductId = 12, ProductName = "Soccer Shoes", Price = 49.99, BeforeQty = 35, SoldQty = 5, CashierName = "Leo" },
            new Transaction { Id = 13, TimeStamp = DateTime.Now, ProductId = 13, ProductName = "Lipstick", Price = 15.99, BeforeQty = 150, SoldQty = 10, CashierName = "Mia" },
            new Transaction { Id = 14, TimeStamp = DateTime.Now, ProductId = 14, ProductName = "Shampoo", Price = 9.99, BeforeQty = 250, SoldQty = 30, CashierName = "Noah" },
            new Transaction { Id = 15, TimeStamp = DateTime.Now, ProductId = 15, ProductName = "Car Battery", Price = 79.99, BeforeQty = 40, SoldQty = 3, CashierName = "Olivia" },
            new Transaction { Id = 16, TimeStamp = DateTime.Now, ProductId = 16, ProductName = "Car Wash Kit", Price = 19.99, BeforeQty = 75, SoldQty = 7, CashierName = "Paul" },
            new Transaction { Id = 17, TimeStamp = DateTime.Now, ProductId = 17, ProductName = "Ring", Price = 299.99, BeforeQty = 15, SoldQty = 1, CashierName = "Quinn" },
            new Transaction { Id = 18, TimeStamp = DateTime.Now, ProductId = 18, ProductName = "Necklace", Price = 199.99, BeforeQty = 20, SoldQty = 2, CashierName = "Rachel" },
            new Transaction { Id = 19, TimeStamp = DateTime.Now, ProductId = 19, ProductName = "Smartwatch", Price = 149.99, BeforeQty = 100, SoldQty = 20, CashierName = "Sam" },
            new Transaction { Id = 20, TimeStamp = DateTime.Now, ProductId = 20, ProductName = "Headphones", Price = 79.99, BeforeQty = 150, SoldQty = 30, CashierName = "Tina" },
            new Transaction { Id = 21, TimeStamp = DateTime.Now, ProductId = 21, ProductName = "Sweater", Price = 39.99, BeforeQty = 80, SoldQty = 10, CashierName = "Ursula" },
            new Transaction { Id = 22, TimeStamp = DateTime.Now, ProductId = 22, ProductName = "Jacket", Price = 89.99, BeforeQty = 40, SoldQty = 5, CashierName = "Victor" },
            new Transaction { Id = 23, TimeStamp = DateTime.Now, ProductId = 23, ProductName = "Water Bottle", Price = 5.99, BeforeQty = 200, SoldQty = 35, CashierName = "Wendy" },
            new Transaction { Id = 24, TimeStamp = DateTime.Now, ProductId = 24, ProductName = "Soda", Price = 2.99, BeforeQty = 400, SoldQty = 50, CashierName = "Xander" },
            new Transaction { Id = 25, TimeStamp = DateTime.Now, ProductId = 25, ProductName = "Washing Machine", Price = 399.99, BeforeQty = 20, SoldQty = 3, CashierName = "Yara" },
            new Transaction { Id = 26, TimeStamp = DateTime.Now, ProductId = 1, ProductName = "Smartphone", Price = 799.99, BeforeQty = 45, SoldQty = 10, CashierName = "Alice" },
            new Transaction { Id = 27, TimeStamp = DateTime.Now, ProductId = 2, ProductName = "Laptop", Price = 1299.99, BeforeQty = 28, SoldQty = 4, CashierName = "Bob" },
            new Transaction { Id = 28, TimeStamp = DateTime.Now, ProductId = 6, ProductName = "Banana", Price = 0.99, BeforeQty = 300, SoldQty = 60, CashierName = "Frank" },
            new Transaction { Id = 29, TimeStamp = DateTime.Now, ProductId = 7, ProductName = "Sofa", Price = 499.99, BeforeQty = 10, SoldQty = 2, CashierName = "Grace" },
            new Transaction { Id = 30, TimeStamp = DateTime.Now, ProductId = 8, ProductName = "Coffee Table", Price = 129.99, BeforeQty = 25, SoldQty = 4, CashierName = "Hannah" });
    }
}