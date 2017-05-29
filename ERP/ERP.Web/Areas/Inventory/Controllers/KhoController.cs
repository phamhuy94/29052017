using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class KhoController : Controller
    {
        // GET: Inventory/Kho
        public ActionResult Index()
        {
            return View();
        }
    }
}