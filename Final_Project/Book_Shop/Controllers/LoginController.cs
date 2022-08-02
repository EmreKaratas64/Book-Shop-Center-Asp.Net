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
    public class LoginController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Login
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(MUSTERILER m)
        {
            var client_info = db.MUSTERILER.FirstOrDefault(u => u.KULLANICIADI == m.KULLANICIADI && u.SIFRE == m.SIFRE);
            if(client_info != null)
            {
                FormsAuthentication.SetAuthCookie(client_info.KULLANICIADI, false);
                Session["KULLANICIADI"] = client_info.KULLANICIADI.ToString();
                Session["FOTOGRAF"] = client_info.FOTOGRAF.ToString();
                return RedirectToAction("Index", "ClientPanel");
            }
            else
            {
                return View();
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}