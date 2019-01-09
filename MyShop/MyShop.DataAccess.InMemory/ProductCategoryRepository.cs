using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
using MyShop.Core;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory category)
        {
            productCategories.Add(category);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(c => c.Id == category.Id);
            if(productCategories != null)
            {
                productCategoryToUpdate = category;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(c => c.Id == id);
            if(productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory category = productCategories.Find(c => c.Id == id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
    }

}
