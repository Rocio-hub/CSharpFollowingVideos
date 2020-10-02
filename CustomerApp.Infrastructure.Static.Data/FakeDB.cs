using ConsoleApp.Core.Entity;
using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Infrastructure.Static.Data
{
    public static class FakeDB
    {
        public static int ID = 1;
        public static readonly List<Customer> CustomerList = new List<Customer>();

        public static int OrderID = 1;
        public static readonly List<Order> OrderList = new List<Order>();
    }
}
