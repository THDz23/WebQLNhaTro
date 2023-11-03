using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Controllers
{
    public class MotelsController : Controller
    {
        NhaTroEntities db = new NhaTroEntities();
        // GET: Motels
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            var item = db.motels.Where(x=>x.MotelID == id).Single();
            return View(item);
        }
    }
}