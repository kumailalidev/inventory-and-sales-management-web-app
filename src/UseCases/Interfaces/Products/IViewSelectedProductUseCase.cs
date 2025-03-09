using CoreBusiness;

namespace UseCases.Interfaces.Products;

public interface IViewSelectedProductUseCase
{
    Product? Execute(int id, bool loadCategory = false);
}
