using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.SoNhatKyChung
{
    public class Api_SoNhatKyChungController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_SoNhatKyChung
        public IQueryable<KT_SO_NHAT_KY_CHUNG> GetKT_SO_NHAT_KY_CHUNG()
        {
            return db.KT_SO_NHAT_KY_CHUNG;
        }

        // GET: api/Api_SoNhatKyChung/5
        [ResponseType(typeof(KT_SO_NHAT_KY_CHUNG))]
        public IHttpActionResult GetKT_SO_NHAT_KY_CHUNG(int id)
        {
            KT_SO_NHAT_KY_CHUNG kT_SO_NHAT_KY_CHUNG = db.KT_SO_NHAT_KY_CHUNG.Find(id);
            if (kT_SO_NHAT_KY_CHUNG == null)
            {
                return NotFound();
            }

            return Ok(kT_SO_NHAT_KY_CHUNG);
        }

        // PUT: api/Api_SoNhatKyChung/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKT_SO_NHAT_KY_CHUNG(int id, KT_SO_NHAT_KY_CHUNG kT_SO_NHAT_KY_CHUNG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kT_SO_NHAT_KY_CHUNG.ID)
            {
                return BadRequest();
            }

            db.Entry(kT_SO_NHAT_KY_CHUNG).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KT_SO_NHAT_KY_CHUNGExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Api_SoNhatKyChung
        [HttpPost]
        [Route("api/Api_SoNhatKyChung/PostKT_SO_NHAT_KY_CHUNG")]
        public IHttpActionResult PostKT_SO_NHAT_KY_CHUNG(DonBanHang kT_SO_NHAT_KY_CHUNG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in kT_SO_NHAT_KY_CHUNG.ChiTietBH)
            {
                KT_SO_NHAT_KY_CHUNG chitiet1 = new KT_SO_NHAT_KY_CHUNG();
                chitiet1.NGAY_HACH_TOAN = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                chitiet1.NGAY_CHUNG_TU = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                chitiet1.SO_CHUNG_TU = kT_SO_NHAT_KY_CHUNG.MA_SO_BH;
                chitiet1.DOI_TUONG = kT_SO_NHAT_KY_CHUNG.MA_KHACH_HANG;
                chitiet1.TRUC_THUOC = kT_SO_NHAT_KY_CHUNG.TRUC_THUOC;
                chitiet1.DIEN_GIAI_CHUNG = kT_SO_NHAT_KY_CHUNG.DIEN_GIAI_CHUNG;
                chitiet1.DIEN_GIAI_CHI_TIET = item.MA_HANG;
                chitiet1.TAI_KHOAN_HACH_TOAN = item.TK_NO;
                chitiet1.TAI_KHOAN_DOI_UNG = item.TK_CO;
                chitiet1.PHAT_SINH_NO =Convert.ToDecimal(item.TIEN_THANH_TOAN);
                chitiet1.PHAT_SINH_CO = 0;
                db.KT_SO_NHAT_KY_CHUNG.Add(chitiet1);
                KT_SO_NHAT_KY_CHUNG chitiet2 = new KT_SO_NHAT_KY_CHUNG();
                chitiet2.NGAY_HACH_TOAN = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                chitiet2.NGAY_CHUNG_TU = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                chitiet2.SO_CHUNG_TU = kT_SO_NHAT_KY_CHUNG.MA_SO_BH;
                chitiet2.DOI_TUONG = kT_SO_NHAT_KY_CHUNG.MA_KHACH_HANG;
                chitiet2.TRUC_THUOC = kT_SO_NHAT_KY_CHUNG.TRUC_THUOC;
                chitiet2.DIEN_GIAI_CHUNG = kT_SO_NHAT_KY_CHUNG.DIEN_GIAI_CHUNG;
                chitiet2.DIEN_GIAI_CHI_TIET = item.MA_HANG;
                chitiet2.TAI_KHOAN_HACH_TOAN = item.TK_CO;
                chitiet2.TAI_KHOAN_DOI_UNG = item.TK_NO;
                chitiet2.PHAT_SINH_NO = 0;
                chitiet2.PHAT_SINH_CO = Convert.ToDecimal(item.TIEN_THANH_TOAN);
                db.KT_SO_NHAT_KY_CHUNG.Add(chitiet2);
                if(item.TK_THUE != null)
                {
                    KT_SO_NHAT_KY_CHUNG chitiet3 = new KT_SO_NHAT_KY_CHUNG();
                    chitiet3.NGAY_HACH_TOAN = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                    chitiet3.NGAY_CHUNG_TU = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                    chitiet3.SO_CHUNG_TU = kT_SO_NHAT_KY_CHUNG.MA_SO_BH;
                    chitiet3.DOI_TUONG = kT_SO_NHAT_KY_CHUNG.MA_KHACH_HANG;
                    chitiet3.TRUC_THUOC = kT_SO_NHAT_KY_CHUNG.TRUC_THUOC;
                    chitiet3.DIEN_GIAI_CHUNG = kT_SO_NHAT_KY_CHUNG.DIEN_GIAI_CHUNG;
                    chitiet3.DIEN_GIAI_CHI_TIET = item.DIEN_GIAI_THUE;
                    chitiet3.TAI_KHOAN_HACH_TOAN = item.TK_NO;
                    chitiet3.TAI_KHOAN_DOI_UNG = item.TK_THUE;
                    chitiet3.PHAT_SINH_NO = Convert.ToDecimal(item.TIEN_THUE_GTGT);
                    chitiet3.PHAT_SINH_CO = 0;
                    db.KT_SO_NHAT_KY_CHUNG.Add(chitiet3);
                    KT_SO_NHAT_KY_CHUNG chitiet4 = new KT_SO_NHAT_KY_CHUNG();
                    chitiet4.NGAY_HACH_TOAN = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                    chitiet4.NGAY_CHUNG_TU = xlnt.Xulydatetime(kT_SO_NHAT_KY_CHUNG.NGAY_BH);
                    chitiet4.SO_CHUNG_TU = kT_SO_NHAT_KY_CHUNG.MA_SO_BH;
                    chitiet4.DOI_TUONG = kT_SO_NHAT_KY_CHUNG.MA_KHACH_HANG;
                    chitiet4.TRUC_THUOC = kT_SO_NHAT_KY_CHUNG.TRUC_THUOC;
                    chitiet4.DIEN_GIAI_CHUNG = kT_SO_NHAT_KY_CHUNG.DIEN_GIAI_CHUNG;
                    chitiet4.DIEN_GIAI_CHI_TIET = item.DIEN_GIAI_THUE;
                    chitiet4.TAI_KHOAN_HACH_TOAN = item.TK_THUE;
                    chitiet4.TAI_KHOAN_DOI_UNG = item.TK_NO;
                    chitiet4.PHAT_SINH_NO = 0;
                    chitiet4.PHAT_SINH_CO = Convert.ToDecimal(item.TIEN_THUE_GTGT);
                    db.KT_SO_NHAT_KY_CHUNG.Add(chitiet4);
                    db.SaveChanges();
                }
                db.SaveChanges();
            }

            return Ok("Hoàn Thành");
        }

        // DELETE: api/Api_SoNhatKyChung/5
        [ResponseType(typeof(KT_SO_NHAT_KY_CHUNG))]
        public IHttpActionResult DeleteKT_SO_NHAT_KY_CHUNG(int id)
        {
            KT_SO_NHAT_KY_CHUNG kT_SO_NHAT_KY_CHUNG = db.KT_SO_NHAT_KY_CHUNG.Find(id);
            if (kT_SO_NHAT_KY_CHUNG == null)
            {
                return NotFound();
            }

            db.KT_SO_NHAT_KY_CHUNG.Remove(kT_SO_NHAT_KY_CHUNG);
            db.SaveChanges();

            return Ok(kT_SO_NHAT_KY_CHUNG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KT_SO_NHAT_KY_CHUNGExists(int id)
        {
            return db.KT_SO_NHAT_KY_CHUNG.Count(e => e.ID == id) > 0;
        }
    }
}