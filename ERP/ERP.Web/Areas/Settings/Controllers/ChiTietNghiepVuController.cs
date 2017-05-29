using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Web.Models.Database;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Areas.Settings.Controllers
{
    [AuthorizeBussiness]
    public class ChiTietNghiepVuController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: HopLong/ChiTietNghiepVu
        public ActionResult Index(string id)
        {
            var cN_CHI_TIET_NGHIEP_VU = db.CN_CHI_TIET_NGHIEP_VU.Where(x=>x.ID_NGHIEP_VU==id);
            return View(cN_CHI_TIET_NGHIEP_VU.ToList());
        }
    }
}
