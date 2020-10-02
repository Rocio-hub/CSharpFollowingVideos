using ConsoleApp.Core.Entity;
using CustomerApp.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomerRepository _customerRepo;
        readonly IOrderRepository _orderRepo;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepo = customerRepository;
            _orderRepo = orderRepository;
        }
        public Customer NewCustomer(string firstName, string lastName, string address)
        {
            var cust = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };
            return cust;
        }

        public Customer CreateCustomer(Customer cust)
        {
            return _customerRepo.Create(cust);
        }       

        public Customer FindCustomerByID(int id)
        {
            return _customerRepo.ReadByID(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.ReadAll().ToList();
        }        

        public List<Customer> GetAllByFirstName(string name)
        {
            var list = _customerRepo.ReadAll();
            var queryContinued = list.Where(cust => cust.FirstName.Equals(name));
            queryContinued.OrderBy(customer => customer.FirstName);
            return queryContinued.ToList();
        }

        public Customer UpdateCustomer(Customer customerUpdate)
        {
            var customer = FindCustomerByID(customerUpdate.ID);
            customer.FirstName = customerUpdate.FirstName;
            customer.LastName = customerUpdate.LastName;
            customer.Address = customerUpdate.Address;

            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepo.Delete(id);
        }

        public Customer FindCustomerByIDIncludeOrders(int id)
        {
            var customer = _customerRepo.ReadByID(id);
            customer.OrdersList = _orderRepo.ReadAll().Where(order => order.Customer.ID == id).ToList();

            return customer;
        }
    }
}
