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

namespace ERP.Web.Api.Kho
{
    public class Api_DM_KHOController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_DM_KHO
        public IQueryable<DM_KHO> GetDM_KHO()
        {
            return db.DM_KHO;
        }

        // GET: api/Api_DM_KHO/5
        [ResponseType(typeof(DM_KHO))]
        public IHttpActionResult GetDM_KHO(string id)
        {
            DM_KHO dM_KHO = db.DM_KHO.Find(id);
            if (dM_KHO == null)
            {
                return NotFound();
            }

            return Ok(dM_KHO);
        }

        // PUT: api/Api_DM_KHO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDM_KHO(string id, DM_KHO dM_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dM_KHO.MA_KHO)
            {
                return BadRequest();
            }

            db.Entry(dM_KHO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DM_KHOExists(id))
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

        // POST: api/Api_DM_KHO
        [ResponseType(typeof(DM_KHO))]
        public IHttpActionResult PostDM_KHO(DM_KHO dM_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_KHO.Add(dM_KHO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_KHOExists(dM_KHO.MA_KHO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_KHO.MA_KHO }, dM_KHO);
        }

        // DELETE: api/Api_DM_KHO/5
        [ResponseType(typeof(DM_KHO))]
        public IHttpActionResult DeleteDM_KHO(string id)
        {
            DM_KHO dM_KHO = db.DM_KHO.Find(id);
            if (dM_KHO == null)
            {
                return NotFound();
            }

            db.DM_KHO.Remove(dM_KHO);
            db.SaveChanges();

            return Ok(dM_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_KHOExists(string id)
        {
            return db.DM_KHO.Count(e => e.MA_KHO == id) > 0;
        }
    }
}