
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

namespace ERP.Web.Api.MuaHang
{
    public class Api_MH_JOIN_DENGHIController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_MH_JOIN_DENGHI
        public IQueryable<MH_DE_NGHI_JOIN_PO_MH> GetMH_DE_NGHI_JOIN_PO_MH()
        {
            return db.MH_DE_NGHI_JOIN_PO_MH;
        }

        // GET: api/Api_MH_JOIN_DENGHI/5
        [ResponseType(typeof(MH_DE_NGHI_JOIN_PO_MH))]
        public IHttpActionResult GetMH_DE_NGHI_JOIN_PO_MH(int id)
        {
            MH_DE_NGHI_JOIN_PO_MH mH_DE_NGHI_JOIN_PO_MH = db.MH_DE_NGHI_JOIN_PO_MH.Find(id);
            if (mH_DE_NGHI_JOIN_PO_MH == null)
            {
                return NotFound();
            }

            return Ok(mH_DE_NGHI_JOIN_PO_MH);
        }

        // PUT: api/Api_MH_JOIN_DENGHI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMH_DE_NGHI_JOIN_PO_MH(int id, MH_DE_NGHI_JOIN_PO_MH mH_DE_NGHI_JOIN_PO_MH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mH_DE_NGHI_JOIN_PO_MH.ID)
            {
                return BadRequest();
            }

            db.Entry(mH_DE_NGHI_JOIN_PO_MH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MH_DE_NGHI_JOIN_PO_MHExists(id))
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

        // POST: api/Api_MH_JOIN_DENGHI
        [Route("api/Api_MH_JOIN_DENGHI/PostMH_DE_NGHI_JOIN_PO_MH")]
        public IHttpActionResult PostMH_DE_NGHI_JOIN_PO_MH(List<MH_DE_NGHI_JOIN_PO_MH> mH_DE_NGHI_JOIN_PO_MH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in mH_DE_NGHI_JOIN_PO_MH)
            {
                MH_DE_NGHI_JOIN_PO_MH newjoin = new MH_DE_NGHI_JOIN_PO_MH();
                newjoin.ID_DE_NGHI = item.ID_DE_NGHI;
                newjoin.ID_PO_DAT_HANG = item.ID_PO_DAT_HANG;
                newjoin.SL_VE = item.SL_VE;
                db.MH_DE_NGHI_JOIN_PO_MH.Add(newjoin);
                db.SaveChanges();
            }

            return Ok();

        }

        // DELETE: api/Api_MH_JOIN_DENGHI/5
        [ResponseType(typeof(MH_DE_NGHI_JOIN_PO_MH))]
        public IHttpActionResult DeleteMH_DE_NGHI_JOIN_PO_MH(int id)
        {
            MH_DE_NGHI_JOIN_PO_MH mH_DE_NGHI_JOIN_PO_MH = db.MH_DE_NGHI_JOIN_PO_MH.Find(id);
            if (mH_DE_NGHI_JOIN_PO_MH == null)
            {
                return NotFound();
            }

            db.MH_DE_NGHI_JOIN_PO_MH.Remove(mH_DE_NGHI_JOIN_PO_MH);
            db.SaveChanges();

            return Ok(mH_DE_NGHI_JOIN_PO_MH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MH_DE_NGHI_JOIN_PO_MHExists(int id)
        {
            return db.MH_DE_NGHI_JOIN_PO_MH.Count(e => e.ID == id) > 0;
        }
    }
}