using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class BaoGiaController : Controller
    {
        // GET: KinhDoanh/BaoGia
        public ActionResult ListBaoGia()
        {
            return View();
        }

        public ActionResult ListBaoGiaDaHuy()
        {
            return View();
        }

        public ActionResult ListBaoGiaThatBai()
        {
            return View();
        }

        public ActionResult ListBaoGiaDangChoPhanHoi()
        {
            return View();
        }

        public ActionResult ListBaoGiaDaLenPO()
        {
            return View();
        }

        public ActionResult ListBaoGiaThanhCong()
        {
            return View();
        }
        public ActionResult BaoGiaHome()
         {
             return View();
         }


        public ActionResult ThemBaoGiaMoi()
        {
            return View();
        }

        public ActionResult EditBaoGia()
        {
            return View();
        }

        public ActionResult BaoGiaLenPO()
        {
            return View();
        }

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
            using (var db = new ERP_DATABASEEntities())
            {
                BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
                if (bH_BAO_GIA == null)
                {
                    return HttpNotFound();
                }
                return View(bH_BAO_GIA);
            }
            
        }
        #region "Báo Giá Thực"
        public ActionResult GetBaoGia_thuc(string sobaogia)
        {

            return View(sobaogia);
        }
        #endregion

        #region "Báo Giá CM"
        public ActionResult GetBaoGia(string sobaogia)
        {

            return View(sobaogia);
        }
        #endregion
    }
}
