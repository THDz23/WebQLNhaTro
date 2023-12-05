using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;
using System.IO;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "2")]
    public class ProductController : Controller
    {
        NhaTroEntities2 db = new NhaTroEntities2();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var sp = db.motels;
            return View(sp);
        }
        
        public ActionResult Add()
        {
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.DanhMuc = new SelectList(db.CategoryMotels.ToList(), "CategoryID", "Type");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(motel tro,FormCollection f,HttpPostedFileBase fileUp, HttpPostedFileBase[] fFileUpload)
        {
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.DanhMuc = new SelectList(db.CategoryMotels.ToList(), "CategoryID", "Type");
            if (fileUp == null && fFileUpload == null)
            {
                return View();
            }
            else {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileUp.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image"), filename);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUp.SaveAs(path);
                    }
                    foreach (var fl in fFileUpload)
                    {
                        var fileNameif = Path.GetFileName(fl.FileName);
                        var paths = Path.Combine(Server.MapPath("~/Image"), fileNameif);
                        tro.ImageMotels.Add(new ImageMotel { 
                            MotelID = tro.MotelID,
                            Image = fileNameif
                        });
                    }
                    tro.Image = filename;
                    tro.Title = f["txtTenTro"];
                    tro.Price = decimal.Parse(f["gia"]);
                    tro.Acreage = int.Parse(f["dientich"]);
                    tro.Address = f["txtDiaChi"];
                    tro.CreateDate = DateTime.Now;
                    tro.ModifiledDate = DateTime.Now;
                    tro.Status = "Chưa duyệt";
                    tro.Description = f["sMoTa"];
                    tro.AreaID = int.Parse(f["KhuVuc"]);
                    tro.CategoryID = int.Parse(f["DanhMuc"]);
                    tro.ID = int.Parse(f["GiaTu"]);
                    tro.HostID = int.Parse(f["Ma"]);
                    db.motels.Add(tro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            return View();
        }
        
    }
}