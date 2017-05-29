using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class ChuyenKhoController : Controller
    {
        // GET: Inventory/ChuyenKho
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DSChuyenKho()
        {
            return View();
        }
    }
}