using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;
using Book_Shop.Models.MyClasses;

namespace Book_Shop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Home
        public ActionResult Index()
        {
            Class1 nesne1 = new Class1();
            nesne1.Kitaplar = db.KITAPLAR.Where(k=>k.DURUM == true).ToList();
            return View(nesne1);
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(TBLILETISIM t)
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}