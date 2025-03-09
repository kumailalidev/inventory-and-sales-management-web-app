
using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class ProductsInMemoryRepository : IProductsRepository
{
    private readonly ICategoryRepository _categoryRepository;

    public ProductsInMemoryRepository(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    private static List<Product> _products = new List<Product>()
    {
        new Product { Id = 1, Name = "Smartphone", CategoryId = 1, Price = 799.99, Quantity = 50 },
        new Product { Id = 2, Name = "Laptop", CategoryId = 1, Price = 1299.99, Quantity = 30 },
        new Product { Id = 3, Name = "Shirt", CategoryId = 2, Price = 29.99, Quantity = 100 },
        new Product { Id = 4, Name = "Jeans", CategoryId = 2, Price = 49.99, Quantity = 60 },
        new Product { Id = 5, Name = "Apple", CategoryId = 3, Price = 1.99, Quantity = 500 },
    };

    public void AddProduct(Product product)
    {
        if (_products != null && _products.Count() > 0)
        {
            var maxId = _products.Max(x => x.Id);
            product.Id = maxId + 1;
        }
        else
        {
            product.Id = 1;
        }

        // 'null' fix
        if (_products == null)
            _products = new List<Product>();

        _products.Add(product);
    }

    public IEnumerable<Product> GetProducts(bool loadCategory = false)
    {
        if (!loadCategory)
        {
            return _products;
        }
        else
        {
            if (_products != null && _products.Count > 0)
            {
                _products.ForEach(prod =>
                {
                    if (prod.CategoryId.HasValue)
                        prod.Category = _categoryRepository.GetCategoryById(prod.CategoryId.Value);
                });
            }

            return _products ?? new List<Product>(); // If it is null return new list of products
        }
    }

    public Product? GetProductById(int productId, bool loadCategory = false)
    {
        var product = _products.FirstOrDefault(x => x.Id == productId);
        if (product != null)
        {
            var prod = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            if (loadCategory && prod.CategoryId.HasValue)
            {
                prod.Category = _categoryRepository.GetCategoryById(prod.CategoryId.Value);
            }
            return prod;
        }

        return null;
    }

    public void UpdateProduct(int productId, Product product)
    {
        if (productId != product.Id) return;

        var productToUpdate = _products.FirstOrDefault(x => x.Id == productId);
        if (productToUpdate != null)
        {
            productToUpdate.Name = product.Name;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.Price = product.Price;
            productToUpdate.CategoryId = product.CategoryId;
        }
    }

    public void DeleteProduct(int productId)
    {
        var product = _products.FirstOrDefault(x => x.Id == productId);
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        var products = _products.Where(product => product.CategoryId == categoryId);
        if (products != null)
            return products.ToList();

        return new List<Product>();
    }
}
