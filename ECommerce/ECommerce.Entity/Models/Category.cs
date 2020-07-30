using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Entity.Trackable;

namespace ECommerce.Entity.Models
{
    public class Category : BaseEntity , TEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<ProductCategory> ProductCategories { get; set; }
    }
}
