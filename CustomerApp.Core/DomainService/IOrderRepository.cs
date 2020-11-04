using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.DomainService
{
    public interface IOrderRepository
    {
        //Create: no id when enter but id when exit
        Order Create(Order order);

        //Read
        Order ReadByID(int id);
        IEnumerable<Order> ReadAll(Filter filter = null);
        int Count();

        //Update
        Order Update(Order orderUpdate);

        //Delete
        Order Delete(int id);

    }
}
