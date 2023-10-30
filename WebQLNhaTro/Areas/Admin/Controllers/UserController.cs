using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        NhaTroEntities1 db = new NhaTroEntities1();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult Login(FormCollection f,string url)
        {
            var User = f["UserName"];
            var Pass = f["PassWord"];
            url = Request["Url"];
            ADMIN ad = db.ADMINs.SingleOrDefault(a => a.Account == User && a.password == Pass);
            if(ad != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["Account"] = ad;
            }
            else
            {
                ViewBag.ThongBao = "Vui lòng kiểm tra lại tài khoản,mật khẩu";
            }
            return Redirect(url);
        }
    }
}