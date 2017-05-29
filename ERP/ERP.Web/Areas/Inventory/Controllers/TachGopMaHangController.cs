using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class TachGopMaHangController : Controller
    {
        // GET: Inventory/TachGopMaHang
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TachMaHang()
        {
            return View();
        }

        public ActionResult GopMaHang()
        {
            return View();
        }

    }
}