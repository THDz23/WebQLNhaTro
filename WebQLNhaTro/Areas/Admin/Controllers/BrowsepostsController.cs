﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    public class BrowsepostsController : Controller
    {
        NhaTroEntities3 db = new NhaTroEntities3();
        // GET: Admin/Browseposts
        public ActionResult Index()
        {
            var sp = db.motels.Where(m => m.Status == "Chưa duyệt").ToList();
            return View(sp);
            
        }
        public ActionResult Browes()
        {
            return View();
        }
        public ActionResult show(int id)
        {
            var item = db.motels.Where(x => x.MotelID == id).Single();
            return View(item);
        }
        public ActionResult Approve(int id)
        {
            var motel = db.motels.Find(id);

            if (motel != null)
            {
                motel.Status = "Đã duyệt";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}