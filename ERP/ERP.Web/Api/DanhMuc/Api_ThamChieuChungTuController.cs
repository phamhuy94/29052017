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

namespace ERP.Web.Api.HeThong
{
    public class Api_ThamChieuChungTuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ThamChieuChungTu
        public List<XL_THAM_CHIEU_CHUNG_TU> GetXL_THAM_CHIEU_CHUNG_TU()
        {
            var vData = db.XL_THAM_CHIEU_CHUNG_TU;
            var result = vData.ToList().Select(x => new XL_THAM_CHIEU_CHUNG_TU()
            {
                ID = x.ID,
                SO_CHUNG_TU_GOC = x.SO_CHUNG_TU_GOC,
                SO_CHUNG_TU_THAM_CHIEU = x.SO_CHUNG_TU_THAM_CHIEU,
            }).ToList();
            return result;
        }

        //
        [Route("api/Api_ThamChieuChungTu/GetThamChieuChungTu/{sct}")]
        public List<Get_XL_ThamChieuChungTu_Result> GetThamChieuChungTu(string sct)
        {
            var query = db.Database.SqlQuery<Get_XL_ThamChieuChungTu_Result>("Get_XL_ThamChieuChungTu @sochungtu", new SqlParameter("sochungtu", sct));

            return query.ToList();
        }


        // GET: api/Api_ThamChieuChungTu/5
        [ResponseType(typeof(XL_THAM_CHIEU_CHUNG_TU))]
        public IHttpActionResult GetXL_THAM_CHIEU_CHUNG_TU(int id)
        {
            XL_THAM_CHIEU_CHUNG_TU xL_THAM_CHIEU_CHUNG_TU = db.XL_THAM_CHIEU_CHUNG_TU.Find(id);
            if (xL_THAM_CHIEU_CHUNG_TU == null)
            {
                return NotFound();
            }

            return Ok(xL_THAM_CHIEU_CHUNG_TU);
        }

        // PUT: api/Api_ThamChieuChungTu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutXL_THAM_CHIEU_CHUNG_TU(int id, XL_THAM_CHIEU_CHUNG_TU xL_THAM_CHIEU_CHUNG_TU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != xL_THAM_CHIEU_CHUNG_TU.ID)
            {
                return BadRequest();
            }

            db.Entry(xL_THAM_CHIEU_CHUNG_TU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XL_THAM_CHIEU_CHUNG_TUExists(id))
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

        // POST: api/Api_ThamChieuChungTu
        [ResponseType(typeof(XL_THAM_CHIEU_CHUNG_TU))]
        public IHttpActionResult PostXL_THAM_CHIEU_CHUNG_TU(XL_THAM_CHIEU_CHUNG_TU xL_THAM_CHIEU_CHUNG_TU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.XL_THAM_CHIEU_CHUNG_TU.Add(xL_THAM_CHIEU_CHUNG_TU);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = xL_THAM_CHIEU_CHUNG_TU.ID }, xL_THAM_CHIEU_CHUNG_TU);
        }

        // DELETE: api/Api_ThamChieuChungTu/5
        [ResponseType(typeof(XL_THAM_CHIEU_CHUNG_TU))]
        public IHttpActionResult DeleteXL_THAM_CHIEU_CHUNG_TU(int id)
        {
            XL_THAM_CHIEU_CHUNG_TU xL_THAM_CHIEU_CHUNG_TU = db.XL_THAM_CHIEU_CHUNG_TU.Find(id);
            if (xL_THAM_CHIEU_CHUNG_TU == null)
            {
                return NotFound();
            }

            db.XL_THAM_CHIEU_CHUNG_TU.Remove(xL_THAM_CHIEU_CHUNG_TU);
            db.SaveChanges();

            return Ok(xL_THAM_CHIEU_CHUNG_TU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool XL_THAM_CHIEU_CHUNG_TUExists(int id)
        {
            return db.XL_THAM_CHIEU_CHUNG_TU.Count(e => e.ID == id) > 0;
        }
    }
}