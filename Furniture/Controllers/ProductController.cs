using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Furniture.Models;

namespace Furniture.Controllers
{
    public class ProductController : Controller
    {
        
        public ActionResult Product()
        {
            List<Product> products = new ProductDAO().SelectALL();
            return View(products);
        }

        public ActionResult ProductDetail(int ID)
        {
            ViewBag.proList = new ProductDAO().SelectALL();
            Product product = new ProductDAO().SelectByID(ID);

            return View(product);
        }
    }
}