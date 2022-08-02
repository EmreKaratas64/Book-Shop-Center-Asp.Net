using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;
using System.Threading;


namespace Book_Shop.Controllers
{
    public class ClientPanelController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: ClientPanel
        public ActionResult Index()
        {
            var username = (string)Session["KULLANICIADI"];
            //var musteri = db.MUSTERILER.FirstOrDefault(y => y.KULLANICIADI == username);
            var duyurular = db.TBLDUYURULAR.ToList();
            var ad = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(x => x.AD + " " + x.SOYAD).FirstOrDefault();
            var mail = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(x => x.MAIL).FirstOrDefault();
            var telefon = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(x => x.TELEFON).FirstOrDefault();
            ViewBag.ad_soyad = ad;
            ViewBag.mail = mail;
            ViewBag.phone = telefon;

            var mus_id = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(y => y.ID).FirstOrDefault();
            var alinankitaplar = db.HAREKETLER.Where(h => h.MUSTERI == mus_id).Count();
            var alinanmesajlar = db.TBLMESAJLAR.Where(m => m.ALICI == username).Count();
            var duyurusayisi = db.TBLDUYURULAR.Count();

            ViewBag.boughts = alinankitaplar;
            ViewBag.rcmessages = alinanmesajlar;
            ViewBag.notifications = duyurusayisi;

            return View(duyurular);
        }

        [HttpPost]
        public ActionResult Index(MUSTERILER m)
        {
            var username = (string)Session["KULLANICIADI"];
            var musteri = db.MUSTERILER.FirstOrDefault(x => x.KULLANICIADI == username);
            musteri.AD = m.AD;
            musteri.SOYAD = m.SOYAD;
            musteri.MAIL = m.MAIL;
            musteri.SIFRE = m.SIFRE;
            musteri.TELEFON = m.TELEFON;
            musteri.FOTOGRAF = m.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PurchaseHistory()
        {
            var username = (string)Session["KULLANICIADI"];
            var id = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(k => k.ID).FirstOrDefault();
            var purchase_his = db.HAREKETLER.Where(h => h.MUSTERI == id).ToList();
            return View(purchase_his);
        }

        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        public PartialViewResult Partial2()
        {
            var username = Session["KULLANICIADI"].ToString();
            var user_id = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(m => m.ID).FirstOrDefault();
            var user_info = db.MUSTERILER.Find(user_id);
            return PartialView("Partial2", user_info);
        }

        public ActionResult BuyBook()
        {
            var books = db.KITAPLAR.Where(k => k.DURUM == true).ToList();
            return View(books);
        }

        [HttpPost]
        public ActionResult BringBook(KITAPYORUM k)
        {
            var username = Session["KULLANICIADI"].ToString();
            var client_id = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(s => s.ID).FirstOrDefault();
            k.MUSTERI = client_id;
            k.TARIH = DateTime.Now;
            if (k.BASLIK != null && k.ICERIK != null)
            {
                db.KITAPYORUM.Add(k);
                db.SaveChanges();
                TempData["CommentSuccess"] = "Comment is made successfully";
            }
            return RedirectToAction("BringBook", new { id = k.KITAP });
        }

        public ActionResult BringBook(int id)
        {
            var book = db.KITAPLAR.Find(id);
            var kitap_yorumları = db.KITAPYORUM.Where(y => y.KITAP == id).ToList();
            ViewBag.book_id = id;
            ViewBag.book_name = book.AD.ToString();
            ViewBag.book_author = book.YAZARLAR.AD.ToString();
            ViewBag.book_price = book.FIYAT.ToString();
            ViewBag.book_publishyear = book.BASIMYIL.ToString();
            ViewBag.book_publisher = book.YAYINEVI.ToString();
            ViewBag.book_photo = book.KITAPRESIM.ToString();
            ViewBag.book_summary = book.KITAPOZET.ToString();
            ViewBag.book_summary_part = book.KITAPOZET.Substring(0, 450);

            return View(kitap_yorumları);
        }

        public ActionResult PuchaseBook(int id)
        {
            var username = Session["KULLANICIADI"].ToString();
            var book = db.KITAPLAR.Find(id);
            var client_id = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(s => s.ID).FirstOrDefault();
            ViewBag.mus_id = client_id;
            ViewBag.kitap_id = id;
            ViewBag.kitap_ad = book.AD;
            ViewBag.birim_fiy = book.FIYAT;
            return View();
        }

        [HttpPost]
        public ActionResult AddSell(HAREKETLER h)
        {
            var book = db.KITAPLAR.Find(h.KITAP);
            var kitap_stok = book.STOK;
            if (h.ADET > kitap_stok)
            {
                ViewBag.mus_id = h.MUSTERI;
                ViewBag.kitap_ad = book.AD;
                ViewBag.birim_fiy = book.FIYAT;
                ViewBag.kitap_id = book.ID;
                TempData["PurchaseFailed"] = "Order is failed bacuse purchase amount is greater than book stock";
                return RedirectToAction("PuchaseBook", new { id = h.KITAP });
            }
            else
            {
                h.TARIH = DateTime.Now;
                h.TUTAR = h.BIRIMFIYAT * decimal.Parse(h.ADET.ToString());
                db.HAREKETLER.Add(h);
                db.SaveChanges();
                return RedirectToAction("PurchaseResult", new { id = h.ID });
            }
            
        }

        public ActionResult PurchaseResult(int id)
        {
            TempData["PurchaseSuccess"] = "Order is made successfully";
            var username = Session["KULLANICIADI"].ToString();
            var user_id = db.MUSTERILER.Where(m => m.KULLANICIADI == username).Select(s => s.ID).FirstOrDefault();
            var sells = db.HAREKETLER.Where(h => h.ID == id).ToList();
            var sell = db.HAREKETLER.Find(id);
            var user = db.MUSTERILER.Find(user_id);
            ViewBag.user_id = user.ID;
            ViewBag.name_surname = user.AD + " " + user.SOYAD;
            ViewBag.tel = user.TELEFON;
            ViewBag.mail = user.MAIL;
            ViewBag.order_id = id;
            ViewBag.total = sell.TUTAR;
            ViewBag.tax = sell.TUTAR * 9 / 100;
            ViewBag.final_price = sell.TUTAR + (sell.TUTAR * 9 / 100) + 300;
            return View(sells);
        }
    }
}