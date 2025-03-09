using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Products;

namespace UseCases.ProductsUseCases;

public class ViewSelectedProductUseCase : IViewSelectedProductUseCase
{
    private readonly IProductsRepository _productsRepository;

    public ViewSelectedProductUseCase(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public Product? Execute(int id, bool loadCategory = false)
    {
        return _productsRepository.GetProductById(id: id, loadCategory: loadCategory);
    }
}
