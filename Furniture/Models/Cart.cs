using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class Cart
    {
        public List<OrderDetail> AddNew(Product product, List<OrderDetail> list)
        {
            int ID = 0;

            if (list == null || list.Count == 0)
                list = new List<OrderDetail>();
            else
            {
                bool checkExists = list.Any(x => x.ID_Product == product.ID);

                if (checkExists)
                {
                    foreach (var detail in list)
                    {
                        if (detail.ID_Product == product.ID)
                        {
                            detail.Quantity++;
                            detail.TotalPrice = detail.Price * detail.Quantity;
                            return list;
                        }
                    }
                }

                if (list.Count > 0)
                    ID = list.Max(x => x.ID);
            }

            OrderDetail newDetail = new OrderDetail
            {
                ID = ID + 1,
                ID_Product = product.ID,
                Name = product.Name,
                Image = product.Image,
                Quantity = 1,
                Price = (int)(product.Price - product.Price * product.Discount * 0.01),
                TotalPrice = (int)product.Price
            };

            list.Add(newDetail);

            return list;
        }

        public OrderDetail SelectByID(int ID, List<OrderDetail> list)
        {
            OrderDetail order = list.Where(x => x.ID == ID).SingleOrDefault();

            return order;
        }

        public List<OrderDetail> Delete(OrderDetail order, List<OrderDetail> list)
        {
            list.Remove(order);

            return list;
        }

        public int countTotalPrice(List<OrderDetail> list)
        {
            int total = 0;

            foreach (var order in list)
                total += order.Price * order.Quantity;

            return total;
        }
    }
}