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

        public void AddNew(Order newOrder)
        {
            db.Orders.InsertOnSubmit(newOrder);
            db.SubmitChanges();
        }

        public int GenerateID()
        {
            List<Order> list = db.Orders.ToList();

            if (list == null || list.Count == 0)
                return 0;

            int ID = list.Max(x => x.ID);
            return ID;
        }

        public List<Order> SelectByID(string userID)
        {
            List<Order> list = db.Orders.Where(x => x.ID_User == userID).ToList();

            return list;
        }
    }
}