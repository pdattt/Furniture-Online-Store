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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            var loginResult = new UserDAO().CheckLogin(user);

            if (ModelState.IsValid)
            {
                if (loginResult)
                {
                    Session["User"] = user.Username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Username", string.Empty);
                    ModelState.AddModelError("Password", string.Empty);
                    ModelState.AddModelError(string.Empty, "Username or password is not correct");
                }
            }

            return View(user);
        }

        public new ActionResult Profile()
        {
            if (Session["User"] != null)
            {
                string username = Session["User"].ToString();

                User user = new UserDAO().GetDetail(username);
                List<Order> history = new OrderDAO().SelectByID(username);
                ViewBag.history = history;

                return View(user);
            }

            return RedirectToAction("Login");
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult HistoryDetail(int ID = 0)
        {
            if (Session["User"] != null)
            {
                List<OrderDetail> list = new OrderDetailDAO().SelectAll(ID);
                return View(list);
            }

            return RedirectToAction("Login");
        }
    }
}