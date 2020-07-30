using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Entity.Models;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Api.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }

        public string file { get; set; }
    }
}
