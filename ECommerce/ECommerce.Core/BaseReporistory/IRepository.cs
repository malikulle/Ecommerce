using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity.Trackable;

namespace ECommerce.Core.BaseReporistory
{
    public interface IRepository<T> where  T : class , TEntity , new()
    {
        T GetById(int id);

        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        IQueryable<T> Quarayable { get; }

        void Create(T entity);

        Task CreateAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        Task SaveChangesAsync();

    }
}
