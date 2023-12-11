using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        private NhaTroEntities4 db = new NhaTroEntities4();

        public ActionResult Index()
        {
            var accounts = db.ADMINs.ToList();
            return View(accounts);
        }
        public ActionResult Details(int id)
        {
            var account = db.ADMINs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        public ActionResult Edit(int id)
        {
            var account = db.ADMINs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(ADMIN admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

       
        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Create(ADMIN admin)
        {
            if (ModelState.IsValid)
            {
                db.ADMINs.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        public ActionResult ConfirmDelete(int id)
        {
            var account = db.ADMINs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var account = db.ADMINs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            db.ADMINs.Remove(account);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
