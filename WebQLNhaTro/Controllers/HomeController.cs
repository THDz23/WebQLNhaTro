using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Controllers
{
    public class HomeController : Controller
    {
        NhaTroEntities1 db = new NhaTroEntities1();
        public ActionResult Index()
        {
            var item = db.motels.OrderByDescending(x => x.CreateDate).Take(6).ToList();
            return View(item);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}