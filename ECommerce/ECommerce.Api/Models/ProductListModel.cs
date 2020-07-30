using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Entity.Models;

namespace ECommerce.Api.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int CurrentCategoryId { get; set; }

        public int TotalPages { get; set; }
    }

    public class ProductListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }

    }
}
