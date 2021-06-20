using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Furniture.Models;

namespace Furniture.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register user)
        {
            var checkExist_username = new UserDAO().CheckExist(user.Username);

            if (checkExist_username)
                ModelState.AddModelError("Username", "Username has been used");
            
            if(ModelState.IsValid)
            {
                User newUser = new User
                {
                    Username = user.Username,
                    Password = UserDAO.Encrypt(user.Password),
                    FullName = user.FullName,
                    Address = user.Address,
                    Phone = user.Phone,
                    Role = 0
                };

                if(new UserDAO().AddNew(newUser))
                    return RedirectToAction("Success");
            }

            return View(user);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}