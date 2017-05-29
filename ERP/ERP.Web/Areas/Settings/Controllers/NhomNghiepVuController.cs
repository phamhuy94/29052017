using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Settings.Controllers
{
    public class NhomNghiepVuController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: Settings/NhomNghiepVu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_NHOM_NGHIEP_VU cN_NHOM_NGHIEP_VU = db.CN_NHOM_NGHIEP_VU.Find(id);
            if (cN_NHOM_NGHIEP_VU == null)
            {
                return HttpNotFound();
            }
            return View(cN_NHOM_NGHIEP_VU);
        }

        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_NHOM_NGHIEP_VU cN_NHOM_NGHIEP_VU = db.CN_NHOM_NGHIEP_VU.Find(id);
            if (cN_NHOM_NGHIEP_VU == null)
            {
                return HttpNotFound();
            }
            return View(cN_NHOM_NGHIEP_VU);
        }
    }
}