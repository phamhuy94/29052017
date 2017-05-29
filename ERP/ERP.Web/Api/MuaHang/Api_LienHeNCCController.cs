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

namespace ERP.Web.Api.MuaHang
{
    public class Api_LienHeNCCController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        [Route("api/Api_LienHeNCC/LocLienHeNCC/{mancc}")]
        public List<Prod_NCC_ShowLienHe_Result> LocLienHeNCC(string mancc)
        {
            var query = db.Database.SqlQuery<Prod_NCC_ShowLienHe_Result>("Prod_NCC_ShowLienHe @mancc", new SqlParameter("mancc", mancc));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_LienHeNCC/5
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult GetNCC_LIEN_HE(int id)
        {
            NCC_LIEN_HE nCC_LIEN_HE = db.NCC_LIEN_HE.Find(id);
            if (nCC_LIEN_HE == null)
            {
                return NotFound();
            }

            return Ok(nCC_LIEN_HE);
        }

        // PUT: api/Api_LienHeNCC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC_LIEN_HE(int id, NCC_LIEN_HE nCC_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nCC_LIEN_HE.ID_LIEN_HE)
            {
                return BadRequest();
            }

            db.Entry(nCC_LIEN_HE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCC_LIEN_HEExists(id))
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

        // POST: api/Api_LienHeNCC
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult PostNCC_LIEN_HE(NCC_LIEN_HE nCC_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NCC_LIEN_HE.Add(nCC_LIEN_HE);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NCC_LIEN_HEExists(nCC_LIEN_HE.ID_LIEN_HE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nCC_LIEN_HE.ID_LIEN_HE }, nCC_LIEN_HE);
        }

        // DELETE: api/Api_LienHeNCC/5
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult DeleteNCC_LIEN_HE(int id)
        {
            NCC_LIEN_HE nCC_LIEN_HE = db.NCC_LIEN_HE.Find(id);
            if (nCC_LIEN_HE == null)
            {
                return NotFound();
            }

            db.NCC_LIEN_HE.Remove(nCC_LIEN_HE);
            db.SaveChanges();

            return Ok(nCC_LIEN_HE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCC_LIEN_HEExists(int id)
        {
            return db.NCC_LIEN_HE.Count(e => e.ID_LIEN_HE == id) > 0;
        }
    }
}