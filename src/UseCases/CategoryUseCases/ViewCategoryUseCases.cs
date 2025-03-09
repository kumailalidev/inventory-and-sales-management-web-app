using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Categories;

namespace UseCases.CategoryUseCases;

public class ViewCategoryUseCases : IViewCategoriesUseCase
{
    private readonly ICategoryRepository categoryRepository; // Violation: _

    // Constructor dependency injection
    public ViewCategoryUseCases(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public IEnumerable<Category> Execute()
    {
        return categoryRepository.GetCategories();
    }
}
