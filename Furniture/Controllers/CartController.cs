using Furniture.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Furniture.Controllers
{
    public class CartController : Controller
    {

        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session["CartSession"];
            var list = new List<Item>();
            if (cart != null)
            {
                list = (List<Item>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int ID, int quantity)
        {
            FurnitureDataContext db = new FurnitureDataContext(ConfigurationManager.ConnectionStrings["strCon"].ConnectionString);
            var cart = Session["CartSession"];
            var product = db.Products.Where(x => x.ID == ID).FirstOrDefault();
            if (cart != null)
            {
                var list = (List<Item>)cart;
                if (list.Exists(x => x.Product.ID == ID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == ID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new Item
                    {
                        Product = product,
                        Quantity = quantity
                    };
                    list.Add(item);
                }
                Session["CartSession"] = list;
            }
            else
            {
                var item = new Item
                {
                    Product = product,
                    Quantity = quantity
                };

                var list = new List<Item>
                {
                    item
                };
                Session["CartSession"] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<Item>>(cartModel);
            var sessionCart = (List<Item>)Session["CartSession"];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session["CartSession"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }


        public ActionResult DeleteItem(int ID)
        {
            List<Item> cart = (List<Item>)Session["CartSession"];
            int index = IsExist(ID);
            cart.RemoveAt(index);
            Session["CartSession"] = cart;
            return RedirectToAction("Index");
        }

        public JsonResult DeleteAll(string cartModel)
        {
            Session["CartSession"] = null;
            return Json(new
            {
                status = true
            });
        }

        private int IsExist(int ID)
        {
            List<Item> cart = (List<Item>)Session["CartSession"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ID.Equals(ID))
                {
                    return i;
                }
            return -1;
        }

        public ActionResult Payment()
        {
            if (Session["Haha"] != null)
            {
                var cart = Session["CartSession"];
                var list = new List<Item>();
                if (cart != null)
                {
                    list = (List<Item>)cart;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public ActionResult Payment( model)
        {
            if (Session["Haha"] != null)
            {
                List<Item> cart = Session["CartSession"] as List<Item>;
                FurnitureDataContext db = new FurnitureDataContext(ConfigurationManager.ConnectionStrings["strCon"].ConnectionString);

                Order order = new Order
                {
                    Datecreate = DateTime.Now,
                    Quantity = model.Quantity,
                    TotalPrice = model
                };
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (Item item in cart)
                {
                    ChiTietOrder orderDetail = new ChiTietOrder
                    {
                        Order_ID = order.ID,
                        MaSP = item.Product.MaSP,
                        SoLuong = item.Quantity,
                        Gia = item.Product.Gia * item.Quantity,
                    };
                    db.ChiTietOrders.Add(orderDetail);
                    db.SaveChanges();

                }
                Session.Remove(CartSession);
                return RedirectToAction("OrderStatus", "Cart", new { id = order.ID });
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult OrderStatus(long id)
        {
            Model1 model = new Model1();
            Order order = model.Orders.Find(id);
            return View(order);
        }
    }

}