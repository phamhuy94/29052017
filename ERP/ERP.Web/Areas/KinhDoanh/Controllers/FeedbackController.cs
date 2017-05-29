using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: KinhDoanh/Feedback
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChamDiem()
        {
            return View();
        }
    }
}