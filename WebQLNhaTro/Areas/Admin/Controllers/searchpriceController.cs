using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public class searchpriceController : Controller
    {

        NhaTroEntities4 db = new NhaTroEntities4();
        // GET: Admin/searchprice
        public ActionResult Index()
        {
            var gia = db.searchprices;
            return View(gia);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(searchprice gia, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                gia.PriceFrom = f["PriceFrom"];
                gia.CreateDate = DateTime.Now;
                db.searchprices.Add(gia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.searchprices.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(searchprice gia, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                gia.ID = int.Parse(f["ID"]);
                gia.PriceFrom = f["PriceFrom"];
                db.searchprices.Attach(gia);
                db.Entry(gia).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gia);
        }
        [HttpGet]
        public ActionResult delete(int id)
        {
            var item = db.searchprices.Find(id);
            return View(item);
        }
        [HttpPost, ActionName("delete")]
        public ActionResult deleteconf(int id)
        {
            var gia = db.searchprices.Find(id);
            var tro = db.motels.Where(tr => tr.ID == id);
            if (tro.Count() > 0)
            {
                ViewBag.ThongBao = "Phân khúc giá này có trong nhà trọ <br>" + "Không thể xóa";
                return View(gia);
            }
            db.searchprices.Remove(gia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}