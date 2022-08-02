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
    public class NotificationController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Notification
        public ActionResult Index(int sayfa=1)
        {
            var duyurular = db.TBLDUYURULAR.ToList().ToPagedList(sayfa, 15);
            return View(duyurular);
        }

        [HttpGet]
        public ActionResult AddNotification()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNotification(TBLDUYURULAR d)
        {
            db.TBLDUYURULAR.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteNotification(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            db.TBLDUYURULAR.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringNotification(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            return View("BringNotification", duyuru);
        }

        public ActionResult UpdateNotifcation(TBLDUYURULAR d)
        {
            var duyuru = db.TBLDUYURULAR.Find(d.ID);
            duyuru.BASLIK = d.BASLIK;
            duyuru.ICERIK = d.ICERIK;
            duyuru.TARIH = d.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}