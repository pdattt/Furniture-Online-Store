using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class OrderDAO
    {
        FurnitureDataContext db = new FurnitureDataContext(ConfigurationManager.ConnectionStrings["strCon"].ConnectionString);

        public List<Order> SelectAll()
        {
            List<Order> orders = db.Orders.ToList();
            return orders;
        }

        public Order AddNew()
        {
            Order newOrder = ne
            return 
        }

        public bool InsertToDatabase(Order newOrder)
        {
            try
            {
                db.Orders.InsertOnSubmit(newOrder);
                db.SubmitChanges();
            }
            catch { return false; }

            return true;
        }
    }
}