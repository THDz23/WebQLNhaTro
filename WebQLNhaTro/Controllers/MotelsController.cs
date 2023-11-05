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
            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.Danhmuc = new SelectList (db.CategoryMotels.ToList(),"CategoryID","type");
            var item = db.motels;
            return View(item);
        }
        
        public ActionResult Detail(int id)
        {
            var item = db.motels.Where(x=>x.MotelID == id).Single();
            return View(item);
        }
        
        public ActionResult AscendingPrice()
        {
            var item = db.motels.OrderBy(x => x.Price).ToList();
            return View(item);
        }
        public ActionResult DecreasePrice()
        {
            var item = db.motels.OrderByDescending(x => x.Price).ToList();
            return View(item);
        }
        public ActionResult DateNew()
        {
            var item = db.motels.OrderBy(x => x.CreateDate).ToList();
            return View(item);
        }
    }
}