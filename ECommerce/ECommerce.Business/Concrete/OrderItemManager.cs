using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemReposiyory _repository;

        public OrderItemManager(IOrderItemReposiyory repository)
        {
            _repository = repository;
        }

        public IQueryable<OrderItem> Quarayable => _repository.Quarayable;

        public void Create(OrderItem entity)
        {
            _repository.Create(entity);
        }

        public async Task CreateAsync(OrderItem entity)
        {
            await _repository.CreateAsync(entity);
        }

        public void Delete(OrderItem entity)
        {
            _repository.Delete(entity);
        }

        public async Task DeleteAsync(OrderItem entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public List<OrderItem> GetAll(Expression<Func<OrderItem, bool>> filter = null)
        {
            return _repository.GetAll(filter);
        }

        public OrderItem GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(OrderItem entity)
        {
            _repository.Update(entity);
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
