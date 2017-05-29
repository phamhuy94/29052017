using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Accounting.Controllers
{
    public class HomeAccountingController : Controller
    {
        // GET: Accounting/HomeAccounting
        public ActionResult Index()
        {
            return View();
        }
       
    }
}