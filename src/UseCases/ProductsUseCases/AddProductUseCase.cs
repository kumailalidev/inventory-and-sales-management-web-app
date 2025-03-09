using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Products;

namespace UseCases.ProductsUseCases;

public class AddProductUseCase : IAddProductUseCase
{
    private readonly IProductsRepository _productsRepository;

    public AddProductUseCase(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void Execute(Product product)
    {
        _productsRepository.AddProduct(product: product);
    }
}
