using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity.Models;

namespace ECommerce.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);

        List<Product> GetAll(Expression<Func<Product, bool>> filter = null);

        IQueryable<Product> Quarayable { get; }

        void Create(Product entity);

        Task CreateAsync(Product entity);

        void Update(Product entity);

        Task UpdateAsync(Product entity);

        void Delete(Product entity);

        void Update(Product entity, List<int> categoryIds);

        Task DeleteAsync(Product entity);

        Task SaveChangesAsync();

        List<Product> GetProductsByCategory(int category, int page,int pageSize);
        
        int GetCountByCategory(int category);
    }
}
