using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Controllers
{
    public class OrderController : Controller
    {
        // GET: Checkout
        public ActionResult Checkout()
        {
            return View();
        }
    }
}