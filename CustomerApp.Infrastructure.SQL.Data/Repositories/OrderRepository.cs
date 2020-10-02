using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;

namespace CustomerApp.Infrastructure.SQL.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly CustomerAppContext _ctx;

        public OrderRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }

        public Order Create(Order order)
        {
            throw new NotImplementedException();
        }

        public Order Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadAll()
        {
            return _ctx.Orders;
        }

        public Order ReadByID(int id)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order orderUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
