
using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Products;

namespace UseCases.ProductsUseCases;

public class ViewProductsUseCase : IViewProductsUseCase
{
    private readonly IProductsRepository _productsRepository;

    public ViewProductsUseCase(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    public IEnumerable<Product> Execute(bool loadCategory = false)
    {
        return _productsRepository.GetProducts(loadCategory: loadCategory);
    }
}
