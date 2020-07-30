using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.BaseReporistory;
using ECommerce.Entity.Trackable;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Core.DataAccess.EfCore
{
    public class EfCoreGenericRepository<T , TContext>  : IRepository<T> 
    where T: class,TEntity,new()
    where TContext : DbContext , new()
    {
        public virtual T GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<T>().ToList()
                    : context.Set<T>().Where(filter).ToList();
            }
        }

        public virtual IQueryable<T> Quarayable  => new TContext().Set<T>();

        public virtual void Create(T entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual async Task CreateAsync(T entity)
        {
            using (var context = new TContext())
            {
               await context.Set<T>().AddAsync(entity);
               await context.SaveChangesAsync();
            }
        }

        public virtual void Update(T entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public virtual void Delete(T entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            using (var context = new TContext())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
