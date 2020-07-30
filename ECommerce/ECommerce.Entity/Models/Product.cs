using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Entity.Trackable;

namespace ECommerce.Entity.Models
{

    public class Product : BaseEntity, TEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public virtual List<ProductCategory> ProductCategories { get; set; }

    }
}
