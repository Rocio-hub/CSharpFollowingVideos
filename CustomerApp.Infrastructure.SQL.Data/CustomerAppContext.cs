using ConsoleApp.Core.Entity;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.SQL.Data
{
    public class CustomerAppContext : DbContext
    {
        //The context is a current in memory version of the database, but it only contains the data 
        //that is relevant to the application right now

        //To call the superclass constructor
        public CustomerAppContext(DbContextOptions<CustomerAppContext> opt)
            : base(opt) { }

        //Mapping relations between tables in our database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                 .HasOne(o => o.Customer)
                 .WithMany(c => c.Orders)
                 .OnDelete(DeleteBehavior.SetNull);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
