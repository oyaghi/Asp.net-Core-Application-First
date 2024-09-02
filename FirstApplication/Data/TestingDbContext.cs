using FirstApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Data
{
    /*
     * DbContext
     * bridge between your domain or entity classes and the database. 
     * It is responsible for managing the entity objects during runtime
     * including retrieving them from the database, tracking changes to those objects, and persisting those changes back to the database.
     */

    public class TestingDbContext: DbContext
    {
        public TestingDbContext(DbContextOptions<TestingDbContext>Options) :base (Options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(

                new Customer { Id = 1, FirstName = "Ali", LastName = "Osama", Address = "Amman", Phone = "123456789" },
                new Customer { Id = 2, FirstName = "Ahmad", LastName = "Osama", Address = "Amman", Phone = "987654321" },
                new Customer { Id = 3, FirstName = "Khalid", LastName = "Osama", Address = "Amman", Phone = "892134765" },
                new Customer { Id = 4, FirstName = "Rahaf", LastName = "Osama", Address = "Amman", Phone = "218934765" }

            );
        }

    }
    
}

