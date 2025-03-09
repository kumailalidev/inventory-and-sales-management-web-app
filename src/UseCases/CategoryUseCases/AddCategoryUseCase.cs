using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Categories;

namespace UseCases.CategoryUseCases;

public class AddCategoryUseCase : IAddCategoryUseCase
{
    private readonly ICategoryRepository categoryRepository; // Violation: _

    // Constructor dependency injection
    public AddCategoryUseCase(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public void Execute(Category category)
    {
        categoryRepository.AddCategory(category);
    }
}
