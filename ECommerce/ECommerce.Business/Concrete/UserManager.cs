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
    public class UserManager : IUserService
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return _repository.GetAll(filter);
        }

        public IQueryable<User> Quarayable => _repository.Quarayable;

        public void Create(User entity)
        {
            _repository.Create(entity);;
        }

        public async Task CreateAsync(User entity)
        {
            await _repository.CreateAsync(entity);
        }

        public void Update(User entity)
        {
            _repository.Update(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public void Delete(User entity)
        {
            _repository.Delete(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public bool IsEmailExist(string email)
        {
            return _repository.IsEmailExist(email);
        }

        public User GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }
    }
}
