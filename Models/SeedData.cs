using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Models
{
    public static class SeedData
    {
         public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {
                if (context.User.Any())
                {
                    return;
                }

                 List<User> users = new List<User> {
                     new User {Name = "hey"}
                 };
                 context.AddRange(users);

                 List<Product>products = new List<Product>{
                     new Product { Description = "hi", ReleaseDate = DateTime.Parse ("10/02/2021"), Price = 00}

                 };
                 context.AddRange(products);

                 List<UserProduct> productList = new List<UserProduct>
                 {
                     new UserProduct {Product = products[0], User = users[0]}

                 };
                 context.AddRange(productList);
                 context.SaveChanges();
            }
        }

    }
}