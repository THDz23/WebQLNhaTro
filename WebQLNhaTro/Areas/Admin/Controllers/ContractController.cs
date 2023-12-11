﻿using System;
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

      
        public ActionResult Index(string statusFilter)
        {
            IQueryable<Order> orders = db.Orders.Include("Custom");

         
            if (!string.IsNullOrEmpty(statusFilter))
            {
                orders = orders.Where(o => o.Status == statusFilter);
            }

            var allOrders = orders.ToList();

            return View(allOrders);
        }

        public ActionResult ApproveContract(int contractId)
        {
            var order = db.Orders.Find(contractId);
            if (order != null && order.Status == "Chờ Duyệt")
            {
                order.Status = "Đã Duyệt";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                UpdateMotelStatus(order.motel.MotelID, "Khóa");

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Không thể duyệt hợp đồng nhà trọ này." });
        }

        public ActionResult LockContract(int contractId)
        {
           
            var order = db.Orders.Find(contractId);
            if (order != null && order.Status == "Duyệt")
            {
                order.Status = "Khóa";
                db.Entry(order).State = EntityState.Modified;
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
            var order = db.Orders.Include("Custom").SingleOrDefault(c => c.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
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
            var order = db.Orders.Find(contractId);
            if (order != null && order.Status == "Khóa")
            {
                order.Status = "Duyệt"; 
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                
                UpdateMotelStatus(order.motel.MotelID, "Duyệt");

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Không thể mở khóa hợp đồng này." });

        }
    }
}