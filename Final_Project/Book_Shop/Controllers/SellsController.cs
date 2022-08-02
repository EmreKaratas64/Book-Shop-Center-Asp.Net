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
    public class SellsController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Sells
        public ActionResult Index(int sayfa = 1)
        {
            var sells = db.HAREKETLER.ToList().ToPagedList(sayfa, 15);
            return View(sells);
        }

        public ActionResult DeleteSell(int id)
        {
            var sell = db.HAREKETLER.Find(id);
            db.HAREKETLER.Remove(sell);
            db.SaveChanges();
            TempData["SellDeleteSuccessful"] = "Sell deleted successfully";
            return RedirectToAction("Index");
        }

        public ActionResult BringSell(HAREKETLER h)
        {
            var sells = db.HAREKETLER.Find(h.ID);
            List<SelectListItem> book = (from x in db.KITAPLAR.Where(k=>k.DURUM == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.AD,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.bk = book;

            List<SelectListItem> client = (from y in db.MUSTERILER.Where(m => m.DURUM == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.AD + " " + y.SOYAD,
                                               Value = y.ID.ToString()
                                           }).ToList();
            ViewBag.cl = client;
            return View("BringSell", sells);
        }

        public ActionResult UpdateSell(HAREKETLER h)
        {
            var sell = db.HAREKETLER.Find(h.ID);
            if (!ModelState.IsValid)
            {
                return View("BringSell", sell);
            }

            var book = db.KITAPLAR.Where(b => b.ID == h.KITAP).FirstOrDefault();
            var client = db.MUSTERILER.Where(m => m.ID == h.MUSTERI).FirstOrDefault();
            sell.KITAP = book.ID;
            sell.MUSTERI = client.ID;

            sell.BIRIMFIYAT = h.BIRIMFIYAT;
            sell.ADET = h.ADET;
            sell.TUTAR = h.BIRIMFIYAT * decimal.Parse(h.ADET.ToString());
            sell.TARIH = h.TARIH;
            sell.KASAID = h.KASAID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}