using ConsoleApp.Core.Entity;
using CustomerApp.Core.DomainService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApp.Infrastructure.SQL.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly CustomerAppContext _ctx;

        public CustomerRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }

        public Customer Create(Customer customer)
        {
            var cust = _ctx.Customers.Add(customer).Entity;
            _ctx.SaveChanges();
            return cust;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _ctx.Customers;
        }

        public Customer ReadByID(int id)
        {
            //S4P26 video for explaination of ChangeTracker
            var changeTracker = _ctx.ChangeTracker.Entries<Customer>();
            return _ctx.Customers.FirstOrDefault(c => c.ID == id);
        }

        public Customer ReadByIDIncludeOrders(int id)
        {
            //It exists also .FromSql("") method to use sql queries
            return _ctx.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.ID == id);
        }

        public Customer Update(Customer customerUpdate)
        {
            throw new NotImplementedException();
        }
        public Customer Delete(int id)
        {
            //For deleting customers we can also the orders that the customers have. In a real situation,
            //we should talk with our customer as a developer about how we should clean up the data
            //sqlite informs you about deleting things with foreign keys

            //Delete Customer in this case should not delete orders

            /*var ordersToRemove = _ctx.Orders.Where(o => o.Customer.ID == id);
            _ctx.RemoveRange(ordersToRemove);*/
            var custRemoved = _ctx.Remove<Customer>(new Customer { ID = id }).Entity;
            _ctx.SaveChanges();
            return custRemoved;
        }      
    }
}
