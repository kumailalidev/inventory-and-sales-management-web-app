using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Categories;

namespace UseCases.CategoryUseCases;

public class ViewSelectedCategoryUseCases : IViewSelectedCategoryUseCase
{
    private readonly ICategoryRepository categoryRepository; // Violation: _

    // Constructor dependency injection
    public ViewSelectedCategoryUseCases(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public Category? Execute(int id)
    {
        return categoryRepository.GetCategoryById(id);
    }
}
