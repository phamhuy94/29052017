using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Settings.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: Settings/NguoiDung
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(string id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                HT_NGUOI_DUNG hT_NGUOI_DUNG = db.HT_NGUOI_DUNG.Find(id);
                if (hT_NGUOI_DUNG == null)
                {
                    return HttpNotFound();
                }
                return View(hT_NGUOI_DUNG);

            }
               
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Index(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Avatar"), fileName);
                        file.SaveAs(path);
                    }
                }
            }
        }
    }
}