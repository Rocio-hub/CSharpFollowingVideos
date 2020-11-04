using ConsoleApp.Core.Entity;
using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Infrastructure.SQL.Data
{
    public class DBInitializer
    {
        public static void SeedDB(CustomerAppContext ctx)
        {
            //Every time I restart I reset the db: IMPORTANT: only on development process !!
            ctx.Database.EnsureCreated();
            var cust1 = ctx.Customers.Add(new Customer()
            {
                ID = 1,
                FirstName = "Mickey",
                LastName = "Mouse",
                Address = "Playhouse"
            }).Entity;

            ctx.Customers.Add(new Customer()
            {
                //ID = 2,
                FirstName = "Ceni",
                LastName = "Cienta",
                Address = "Castillo"
            });

            ctx.Orders.Add(new Order()
            {
                // ID = 1,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            ctx.Orders.Add(new Order()
            {
                // ID = 1,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            ctx.SaveChanges();
        }
    }
}
