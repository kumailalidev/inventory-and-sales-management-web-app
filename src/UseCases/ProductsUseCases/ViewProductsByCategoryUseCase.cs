
using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Products;

namespace UseCases.ProductsUseCases;

public class ViewProductsByCategoryIdUseCase : IViewProductsByCategoryIdUseCase
{
    private readonly IProductsRepository _productsRepository;

    public ViewProductsByCategoryIdUseCase(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public IEnumerable<Product>? Execute(int id)
    {
        return _productsRepository.GetProductsByCategoryId(id: id);
    }
}
