using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreBusiness;

using Microsoft.EntityFrameworkCore;

using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class ProductSQLRepository : IProductsRepository
    {
        private readonly MarketContext db;

        public ProductSQLRepository(MarketContext db)
        {
            this.db = db;
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return;
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product? GetProductById(int id, bool loadCategory = false)
        {
            if (loadCategory)
                return db.Products
                        .Include(p => p.Category) // Uses 'Category' navigation property inside Product model
                        .FirstOrDefault(p => p.Id == id);
            else
                return db.Products
                        .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(bool loadCategory = false)
        {
            if (loadCategory)
                return db.Products.Include(p => p.Category).OrderBy(p => p.Id).ToList();
            else
                return db.Products.OrderBy(p => p.Id).ToList();
        }

        public IEnumerable<Product>? GetProductsByCategoryId(int id)
        {
            return db.Products.Where(p => p.CategoryId == id).ToList();
        }

        public void UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return;

            var prod = db.Products.Find(id);
            if (prod == null)
                return;

            // Updating the product
            prod.CategoryId = product.CategoryId;
            prod.Name = product.Name;
            prod.Price = product.Price;
            prod.Quantity = product.Quantity;

            db.SaveChanges();
        }
    }
}
