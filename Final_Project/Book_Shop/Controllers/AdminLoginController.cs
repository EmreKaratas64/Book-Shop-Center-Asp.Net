using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Book_Shop.Models.Entity;

namespace Book_Shop.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ADMINLER p)
        {
            var admin_info = db.ADMINLER.FirstOrDefault(x => x.MAIL == p.MAIL && x.SIFRE == p.SIFRE);
            if(admin_info != null)
            {
                FormsAuthentication.SetAuthCookie(admin_info.MAIL, false);
                Session["MAIL"] = admin_info.MAIL.ToString();
                return RedirectToAction("Index", "Statistic");
            }
            else
            {
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}