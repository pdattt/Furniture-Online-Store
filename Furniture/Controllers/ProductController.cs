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
            List<Product> products = new FurDAO().SelectALL();
            return View(products);
        }
    }
}