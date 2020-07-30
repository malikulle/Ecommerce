using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.DataAccess.EfCore;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete.EfCore
{
    public class EfCoreProductRepository :EfCoreGenericRepository<Product,ECommerceContext>,IProductRepository
    {
        public List<Product> GetProductsByCategory(int category, int page,int pageSize)
        {
            using (var context = new ECommerceContext())
            {
                var products = context.Products.OrderByDescending(x => x.Id).AsQueryable();

                if (category != 0)
                {
                    products = products.Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(i => i.CategoryId == category));
                }

                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public int GetCountByCategory(int category)
        {
            using (var context = new ECommerceContext())
            {
                var products = context.Products.AsQueryable();

                if (category != 0)
                {
                    products = products.Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(i => i.CategoryId == category));
                }

                return products.Count();
            }
        }

        public void Update(Product entity , List<int> categoryId)
        {
            using (var context = new ECommerceContext())
            {
                var product = context.Products
                    .Include(x => x.ProductCategories)
                    .FirstOrDefault(x => x.Id == entity.Id);

                if(product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.ImageUrl = entity.ImageUrl;
                    product.Description = entity.Description;
                    product.Stock = entity.Stock;

                    product.ProductCategories = categoryId.Select(x => new ProductCategory()
                    {
                        CategoryId = x,
                        ProductId = product.Id
                    }).ToList();

                    context.SaveChanges();
                }

            }

        }
    }
}
