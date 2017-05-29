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
using System.Data.SqlClient;
using ERP.Web.Models.Database;

namespace ERP.Api.Controllers.NV
{
    
    public class LichLamViecController : ApiController
    {
        

        // GET: api/LichLamViec/GetLichLamViec
        [Route("api/LichLamViec/GetLichLamViec/{macongty}")]
        public List<Prod_NV_LichLamViec_Result> GetLichLamViec(string macongty)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_NV_LichLamViec_Result>("Prod_NV_LichLamViec @macongty", new SqlParameter("macongty", macongty));
                var result = query.ToList();
                return result;
            }
        }

        // GET: api/LichLamViec/5
        [Route("api/LichLamViec/GetChiTietLichLamViec/{id}")]
        [ResponseType(typeof(NV_LICH_LAM_VIEC))]
        public IHttpActionResult GetChiTietLichLamViec(int id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                NV_LICH_LAM_VIEC nV_LICH_LAM_VIEC = db.NV_LICH_LAM_VIEC.Find(id);
                if (nV_LICH_LAM_VIEC == null)
                {
                    return NotFound();
                }

                return Ok(nV_LICH_LAM_VIEC);
            }
        }

  
        // PUT: api/LichLamViec/5
        [Route("api/LichLamViec/PutLichLamViec/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLichLamViec(int id, NV_LICH_LAM_VIEC nV_LICH_LAM_VIEC)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.NV_LICH_LAM_VIEC.Where(x => x.ID == id).FirstOrDefault();
                if (query != null)
                {
                    query.TIEU_DE_CONG_VIEC = nV_LICH_LAM_VIEC.TIEU_DE_CONG_VIEC;
                    query.NOI_DUNG_CONG_VIEC = nV_LICH_LAM_VIEC.NOI_DUNG_CONG_VIEC;
                    query.DIA_DIEM_LAM_VIEC = nV_LICH_LAM_VIEC.DIA_DIEM_LAM_VIEC;
                    query.THOI_GIAN_BAT_DAU = nV_LICH_LAM_VIEC.THOI_GIAN_BAT_DAU;
                    query.THOI_GIAN_KET_THUC = nV_LICH_LAM_VIEC.THOI_GIAN_KET_THUC;
                    query.HUY_CONG_VIEC = nV_LICH_LAM_VIEC.HUY_CONG_VIEC;
                    query.TRANG_THAI = nV_LICH_LAM_VIEC.TRANG_THAI;
                    query.GHI_CHU = nV_LICH_LAM_VIEC.GHI_CHU;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // POST: api/LichLamViec
        [Route("api/LichLamViec/PostLichLamViec")]
        [ResponseType(typeof(NV_LICH_LAM_VIEC))]
        public IHttpActionResult PostLichLamViec(NV_LICH_LAM_VIEC nV_LICH_LAM_VIEC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ERP_DATABASEEntities())
            {
                db.NV_LICH_LAM_VIEC.Add(nV_LICH_LAM_VIEC);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = nV_LICH_LAM_VIEC.ID }, nV_LICH_LAM_VIEC);
            }
        }

        // DELETE: api/LichLamViec/5
        [Route("api/LichLamViec/DeleteLichLamViec/{id}")]
        [ResponseType(typeof(NV_LICH_LAM_VIEC))]
        public IHttpActionResult DeleteLichLamViec(int id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                NV_LICH_LAM_VIEC nV_LICH_LAM_VIEC = db.NV_LICH_LAM_VIEC.Find(id);
                if (nV_LICH_LAM_VIEC == null)
                {
                    return NotFound();
                }

                db.NV_LICH_LAM_VIEC.Remove(nV_LICH_LAM_VIEC);
                db.SaveChanges();

                return Ok(nV_LICH_LAM_VIEC);
            }
        }

        protected override void Dispose(bool disposing)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        private bool NV_LICH_LAM_VIECExists(int id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                return db.NV_LICH_LAM_VIEC.Count(e => e.ID == id) > 0;
            }
        }
    }
}