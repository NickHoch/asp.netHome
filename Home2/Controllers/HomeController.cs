using Home2.ModelsDB;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Home2.Models;

namespace Home2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MakeOrder(FormCollection formCollection)
        {
            Session["Order"] = new Order
            {
                User = new Models.User
                {
                    Name = "Vashya",
                    PhoneNumber = "+380988877755"
                },
                Items = new Dictionary<Product, int>()
                {
                    {                        
                        new Product(Convert.ToInt32(formCollection["Products"]), "test"),
                        Convert.ToInt32(formCollection["quantity"])
                    }
                }
            };
            return View("Index");
        }
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.AvailableProducts.AddRange(new List<Product>
            {
                new Product(1, "Pizza Ukrainian"),
                new Product(2, "Pizza Hawaiian"),
                new Product(3, "Pizza Milano")
            });

            if (Session["Order"] != null)
            {
                model.Order = Session["Order"] as Order;
            }
            ViewBag.Products = new SelectList(model.AvailableProducts, "Id", "Name", 1);
            return View();
        }
        public ActionResult SignIn()
        {
            NameValueCollection nvc = Request.Form;
            string login = String.Empty;
            string password = String.Empty;
            if (!string.IsNullOrEmpty(nvc["login"]))
            {
                login = nvc["login"];
            }
            if (!string.IsNullOrEmpty(nvc["password"]))
            {
                password = nvc["password"];
            }
            Model _ctx = new Model();

            var user = _ctx.Users.SingleOrDefault(x => x.Login == login);
            if(user != null)
            {
                var passwordDb = user.Password;
                var passwordHashed = Hash.HashPassword(password);

                if (String.Equals(passwordHashed, passwordDb))
                {
                    var token = Guid.NewGuid().ToString();
                    user.Token = token;
                    _ctx.SaveChanges();
                    if (user.IsAdmin)
                    {
                        Response.Cookies.Add(new HttpCookie("IsAdmin", "1"));
                    }
                    else
                    {
                        Response.Cookies.Add(new HttpCookie("IsAdmin", "0"));
                    }
                    Response.Cookies.Add(new HttpCookie("Token", token));
                    Response.Cookies.Add(new HttpCookie("User", user.Login));
                    //ViewBag.User = user;
                }
            }           
            return View("Index");
        }

        public ActionResult LogOut()
        {
            if (Request.Cookies["IsAdmin"] != null)
            {
                var c = new HttpCookie("IsAdmin");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            if (Request.Cookies["Token"] != null)
            {
                var c = new HttpCookie("Token");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return View("Index");
        }
    }
}
