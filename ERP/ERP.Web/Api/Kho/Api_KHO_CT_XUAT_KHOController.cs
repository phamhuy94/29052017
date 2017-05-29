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

namespace ERP.Web.Api.Kho
{
    public class Api_KHO_CT_XUAT_KHOController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_KHO_CT_XUAT_KHO
        [Route("api/Api_KHO_CT_XUAT_KHO/GetCTPhieuXuatKho/{sct}")]
        public List<GetChiTietPhieuXuatKho_Result> GetCTPhieuXuatKho(string sct)
        {
            var query = db.Database.SqlQuery<GetChiTietPhieuXuatKho_Result>("GetChiTietPhieuXuatKho @sochungtu, @macongty", new SqlParameter("sochungtu", sct), new SqlParameter("macongty", "HOPLONG"));

            return query.ToList();
        }
        // GET: api/Api_KHO_CT_XUAT_KHO/5
        [ResponseType(typeof(KHO_CT_XUAT_KHO))]
        public IHttpActionResult GetKHO_CT_XUAT_KHO(int id)
        {
            KHO_CT_XUAT_KHO kHO_CT_XUAT_KHO = db.KHO_CT_XUAT_KHO.Find(id);
            if (kHO_CT_XUAT_KHO == null)
            {
                return NotFound();
            }

            return Ok(kHO_CT_XUAT_KHO);
        }

        // PUT: api/Api_KHO_CT_XUAT_KHO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHO_CT_XUAT_KHO(int id, KHO_CT_XUAT_KHO kHO_CT_XUAT_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHO_CT_XUAT_KHO.ID)
            {
                return BadRequest();
            }

            db.Entry(kHO_CT_XUAT_KHO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHO_CT_XUAT_KHOExists(id))
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

        // POST: api/Api_KHO_CT_XUAT_KHO
        [ResponseType(typeof(KHO_CT_XUAT_KHO))]
        public IHttpActionResult PostKHO_CT_XUAT_KHO(KHO_CT_XUAT_KHO kHO_CT_XUAT_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KHO_CT_XUAT_KHO.Add(kHO_CT_XUAT_KHO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kHO_CT_XUAT_KHO.ID }, kHO_CT_XUAT_KHO);
        }

        // DELETE: api/Api_KHO_CT_XUAT_KHO/5
        [ResponseType(typeof(KHO_CT_XUAT_KHO))]
        public IHttpActionResult DeleteKHO_CT_XUAT_KHO(int id)
        {
            KHO_CT_XUAT_KHO kHO_CT_XUAT_KHO = db.KHO_CT_XUAT_KHO.Find(id);
            if (kHO_CT_XUAT_KHO == null)
            {
                return NotFound();
            }

            db.KHO_CT_XUAT_KHO.Remove(kHO_CT_XUAT_KHO);
            db.SaveChanges();

            return Ok(kHO_CT_XUAT_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHO_CT_XUAT_KHOExists(int id)
        {
            return db.KHO_CT_XUAT_KHO.Count(e => e.ID == id) > 0;
        }
    }
}