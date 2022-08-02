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
    public class ClientController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: Client
        public ActionResult Index(int sayfa = 1)
        {
            var values = db.MUSTERILER.Where(m=>m.DURUM == true).ToList().ToPagedList(sayfa, 15);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClient(MUSTERILER m)
        {
            if (!ModelState.IsValid)
            {
                return View("AddClient");
            }
            m.DURUM = true;
            m.FOTOGRAF = "https://i.hizliresim.com/nzv4v2t.png";
            db.MUSTERILER.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteClient(int id)
        {
            var client = db.MUSTERILER.Find(id);
            client.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringClient(int id)
        {
            var client = db.MUSTERILER.Find(id);
            return View("BringClient", client);
        }

        public ActionResult UpdateClient(MUSTERILER m)
        {
            var client = db.MUSTERILER.Find(m.ID);
            if (!ModelState.IsValid)
            {
                return View("BringClient", client);
            }
            client.AD = m.AD;
            client.SOYAD = m.SOYAD;
            client.MAIL = m.MAIL;
            client.KULLANICIADI = m.KULLANICIADI;
            client.TELEFON = m.TELEFON;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PurchaseHistory(int id)
        {
            var clienthist = db.HAREKETLER.Where(h => h.MUSTERI == id).ToList();
            var client = db.MUSTERILER.Where(m => m.ID == id).Select(m => m.AD + " " + m.SOYAD).FirstOrDefault();
            ViewBag.client_name = client;
            return View(clienthist);
        }
    }
}