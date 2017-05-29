using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class HangCanDatController : Controller
    {
        // GET: KinhDoanh/HangCanDat
        public ActionResult HangCanDat()
        {
            return View();
        }
        public ActionResult HangCanDatChuaDat()
        {
            return View();
        }
        public ActionResult HangCanDatDaDat()
        {
            return View();
        }

        public ActionResult HangCanDatHome()
        {
            return View();
        }
    }
}