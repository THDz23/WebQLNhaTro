using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    public class KhuVucController : Controller
    {
        NhaTroEntities db = new NhaTroEntities();
        // GET: Admin/KhuVuc
        public ActionResult Index()
        {
            var kv = db.KhuVucs;
            return View(kv);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(KhuVuc kv,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kv.TenKhuVuc = f["TenKhuVuc"];
                kv.NgayDang = DateTime.Now;
                kv.NgaySua = DateTime.Now;
                db.KhuVucs.Add(kv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}