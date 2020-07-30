using ECommerce.Core.DataAccess.EfCore;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Concrete.EfCore
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order,ECommerceContext> , IOrderRepository
    {
    }
}
