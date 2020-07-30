using ECommerce.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface IOrderService
    {
        Order GetById(int id);

        List<Order> GetAll(Expression<Func<Order, bool>> filter = null);

        IQueryable<Order> Quarayable { get; }

        void Create(Order entity);

        Task CreateAsync(Order entity);

        void Update(Order entity);

        Task UpdateAsync(Order entity);

        void Delete(Order entity);

        Task DeleteAsync(Order entity);
    }
}
