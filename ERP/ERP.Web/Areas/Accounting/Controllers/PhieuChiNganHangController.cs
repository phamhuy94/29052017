using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Accounting.Controllers
{
    public class PhieuChiNganHangController : Controller
    {
        // GET: /<controller>/
        public ActionResult TraCacKhoanVay()
        {
            return View();
        }
        public ActionResult TamUngNhanVien()
        {
            return View();
        }
        public ActionResult ChiKhac()
        {
            return View();
        }

        public ActionResult DanhSach()
        {
            return View();
        }
    }
}