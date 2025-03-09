using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces;

public interface IProductsRepository
{
    IEnumerable<Product> GetProducts(bool loadCategory = false);
    Product? GetProductById(int id, bool loadCategory = false);
    IEnumerable<Product>? GetProductsByCategoryId(int id);
    void AddProduct(Product product);
    void UpdateProduct(int id, Product product);
    void DeleteProduct(int id);
}
