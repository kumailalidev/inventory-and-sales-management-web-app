using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class CategoriesInMemoryRepository : ICategoryRepository
{
    // In memory data store
    private List<Category> _categories = new List<Category>()
    {
        new Category { Id = 1, Name = "Electronics", Description = "Electronic devices" },
        new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion" },
        new Category { Id = 3, Name = "Groceries", Description = "Food and groceries" },
    };

    public void AddCategory(Category category)
    {
        if (_categories != null && _categories.Count() > 0)
        {
            var maxId = _categories.Max(x => x.Id);
            category.Id = maxId + 1;
        }
        else
        {
            category.Id = 1;
        }

        // 'null' fix
        if (_categories == null)
            _categories = new List<Category>();

        _categories.Add(category);
    }

    public IEnumerable<Category> GetCategories() => _categories;

    public Category? GetCategoryById(int id)
    {
        var category = _categories.FirstOrDefault(cat => cat.Id == id);
        if (category != null)
            // Create copy (just like actual database, it always return copy of data)
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

        return null;
    }

    public void UpdateCategory(int id, Category category)
    {
        if (id != category.Id)
            return;

        // var categoryToUpdate = GetCategoryById(id); // ISSUE: returns copy instead of in memory data
        var categoryToUpdate = _categories.FirstOrDefault(cat => cat.Id == id);

        if (categoryToUpdate != null)
        {
            // Can also use AutoMapper
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
        }
    }

    public void DeleteCategory(int id)
    {
        var category = _categories.FirstOrDefault(cat => cat.Id == id);
        if (category != null)
            _categories.Remove(category);
    }
}
