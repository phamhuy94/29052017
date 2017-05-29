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
using System.Text.RegularExpressions;

namespace ERP.Web.Areas.KinhDoanh.Controllers
{
    public class DonDuKienController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang XLNT = new XuLyNgayThang();
        // GET: KinhDoanh/DonDuKien
        public ActionResult Index()
        {
            var bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Include(b => b.KH).Include(b => b.CCTC_NHAN_VIEN).Include(b => b.CCTC_CONG_TY);
            return View(bH_DON_HANG_DU_KIEN.ToList());
        }

        // GET: KinhDoanh/DonDuKien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            if (bH_DON_HANG_DU_KIEN == null)
            {
                return HttpNotFound();
            }
            return View(bH_DON_HANG_DU_KIEN);
        }

        // GET: KinhDoanh/DonDuKien/Create
        public ActionResult Create()
        {
            ViewBag.MA_KHACH_HANG = new SelectList(db.KHs, "MA_KHACH_HANG", "TEN_CONG_TY");
            ViewBag.ID_LIEN_HE = new SelectList(db.KH_LIEN_HE, "ID_LIEN_HE", "NGUOI_LIEN_HE");
            return View();
        }
        public string GeneralChungTu()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string SoChungTu = (from nhapkho in db.BH_DON_HANG_DU_KIEN where nhapkho.MA_DU_KIEN.Contains("YC") select nhapkho.MA_DU_KIEN).Max();
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (SoChungTu == null)
            {
                return "YC" + year + month + "00001";
            }
            SoChungTu = SoChungTu.Substring(6, SoChungTu.Length - 6);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 5 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "YC" + year + month + result;
        }

        // POST: KinhDoanh/DonDuKien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NGAY_TAO,MA_KHACH_HANG, ID_LIEN_HE, THANH_CONG,THAT_BAI,LY_DO_THAT_BAI")] BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN)
        {
            if (ModelState.IsValid)
            {

                bH_DON_HANG_DU_KIEN.MA_DU_KIEN = GeneralChungTu();
                var ngaytao = bH_DON_HANG_DU_KIEN.NGAY_TAO.ToString("dd/MM/yyyy");
                bH_DON_HANG_DU_KIEN.NGAY_TAO = XLNT.Xulydatetime(ngaytao);
                bH_DON_HANG_DU_KIEN.TRUC_THUOC = "HOPLONG";
                bH_DON_HANG_DU_KIEN.SALES_QUAN_LY = Session["USERNAME"].ToString();
                db.BH_DON_HANG_DU_KIEN.Add(bH_DON_HANG_DU_KIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_KHACH_HANG = new SelectList(db.KHs, "MA_KHACH_HANG", "TEN_CONG_TY", bH_DON_HANG_DU_KIEN.MA_KHACH_HANG);
            ViewBag.ID_LIEN_HE = new SelectList(db.KHs, "ID_LIEN_HE", "NGUOI_LIEN_HE", bH_DON_HANG_DU_KIEN.ID_LIEN_HE);
            return View("Index");
        }

        // GET: KinhDoanh/DonDuKien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            if (bH_DON_HANG_DU_KIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_KHACH_HANG = new SelectList(db.KHs, "MA_KHACH_HANG", "TEN_CONG_TY", bH_DON_HANG_DU_KIEN.MA_KHACH_HANG);
            ViewBag.SALES_QUAN_LY = new SelectList(db.CCTC_NHAN_VIEN, "USERNAME", "GIOI_TINH", bH_DON_HANG_DU_KIEN.SALES_QUAN_LY);
            ViewBag.TRUC_THUOC = new SelectList(db.CCTC_CONG_TY, "MA_CONG_TY", "TEN_CONG_TY", bH_DON_HANG_DU_KIEN.TRUC_THUOC);
            return View(bH_DON_HANG_DU_KIEN);
        }

        // POST: KinhDoanh/DonDuKien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_DU_KIEN,NGAY_TAO,MA_KHACH_HANG,THANH_CONG,THAT_BAI,LY_DO_THAT_BAI,TRUC_THUOC,SALES_QUAN_LY")] BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bH_DON_HANG_DU_KIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_KHACH_HANG = new SelectList(db.KHs, "MA_KHACH_HANG", "TEN_CONG_TY", bH_DON_HANG_DU_KIEN.MA_KHACH_HANG);
            ViewBag.SALES_QUAN_LY = new SelectList(db.CCTC_NHAN_VIEN, "USERNAME", "GIOI_TINH", bH_DON_HANG_DU_KIEN.SALES_QUAN_LY);
            ViewBag.TRUC_THUOC = new SelectList(db.CCTC_CONG_TY, "MA_CONG_TY", "TEN_CONG_TY", bH_DON_HANG_DU_KIEN.TRUC_THUOC);
            return View(bH_DON_HANG_DU_KIEN);
        }

        // GET: KinhDoanh/DonDuKien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            if (bH_DON_HANG_DU_KIEN == null)
            {
                return HttpNotFound();
            }
            return View(bH_DON_HANG_DU_KIEN);
        }

        // POST: KinhDoanh/DonDuKien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            db.BH_DON_HANG_DU_KIEN.Remove(bH_DON_HANG_DU_KIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
