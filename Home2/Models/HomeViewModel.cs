using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            AvailableProducts = new List<Product>();
        }
        public Order Order { get; set; }
        public List<Product> AvailableProducts { get; set; }
    }
}