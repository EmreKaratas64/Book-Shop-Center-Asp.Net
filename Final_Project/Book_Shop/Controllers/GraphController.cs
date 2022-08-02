using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models;

namespace Book_Shop.Controllers
{
    [Authorize(Roles ="MANAGER")]
    public class GraphController : Controller
    {
        // GET: Graph
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizeBookResult()
        {
            return Json(liste());
        }

        public List<Graphs> liste()
        {
            List<Graphs> nesne = new List<Graphs>();
            nesne.Add(new Graphs()
            {
                Yayınevi = "Koridor",
                sayi = 7
            });
            nesne.Add(new Graphs()
            {
                Yayınevi = "Yıldız",
                sayi = 5
            });
            nesne.Add(new Graphs()
            {
                Yayınevi = "Collins",
                sayi = 9
            });

            return nesne;

        }
    }
}