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

        List<OrderDetail> list;

        public OrderDetailDAO()
        {
            list = new List<OrderDetail>();
        }

        public void AddNew(Product product)
        {
            OrderDetail detail = new OrderDetail();

            if (new ProductDAO().SelectByID(product.ID) != null)
            {
                detail.ID_Product = product.ID;
                detail.Price = product.Price;
                detail.Quantity = 1;
                detail.TotalPrice = detail.Quantity * detail.Price;

                list.Add(detail);
            }
            else
            {
                for(int i = 0; i < list.Count; i++)
                {
                    if(product.ID == list[i].ID_Product)
                    {
                        list[i].Quantity += 1;
                        detail.TotalPrice = detail.Quantity * detail.Price;
                    }
                }
            }
        }

        public List<OrderDetail> SelectAll()
        {
            return list;
        }
    }
}