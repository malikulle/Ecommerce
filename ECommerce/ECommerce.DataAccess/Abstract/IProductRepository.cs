using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Core.BaseReporistory;
using ECommerce.Entity.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByCategory(int category , int page, int pageSize);
        
        int GetCountByCategory(int category);

        void Update(Product entity , List<int> categoryIds);
    }
}
