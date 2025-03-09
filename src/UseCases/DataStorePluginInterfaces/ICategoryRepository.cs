
using CoreBusiness;

namespace UseCases.CategoryUseCases.DataStorePluginInterfaces;

public interface ICategoryRepository
{
    void AddCategory(Category category);
    void DeleteCategory(int id);
    IEnumerable<Category> GetCategories();
    Category? GetCategoryById(int id);
    void UpdateCategory(int id, Category category);
}