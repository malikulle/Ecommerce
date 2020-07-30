using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Core.BaseReporistory;
using ECommerce.Entity.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsEmailExist(string email);
        User GetByEmail(string email);
    }
}
