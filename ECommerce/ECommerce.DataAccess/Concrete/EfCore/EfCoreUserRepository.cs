using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerce.Core.DataAccess.EfCore;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;

namespace ECommerce.DataAccess.Concrete.EfCore
{
    public class EfCoreUserRepository : EfCoreGenericRepository<User,ECommerceContext> , IUserRepository
    {
        public bool IsEmailExist(string email)
        {
            using (var context = new ECommerceContext())
            {
                return context.Users.Any(x => x.Email == email);
            }
        }

        public User GetByEmail(string email)
        {
            using (var context = new ECommerceContext())
            {
                return context.Users.FirstOrDefault(x => x.Email == email);
            }
        }
    }
}
