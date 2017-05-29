using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Web.Models.Database;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
   
    public class DonBanHangController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: KinhDoanh/DonBanHang
        public ActionResult ListDonBanHang()
        {
            return View();
        }


        public ActionResult ListDonBanHangChuaXuatKho()
        {
            return View();
        }

        public ActionResult ListDonBanHangDaXuatKho()
        {
            return View();
        }

        public ActionResult DonBanHangHome()

        {
            return View();
        }

        public ActionResult PhieuBanHang()

        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BH_DON_BAN_HANG bH_DON_BAN_HANG = db.BH_DON_BAN_HANG.Find(id);
            if (bH_DON_BAN_HANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_KHACH_HANG = new SelectList(db.KHs, "MA_KHACH_HANG", "TEN_CONG_TY", bH_DON_BAN_HANG.MA_KHACH_HANG);
            ViewBag.NHAN_VIEN_QUAN_LY = new SelectList(db.CCTC_NHAN_VIEN, "USERNAME", "GIOI_TINH", bH_DON_BAN_HANG.NHAN_VIEN_QUAN_LY);
            ViewBag.TRUC_THUOC = new SelectList(db.CCTC_CONG_TY, "MA_CONG_TY", "TEN_CONG_TY", bH_DON_BAN_HANG.TRUC_THUOC);
            return View(bH_DON_BAN_HANG);
        }

        public ActionResult Index()
        {
            var bH_DON_BAN_HANG = db.BH_DON_BAN_HANG.Include(b => b.KH).Include(b => b.CCTC_NHAN_VIEN).Include(b => b.CCTC_CONG_TY);
            return View(bH_DON_BAN_HANG.ToList());
        }
    }
}

