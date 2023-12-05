﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "2")]
    public class ManagesController : Controller
    {

        NhaTroEntities2 db = new NhaTroEntities2();
        // GET: Admin/Manages
        public ActionResult Index()
        {
            return View();
        }
    }
}