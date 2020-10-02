using ConsoleApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.DomainService
{
    public interface ICustomerRepository
    {
        //Create Data : no ID when enter but yes when exit
        Customer Create(Customer customer);

        //Read Data:
        Customer ReadByID(int id);
        IEnumerable<Customer> ReadAll();

        //Update Data
        Customer Update(Customer customerUpdate);

        //Delete Data
        Customer Delete(int id);

    }
}
