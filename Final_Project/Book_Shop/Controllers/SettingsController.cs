using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;

namespace Book_Shop.Controllers
{
    [Authorize(Roles="MANAGER")]
    public class SettingsController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: Settings
        public ActionResult Index()
        {
            var admins = db.ADMINLER.ToList();
            return View(admins);
        }
        [HttpPost]
        public ActionResult AddAdmin(ADMINLER a)
        {
            db.ADMINLER.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var current_admin = db.ADMINLER.Find(id);
            var current_mail = current_admin.MAIL.ToString();
            var user = Session["MAIL"].ToString();
            if(current_mail != user)
            {
                db.ADMINLER.Remove(current_admin);
                db.SaveChanges();
                TempData["DeleteAccept"] = "The admin is deleted successfully";
            }
            else
            {
                TempData["DeleteDenied"] = "The admin is in usage, that's why cannot be deleted :/";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateAdmin(int id)
        {
            var admin = db.ADMINLER.Find(id);
            return View("UpdateAdmin", admin);
        }

        [HttpPost]
        public ActionResult UpdateAdmin(ADMINLER a)
        {
            var admin = db.ADMINLER.Find(a.ID);
            admin.MAIL = a.MAIL;
            admin.SIFRE = a.SIFRE;
            admin.YETKI = a.YETKI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}