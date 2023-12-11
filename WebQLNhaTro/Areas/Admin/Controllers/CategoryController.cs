using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public class CategoryController : Controller
    {

        NhaTroEntities4 db = new NhaTroEntities4();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var tro = db.CategoryMotels;
            return View(tro);
        }
        public ActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(CategoryMotel tro, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                tro.Type = f["Type"];
                tro.Description = f["Description"];          
                db.CategoryMotels.Add(tro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.CategoryMotels.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryMotel tro, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                tro.CategoryID = int.Parse(f["CategoryID"]);
                tro.Type = f["Type"];
                tro.Description = f["Description"];
                db.CategoryMotels.Attach(tro);
                db.Entry(tro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tro);
        }
        [HttpGet]
        public ActionResult delete(int id)
        {
            var item = db.CategoryMotels.Find(id);
            return View(item);
        }
        [HttpPost, ActionName("delete")]
        public ActionResult deleteconf(int id)
        {
            var dm = db.CategoryMotels.Find(id);
            var tro = db.motels.Where(tr => tr.CategoryID == id);
            if (tro.Count() > 0)
            {
                ViewBag.ThongBao = "Danh mục trọ này có trong nhà trọ <br>" + "Không thể xóa";
                return View(dm);
            }
            db.CategoryMotels.Remove(dm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}