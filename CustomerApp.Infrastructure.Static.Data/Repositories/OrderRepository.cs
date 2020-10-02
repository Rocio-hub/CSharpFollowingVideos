using ConsoleApp.Core.Entity;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApp.Infrastructure.Static.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
            if (FakeDB.OrderList.Count > 0) return;

            var order1 = new Order()
            {
                ID = FakeDB.OrderID++,
                DeliveryDate = DateTime.Now.AddMonths(2),
                OrderDate = DateTime.Now.AddMonths(-1),
                Customer = new Customer() { ID = 1 }
            };
            FakeDB.OrderList.Add(order1);

            var order2 = new Order()
            {
                ID = FakeDB.OrderID++,
                DeliveryDate = DateTime.Now.AddMonths(1),
                OrderDate = DateTime.Now.AddMonths(-2),
                Customer = new Customer() { ID = 1 }
            };
            FakeDB.OrderList.Add(order2);
        }

        public Order Create(Order order)
        {
            order.ID = FakeDB.OrderID++;
            FakeDB.OrderList.Add(order);
            return order;
        }

        public Order ReadByID(int id)
        {
            return FakeDB.OrderList.FirstOrDefault(order => order.ID == id);
        }
        public IEnumerable<Order> ReadAll()
        {
            return FakeDB.OrderList;
        }

        public Order Update(Order orderUpdate)
        {
            var orderFromDB = ReadByID(orderUpdate.ID);
            if (orderFromDB == null) return null;

            orderFromDB.DeliveryDate = orderUpdate.DeliveryDate;
            orderFromDB.OrderDate = orderUpdate.OrderDate;
            if(orderUpdate.Customer != null && orderFromDB.Customer!=null)
            {
                orderFromDB.Customer.ID = orderUpdate.ID;
            }
            return orderFromDB;
        }

        public Order Delete(int id)
        {
            var orderFound = ReadByID(id);
            if (orderFound == null) return null;

            FakeDB.OrderList.Remove(orderFound);
            return orderFound;
        }
    }
}
