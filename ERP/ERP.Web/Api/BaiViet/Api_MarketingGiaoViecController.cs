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

namespace ERP.Web.Api.BaiViet
{
    public class Api_MarketingGiaoViecController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        [Route("api/Api_MarketingGiaoViec/ListNhanVienHL")]
        public List<HopLong_GetAll_Nhanvienhl_Result> ListNhanVienHL()
        {
            var query = db.Database.SqlQuery<HopLong_GetAll_Nhanvienhl_Result>("HopLong_GetAll_Nhanvienhl");
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_MarketingGiaoViec/5
        [ResponseType(typeof(HT_THONG_BAO_MARKETING))]
        public IHttpActionResult GetHT_THONG_BAO_MARKETING(int id)
        {
            HT_THONG_BAO_MARKETING hT_THONG_BAO_MARKETING = db.HT_THONG_BAO_MARKETING.Find(id);
            if (hT_THONG_BAO_MARKETING == null)
            {
                return NotFound();
            }

            return Ok(hT_THONG_BAO_MARKETING);
        }

        // PUT: api/Api_MarketingGiaoViec/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHT_THONG_BAO_MARKETING(int id, HT_THONG_BAO_MARKETING hT_THONG_BAO_MARKETING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hT_THONG_BAO_MARKETING.ID)
            {
                return BadRequest();
            }

            db.Entry(hT_THONG_BAO_MARKETING).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HT_THONG_BAO_MARKETINGExists(id))
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

        // POST: api/Api_MarketingGiaoViec
        [Route("api/Api_MarketingGiaoViec/PostHT_THONG_BAO_MARKETING")]
        public IHttpActionResult PostHT_THONG_BAO_MARKETING(MKGiaoViec giaoviec)
        {
            HT_CONG_VIEC_NHAN_VIEN congviecnv = new HT_CONG_VIEC_NHAN_VIEN();
            congviecnv.NGAY_CAP_NHAT = DateTime.Today.Date;
            congviecnv.USERNAME = giaoviec.USERNAME;
            congviecnv.NGUOI_CAP_NHAT = giaoviec.NGUOI_CAP_NHAT;
            congviecnv.NHIEM_VU_CHI_TIET = giaoviec.NHIEM_VU_CHI_TIET;
            congviecnv.MUC_TIEU_CONG_VIEC = giaoviec.MUC_TIEU_CONG_VIEC;
            db.HT_CONG_VIEC_NHAN_VIEN.Add(congviecnv);
            db.SaveChanges();

            HT_NHIEM_VU_PHONG_BAN nvphongban = new HT_NHIEM_VU_PHONG_BAN();
            nvphongban.NGAY_CAP_NHAT = DateTime.Today.Date;
            nvphongban.MA_PHONG_BAN = giaoviec.MA_PHONG_BAN;
            nvphongban.NGUOI_CAP_NHAT = giaoviec.NGUOI_CAP_NHAT;
            nvphongban.NHIEM_VU_PHAI_LAM = giaoviec.NHIEM_VU_PHAI_LAM;
            db.HT_NHIEM_VU_PHONG_BAN.Add(nvphongban);
            db.SaveChanges();

            HT_THONG_BAO_MARKETING thongbaomk = new HT_THONG_BAO_MARKETING();
            thongbaomk.NGAY_THONG_BAO = DateTime.Today.Date;
            thongbaomk.MA_PHONG_BAN = giaoviec.MA_PHONG_BAN_MARK;
            thongbaomk.NGUOI_THONG_BAO = giaoviec.NGUOI_CAP_NHAT;
            thongbaomk.NOI_DUNG = giaoviec.NOI_DUNG_MARK;
            db.HT_THONG_BAO_MARKETING.Add(thongbaomk);
            db.SaveChanges();

            HT_THONG_BAO_MARKETING thongbaomk1 = new HT_THONG_BAO_MARKETING();
            thongbaomk1.NGAY_THONG_BAO = DateTime.Today.Date;
            thongbaomk1.MA_PHONG_BAN = giaoviec.MA_PHONG_BAN_SALE;
            thongbaomk1.NGUOI_THONG_BAO = giaoviec.NGUOI_CAP_NHAT;
            thongbaomk1.NOI_DUNG = giaoviec.NOI_DUNG_MARK;
            db.HT_THONG_BAO_MARKETING.Add(thongbaomk1);
            db.SaveChanges();

            

            return Ok();
        }

        // DELETE: api/Api_MarketingGiaoViec/5
        [ResponseType(typeof(HT_THONG_BAO_MARKETING))]
        public IHttpActionResult DeleteHT_THONG_BAO_MARKETING(int id)
        {
            HT_THONG_BAO_MARKETING hT_THONG_BAO_MARKETING = db.HT_THONG_BAO_MARKETING.Find(id);
            if (hT_THONG_BAO_MARKETING == null)
            {
                return NotFound();
            }

            db.HT_THONG_BAO_MARKETING.Remove(hT_THONG_BAO_MARKETING);
            db.SaveChanges();

            return Ok(hT_THONG_BAO_MARKETING);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HT_THONG_BAO_MARKETINGExists(int id)
        {
            return db.HT_THONG_BAO_MARKETING.Count(e => e.ID == id) > 0;
        }
    }
}