using CoreBusiness;

namespace UseCases.Interfaces.Categories;

public interface IEditCategoryUseCase
{
    void Execute(int id, Category category);
}
