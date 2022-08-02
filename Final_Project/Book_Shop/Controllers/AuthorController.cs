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
    public class AuthorController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: Author
        public ActionResult Index(int sayfa=1)
        {
            var values = db.YAZARLAR.Where(y=>y.DURUM == true).ToList().ToPagedList(sayfa,15);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddAuthor()
        {
            return View();
        }

        public ActionResult DeleteAuthor(int id)
        {
            var author = db.YAZARLAR.Find(id);
            author.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddAuthor(YAZARLAR y)
        {
            if (!ModelState.IsValid)
            {
                return View("AddAuthor");
            }
            y.DURUM = true;
            db.YAZARLAR.Add(y);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringAuthor(int id)
        {
            var author = db.YAZARLAR.Find(id);
            return View("BringAuthor", author);
        }

        public ActionResult UpdateAuthor(YAZARLAR y)
        {
            var author = db.YAZARLAR.Find(y.ID);
            if (!ModelState.IsValid)
            {
                return View("BringAuthor", author);
            }
            author.AD = y.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AuthorBook(int id,int sayfa=1)
        {
            var authorbooks = db.KITAPLAR.Where(k=>k.YAZAR == id).ToList().ToPagedList(sayfa, 5);
            ViewBag.author_name = db.YAZARLAR.Where(y => y.ID == id).Select(k => k.AD).FirstOrDefault().ToString();
            return View(authorbooks);
        }


    }
}