using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Plugins.DataStore.SQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BeforeQty = table.Column<int>(type: "int", nullable: false),
                    SoldQty = table.Column<int>(type: "int", nullable: false),
                    CashierName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic devices", "Electronics" },
                    { 2, "Apparel and fashion", "Clothing" },
                    { 3, "Food and groceries", "Groceries" },
                    { 4, "Home furniture", "Furniture" },
                    { 5, "Books and literature", "Books" },
                    { 6, "Toys for kids", "Toys" },
                    { 7, "Sports equipment", "Sports" },
                    { 8, "Beauty products", "Beauty" },
                    { 9, "Automotive accessories", "Automotive" },
                    { 10, "Jewelry and accessories", "Jewelry" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "BeforeQty", "CashierName", "Price", "ProductId", "ProductName", "SoldQty", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 50, "Alice", 799.99000000000001, 1, "Smartphone", 5, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1707) },
                    { 2, 30, "Bob", 1299.99, 2, "Laptop", 3, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1731) },
                    { 3, 100, "Charlie", 29.989999999999998, 3, "Shirt", 10, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1736) },
                    { 4, 60, "Diana", 49.990000000000002, 4, "Jeans", 8, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1740) },
                    { 5, 500, "Eve", 1.99, 5, "Apple", 50, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1743) },
                    { 6, 300, "Frank", 0.98999999999999999, 6, "Banana", 40, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1747) },
                    { 7, 10, "Grace", 499.99000000000001, 7, "Sofa", 1, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1750) },
                    { 8, 25, "Hannah", 129.99000000000001, 8, "Coffee Table", 3, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1754) },
                    { 9, 200, "Isaac", 19.989999999999998, 9, "Novel", 20, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1757) },
                    { 10, 100, "Jack", 9.9900000000000002, 10, "Toy Car", 15, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1760) },
                    { 11, 50, "Katherine", 25.989999999999998, 11, "Football", 8, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1764) },
                    { 12, 35, "Leo", 49.990000000000002, 12, "Soccer Shoes", 5, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1767) },
                    { 13, 150, "Mia", 15.99, 13, "Lipstick", 10, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1772) },
                    { 14, 250, "Noah", 9.9900000000000002, 14, "Shampoo", 30, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1776) },
                    { 15, 40, "Olivia", 79.989999999999995, 15, "Car Battery", 3, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1779) },
                    { 16, 75, "Paul", 19.989999999999998, 16, "Car Wash Kit", 7, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1782) },
                    { 17, 15, "Quinn", 299.99000000000001, 17, "Ring", 1, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1786) },
                    { 18, 20, "Rachel", 199.99000000000001, 18, "Necklace", 2, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1789) },
                    { 19, 100, "Sam", 149.99000000000001, 19, "Smartwatch", 20, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1792) },
                    { 20, 150, "Tina", 79.989999999999995, 20, "Headphones", 30, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1795) },
                    { 21, 80, "Ursula", 39.990000000000002, 21, "Sweater", 10, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1799) },
                    { 22, 40, "Victor", 89.989999999999995, 22, "Jacket", 5, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1802) },
                    { 23, 200, "Wendy", 5.9900000000000002, 23, "Water Bottle", 35, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1805) },
                    { 24, 400, "Xander", 2.9900000000000002, 24, "Soda", 50, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1808) },
                    { 25, 20, "Yara", 399.99000000000001, 25, "Washing Machine", 3, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1812) },
                    { 26, 45, "Alice", 799.99000000000001, 1, "Smartphone", 10, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1815) },
                    { 27, 28, "Bob", 1299.99, 2, "Laptop", 4, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1818) },
                    { 28, 300, "Frank", 0.98999999999999999, 6, "Banana", 60, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1821) },
                    { 29, 10, "Grace", 499.99000000000001, 7, "Sofa", 2, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1825) },
                    { 30, 25, "Hannah", 129.99000000000001, 8, "Coffee Table", 4, new DateTime(2025, 3, 9, 7, 34, 47, 478, DateTimeKind.Local).AddTicks(1828) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "Smartphone", 799.99000000000001, 50 },
                    { 2, 1, "Laptop", 1299.99, 30 },
                    { 3, 2, "Shirt", 29.989999999999998, 100 },
                    { 4, 2, "Jeans", 49.990000000000002, 60 },
                    { 5, 3, "Apple", 1.99, 500 },
                    { 6, 3, "Banana", 0.98999999999999999, 300 },
                    { 7, 4, "Sofa", 499.99000000000001, 10 },
                    { 8, 4, "Coffee Table", 129.99000000000001, 25 },
                    { 9, 5, "Novel", 19.989999999999998, 200 },
                    { 10, 6, "Toy Car", 9.9900000000000002, 100 },
                    { 11, 7, "Football", 25.989999999999998, 50 },
                    { 12, 7, "Soccer Shoes", 49.990000000000002, 35 },
                    { 13, 8, "Lipstick", 15.99, 150 },
                    { 14, 8, "Shampoo", 9.9900000000000002, 250 },
                    { 15, 9, "Car Battery", 79.989999999999995, 40 },
                    { 16, 9, "Car Wash Kit", 19.989999999999998, 75 },
                    { 17, 10, "Ring", 299.99000000000001, 15 },
                    { 18, 10, "Necklace", 199.99000000000001, 20 },
                    { 19, 1, "Smartwatch", 149.99000000000001, 100 },
                    { 20, 1, "Headphones", 79.989999999999995, 150 },
                    { 21, 2, "Sweater", 39.990000000000002, 80 },
                    { 22, 2, "Jacket", 89.989999999999995, 40 },
                    { 23, 3, "Water Bottle", 5.9900000000000002, 200 },
                    { 24, 3, "Soda", 2.9900000000000002, 400 },
                    { 25, 4, "Washing Machine", 399.99000000000001, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
