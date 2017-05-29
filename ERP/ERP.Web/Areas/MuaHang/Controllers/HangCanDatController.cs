using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.MuaHang.Controllers
{
    public class HangCanDatController : Controller
    {
        // GET: MuaHang/HangCanDat

        public ActionResult HangCanDatHome()
        {
            return View();
        }

        public ActionResult DatHang()
        {
            return View();
        }

        public ActionResult DonDatHang()
        {
            return View();
        }

        public ActionResult ChiTietDonDatHang(string id)
        {
            return View();
        }

        public ActionResult SuaDonDatHang(string id)
        {
            return View();
        }

        public ActionResult DeNghiNhapKho(string id)
        {
            return View();
        }
    }
}