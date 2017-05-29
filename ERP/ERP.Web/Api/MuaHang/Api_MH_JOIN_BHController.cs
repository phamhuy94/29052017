
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
using ERP.Web.Models.NewModels.MuaHang;

namespace ERP.Web.Api.MuaHang
{
    public class Api_MH_JOIN_BHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_MH_JOIN_BH
        public IQueryable<MH_JOIN_BH> GetMH_JOIN_BH()
        {
            return db.MH_JOIN_BH;
        }

        // GET: api/Api_MH_JOIN_BH/5
        [ResponseType(typeof(MH_JOIN_BH))]
        public IHttpActionResult GetMH_JOIN_BH(int id)
        {
            MH_JOIN_BH mH_JOIN_BH = db.MH_JOIN_BH.Find(id);
            if (mH_JOIN_BH == null)
            {
                return NotFound();
            }

            return Ok(mH_JOIN_BH);
        }

        // PUT: api/Api_MH_JOIN_BH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMH_JOIN_BH(int id, MH_JOIN_BH mH_JOIN_BH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mH_JOIN_BH.ID)
            {
                return BadRequest();
            }

            db.Entry(mH_JOIN_BH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MH_JOIN_BHExists(id))
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

        // POST: api/Api_MH_JOIN_BH
        [Route("api/Api_MH_JOIN_BH/PostMH_JOIN_BH")]
        public IHttpActionResult PostMH_JOIN_BH(List<List_MUA_JOIN_BAN> mH_JOIN_BH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach(var item in mH_JOIN_BH)
            {
                MH_JOIN_BH newjoin = new MH_JOIN_BH();
                newjoin.ID_PO_BAN_HANG = item.ID_PO_BAN_HANG;
                newjoin.ID_PO_MUA_HANG = item.ID_PO_MUA_HANG;
                db.MH_JOIN_BH.Add(newjoin);
                db.SaveChanges();
            }

            return Ok();
        }

        // DELETE: api/Api_MH_JOIN_BH/5
        [ResponseType(typeof(MH_JOIN_BH))]
        public IHttpActionResult DeleteMH_JOIN_BH(int id)
        {
            MH_JOIN_BH mH_JOIN_BH = db.MH_JOIN_BH.Find(id);
            if (mH_JOIN_BH == null)
            {
                return NotFound();
            }

            db.MH_JOIN_BH.Remove(mH_JOIN_BH);
            db.SaveChanges();

            return Ok(mH_JOIN_BH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MH_JOIN_BHExists(int id)
        {
            return db.MH_JOIN_BH.Count(e => e.ID == id) > 0;
        }
    }
}