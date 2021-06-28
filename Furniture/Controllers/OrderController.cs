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
    public class OrderController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;

            return View(list);
        }

        public ActionResult AddToCart(int ID)
        {
            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;
            Product product = new ProductDAO().SelectByID(ID); 

            list = new OrderDetailDAO().AddNew(product, list);

            Session["Order"] = list;

            return RedirectToAction("Cart");
        }
        
        public ActionResult RemoveFromCart(int ID)
        {
            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;
            OrderDetail order = new OrderDetailDAO().SelectByID(ID, list);

            list = new OrderDetailDAO().Delete(order, list);

            Session["Order"] = list;

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult Update(FormCollection fc)
        {
            string[] quantities = fc.GetValues("quantity");
            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;

            for(int i = 0; i<list.Count; i++)
            {
                list[i].Quantity = int.Parse(quantities[i]);
            }

            Session["Order"] = list;

            return RedirectToAction("Cart");
        }

        public ActionResult DeleteAll()
        {
            Session["Order"] = null;

            return RedirectToAction("Cart");
        }

        public ActionResult Checkout()
        {

            return View();
        }
    }

}