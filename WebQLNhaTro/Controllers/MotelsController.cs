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

        NhaTroEntities4 db = new NhaTroEntities4();

        // GET: Motels

        public ActionResult Index(int? page, string keyword, int Danhmuc = 0, int KhuVuc = 0, int giatu = 0)
        {

            int ipagesize = 6;
            int ipagenum = (page ?? 1);
            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.Danhmuc = new SelectList(db.CategoryMotels.ToList(), "CategoryID", "type");
            IOrderedQueryable<motel> item = db.motels;
            if (!string.IsNullOrEmpty(keyword) && Danhmuc == 0 && KhuVuc == 0 && giatu == 0)
            {
                item = item.Where(x => x.Title.Contains(keyword)).OrderBy(x => x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc != 0 && giatu == 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc && x.AreaID == KhuVuc).OrderBy(x => x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc == 0 && giatu != 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc && x.ID == giatu).OrderBy(x => x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc != 0 && giatu != 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc && x.AreaID == KhuVuc && x.ID == giatu).OrderBy(x => x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc == 0 && KhuVuc != 0 && giatu != 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.AreaID == KhuVuc && x.ID == giatu).OrderBy(x => x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (!string.IsNullOrEmpty(keyword) && Danhmuc != 0 && KhuVuc == 0 && giatu == 0)
            {
                item = item.Where(x => x.Title.Contains(keyword) && x.CategoryID == Danhmuc).OrderBy(x => x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            if (Danhmuc != 0 && KhuVuc == 0 && giatu == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.CategoryID == Danhmuc);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (Danhmuc != 0 && KhuVuc != 0 && giatu == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.CategoryID == Danhmuc && x.AreaID == KhuVuc);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (Danhmuc != 0 && KhuVuc != 0 && giatu != 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.CategoryID == Danhmuc && x.AreaID == KhuVuc && x.ID == giatu);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (KhuVuc != 0 && giatu != 0 && Danhmuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu && x.AreaID == KhuVuc);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (KhuVuc != 0 && giatu == 0 && Danhmuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.AreaID == KhuVuc);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (KhuVuc == 0 && giatu != 0 && Danhmuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else if (Danhmuc != 0 && giatu != 0 && KhuVuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu && x.CategoryID == Danhmuc);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else
            {
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
        }

        public JsonResult GetByName(string keyword)
        {
            var allsearch = db.motels.Where(x => x.Title.Contains(keyword)).Select(x => new Seach
            {
                label = x.Title,
                val = x.MotelID.ToString()
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Detail(int id)
        {
            var item = db.motels.Where(x => x.MotelID == id).Single();
            return View(item);
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(int motelId)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    string userName = User.Identity.Name;


                    int customId = db.customs
                        .Where(u => u.Account == userName)
                        .Select(u => u.CustomID)
                        .FirstOrDefault();
                    var motel = db.motels.SingleOrDefault(m => m.MotelID == motelId);
                    var order = new Order
                    {
                        CustomID = customId,
                        MotelID = motelId,
                        CreateDate = DateTime.Now,
                        Price = motel.Price,
                        Status = "Chờ duyệt"
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Đăng ký thành công" });
                }
                else
                {

                    return Json(new { success = false, message = "Người dùng chưa đăng nhập" });
                }
            }

            // Nếu model không hợp lệ, trả về thông báo lỗi
            return Json(new { success = false, message = "Lỗi khi đăng ký", errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

    }
}