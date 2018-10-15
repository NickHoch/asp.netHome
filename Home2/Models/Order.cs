using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2.Models
{
    public class Order
    {
        public Order()
        {
            Items = new Dictionary<Product, int>();
        }
        public User User { get; set; }
        public Dictionary<Product, int> Items { get; set; }
    }
}