using ECommerce.Core.BaseReporistory;
using ECommerce.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
