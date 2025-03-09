using CoreBusiness;

namespace UseCases.Interfaces.Products;

public interface IEditProductUseCase
{
    void Execute(int id, Product product);
}
