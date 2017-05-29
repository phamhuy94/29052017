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
    public class DonHangPOController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: DonHangPO
        public ActionResult Index()
        {
            var bH_DON_HANG_PO = db.BH_DON_HANG_PO.Include(b => b.KH).Include(b => b.CCTC_NHAN_VIEN);
            return View(bH_DON_HANG_PO.ToList());
        }

        public ActionResult GetPrintPO()
        {

            return View();
        }

        public ActionResult DanhSachDuyetPO()
        {

            return View();
        }

        public ActionResult ChiTietDonPO()
        {

            return View();
        }


            public ActionResult POLenBanHang()
        {

            return View();
        }

        public ActionResult ListPO_DangGiuDo()
        {

            return View();
        }

        public ActionResult ListPO_DaGiuDayDu()
        {

            return View();
        }

        public ActionResult ChiTietPOChuaGiuDu()
        {
            return View();
        }


        // GET: DonHangPO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BH_DON_HANG_PO bH_DON_HANG_PO = db.BH_DON_HANG_PO.Find(id);
            if (bH_DON_HANG_PO == null)
            {
                return HttpNotFound();
            }
            return View(bH_DON_HANG_PO);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BH_DON_HANG_PO bH_DON_HANG_PO = db.BH_DON_HANG_PO.Find(id);
            if (bH_DON_HANG_PO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_KHACH_HANG = new SelectList(db.KHs, "MA_KHACH_HANG", "TEN_CONG_TY", bH_DON_HANG_PO.MA_KHACH_HANG);
            ViewBag.NHAN_VIEN_QUAN_LY = new SelectList(db.CCTC_NHAN_VIEN, "USERNAME", "GIOI_TINH", bH_DON_HANG_PO.NHAN_VIEN_QUAN_LY);
            return View(bH_DON_HANG_PO);
        }

        // GET: KinhDoanh/DonHangPO
        public ActionResult ListPO()
        {
            return View();
        }

        public ActionResult ListPO_DaDuyet()
        {
            return View();
        }

        public ActionResult ListPO_DaHuy()
        {
            return View();
        }

        public ActionResult ListPO_DangChoDuyet()
        {
            return View();
        }

        public ActionResult ListPO_DangDuyet()
        {
            return View();
        }

        public ActionResult ListPO_DaLenDonBanHang()
        {
            return View();
        }

        public ActionResult ListPO_CanBanNgay()
        {
            return View();
        }

        public ActionResult ListPO_DangXuatDo()
        {
            return View();
        }


        public ActionResult DonPOHome()
        {
            return View();
        }

        #region "Thêm mới PO"
        public ActionResult ThemMoiPO()
        {
            return View();
        }
        #endregion
    }

}

