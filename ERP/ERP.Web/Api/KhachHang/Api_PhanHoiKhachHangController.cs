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

namespace ERP.Web.Api.KhachHang
{
    public class Api_PhanHoiKhachHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_PhanHoiKhachHang
        [Route("api/Api_PhanHoiKhachHang/GetKhachHanghl/{makh}")]
        public List<KhachHanghl> GetKhachHanghl(string makh)
        {
            var vData = (from t1 in db.KHs
                         join t2 in db.KH_PHAN_HOI_KHACH_HANG on t1.MA_KHACH_HANG equals t2.MA_KHACH_HANG
                         join t3 in db.HT_NGUOI_DUNG on t2.NGUOI_PHAN_HOI equals t3.USERNAME
                         where t2.MA_KHACH_HANG == makh
                         select new
                         {
                             t1.MA_KHACH_HANG,
                             t2.ID,
                             t2.NGUOI_PHAN_HOI,
                             t2.NGAY_PHAN_HOI,
                             t2.THONG_TIN_PHAN_HOI,
                             t1.TEN_CONG_TY,
                             t3.HO_VA_TEN,
                         });
            var result = vData.ToList().Select(x => new KhachHanghl()
            {
                ID = x.ID,
                NGUOI_PHAN_HOI = x.NGUOI_PHAN_HOI,
                MA_KHACH_HANG = x.MA_KHACH_HANG,
                NGAY_PHAN_HOI = x.NGAY_PHAN_HOI.ToString("dd/MM/yyyy"),
                THONG_TIN_PHAN_HOI = x.THONG_TIN_PHAN_HOI,
                TEN_CONG_TY = x.TEN_CONG_TY,
                HO_VA_TEN = x.HO_VA_TEN,
            }).ToList();
            return result;
        }

        // GET: api/Api_PhanHoiKhachHang/5
        [ResponseType(typeof(KH_PHAN_HOI_KHACH_HANG))]
        public IHttpActionResult GetKH_PHAN_HOI_KHACH_HANG(int id)
        {
            KH_PHAN_HOI_KHACH_HANG kH_PHAN_HOI_KHACH_HANG = db.KH_PHAN_HOI_KHACH_HANG.Find(id);
            if (kH_PHAN_HOI_KHACH_HANG == null)
            {
                return NotFound();
            }

            return Ok(kH_PHAN_HOI_KHACH_HANG);
        }

        // PUT: api/Api_PhanHoiKhachHang/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_PHAN_HOI_KHACH_HANG(int id, KH_PHAN_HOI_KHACH_HANG kH_PHAN_HOI_KHACH_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var query = db.KH_PHAN_HOI_KHACH_HANG.Where(x => x.ID == id).FirstOrDefault();
            if(query != null)
            {
                query.THONG_TIN_PHAN_HOI = kH_PHAN_HOI_KHACH_HANG.THONG_TIN_PHAN_HOI;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_PHAN_HOI_KHACH_HANGExists(id))
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

        // POST: api/Api_PhanHoiKhachHang
        [ResponseType(typeof(KH_PHAN_HOI_KHACH_HANG))]
        public IHttpActionResult PostKH_PHAN_HOI_KHACH_HANG(KH_PHAN_HOI_KHACH_HANG kH_PHAN_HOI_KHACH_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            KH_PHAN_HOI_KHACH_HANG kh = new KH_PHAN_HOI_KHACH_HANG();
            kh.MA_KHACH_HANG = kH_PHAN_HOI_KHACH_HANG.MA_KHACH_HANG;
            kh.NGAY_PHAN_HOI = DateTime.Today.Date;
            kh.NGUOI_PHAN_HOI = kH_PHAN_HOI_KHACH_HANG.NGUOI_PHAN_HOI;
            kh.THONG_TIN_PHAN_HOI = kH_PHAN_HOI_KHACH_HANG.THONG_TIN_PHAN_HOI;
            db.KH_PHAN_HOI_KHACH_HANG.Add(kh);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kH_PHAN_HOI_KHACH_HANG.ID }, kH_PHAN_HOI_KHACH_HANG);
        }

        // DELETE: api/Api_PhanHoiKhachHang/5
        [ResponseType(typeof(KH_PHAN_HOI_KHACH_HANG))]
        public IHttpActionResult DeleteKH_PHAN_HOI_KHACH_HANG(int id)
        {
            KH_PHAN_HOI_KHACH_HANG kH_PHAN_HOI_KHACH_HANG = db.KH_PHAN_HOI_KHACH_HANG.Find(id);
            if (kH_PHAN_HOI_KHACH_HANG == null)
            {
                return NotFound();
            }

            db.KH_PHAN_HOI_KHACH_HANG.Remove(kH_PHAN_HOI_KHACH_HANG);
            db.SaveChanges();

            return Ok(kH_PHAN_HOI_KHACH_HANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_PHAN_HOI_KHACH_HANGExists(int id)
        {
            return db.KH_PHAN_HOI_KHACH_HANG.Count(e => e.ID == id) > 0;
        }
    }
}