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
        NhaTroEntities2 db = new NhaTroEntities2();
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
        public ActionResult Update(Contract c,FormCollection f) 
        {
            if (ModelState.IsValid)
            {
                c.Priece = Decimal.Parse(f["sum"]);

            }
            return View();
        }
    }
}