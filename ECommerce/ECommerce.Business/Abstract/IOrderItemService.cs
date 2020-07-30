using ECommerce.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface IOrderItemService
    {
        OrderItem GetById(int id);

        List<OrderItem> GetAll(Expression<Func<OrderItem, bool>> filter = null);

        IQueryable<OrderItem> Quarayable { get; }

        void Create(OrderItem entity);

        Task CreateAsync(OrderItem entity);

        void Update(OrderItem entity);

        Task UpdateAsync(OrderItem entity);

        void Delete(OrderItem entity);

        Task DeleteAsync(OrderItem entity);
    }
}
