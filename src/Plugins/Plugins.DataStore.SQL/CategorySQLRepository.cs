using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreBusiness;

using UseCases.CategoryUseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class CategorySQLRepository : ICategoryRepository
    {
        private readonly MarketContext db;

        public CategorySQLRepository(MarketContext db)
        {
            this.db = db;
        }
         
        public void AddCategory(Category category)
        {
            db.Categories.Add(entity: category);
            db.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
                return;

            db.Categories.Remove(entity: category);
            db.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return db.Categories.Find(id);
        }

        public void UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
                return;

            var cat = db.Categories.Find(id);
            if (cat == null)
                return;

            cat.Name = category.Name;
            cat.Description = category.Description;

            db.SaveChanges();
        }
    }
}
