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
        // GET: Cart
        public ActionResult Index()
        {
            Order newOrder = new OrderDAO();
            var list = Session["Order"];

            return View((List<OrderDetail>)list);
        }

        public ActionResult AddToCart(int ID)
        {
           

            List<OrderDetail> list = new OrderDetailDAO().SelectAll();
            Session["Order"] = list;

            return RedirectToAction("Index");
        }
        
        
    }

}