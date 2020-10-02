using ConsoleApp.Core.Entity;
using CustomerApp.Core.DomainService;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApp.Infrastructure.Static.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository()
        {
            if (FakeDB.CustomerList.Count >= 1) return;
            
            var cust1 = new Customer()
            {
                ID = FakeDB.ID++,
                FirstName = "AAA",
                LastName = "BBB",
                Address = "CCC"
            };
            FakeDB.CustomerList.Add(cust1);

            var cust2 = new Customer()
            {
                ID = FakeDB.ID++,
                FirstName = "DDD",
                LastName = "EEE",
                Address = "FFF"
            };
            FakeDB.CustomerList.Add(cust2);
        }

        public Customer Create(Customer customer)
        {
            customer.ID = FakeDB.ID++;
            FakeDB.CustomerList.Add(customer);
            return customer;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return FakeDB.CustomerList;
        }

        public Customer ReadByID(int id)
        {
            FakeDB.CustomerList.
                Select(c => new Customer()
                {
                    ID = c.ID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address
                }).
                FirstOrDefault(c => c.ID == id);

            foreach (var customer in FakeDB.CustomerList)
            {
                if (customer.ID == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer Update(Customer customerUpdate)
        {
            var customerFromDB = this.ReadByID(customerUpdate.ID);
            if (customerFromDB != null)
            {
                customerFromDB.FirstName = customerUpdate.FirstName;
                customerFromDB.LastName = customerUpdate.LastName;
                customerFromDB.Address = customerUpdate.Address;
                return customerFromDB;
            }
            return null;
        }

        public Customer Delete(int id)
        {
            var customerFound = this.ReadByID(id);

            if (customerFound != null)
            {
                FakeDB.CustomerList.Remove(customerFound);
                return customerFound;
            }
            return null;
        }

    }
}
