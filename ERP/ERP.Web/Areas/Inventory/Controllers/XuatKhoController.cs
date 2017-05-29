using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class XuatKhoController : Controller
    {
        // GET: Inventory/XuatKho
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XuatKhoBanHang()
        {
            return View();
        }
        public ActionResult DSXuatKho()
        {
            return View();
        }
    }
}