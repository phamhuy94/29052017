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

namespace ERP.Web.Api.BaoGia
{
    public class Api_PhieuBaoGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_PhieuBaoGia
        public IQueryable<BH_BAO_GIA> GetBH_BAO_GIA()
        {
            return db.BH_BAO_GIA;
        }

        [Route("api/Api_PhieuBaoGia/GetThongTinChung/{so_bao_gia}")]
        public List<GetAll_ThongTinBaoGia_Result> GetThongTinChung(string so_bao_gia)
        {
            var query = db.Database.SqlQuery<GetAll_ThongTinBaoGia_Result>("GetAll_ThongTinBaoGia  @so_bao_gia, @ma_cong_ty", new SqlParameter("so_bao_gia", so_bao_gia), new SqlParameter("ma_cong_ty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_PhieuBaoGia/GetThongTinChiTiet/{so_bao_gia}")]
        public List<GetAll_ThongTinChiTietBaoGia_Result> GetThongTinChiTiet(string so_bao_gia)
        {
            var query = db.Database.SqlQuery<GetAll_ThongTinChiTietBaoGia_Result>("GetAll_ThongTinChiTietBaoGia @so_bao_gia", new SqlParameter("so_bao_gia", so_bao_gia));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_PhieuBaoGia/5
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult GetBH_BAO_GIA(string id)
        {
            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            return Ok(bH_BAO_GIA);
        }

        // PUT: api/Api_PhieuBaoGia/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_BAO_GIA(string id, BH_BAO_GIA bH_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_BAO_GIA.SO_BAO_GIA)
            {
                return BadRequest();
            }

            db.Entry(bH_BAO_GIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_BAO_GIAExists(id))
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

        // POST: api/Api_PhieuBaoGia
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult PostBH_BAO_GIA(BH_BAO_GIA bH_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_BAO_GIA.Add(bH_BAO_GIA);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_BAO_GIAExists(bH_BAO_GIA.SO_BAO_GIA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bH_BAO_GIA.SO_BAO_GIA }, bH_BAO_GIA);
        }

        // DELETE: api/Api_PhieuBaoGia/5
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult DeleteBH_BAO_GIA(string id)
        {
            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            db.BH_BAO_GIA.Remove(bH_BAO_GIA);
            db.SaveChanges();

            return Ok(bH_BAO_GIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_BAO_GIAExists(string id)
        {
            return db.BH_BAO_GIA.Count(e => e.SO_BAO_GIA == id) > 0;
        }
    }
}