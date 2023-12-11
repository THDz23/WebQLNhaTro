using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
   
    public class AccountController : Controller
    {

        NhaTroEntities4 db = new NhaTroEntities4();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        //
        [HttpPost]
        public ActionResult Login(ADMIN ad, string returnUrl)
        {
            var model = db.ADMINs.SingleOrDefault(x => x.Account == ad.Account && x.password == ad.password);
            if(model != null)
            {
                FormsAuthentication.SetAuthCookie(model.Account, false);
                if(Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    Session["Account"] = ad;
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.ThongBao = "Vui lòng kiểm tra lại tài khoản va mật khẩu";
            }
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}