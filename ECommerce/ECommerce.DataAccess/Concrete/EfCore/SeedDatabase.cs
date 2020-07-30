using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerce.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ECommerceContext();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories);
                }

                if (!context.Products.Any())
                {
                    context.Products.AddRange(Products);
                }
                context.SaveChanges();

        }

        private static readonly Category[] Categories =
        {
            new Category(){Name = "Telefon"},
            new Category(){Name = "Bilgisayar"}
        };

        private static readonly Product[] Products =
        {
            new Product(){Name = "Samsung S5",Price = 2000 ,ImageUrl = "1.jpg" ,Stock = 2},
            new Product(){Name = "Samsung S6",Price = 3000 ,ImageUrl = "2.jpg",Stock = 2},
            new Product(){Name = "Samsung S7",Price = 4000 ,ImageUrl = "3.jpg",Stock = 2},
            new Product(){Name = "Samsung S8",Price = 5000 ,ImageUrl = "4.jpg",Stock = 2},
            new Product(){Name = "Samsung S9",Price = 6000 ,ImageUrl = "5.jpg",Stock = 2},
            new Product(){Name = "IPhone 6",Price = 4000 ,ImageUrl = "6.jpg",Stock = 2},
            new Product(){Name = "IPhone 7",Price = 5000 ,ImageUrl = "7.jpg",Stock = 2}
        };
    }
}
