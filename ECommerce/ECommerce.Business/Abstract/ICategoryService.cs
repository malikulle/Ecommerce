using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity.Models;

namespace ECommerce.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);

        List<Category> GetAll(Expression<Func<Category, bool>> filter = null);

        IQueryable<Category> Quarayable { get; }

        void Create(Category entity);

        Task CreateAsync(Category entity);

        void Update(Category entity);

        Task UpdateAsync(Category entity);

        void Delete(Category entity);

        Task DeleteAsync(Category entity);

        void DeleteCategory(int category);

        Task SaveChangesAsync();
    }
}
