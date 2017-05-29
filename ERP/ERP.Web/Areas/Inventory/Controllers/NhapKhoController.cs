using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class NhapKhoController : Controller
    {
        // GET: Inventory/NhapKho
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DSNhapKho()
        {
            return View();
        }
        public ActionResult NhapKhoMH()
        {
            return View();
        }
    }
}