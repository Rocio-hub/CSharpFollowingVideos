using ConsoleApp.Core.Entity;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApp.Infrastructure.SQL.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly CustomerAppContext _ctx;

        public OrderRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }

        public int Count()
        {
            return _ctx.Orders.Count();
        }

        public Order Create(Order order)
        {
            /* if (order.Customer != null &&
                 _ctx.ChangeTracker.Entries<Customer>()
                 .FirstOrDefault(ce => ce.Entity.ID == order.Customer.ID) == null)
             {
                 _ctx.Attach(order.Customer);
             }
             var saved = _ctx.Orders.Add(order).Entity;
             _ctx.SaveChanges();
             return saved;*/
            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        public Order Delete(int id)
        {
            var removed = _ctx.Remove(new Order { ID = id }).Entity;
            _ctx.SaveChanges();
            return removed;
        }

        public IEnumerable<Order> ReadAll()
        {
            return _ctx.Orders;
        }

        public IEnumerable<Order> ReadAll(Filter filter)
        {
            if (filter == null)
            {
                return _ctx.Orders;
            }
            return _ctx.Orders
                .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage);
        }

        public Order ReadByID(int id)
        {
            return _ctx.Orders.Include(o => o.Customer)
                .FirstOrDefault(o => o.ID == id);
        }

        public Order Update(Order orderUpdate)
        {
            /*if(orderUpdate.Customer != null &&
            _ctx.ChangeTracker.Entries<Customer>()
              .FirstOrDefault(ce => ce.Entity.ID == orderUpdate.Customer.ID) == null)
            {
                _ctx.Attach(orderUpdate.Customer);
            }
            else
            {
                _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            }
            var updated = _ctx.Update(orderUpdate).Entity;
            _ctx.SaveChanges();
            return updated;*/
            _ctx.Attach(orderUpdate).State = EntityState.Modified;
            _ctx.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            _ctx.SaveChanges();
            return orderUpdate;
        }
    }
}
