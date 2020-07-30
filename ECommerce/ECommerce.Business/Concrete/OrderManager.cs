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
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderManager(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Order> Quarayable => _repository.Quarayable;

        public void Create(Order entity)
        {
            _repository.Create(entity);
        }

        public async Task CreateAsync(Order entity)
        {
            await _repository.CreateAsync(entity);
        }

        public void Delete(Order entity)
        {
            _repository.Delete(entity);
        }

        public async Task DeleteAsync(Order entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public List<Order> GetAll(Expression<Func<Order, bool>> filter = null)
        {
            return _repository.GetAll(filter);
        }

        public Order GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Order entity)
        {
            _repository.Update(entity);
        }

        public async Task UpdateAsync(Order entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
