using ERP.Web.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.HopLong.Controllers
{
    [AuthorizeBussiness]
    public class TaiKhoanHachToanController : Controller
    {
        // GET: HopLong/TaiKhoanHachToanHL
        public ActionResult Index()
        {
            return View();
        }
    }
}