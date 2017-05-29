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
using System.Data.SqlClient;

namespace ERP.Web.Api.NguoiDung
{
    public class Api_GiaoViecController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_GiaoViec
        public IQueryable<NV_GIAO_VIEC> GetNV_GIAO_VIEC()
        {
            return db.NV_GIAO_VIEC;
        }

        // GET: api/Api_GiaoViec/5
        [Route("api/Api_GiaoViec/GetGiaoViec/{manv}")]
        public List<GetAll_ThongTinGiaoViec_Result> GetGiaoViec(string manv)
        {
            var query = db.Database.SqlQuery<GetAll_ThongTinGiaoViec_Result>("GetAll_ThongTinGiaoViec @manhanvien", new SqlParameter("manhanvien", manv));
            var result = query.ToList();
            return result;
        }

        // PUT: api/Api_GiaoViec/5
        [Route("api/Api_GiaoViec/PutNV_GIAO_VIEC/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNV_GIAO_VIEC(int id, NV_GIAO_VIEC nV_GIAO_VIEC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = db.NV_GIAO_VIEC.Where(x => x.ID == id).FirstOrDefault();
            if(query != null)
            {
                if (nV_GIAO_VIEC.TRANG_THAI == "Đã xong việc")
                    query.THOI_GIAN_HOAN_THANH =Convert.ToString(DateTime.Now);
                query.TRANG_THAI = nV_GIAO_VIEC.TRANG_THAI;
                query.GHI_CHU = nV_GIAO_VIEC.GHI_CHU;
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NV_GIAO_VIECExists(id))
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

        // POST: api/Api_GiaoViec
        [Route("api/Api_GiaoViec/PostNV_GIAO_VIEC")]
        public IHttpActionResult PostNV_GIAO_VIEC(NV_GIAO_VIEC giaoviec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            NV_GIAO_VIEC newviec = new NV_GIAO_VIEC();
            newviec.TIEU_DE_CONG_VIEC = giaoviec.TIEU_DE_CONG_VIEC;
            newviec.NGAY_GIAO_VIEC = DateTime.Today.Date;
            newviec.NOI_DUNG_CONG_VIEC = giaoviec.NOI_DUNG_CONG_VIEC;
            newviec.THOI_GIAN_HOAN_THANH = giaoviec.THOI_GIAN_HOAN_THANH;
            newviec.NGUOI_GIAO_VIEC = giaoviec.NGUOI_GIAO_VIEC;
            newviec.NHAN_VIEN_THUC_HIEN = giaoviec.NHAN_VIEN_THUC_HIEN;
            newviec.TRANG_THAI = giaoviec.TRANG_THAI;
            newviec.GHI_CHU = giaoviec.GHI_CHU;
            db.NV_GIAO_VIEC.Add(newviec);
            db.SaveChanges();

            return Ok(newviec);
        }

        // DELETE: api/Api_GiaoViec/5
        [ResponseType(typeof(NV_GIAO_VIEC))]
        public IHttpActionResult DeleteNV_GIAO_VIEC(int id)
        {
            NV_GIAO_VIEC nV_GIAO_VIEC = db.NV_GIAO_VIEC.Find(id);
            if (nV_GIAO_VIEC == null)
            {
                return NotFound();
            }

            db.NV_GIAO_VIEC.Remove(nV_GIAO_VIEC);
            db.SaveChanges();

            return Ok(nV_GIAO_VIEC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NV_GIAO_VIECExists(int id)
        {
            return db.NV_GIAO_VIEC.Count(e => e.ID == id) > 0;
        }
    }
}