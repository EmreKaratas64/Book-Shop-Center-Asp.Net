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
    public class EmployeeController : Controller
    {
        DBBookShopEntities db = new DBBookShopEntities();
        // GET: Employee
        public ActionResult Index(int sayfa=1)
        {
            var employees = db.PERSONELLER.Where(e=>e.DURUM==true).ToList().ToPagedList(sayfa,15);
            return View(employees);
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(PERSONELLER p)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEmployee");
            }
            p.DURUM = true;
            db.PERSONELLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = db.PERSONELLER.Find(id);
            employee.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringEmployee(int id)
        {
            var emp = db.PERSONELLER.Find(id);
            return View("BringEmployee", emp);
        }

        public ActionResult UpdateEmployee(PERSONELLER p)
        {
            var employee = db.PERSONELLER.Find(p.ID);
            if (!ModelState.IsValid)
            {
                return View("BringEmployee", employee);
            }
            employee.PERSONEL = p.PERSONEL;
            employee.MAIL = p.MAIL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}