using CoreBusiness;

namespace UseCases.Interfaces.Categories;

public interface IViewCategoriesUseCase
{
    IEnumerable<Category> Execute();
}
