using UseCases.CategoryUseCases.DataStorePluginInterfaces;

namespace UseCases.Interfaces.Categories;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly ICategoryRepository categoryRepository; // Violation: _

    // Constructor dependency injection
    public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public void Execute(int id)
    {
        categoryRepository.DeleteCategory(id);
    }
}
