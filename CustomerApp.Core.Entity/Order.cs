using ConsoleApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Core.Entity
{j
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Customer Customer{ get; set; }
    }
}
