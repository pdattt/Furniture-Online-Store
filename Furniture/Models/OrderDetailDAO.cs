using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class OrderDetailDAO
    {
        FurnitureDataContext db = new FurnitureDataContext(ConfigurationManager.ConnectionStrings["strCon"].ConnectionString);

        public void AddNew(OrderDetail detail)
        {
            OrderDetail newDetail = new OrderDetail
            {
                ID_Order = detail.ID_Order,
                ID_Product = detail.ID,
                Name = detail.Name,
                Image = detail.Image,
                Quantity = detail.Quantity,
                Price = detail.Price,
                TotalPrice = detail.TotalPrice
            };

            db.OrderDetails.InsertOnSubmit(newDetail);
            db.SubmitChanges();
        }

        public List<OrderDetail> SelectAll(int ID_Order)
        {
            List<OrderDetail> list = db.OrderDetails.Where(x => x.ID_Order == ID_Order).ToList();

            return list;
        }
    }
}