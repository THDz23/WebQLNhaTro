using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Controllers
{
    public class CustomAccountController : Controller
    {
        NhaTroEntities4 db = new NhaTroEntities4();
        // GET: CustomAccount
        public ActionResult Index()
        {
            var cm = db.customs;
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
        public ActionResult Login(custom cm, string returnUrl)
        {
            var model = db.customs.SingleOrDefault(x => x.Account == cm.Account && x.PassWord == cm.PassWord);
            if (model != null)
            {
                FormsAuthentication.SetAuthCookie(model.Account, false);

                // Store the user model in the session
                Session["Account"] = model;

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Redirect to home if returnUrl is not valid
                }
            }
            else
            {
                ViewBag.ThongBao = "Vui lòng kiểm tra lại tài khoản và mật khẩu";
                return View();
            }
        }
        public ActionResult SignOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(custom cm, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                cm.fullName = f["fullName"];
                cm.Account = f["Account"];
                cm.PassWord = f["PassWord"];
                cm.gender = f["gender"];
                cm.Email = f["Email"];
                db.customs.Add(cm);
                db.SaveChanges();
                ViewBag.ThongBao = "Đăng ký thành công. Hãy đăng nhập để tiếp tục.";
                return RedirectToAction("Login", "CustomAccount");
            }

            return View();
        }
    }
}