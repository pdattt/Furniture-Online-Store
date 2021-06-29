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

            list = new Cart().AddNew(product, list);

            Session["Order"] = list;

            return RedirectToAction("Cart");
        }
        
        public ActionResult RemoveFromCart(int ID)
        {
            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;
            OrderDetail order = new Cart().SelectByID(ID, list);

            list = new Cart().Delete(order, list);

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
            if (Session["User"] == null)
                return RedirectToAction("Login", "User");

            var sessionUser = Session["User"].ToString();
            User user = new UserDAO().GetDetail(sessionUser);
            ViewBag.user = user;

            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;

            return View(list);
        }

        public ActionResult Finish()
        {
            var sessionList = Session["Order"];
            var list = (List<OrderDetail>)sessionList;

            if (list == null || list.Count == 0)
                return View("Cart");

            var userID = Session["User"].ToString();
            
            if(userID == null)
                return View("Cart");

            int totalPrice = new Cart().countTotalPrice(list);

            Order newOrder = new Order
            {
                ID = new OrderDAO().GenerateID(),
                ID_User = userID,
                TotalPrice = totalPrice,
                DateCreate = DateTime.Now
            };

            new OrderDAO().AddNew(newOrder);

            foreach(var detail in list)
            {
                detail.ID_Order = newOrder.ID;
                new OrderDetailDAO().AddNew(detail);
            }

            Session["Order"] = null;

            return View();
        }
    }

}