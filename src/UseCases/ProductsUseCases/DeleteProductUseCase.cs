using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Products;

namespace UseCases.ProductsUseCases;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductsRepository _productsRepository;

    public DeleteProductUseCase(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void Execute(int id)
    {
        _productsRepository.DeleteProduct(id: id);
    }
}
