using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class GiuKhoController : Controller
    {
        // GET: KinhDoanh/GiuKho
        public ActionResult TongHop()
        {
            return View();
        }

        public ActionResult GiuKhoSale()
        {
            return View();
        }

        public ActionResult GiuKhoHome()
        {
            return View();
        }
    }
}