using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models.Entity;
using System.Threading;

namespace Book_Shop.Controllers
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: SignUp
        public ActionResult Sign()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sign(MUSTERILER m)
        {
            if (ModelState.IsValid)
            {
                m.FOTOGRAF = "https://i.hizliresim.com/nzv4v2t.png";
                m.DURUM = true;
                
                db.MUSTERILER.Add(m);
                db.SaveChanges();
                return RedirectToAction("SignIn","Login");
            }
            else
            {
                TempData["AlertMessage"] = "Registration failded :/";
                return View("Sign");
            }
            
        }
    }
}