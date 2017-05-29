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
using ERP.Web.Areas.HopLong.Models;

namespace ERP.Web.Areas.HopLong.Api.HeThong
{
    public class Api_NhanvienHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();

        // GET: api/Api_NhanvienHL
        public List<CCTC_NHAN_VIEN> GetCCTC_NHAN_VIEN(string id)
        {
            var vData = db.CCTC_NHAN_VIEN.Where(x => x.USERNAME == id);
            var result = vData.ToList().Select(x => new CCTC_NHAN_VIEN()
            {
                USERNAME = x.USERNAME,
                GIOI_TINH = x.GIOI_TINH,
                NGAY_SINH = x.NGAY_SINH,
                QUE_QUAN = x.QUE_QUAN,
                THANH_TICH_CONG_TAC = x.THANH_TICH_CONG_TAC,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                LINH_VUC_CONG_TAC = x.LINH_VUC_CONG_TAC,
            }).ToList();
            return result;
        }


        // GET: api/NGUOI_DUNG_FULL/
        [Route("api/Api_NhanvienHL/GetListNhanvien")]
        public List<NguoiDungPhongBan> GetListNhanvien()
        {
            var vData = (from t1 in db.CCTC_NHAN_VIEN
                         join t2 in db.HT_NGUOI_DUNG on t1.USERNAME equals t2.USERNAME
                         join t3 in db.CCTC_PHONG_BAN on t1.MA_PHONG_BAN equals t3.MA_PHONG_BAN

                         select new { t1.LINH_VUC_CONG_TAC,t1.USERNAME, t1.GIOI_TINH, t1.NGAY_SINH, t1.CHUC_VU, t1.QUE_QUAN, t1.THANH_TICH_CONG_TAC, t1.TRINH_DO_HOC_VAN, t2.HO_VA_TEN, t2.EMAIL, t2.SDT, t2.AVATAR, t3.TEN_PHONG_BAN, t1.MA_PHONG_BAN });


            var result = vData.ToList().Select(x => new NguoiDungPhongBan()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                EMAIL = x.EMAIL,
                CHUC_VU = x.CHUC_VU,
                SDT = x.SDT,
                GIOI_TINH = x.GIOI_TINH,
                NGAY_SINH = x.NGAY_SINH.ToString(),
                QUE_QUAN = x.QUE_QUAN,
                THANH_TICH_CONG_TAC = x.THANH_TICH_CONG_TAC,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                AVATAR = x.AVATAR,
                TEN_PHONG_BAN = x.TEN_PHONG_BAN,
                USERNAME = x.USERNAME,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                LINH_VUC_CONG_TAC = x.LINH_VUC_CONG_TAC,
            }).ToList();
            return result;
        }




        // GET: api/Api_NhanvienHL/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult GetCCTC_NHAN_VIEN()
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find();
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            return Ok(cCTC_NHAN_VIEN);
        }

        // PUT: api/Api_NhanvienHL/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_NHAN_VIEN(string id, NHAN_VIEN_MODEL nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanvien.USERNAME)
            {
                return BadRequest();
            }
            CCTC_NHAN_VIEN nv = new CCTC_NHAN_VIEN();
            nv.USERNAME = nhanvien.USERNAME;
            nv.GIOI_TINH = nhanvien.GIOI_TINH;
            if (nhanvien.NGAY_SINH != null)
            nv.NGAY_SINH = xlnt.Xulydatetime(nhanvien.NGAY_SINH);
            nv.QUE_QUAN = nhanvien.QUE_QUAN;
            nv.THANH_TICH_CONG_TAC = nhanvien.THANH_TICH_CONG_TAC;
            nv.LINH_VUC_CONG_TAC = nhanvien.LINH_VUC_CONG_TAC;
            nv.TRINH_DO_HOC_VAN = nhanvien.TRINH_DO_HOC_VAN;
            nv.MA_PHONG_BAN = nhanvien.MA_PHONG_BAN;
            db.Entry(nv).State = EntityState.Modified;
            


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

        // POST: api/Api_NhanvienHL
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult PostCCTC_NHAN_VIEN(NHAN_VIEN_MODEL nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CCTC_NHAN_VIEN nv = new CCTC_NHAN_VIEN();
            nv.USERNAME = nhanvien.USERNAME;
            nv.GIOI_TINH = nhanvien.GIOI_TINH;
            if (nhanvien.NGAY_SINH != null)
                nv.NGAY_SINH = xlnt.Xulydatetime(nhanvien.NGAY_SINH);
            nv.QUE_QUAN = nhanvien.QUE_QUAN;
            nv.THANH_TICH_CONG_TAC = nhanvien.THANH_TICH_CONG_TAC;
            nv.TRINH_DO_HOC_VAN = nhanvien.TRINH_DO_HOC_VAN;
            nv.MA_PHONG_BAN = nhanvien.MA_PHONG_BAN;
            nv.LINH_VUC_CONG_TAC = nhanvien.LINH_VUC_CONG_TAC;
            db.CCTC_NHAN_VIEN.Add(nv);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_NHAN_VIENExists(nv.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nv.USERNAME }, nv);
        }

        // DELETE: api/Api_NhanvienHL/5
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