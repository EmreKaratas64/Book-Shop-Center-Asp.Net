using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Book_Shop.Models.Entity;

namespace Book_Shop.Controllers
{
    public class FinancelController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Financel
        public ActionResult Index(int sayfa = 1)
        {
            var kasa = db.KASA.ToList().ToPagedList(sayfa, 15);
            return View(kasa);
        }

        [HttpGet]
        public ActionResult AddFinancel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFinancel(KASA k)
        {
            if (!ModelState.IsValid)
            {
                return View("AddFinancel");
            }
            db.KASA.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringFinancel(KASA k)
        {
            var kasa = db.KASA.Find(k.ID);
            return View("BringFinancel", kasa);
        }

        public ActionResult UpdateFinancel(KASA k)
        {
            var kasa = db.KASA.Find(k.ID);
            if (!ModelState.IsValid)
            {
                return View("BringSell", kasa);
            }
            kasa.AYVEYIL = k.AYVEYIL;
            kasa.TUTAR = k.TUTAR;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}