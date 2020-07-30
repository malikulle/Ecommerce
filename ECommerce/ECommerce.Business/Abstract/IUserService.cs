using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity.Models;

namespace ECommerce.Business.Abstract
{
    public interface IUserService 
    {
        User GetById(int id);

        List<User> GetAll(Expression<Func<User, bool>> filter = null);

        IQueryable<User> Quarayable { get; }

        void Create(User entity);

        Task CreateAsync(User entity);

        void Update(User entity);

        Task UpdateAsync(User entity);

        void Delete(User entity);

        Task DeleteAsync(User entity);

        bool IsEmailExist(string email);

        User GetByEmail(string email);

    }
}
