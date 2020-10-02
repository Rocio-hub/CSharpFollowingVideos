using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRepo;
        readonly ICustomerRepository _customerRepo;

        public OrderService(IOrderRepository orderRepo, ICustomerRepository customerRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
        }

        public Order New()
        {
            return new Order();
        }

        public Order CreateOrder(Order order)
        {
            if (order.Customer == null || order.Customer.ID <= 0)
            {
                throw new InvalidDataException("You need a Customer to create an Order");
            } 
            if (_customerRepo.ReadByID(order.Customer.ID) == null)
            {
                throw new InvalidDataException("Customer not found");
            }
            if (order.OrderDate == null)
            {
                throw new InvalidDataException("Order needs an Order Date");
            }
            return _orderRepo.Create(order);
        }

        public Order FindOrderByID(int id)
        {
            return _orderRepo.ReadByID(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepo.ReadAll().ToList();
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            return _orderRepo.Update(orderUpdate);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepo.Delete(id);
        }
    }
}
