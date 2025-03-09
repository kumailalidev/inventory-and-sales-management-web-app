using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Products;

namespace UseCases.ProductsUseCases;

public class EditProductUseCase : IEditProductUseCase
{
    private readonly IProductsRepository _productsRepository;

    public EditProductUseCase(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void Execute(int id, Product product)
    {
        _productsRepository.UpdateProduct(id: id, product: product);
    }
}
