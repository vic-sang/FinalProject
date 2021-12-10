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
                     new User {Name = "Polo Shirt"},
                     new User {Name = "Jersey"},
                     new User {Name = "Golf Shirt"},
                     new User {Name = "T-Shirt"},
                     new User {Name = "Tuxedo Shirt"},
                     new User {Name = "Dress Belt"},
                     new User {Name = "Snap Belt"},
                     new User {Name = "Leather Belt"},
                     new User {Name = "Military Belt"},
                     new User {Name = "Reversible Belt"},
                     new User {Name = "Jeans"},
                     new User {Name = " Hiking Pants"},
                     new User {Name = " Overalls"},
                     new User {Name = "Hammer Pants"},
                     new User {Name = "Bonage pants"},
                     new User {Name = "Hot Pants"},
                     new User {Name = "Happy Toes"},
                     new User {Name = "Warm Feet Socks"},
                     new User {Name = "So SOcksy"},
                     new User {Name = "Sole Comfort"},
                     new User {Name = "Boots"},
                     new User {Name = "Knee High boots"},
                     new User {Name = " Uggs"},
                     new User {Name = "Cowboy boots"},
                     new User {Name = "Timberland Boots"},
                     new User {Name = "Ascot Caps"},
                     new User {Name = "Apple Caps"},
                     new User {Name = "Baseball Cap"},
                     new User {Name = "Beret"},
                     new User {Name = "Floppy Hat"}



            

                 };
                 context.AddRange(users);

                 List<Product>products = new List<Product>{
                     new Product { Description = "Shirt"},
                     new Product { Description =  "Belt"},
                     new Product { Description = "Pant"},
                     new Product { Description = "Sock"},
                     new Product { Description = "Shoes"}, 
                     new Product { Description = "Hat"},

                      

                 };
                 context.AddRange(products);

                 List<UserProduct> productList = new List<UserProduct>
                 {
                     new UserProduct {ProductID = 1, userID = 1},
                     new UserProduct {ProductID = 1, userID = 2},
                     new UserProduct {ProductID = 1, userID = 3},
                     new UserProduct {ProductID = 1, userID = 4},
                     new UserProduct {ProductID = 1, userID = 5},
                     
                     new UserProduct {ProductID = 2, userID = 6},
                     new UserProduct {ProductID = 2, userID = 7},
                     new UserProduct {ProductID = 2, userID = 8},
                     new UserProduct {ProductID = 2, userID = 9},
                     new UserProduct {ProductID = 2, userID = 10},

                     new UserProduct {ProductID = 3, userID = 11},
                     new UserProduct {ProductID = 3, userID = 12},
                     new UserProduct {ProductID = 3, userID = 13},
                     new UserProduct {ProductID = 3, userID = 14},
                     new UserProduct {ProductID = 3, userID = 15},
                     new UserProduct {ProductID = 3, userID = 16},

                     new UserProduct {ProductID = 4, userID = 17},
                     new UserProduct {ProductID = 4, userID = 18},
                     new UserProduct {ProductID = 4, userID = 19},
                     new UserProduct {ProductID = 4, userID = 20},
                     

                     new UserProduct {ProductID = 5, userID = 21},
                     new UserProduct {ProductID = 5, userID = 22},
                     new UserProduct {ProductID = 5, userID = 23},
                     new UserProduct {ProductID = 5, userID = 24},
                     new UserProduct {ProductID = 5, userID = 25},

                    new UserProduct {ProductID = 6, userID = 26},
                     new UserProduct {ProductID = 6, userID = 27},
                     new UserProduct {ProductID = 6, userID = 28},
                     new UserProduct {ProductID = 6, userID = 29},
                     new UserProduct {ProductID = 6, userID = 30},

                 };
                 context.AddRange(productList);
                 context.SaveChanges();
            }
        }

    }
}