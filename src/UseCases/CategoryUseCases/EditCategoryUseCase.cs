using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Categories;

namespace UseCases.CategoryUseCases;

public class EditCategoryUseCase : IEditCategoryUseCase
{
    private readonly ICategoryRepository categoryRepository; // Violation: _

    // Constructor dependency injection
    public EditCategoryUseCase(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public void Execute(int id, Category category)
    {
        categoryRepository.UpdateCategory(id, category);
    }
}
