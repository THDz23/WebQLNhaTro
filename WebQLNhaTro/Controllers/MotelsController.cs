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
        
        public ActionResult Index(int ?page,string keyword,int Danhmuc = 0,int KhuVuc=0,int giatu = 0)
        {

            int ipagesize = 6;
            int ipagenum = (page ?? 1);
            ViewBag.KhuVuc = new SelectList(db.areas.ToList(), "AreaID", "ProvinceName");
            ViewBag.GiaTu = new SelectList(db.searchprices.ToList(), "ID", "PriceFrom");
            ViewBag.Danhmuc = new SelectList (db.CategoryMotels.ToList(),"CategoryID","type");
            IOrderedQueryable<motel> item = db.motels;
            if (!string.IsNullOrEmpty(keyword) && Danhmuc == 0 && KhuVuc == 0 && giatu == 0)
            {
                item = item.Where(x => x.Title.Contains(keyword)).OrderBy(x=>x.Price);
                return View(item.Where(x => x.Status.Equals("Duyệt")).OrderByDescending(x=>x.CreateDate).ToPagedList(ipagenum,ipagesize));
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
                item = item.Where(x => x.Title.Contains(keyword) && x.AreaID  == KhuVuc && x.ID == giatu).OrderBy(x => x.Price);
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
            else if(Danhmuc != 0 && KhuVuc != 0 && giatu != 0)
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
            else if(Danhmuc != 0 && giatu != 0 && KhuVuc == 0)
            {
                item = (IOrderedQueryable<motel>)item.Where(x => x.ID == giatu && x.CategoryID == Danhmuc);
                return View(item.Where(x => x.Status.Equals("Duyệt")).ToList().OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
            else
            {
                return View(item.Where(x=>x.Status.Equals("Duyệt")).OrderByDescending(x => x.CreateDate).ToPagedList(ipagenum, ipagesize));
            }
        }

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
        public ActionResult ListWithCategoryIDOne()
        {
            // Lấy danh sách nhà trọ có mã là 1
            var motelsWithCategoryIDOne = db.motels.Where(x => x.CategoryID == 1).ToList();

            // Trả về view hiển thị danh sách nhà trọ có mã là 1
            return View("Index", motelsWithCategoryIDOne);
        }

        // Các action khác...

        // Nếu bạn muốn sử dụng danh sách này ở nhiều nơi, bạn có thể đặt nó trong một phương thức riêng
        private List<motel> GetMotelsWithCategoryIDOne()
        {
            return db.motels.Where(x => x.CategoryID == 1).ToList();
        }

        public ActionResult ContractDetails(int contractId)
        {
            try
            {
                // Lấy thông tin hợp đồng từ cơ sở dữ liệu dựa trên ContractID
                var contract = db.Contracts.SingleOrDefault(c => c.ContractID == contractId);

                if (contract == null)
                {
                    ViewBag.ErrorMessage = "Không tìm thấy thông tin hợp đồng.";
                    return View("Error"); // Thay "Error" bằng tên trang hiển thị thông báo lỗi của bạn
                }

                // Truyền thông tin hợp đồng đến view để hiển thị
                return View(contract);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi, có thể chuyển hướng hoặc hiển thị thông báo lỗi
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi lấy thông tin hợp đồng: " + ex.Message;
                return View("Error"); // Thay "Error" bằng tên trang hiển thị thông báo lỗi của bạn
            }
        }
 [HttpPost]
        public ActionResult RegisterRoom(int id)
        {
            var motel = db.motels.Find(id);

            if (motel == null)
            {
                // Xử lý khi không tìm thấy phòng trọ
                return RedirectToAction("Index");
            }

            // Truyền thông tin phòng trọ qua ViewBag hoặc ViewData (tùy chọn)
            ViewBag.MotelInfo = motel;

        
            // Chuyển hướng đến trang đặt phòng và truyền thông tin phòng trọ
            return View("RegisterRoom", motel);
        }
    }
}