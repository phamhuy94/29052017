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

namespace ERP.Web.Api.HeThong
{
    public class Api_TongHopNhanVienController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_TongHopNhanVien
        public List<nhanvien> GetNhanVien()
        {
            var vData = (from t1 in db.CCTC_NHAN_VIEN
                         join t2 in db.HT_NGUOI_DUNG on t1.USERNAME equals t2.USERNAME
                         join t3 in db.CCTC_PHONG_BAN on t1.MA_PHONG_BAN equals t3.MA_PHONG_BAN
                         where t2.MA_CONG_TY == "HOPLONG"
                         select new { t1.GIOI_TINH, t1.USERNAME, t1.NGAY_SINH, t1.MA_PHONG_BAN, t1.CHUC_VU, t1.QUE_QUAN, t1.THANH_TICH_CONG_TAC, t1.TRINH_DO_HOC_VAN, t2.HO_VA_TEN, t2.EMAIL, t2.SDT, t2.AVATAR, t3.TEN_PHONG_BAN });


            var result = vData.ToList().Select(x => new nhanvien()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                EMAIL = x.EMAIL,
                USERNAME = x.USERNAME,
                CHUC_VU = x.CHUC_VU,
                SDT = x.SDT,
                GIOI_TINH = x.GIOI_TINH,
                NGAY_SINH = x.NGAY_SINH.ToString(),
                QUE_QUAN = x.QUE_QUAN,
                TEN_PHONG_BAN = x.TEN_PHONG_BAN,
                THANH_TICH_CONG_TAC = x.THANH_TICH_CONG_TAC,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                AVATAR = x.AVATAR
            }).ToList();
            return result;
        }

        // GET: api/Api_TongHopNhanVien
        [HttpPost]
        [Route("api/Api_TongHopNhanVien/GetNhanVienHL/{mnv}")]
        public List<nhanvien> GetNhanVienHL(string mnv)
        {
            var vData = (from t1 in db.CCTC_NHAN_VIEN
                         join t2 in db.HT_NGUOI_DUNG on t1.USERNAME equals t2.USERNAME
                         join t3 in db.CCTC_PHONG_BAN on t1.MA_PHONG_BAN equals t3.MA_PHONG_BAN
                         where t2.MA_CONG_TY == "HOPLONG"
                         select new { t1.GIOI_TINH, t1.USERNAME, t1.NGAY_SINH, t1.MA_PHONG_BAN, t1.CHUC_VU, t1.QUE_QUAN, t1.THANH_TICH_CONG_TAC, t1.TRINH_DO_HOC_VAN, t2.HO_VA_TEN, t2.EMAIL, t2.SDT, t2.AVATAR, t3.TEN_PHONG_BAN });


            var result = vData.ToList().Select(x => new nhanvien()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                EMAIL = x.EMAIL,
                USERNAME = x.USERNAME,
                CHUC_VU = x.CHUC_VU,
                SDT = x.SDT,
                GIOI_TINH = x.GIOI_TINH,
                NGAY_SINH = x.NGAY_SINH.ToString(),
                QUE_QUAN = x.QUE_QUAN,
                TEN_PHONG_BAN = x.TEN_PHONG_BAN,
                THANH_TICH_CONG_TAC = x.THANH_TICH_CONG_TAC,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                AVATAR = x.AVATAR
            }).ToList();
            var kq = result.Where(x => x.HO_VA_TEN.ToLower().Contains(mnv.ToLower())).Take(10).ToList();
            return kq;
           
        }

        // GET: api/Api_TongHopNhanVien/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult GetCCTC_NHAN_VIEN(string id)
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find(id);
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            return Ok(cCTC_NHAN_VIEN);
        }

        // PUT: api/Api_TongHopNhanVien/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_NHAN_VIEN(string id, CCTC_NHAN_VIEN cCTC_NHAN_VIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_NHAN_VIEN.USERNAME)
            {
                return BadRequest();
            }

            db.Entry(cCTC_NHAN_VIEN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_NHAN_VIENExists(id))
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

        // POST: api/Api_TongHopNhanVien
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult PostCCTC_NHAN_VIEN(CCTC_NHAN_VIEN cCTC_NHAN_VIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CCTC_NHAN_VIEN.Add(cCTC_NHAN_VIEN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_NHAN_VIENExists(cCTC_NHAN_VIEN.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cCTC_NHAN_VIEN.USERNAME }, cCTC_NHAN_VIEN);
        }

        // DELETE: api/Api_TongHopNhanVien/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult DeleteCCTC_NHAN_VIEN(string id)
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find(id);
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            db.CCTC_NHAN_VIEN.Remove(cCTC_NHAN_VIEN);
            db.SaveChanges();

            return Ok(cCTC_NHAN_VIEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_NHAN_VIENExists(string id)
        {
            return db.CCTC_NHAN_VIEN.Count(e => e.USERNAME == id) > 0;
        }
    }
}