using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;

using UseCases.CategoryUseCases;
using UseCases.CategoryUseCases.DataStorePluginInterfaces;
using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Categories;
using UseCases.Interfaces.Products;
using UseCases.Interfaces.Transactions;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUsesCases;

using WebApp.Authorization;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Adding services
builder.Services.AddControllersWithViews();

// Dependency injection

// Entity Framework Core
builder.Services.AddDbContext<MarketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "Default"));
});

// Identity (Uses same database)
builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "Default"));
});

builder.Services
    .AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AccountContext>();

// Register custom authorization handlers
builder.Services.AddSingleton<IAuthorizationHandler, AdminOrManagerHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminOrSellerHandler>();

// Authorization Configuration
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireClaim("Position", "Admin"));
    options.AddPolicy("Seller", p => p.RequireClaim("Position", "Seller"));
    options.AddPolicy("Manager", p => p.RequireClaim("Position", "Manager"));

    // Define the combined custom policy using the custom handler
    options.AddPolicy("AdminOrManager", policy =>
        policy.Requirements.Add(new AdminOrManagerRequirement()));
    // Define the combined custom policy using the custom handler
    options.AddPolicy("AdminOrSeller", policy =>
        policy.Requirements.Add(new AdminOrSellerRequirement()));
});

// Razor pages
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

if (builder.Environment.IsEnvironment(environmentName: "QA"))
{
    // Load in memory database
    builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>(); // Only one instance of categories will exist through the lifespan of application; ASP.NET is not stateful application by default
    builder.Services.AddSingleton<IProductsRepository, ProductsInMemoryRepository>();
    builder.Services.AddSingleton<ITransactionsRepository, TransactionsInMemoryRepository>();
}
else
{
    // Load data from SQL server
    builder.Services.AddTransient<ICategoryRepository, CategorySQLRepository>();
    builder.Services.AddTransient<IProductsRepository, ProductSQLRepository>();
    builder.Services.AddTransient<ITransactionsRepository, TransactionSQLRepository>();
}

// Categories
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoryUseCases>(); // Every time a user request a page a new instance will be created for a page
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCases>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

// Products
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();
builder.Services.AddTransient<IViewProductsByCategoryIdUseCase, ViewProductsByCategoryIdUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();

// Transactions
builder.Services.AddTransient<IAddTransactionUseCase, AddTransactionUseCase>();
builder.Services.AddTransient<IGetTransactionsByDayAndCashierUseCase, GetTransactionsByDayAndCashierUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();

var app = builder.Build();

// Apply any pending migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Seed the users and roles
    await DataSeeder.SeedAsync(userManager, roleManager);
}

// Middleware Section

// Static files middleware
app.UseStaticFiles();

app.UseRouting();

// Use Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    // Default route ('/', controller, action, and id values are optional)
    // controller/action?id=foo
    // id? is optional query string
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
