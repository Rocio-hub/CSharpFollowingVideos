using ConsoleApp.Core.Entity;
using CustomerApp.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return _ctx.Customers.FirstOrDefault(c => c.ID == id);
        }

        public Customer Update(Customer customerUpdate)
        {
            throw new NotImplementedException();
        }
        public Customer Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
