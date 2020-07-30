using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.DataAccess.EfCore;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category,ECommerceContext> , ICategoryRepository
    {
        public void DeleteCategory(int category)
        {
            using (var context = new ECommerceContext())
            {

                var cmd = @"delete from ProductCategory where CategoryId=@p0";

                context.Database.ExecuteSqlCommand(cmd, category);

                context.Categories.Remove(context.Categories.Find(category));

                context.SaveChanges();

            }
        }
    }
}
