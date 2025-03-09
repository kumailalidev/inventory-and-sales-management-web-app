using CoreBusiness;

namespace UseCases.Interfaces.Categories;

public interface IAddCategoryUseCase
{
    void Execute(Category category);
}
