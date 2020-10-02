using CustomerApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Core.Entity
{
    //Modifiers: public, internal, protected, private
    //Blueprint
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<Order> OrdersList { get; set; }
    }
}
