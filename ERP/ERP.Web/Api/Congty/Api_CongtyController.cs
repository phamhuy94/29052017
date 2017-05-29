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

namespace ERP.Web.Areas.HopLong.Api.HeThong
{
    public class Api_CongtyController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Congty
        public IQueryable<CCTC_CONG_TY> GetCCTC_CONG_TY()
        {
            return db.CCTC_CONG_TY;
        }

        // GET: api/Api_Congty/5
        [ResponseType(typeof(CCTC_CONG_TY))]
        public IHttpActionResult GetCCTC_CONG_TY(string id)
        {
            CCTC_CONG_TY cCTC_CONG_TY = db.CCTC_CONG_TY.Find(id);
            if (cCTC_CONG_TY == null)
            {
                return NotFound();
            }

            return Ok(cCTC_CONG_TY);
        }

        // PUT: api/Api_Congty/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_CONG_TY(string id, CCTC_CONG_TY cCTC_CONG_TY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_CONG_TY.MA_CONG_TY)
            {
                return BadRequest();
            }

            db.Entry(cCTC_CONG_TY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_CONG_TYExists(id))
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

        // POST: api/Api_Congty
        [ResponseType(typeof(CCTC_CONG_TY))]
        public IHttpActionResult PostCCTC_CONG_TY(CCTC_CONG_TY cCTC_CONG_TY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CCTC_CONG_TY.Add(cCTC_CONG_TY);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_CONG_TYExists(cCTC_CONG_TY.MA_CONG_TY))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cCTC_CONG_TY.MA_CONG_TY }, cCTC_CONG_TY);
        }

        // DELETE: api/Api_Congty/5
        [ResponseType(typeof(CCTC_CONG_TY))]
        public IHttpActionResult DeleteCCTC_CONG_TY(string id)
        {
            CCTC_CONG_TY cCTC_CONG_TY = db.CCTC_CONG_TY.Find(id);
            if (cCTC_CONG_TY == null)
            {
                return NotFound();
            }

            db.CCTC_CONG_TY.Remove(cCTC_CONG_TY);
            db.SaveChanges();

            return Ok(cCTC_CONG_TY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_CONG_TYExists(string id)
        {
            return db.CCTC_CONG_TY.Count(e => e.MA_CONG_TY == id) > 0;
        }
    }
}