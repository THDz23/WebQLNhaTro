using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "2")]
    public class ManageMotelsController : Controller
    {
        NhaTroEntities4 db = new NhaTroEntities4();
        // GET: Admin/ManageMotels
        public ActionResult Index()
        {
            ADMIN ad = (ADMIN)Session["Account"];
            var item = from c in db.Contracts
                       join h in db.Hosts on c.HostID equals h.HostID
                       join a in db.ADMINs on h.IDADmin equals a.IDAdmin
                       where a.Account == ad.Account
                       select c;
            return View(item);
        }
        public ActionResult Calculate(int id)
        {
            var item = db.Contracts.Where(x => x.ContractID == id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(Contract c,FormCollection f) 
        {
            if (!ModelState.IsValid)
            {
                decimal e = decimal.Parse(f["newValueE"]);
                decimal w = decimal.Parse(f["newValueW"]);
                decimal p = decimal.Parse(f["TP"]);
                decimal m = decimal.Parse(f["TM"]);

                c.ContractID = int.Parse(f["Contract"]);
                c.Priece = decimal.Parse(f["sumVal"]);
                c.Datefounded = Convert.ToDateTime(f["datef"]);
                c.Expirationdate = Convert.ToDateTime(f["edate"]);
                c.HostID = int.Parse(f["host"]);
                c.MotelID = int.Parse(f["motel"]);
                c.CustomID = int.Parse(f["cus"]);
                c.Electric = Decimal.Parse( f["elec"]);
                c.Wifi = Decimal.Parse(f["wifi"]);
                c.Water = Decimal.Parse(f["water"]);
                c.Status = f["sta"];
                string email;
                string cusName;
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/Mail.html"));
                content = content.Replace("{{Elec}}", e.ToString());
                content = content.Replace("{{Water}}", w.ToString());
                content = content.Replace("{{Price}}", p.ToString());
                content = content.Replace("{{Wifi}}", m.ToString());
                content = content.Replace("{{SumPrice}}", c.Priece.ToString());

                var custom = db.customs.FirstOrDefault(cus => cus.CustomID == c.CustomID);
                if (custom != null)
                {
                    email = custom.Email;
                    cusName = custom.fullName;
                    content = content.Replace("{{customName}}", cusName);
                    new Email().sendmail(email, "Tiền trọ tháng này", content);
                }
                
                db.Contracts.Attach(c);
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.ThongBao = "Cập nhập thành công !!!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.Contracts.Where(a => a.ContractID == id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contract c,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                c.ContractID = int.Parse(f["idCon"]);
                c.Datefounded = Convert.ToDateTime(f["dateF"]);
                c.Expirationdate = Convert.ToDateTime( f["DateE"]);
                c.HostID = int.Parse(f["idHost"]);
                c.MotelID = int.Parse(f["idMo"]);
                c.CustomID = int.Parse(f["idCus"]);
                c.Electric = decimal.Parse(f["elec"]);
                c.Water = decimal.Parse(f["water"]);
                var a = f["sumVal"];
                if(a == "")
                {
                    c.Priece = 0;
                }
                else
                {
                    c.Priece = decimal.Parse(a);
                }
                c.Wifi = decimal.Parse(f["wifi"]);
                var b = f["status"];
                if(b == "")
                {
                    c.Status = Convert.ToString("1");
                }
                else
                {
                    c.Status = Convert.ToString(b);
                }

                db.Contracts.Attach(c);
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult delete(int id)
        {
            var item = db.Contracts.Where(a => a.ContractID == id);
            return View(item);
        }
        [HttpPost,ActionName ( "delete")]
        public ActionResult deletecof(int id)
        {
            var item = db.Contracts.FirstOrDefault(a => a.ContractID == id);
            db.Contracts.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}