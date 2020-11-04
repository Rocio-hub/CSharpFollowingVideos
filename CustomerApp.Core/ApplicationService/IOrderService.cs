using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.ApplicationService
{
    public interface IOrderService
    {
        //New order
        Order New();

        //Create - POST
        Order CreateOrder(Order order);

        //Read - GET
        Order FindOrderByID(int id);
        List<Order> GetAllOrders();
        List<Order> GetFilteredOrders(Filter filter);

        //Update
        Order UpdateOrder(Order orderUpdate);

        //Delete
        Order DeleteOrder(int id);
        
    }
}
