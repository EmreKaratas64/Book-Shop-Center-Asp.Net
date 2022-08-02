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
    public class BookController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: Book
        public ActionResult Index(string p, int sayfa = 1)
        {
            var books = from k in db.KITAPLAR select k;
            if (!string.IsNullOrEmpty(p))
            {
                books = books.Where(b => b.AD.Contains(p));
            }
            return View(books.Where(b=>b.DURUM == true).ToList().ToPagedList(sayfa, 15));
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            List<SelectListItem> category = (from x in db.KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.AD,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.cat = category;

            List<SelectListItem> author = (from y in db.YAZARLAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.AD,
                                               Value = y.ID.ToString()
                                           }).ToList();
            ViewBag.aut = author;

            return View();
        }

        [HttpPost]
        public ActionResult AddBook(KITAPLAR p)
        {
            if (!ModelState.IsValid)
            {
                return View("AddBook");
            }
            var category = db.KATEGORILER.Where(k => k.ID == p.KATEGORI).FirstOrDefault();
            var author = db.YAZARLAR.Where(y => y.ID == p.YAZAR).FirstOrDefault();
            p.KATEGORI = category.ID;
            p.YAZAR = author.ID;
            p.DURUM = true;
            db.KITAPLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBook(int id)
        {
            var book = db.KITAPLAR.Find(id);
            book.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringBook(int id)
        {
            var values = db.KITAPLAR.Find(id);
            List<SelectListItem> category = (from x in db.KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.AD,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.cat = category;

            List<SelectListItem> author = (from y in db.YAZARLAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.AD,
                                               Value = y.ID.ToString()
                                           }).ToList();
            ViewBag.aut = author;


            return View("BringBook", values);
        }

        public ActionResult UpdateBook(KITAPLAR k)
        {
            var book = db.KITAPLAR.Find(k.ID);
            if (!ModelState.IsValid)
            {
                return View("BringBook", book);
            }
            book.AD = k.AD;
            book.BASIMYIL = k.BASIMYIL;
            book.SAYFA = k.SAYFA;
            book.YAYINEVI = k.YAYINEVI;
            book.STOK = k.STOK;
            var category = db.KATEGORILER.Where(b => b.ID == k.KATEGORI).FirstOrDefault();
            var author = db.YAZARLAR.Where(y => y.ID == k.YAZAR).FirstOrDefault();
            book.KATEGORI = category.ID;
            book.YAZAR = author.ID;
            book.FIYAT = k.FIYAT;
            book.KITAPRESIM = k.KITAPRESIM;
            book.KITAPOZET = k.KITAPOZET;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BooksDetail(int sayfa = 1)
        {
            var booksdetails = db.KITAPLAR.Where(k => k.DURUM == true).ToList().ToPagedList(sayfa, 15);
            return View(booksdetails);
        }
    }
}