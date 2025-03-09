using CoreBusiness;

namespace UseCases.Interfaces.Products;

public interface IAddProductUseCase
{
    void Execute(Product product);
}
