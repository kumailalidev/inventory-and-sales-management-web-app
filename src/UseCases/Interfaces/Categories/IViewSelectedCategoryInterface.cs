using CoreBusiness;

namespace UseCases.Interfaces.Categories;

public interface IViewSelectedCategoryUseCase
{
    Category? Execute(int id);
}
