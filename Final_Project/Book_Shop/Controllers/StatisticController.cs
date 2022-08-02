using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.MyClasses;
using Book_Shop.Models.Entity;

namespace Book_Shop.Controllers
{

    public class StatisticController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Statistic
        public ActionResult Index()
        {
            Class1 myc = new Class1();
            myc.Kategoriler = db.KATEGORILER.ToList();
            myc.Kasa = db.KASA.ToList();
            myc.Musteriler = db.MUSTERILER.ToList();
            myc.Hareketler = db.HAREKETLER.ToList();
            myc.Kitaplar = db.KITAPLAR.ToList();
            myc.tbliletisim = db.TBLILETISIM.ToList();
            return View(myc);
        }

        public ActionResult WeatherCard()
        {
            return View();
        }
    }
}