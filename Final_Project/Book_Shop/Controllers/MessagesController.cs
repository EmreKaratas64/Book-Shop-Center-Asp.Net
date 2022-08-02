using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;

namespace Book_Shop.Controllers
{
    public class MessagesController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();

        // GET: Messages
        public ActionResult Index()
        {
            var username = (string)Session["KULLANICIADI"].ToString();
            var gelenmesajlar = db.TBLMESAJLAR.Where(m => m.ALICI == username).ToList();

            //ViewBag.gelen_sayi = db.TBLMESAJLAR.Where(m => m.ALICI == username).Count();
            //ViewBag.giden_sayi = db.TBLMESAJLAR.Where(m => m.GONDEREN == username).Count();
            return View(gelenmesajlar);
        }

        public ActionResult SentMessages()
        {
            var username = (string)Session["KULLANICIADI"].ToString();
            var gonderilermesajlar = db.TBLMESAJLAR.Where(m => m.GONDEREN == username).ToList();

            
            //ViewBag.gelen_sayi = db.TBLMESAJLAR.Where(m => m.ALICI == username).Count();
            return View(gonderilermesajlar);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            var username = (string)Session["KULLANICIADI"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(TBLMESAJLAR m)
        {
            var username = (string)Session["KULLANICIADI"].ToString();
            var gonderilermesajlar = db.TBLMESAJLAR.Where(g => g.GONDEREN == username).ToList();
            m.GONDEREN = Session["KULLANICIADI"].ToString();
            m.TARIH = DateTime.Now;
            db.TBLMESAJLAR.Add(m);
            db.SaveChanges();
            return RedirectToAction("SentMessages",gonderilermesajlar);
        }


        public PartialViewResult Folder_Label()
        {
            var user = Session["KULLANICIADI"].ToString();
            ViewBag.giden_sayi = db.TBLMESAJLAR.Where(m => m.GONDEREN == user).Count();
            ViewBag.gelen_sayi = db.TBLMESAJLAR.Where(m => m.ALICI == user).Count();
            return PartialView();
        }
    }
}