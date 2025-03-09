using CoreBusiness;

namespace UseCases.Interfaces.Products;

public interface IViewProductsByCategoryIdUseCase
{
    IEnumerable<Product>? Execute(int id);
}
