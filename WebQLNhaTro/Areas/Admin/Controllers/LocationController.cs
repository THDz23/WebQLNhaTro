using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public class LocationController : Controller
    {


        NhaTroEntities4 db = new NhaTroEntities4();
        // GET: Admin/Location
       
        public ActionResult Index()
        {
            var kv = db.areas;
            return View(kv);
        }
       
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(area kv,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kv.ProvinceName = f["ProvinceName"];
                kv.CreateDate = DateTime.Now;
                kv.ModifiedDate = DateTime.Now;
                db.areas.Add(kv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.areas.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(area kv,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kv.ModifiedDate = DateTime.Now;
                kv.ProvinceName = f["Tkv"];
                kv.AreaID = int.Parse(f["Mkv"]);
                db.areas.Attach(kv);
                db.Entry(kv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kv);
        }
        [HttpGet]
        public ActionResult delete(int id)
        {
            var item = db.areas.Find(id);
            return View(item);
        }
        [HttpPost,ActionName("delete")]
        public ActionResult deleteconf(int id)
        {
            var kv = db.areas.Find(id);
            var tro = db.motels.Where(tr => tr.AreaID == id);
            if(tro.Count() > 0)
            {
                ViewBag.ThongBao = "Khu vực này có trong nhà trọ <br>" + "Không thể xóa";
                return View(kv);
            }
            db.areas.Remove(kv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}