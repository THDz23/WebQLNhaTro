using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQLNhaTro.Models;

namespace WebQLNhaTro.Areas.Admin.Controllers
{
    public class ContractController : Controller
    {
        private NhaTroEntities3 db = new NhaTroEntities3();

        public object[] MotelID { get; private set; }

        // GET: Admin/Contract
        public ActionResult Index(string statusFilter)
        {
            IQueryable<Contract> contracts = db.Contracts.Include("Custom");

            // Lọc theo trạng thái nếu được chọn
            if (!string.IsNullOrEmpty(statusFilter))
            {
                contracts = contracts.Where(c => c.Status == statusFilter);
            }

            // Lấy danh sách tất cả hợp đồng
            var allContracts = contracts.ToList();

            return View(allContracts);
        }

        public ActionResult ApproveContract(int contractId)
        {
            var contract = db.Contracts.Find(contractId);
            if (contract != null && contract.Status == "Chờ Duyệt")
            {
                contract.Status = "Đã Duyệt";
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();

                // Cập nhật trạng thái cho nhà trọ
                UpdateMotelStatus(contract.motel.MotelID, "Khóa");

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Không thể duyệt hợp đồng nhà trọ này." });
        }

        public ActionResult LockContract(int contractId)
        {
           
            var contract = db.Contracts.Find(contractId);
            if (contract != null && contract.Status == "Duyệt")
            {
                contract.Status = "Khóa";
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Không thể khóa hợp đồng này." });
        }
        private void UpdateMotelStatus(int motelId, string status)
        {
            var motel = db.motels.Find(motelId);
            if (motel != null)
            {
                motel.Status = status;
                db.Entry(motel).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public ActionResult Details(int id)
        {
            // Sử dụng Include để load thông tin Custom liên quan
            var contract = db.Contracts.Include("Custom").SingleOrDefault(c => c.ContractID == id);

            if (contract == null)
            {
                return HttpNotFound();
            }

            return View(contract);
        }

       
        public ActionResult CustomDetails(int customId)
        {
            var custom = db.customs.Find(customId);

            if (custom == null)
            {
                return HttpNotFound();
            }

            return View(custom);
        }
        public ActionResult UnlockContract(int contractId)
        {
            var contract = db.Contracts.Find(contractId);
            if (contract != null && contract.Status == "Khóa")
            {
                contract.Status = "Duyệt"; // Cập nhật trạng thái là "Đã Duyệt" khi mở khóa
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();

                // Cập nhật trạng thái cho nhà trọ
                UpdateMotelStatus(contract.motel.MotelID, "Duyệt");

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Không thể mở khóa hợp đồng này." });

        }
    }
}
