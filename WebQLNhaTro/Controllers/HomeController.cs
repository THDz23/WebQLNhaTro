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
        NhaTroEntities3 db = new NhaTroEntities3();


        public ActionResult Index()
        {
            // Lấy danh sách nhà trọ có Status là "Duyệt"
            var motels = db.motels.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).Take(6).ToList();

            // Trả về view hiển thị danh sách nhà trọ
            return View(motels);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult ListWithAreaIDOne()
        {
            // Lấy danh sách nhà trọ có AreaID là 1 và Status là "Duyệt"
            var motelsWithAreaIDOne = db.motels.Where(x => x.AreaID == 1 && x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).Take(6).ToList();

            // Trả về view hiển thị danh sách nhà trọ có AreaID là 1
            return View("Index", motelsWithAreaIDOne);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
