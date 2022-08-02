using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Book_Shop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DBBookShopEntities db = new DBBookShopEntities();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.KATEGORILER.Where(k=>k.DURUM == true).ToList().ToPagedList(sayfa,15);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(KATEGORILER k)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCategory");
            }
            k.DURUM = true;
            db.KATEGORILER.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = db.KATEGORILER.Find(id);
            category.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringCategory(int id)
        {
            var ktg = db.KATEGORILER.Find(id);
            return View("BringCategory", ktg);
        }

        public ActionResult UpdateCategory(KATEGORILER k)
        {
            var ktg = db.KATEGORILER.Find(k.ID);
            if (!ModelState.IsValid)
            {
                return View("BringCategory", ktg);
            }
            ktg.AD = k.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}