using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;

namespace Book_Shop.Controllers
{
    [AllowAnonymous]
    public class PasswordForgetController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: PasswordForget
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ADMINLER a)
        {
            var admin_id = db.ADMINLER.Where(k => k.MAIL == a.MAIL && k.YETKI.ToUpper() == a.YETKI.ToUpper()).Select(b => b.ID).FirstOrDefault();
            var admin = db.ADMINLER.Find(admin_id);
            if (admin != null)
            {
                admin.SIFRE = a.SIFRE;
                db.SaveChanges();
                return RedirectToAction("Login", "AdminLogin");
            }
            else
            {
                TempData["AdminNotFound"] = "Admin could not be found :/";
                return View();
            }
            
        }

        public ActionResult ClientPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientPassword(MUSTERILER m)
        {
            var client_id = db.MUSTERILER.Where(c=>c.AD.ToUpper() == m.AD.ToUpper() && c.SOYAD.ToUpper()==m.SOYAD.ToUpper() && c.MAIL == m.MAIL && c.KULLANICIADI == m.KULLANICIADI).Select(b => b.ID).FirstOrDefault();
            var client = db.MUSTERILER.Find(client_id);
            if (client != null)
            {
                client.SIFRE = m.SIFRE;
                db.SaveChanges();
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                TempData["ClientNotFound"] = "Client could not be found :/";
                return View();
            }
            
        }
    }
}