using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;

namespace ECommerce.Business.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductManager(IProductRepository repository)
        {
            _repository = repository;
        }

        public Product GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return _repository.GetAll(filter);
        }

        public IQueryable<Product> Quarayable => _repository.Quarayable;

        public void Create(Product entity)
        {
            _repository.Create(entity);
        }

        public async Task CreateAsync(Product entity)
        {
            await _repository.CreateAsync(entity);
        }

        public void Update(Product entity)
        {
            _repository.Update(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public void Delete(Product entity)
        {
            _repository.Delete(entity);
        }

        public async Task DeleteAsync(Product entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public List<Product> GetProductsByCategory(int category,int page,int pageSize)
        {
            return _repository.GetProductsByCategory(category,page,pageSize);
        }

        public int GetCountByCategory(int category)
        {
            return _repository.GetCountByCategory(category);
        }

        public void Update(Product entity, List<int> categoryIds)
        {
            _repository.Update(entity, categoryIds);
        }
    }
}
