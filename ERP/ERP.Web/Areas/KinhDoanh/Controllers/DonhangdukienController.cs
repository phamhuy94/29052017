using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class DonhangdukienController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: Donhangdukien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TaoMoiDonDuKien()
        {

            return View();
        }

        public ActionResult TaoDuKienMoi()
        {
            return View();
        }
    }
}