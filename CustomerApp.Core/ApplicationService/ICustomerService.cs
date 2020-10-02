using ConsoleApp.Core.Entity;
using System.Collections.Generic;

namespace CustomerApp.Core.ApplicationService
{
    public interface ICustomerService
    {
        //New customer
        Customer NewCustomer(string firstName, string lastName, string address);

        //Create - POST
        Customer CreateCustomer(Customer cust);

        //Read - GET
        Customer FindCustomerByID(int id);
        Customer FindCustomerByIDIncludeOrders(int id);
        List<Customer> GetAllCustomers();
        List<Customer> GetAllByFirstName(string name);

        //Update - PUT
        Customer UpdateCustomer(Customer customerUpdate);

        //Delete - DELETE
        Customer DeleteCustomer(int id);


    }
}
