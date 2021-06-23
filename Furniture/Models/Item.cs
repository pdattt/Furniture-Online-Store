using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class Item
    {
        public User User { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}