using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class HomeSalesController : Controller
    {
        // GET: KinhDoanh/HomeSales
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Action()
        {
            return View();
        }
    }
}