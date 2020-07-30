using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Core.BaseReporistory;
using ECommerce.Entity.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void DeleteCategory(int category);
    }
}
