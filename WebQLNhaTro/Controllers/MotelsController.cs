using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;
using PagedList;
using PagedList.Mvc;
namespace WebQLNhaTro.Controllers
{
    public class MotelsController : Controller
    {
        public bool IsFavorite { get; set; }
        NhaTroEntities3 db = new NhaTroEntities3();
        public ActionResult Index(int ? page)
        {

            int pageSize = 6;
            int ipagenum = (page ?? 1);
            var item = db.motels;
            return View(item.ToPagedList(ipagenum, pageSize));
        }
        [HttpPost]
        public ActionResult ToggleFavorite(int motelId, bool isFavorite)
        {
            try
            {
                var motel = db.motels.Find(motelId);
                if (motel != null)
                {
                    motel.IsFavorite = isFavorite;
                    db.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy phòng trọ" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        // GET: Motels
        [HttpGet]
        public ActionResult Index(string keyword,int Danhmuc = 0,int KhuVuc=0,int giatu = 0)
        {
           

            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.Danhmuc = new SelectList (db.CategoryMotels.ToList(),"CategoryID","type");
            IOrderedQueryable<motel> item = db.motels;
            if (!string.IsNullOrEmpty(keyword) && Danhmuc == 0 && KhuVuc == 0 && giatu == 0)
            {
                item = item.Where(x => x.Title.Contains(keyword)).OrderBy(x=>x.Price);
                return View(item);
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc != 0 && giatu == 0)
            {
                    item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc && x.AreaID == KhuVuc).OrderBy(x => x.Price);
                    return View(item);
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc == 0 && giatu != 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc && x.ID == giatu).OrderBy(x => x.Price);
                return View(item);
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc != 0 && giatu != 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc && x.AreaID == KhuVuc && x.ID == giatu).OrderBy(x => x.Price);
                return View(item);
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc == 0 && KhuVuc != 0 && giatu != 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.AreaID  == KhuVuc && x.ID == giatu).OrderBy(x => x.Price);
                return View(item);
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc == 0 && giatu == 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc).OrderBy(x => x.Price);
                return View(item);
            }
            if (Danhmuc != 0 && KhuVuc == 0 && giatu == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.CategoryID == Danhmuc);
                return View(item.ToList());
            }
            else if (Danhmuc != 0 && KhuVuc != 0 && giatu == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.CategoryID == Danhmuc && x.AreaID == KhuVuc);
                return View(item.ToList());
            }
            else if(Danhmuc != 0 && KhuVuc != 0 && giatu != 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.CategoryID == Danhmuc && x.AreaID == KhuVuc && x.ID == giatu);
                return View(item.ToList());
            }
            else if (KhuVuc != 0 && giatu != 0 && Danhmuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu && x.AreaID == KhuVuc);
                return View(item.ToList());
            }
            else if (KhuVuc != 0 && giatu == 0 && Danhmuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.AreaID == KhuVuc);
                return View(item.ToList());
            }
            else if (KhuVuc == 0 && giatu != 0 && Danhmuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu);
                return View(item.ToList());
            }
            else if(Danhmuc != 0 && giatu != 0 && KhuVuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu && x.CategoryID == Danhmuc);
                return View(item.ToList());
            }
            else
            {
                return View(item);
            }
        }
        // bang hop dong,duyet quan ly dang ky
        // ung dung thanh toan online
        // order chi tiet
        // dua thông tin khách vào 
        public JsonResult GetByName(string keyword)
        {
            var allsearch = db.motels.Where(x => x.Title.Contains(keyword)).Select(x => new Seach { 
                label = x.Title,
                val = x.MotelID.ToString()
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior =  JsonRequestBehavior.AllowGet };
        }
        public ActionResult Detail(int id)
        {
            var item = db.motels.Where(x=>x.MotelID == id).Single();
            return View(item);
        }
        
        public ActionResult AscendingPrice()
        {
            var item = db.motels.OrderBy(x => x.Price).ToList();
            return View(item);
        }
        public ActionResult DecreasePrice()
        {
            var item = db.motels.OrderByDescending(x => x.Price).ToList();
            return View(item);
        }
        public ActionResult DateNew()
        {
            var item = db.motels.OrderBy(x => x.CreateDate).ToList();
            return View(item);
        }
       
    }
}