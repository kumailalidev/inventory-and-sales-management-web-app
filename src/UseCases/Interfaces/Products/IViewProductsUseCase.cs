using CoreBusiness;

namespace UseCases.Interfaces.Products;

public interface IViewProductsUseCase
{
    IEnumerable<Product> Execute(bool loadCategory = false);
}
