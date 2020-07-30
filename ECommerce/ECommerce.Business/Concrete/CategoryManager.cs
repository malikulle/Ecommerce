using ECommerce.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;

namespace ECommerce.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryManager(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public Category GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            return _repository.GetAll(filter);
        }

        public IQueryable<Category> Quarayable => _repository.Quarayable;

        public void Create(Category entity)
        {
            _repository.Create(entity);
        }

        public async Task CreateAsync(Category entity)
        {
            await _repository.CreateAsync(entity);
        }

        public void Update(Category entity)
        {
            _repository.Update(entity);
        }

        public async Task UpdateAsync(Category entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public void Delete(Category entity)
        {
            _repository.Delete(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public void DeleteCategory(int category)
        {
            _repository.DeleteCategory(category);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }
}
