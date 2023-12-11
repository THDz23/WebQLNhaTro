using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;
using System.IO;
using PagedList;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    [Authorize(Roles = "2")]
    public class ProductController : Controller
    {

        NhaTroEntities4 db = new NhaTroEntities4();
        // GET: Admin/Product
        public ActionResult Index(int ? page)
        {
            var sp = db.motels;
            int ipagesize = 6;
            int ipagenum = (page ?? 1);
            return View(sp.OrderByDescending(x=>x.CreateDate).ToPagedList(ipagenum,ipagesize));
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
        public ActionResult Edit(int id)
        {
            var item = db.motels.Find(id);
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.DanhMuc = new SelectList(db.CategoryMotels.ToList(), "CategoryID", "Type");
            return View(item);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(motel tro, FormCollection f,HttpPostedFileBase fileUp)
        {
            if (ModelState.IsValid)
            {
                if (fileUp != null)
                {
                    var sFileName = Path.GetFileName(fileUp.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUp.SaveAs(path);
                    }
                    tro.Image = sFileName;
                }
                tro.MotelID = int.Parse(f["MoID"]);
                tro.CreateDate = Convert.ToDateTime(f["ngay"]);
                tro.Title = f["txtTenTro"];
                tro.Price = decimal.Parse(f["gia"]);
                tro.Address = f["txtDiaChi"];
                tro.Status = f["txtTrangThai"];
                tro.ID = int.Parse(f["GiaTu"]);
                tro.Acreage = int.Parse(f["dientich"]);
                tro.ModifiledDate = DateTime.Now;
                tro.AreaID = int.Parse(f["KhuVuc"]);
                tro.CategoryID = int.Parse(f["DanhMuc"]);
                tro.Description = f["sMoTa"];
                tro.HostID = int.Parse(f["Ma"]);
                db.motels.Attach(tro);
                db.Entry(tro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tro);
        }

        public ActionResult delete(int id)
        {
            var iem = db.motels.Find(id);
            return View(iem);
        }
        [HttpPost, ActionName("delete")]
        public ActionResult deletecof(int id)
        {
            var tro = db.motels.Find(id);
            var con = db.Contracts.Where(x => x.MotelID == id);
            if(con.Count() > 0)
            {
                ViewBag.ThongBao = "Nhà trọ này có trong hợp đồng <br>" + "Không thể xóa";
                return View(tro);
            }
            var imotel = db.ImageMotels.Where(i => i.MotelID == id);
            if(imotel != null)
            {
                db.ImageMotels.RemoveRange(imotel);
                db.SaveChanges();
            }
            db.motels.Remove(tro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}