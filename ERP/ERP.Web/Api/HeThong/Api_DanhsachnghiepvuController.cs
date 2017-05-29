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

namespace ERP.Web.Api.HeThong
{
    public class Api_DanhsachnghiepvuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Danhsachnghiepvu
        public IQueryable<CN_NGHIEP_VU> GetCN_NGHIEP_VU()
        {
            return db.CN_NGHIEP_VU;
        }

        // GET: api/Api_Danhsachnghiepvu/5
        [ResponseType(typeof(CN_NGHIEP_VU))]
        public IHttpActionResult GetCN_NGHIEP_VU(string id)
        {
            CN_NGHIEP_VU cN_NGHIEP_VU = db.CN_NGHIEP_VU.Find(id);
            if (cN_NGHIEP_VU == null)
            {
                return NotFound();
            }

            return Ok(cN_NGHIEP_VU);
        }

        // PUT: api/Api_Danhsachnghiepvu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCN_NGHIEP_VU(string id, CN_NGHIEP_VU cN_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cN_NGHIEP_VU.ID)
            {
                return BadRequest();
            }

            db.Entry(cN_NGHIEP_VU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CN_NGHIEP_VUExists(id))
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

        // POST: api/Api_Danhsachnghiepvu
        [ResponseType(typeof(CN_NGHIEP_VU))]
        public IHttpActionResult PostCN_NGHIEP_VU(CN_NGHIEP_VU cN_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CN_NGHIEP_VU.Add(cN_NGHIEP_VU);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CN_NGHIEP_VUExists(cN_NGHIEP_VU.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cN_NGHIEP_VU.ID }, cN_NGHIEP_VU);
        }

        // DELETE: api/Api_Danhsachnghiepvu/5
        [ResponseType(typeof(CN_NGHIEP_VU))]
        public IHttpActionResult DeleteCN_NGHIEP_VU(string id)
        {
            CN_NGHIEP_VU cN_NGHIEP_VU = db.CN_NGHIEP_VU.Find(id);
            if (cN_NGHIEP_VU == null)
            {
                return NotFound();
            }

            db.CN_NGHIEP_VU.Remove(cN_NGHIEP_VU);
            db.SaveChanges();

            return Ok(cN_NGHIEP_VU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CN_NGHIEP_VUExists(string id)
        {
            return db.CN_NGHIEP_VU.Count(e => e.ID == id) > 0;
        }
    }
}