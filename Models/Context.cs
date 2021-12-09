using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProduct>().HasKey(s => new {s.ProductID, s.UserID});
        }

        public DbSet<Product> Product {get; set;}
        public DbSet<User> User {get; set;}
        public DbSet<UserProduct> UserProduct {get; set;}
    }
}