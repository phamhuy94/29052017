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
    public class Api_DichVuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_DichVu
        public List<DV> GetDVs()
        {
            var vData = db.DVs;
            var result = vData.ToList().Select(x => new DV()
            {
                MA_DICH_VU = x.MA_DICH_VU,
                TEN_DICH_VU = x.TEN_DICH_VU,
                GHI_CHU = x.GHI_CHU
            }).ToList();
            return result;
        }

        // GET: api/Api_DichVu/5
        [ResponseType(typeof(DV))]
        public IHttpActionResult GetDV(string id)
        {
            DV dV = db.DVs.Find(id);
            if (dV == null)
            {
                return NotFound();
            }

            return Ok(dV);
        }

        // PUT: api/Api_DichVu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDV(string id, DV dV)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dV.MA_DICH_VU)
            {
                return BadRequest();
            }

            db.Entry(dV).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DVExists(id))
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

        // POST: api/Api_DichVu
        [ResponseType(typeof(DV))]
        public IHttpActionResult PostDV(DV dV)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DVs.Add(dV);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DVExists(dV.MA_DICH_VU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dV.MA_DICH_VU }, dV);
        }

        // DELETE: api/Api_DichVu/5
        [ResponseType(typeof(DV))]
        public IHttpActionResult DeleteDV(string id)
        {
            DV dV = db.DVs.Find(id);
            if (dV == null)
            {
                return NotFound();
            }

            db.DVs.Remove(dV);
            db.SaveChanges();

            return Ok(dV);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DVExists(string id)
        {
            return db.DVs.Count(e => e.MA_DICH_VU == id) > 0;
        }
    }
}