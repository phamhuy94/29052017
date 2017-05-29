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
    public class Api_KHO_CT_NHAP_KHOController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_KHO_CT_NHAP_KHO
        [Route("api/Api_KHO_CT_NHAP_KHO/GetCTPhieuNhapKho/{sct}")]
        public List<GetCTNhapKho_Result> GetCTPhieuNhapKho(string sct)
        {
            var query = db.Database.SqlQuery<GetCTNhapKho_Result>("GetCTNhapKho @sochungtu,@macongty ", new SqlParameter("sochungtu", sct), new SqlParameter("macongty", "HOPLONG"));

            return query.ToList();
        }

        // GET: api/Api_KHO_CT_NHAP_KHO/5
        [ResponseType(typeof(KHO_CT_NHAP_KHO))]
        public IHttpActionResult GetKHO_CT_NHAP_KHO(int id)
        {
            KHO_CT_NHAP_KHO kHO_CT_NHAP_KHO = db.KHO_CT_NHAP_KHO.Find(id);
            if (kHO_CT_NHAP_KHO == null)
            {
                return NotFound();
            }

            return Ok(kHO_CT_NHAP_KHO);
        }

        // PUT: api/Api_KHO_CT_NHAP_KHO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHO_CT_NHAP_KHO(int id, KHO_CT_NHAP_KHO kHO_CT_NHAP_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHO_CT_NHAP_KHO.ID)
            {
                return BadRequest();
            }

            db.Entry(kHO_CT_NHAP_KHO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHO_CT_NHAP_KHOExists(id))
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

        // POST: api/Api_KHO_CT_NHAP_KHO
        [ResponseType(typeof(KHO_CT_NHAP_KHO))]
        public IHttpActionResult PostKHO_CT_NHAP_KHO(KHO_CT_NHAP_KHO kHO_CT_NHAP_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KHO_CT_NHAP_KHO.Add(kHO_CT_NHAP_KHO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kHO_CT_NHAP_KHO.ID }, kHO_CT_NHAP_KHO);
        }

        // DELETE: api/Api_KHO_CT_NHAP_KHO/5
        [ResponseType(typeof(KHO_CT_NHAP_KHO))]
        public IHttpActionResult DeleteKHO_CT_NHAP_KHO(int id)
        {
            KHO_CT_NHAP_KHO kHO_CT_NHAP_KHO = db.KHO_CT_NHAP_KHO.Find(id);
            if (kHO_CT_NHAP_KHO == null)
            {
                return NotFound();
            }

            db.KHO_CT_NHAP_KHO.Remove(kHO_CT_NHAP_KHO);
            db.SaveChanges();

            return Ok(kHO_CT_NHAP_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHO_CT_NHAP_KHOExists(int id)
        {
            return db.KHO_CT_NHAP_KHO.Count(e => e.ID == id) > 0;
        }
    }
}