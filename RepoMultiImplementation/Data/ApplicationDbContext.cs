using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepoMultiImplementation.Models;

namespace RepoMultiImplementation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Car> Car { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>().HasData(
                new Customer() { Id = 1, Name = "Steve" },
                new Customer() { Id = 2, Name = "Jimmy" });

            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Repository" },
                new Product { Id = 2, Name = "Not a repository" });

            builder.Entity<Car>().HasData(
                new Car { Id = 1, Name = "Tesla Model X" },
                new Car { Id = 2, Name = "Honda Ridgeline" });
        }
    }
}
