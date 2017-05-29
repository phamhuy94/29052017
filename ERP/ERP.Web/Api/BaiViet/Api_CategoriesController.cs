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
    public class Api_CategoriesController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Categories
        public List<CATEGORy> GetCATEGORy()
        {
            var vData = db.CATEGORIES;
            var result = vData.ToList().Select(x => new CATEGORy()
            {
                MA_DANH_MUC = x.MA_DANH_MUC,
                TEN_DANH_MUC = x.TEN_DANH_MUC,
                MA_DANH_MUC_CHA = x.MA_DANH_MUC_CHA,
            }).ToList();
            return result;
        }

        // GET: api/Api_Categories/5
        [ResponseType(typeof(CATEGORy))]
        public IHttpActionResult GetCATEGORy(string id)
        {
            CATEGORy cATEGORy = db.CATEGORIES.Find(id);
            if (cATEGORy == null)
            {
                return NotFound();
            }

            return Ok(cATEGORy);
        }

        // PUT: api/Api_Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCATEGORy(string id, CATEGORy cATEGORy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cATEGORy.MA_DANH_MUC)
            {
                return BadRequest();
            }

            db.Entry(cATEGORy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CATEGORyExists(id))
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

        // POST: api/Api_Categories
        [ResponseType(typeof(CATEGORy))]
        public IHttpActionResult PostCATEGORy(CATEGORy cATEGORy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CATEGORIES.Add(cATEGORy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CATEGORyExists(cATEGORy.MA_DANH_MUC))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cATEGORy.MA_DANH_MUC }, cATEGORy);
        }

        // DELETE: api/Api_Categories/5
        [ResponseType(typeof(CATEGORy))]
        public IHttpActionResult DeleteCATEGORy(string id)
        {
            CATEGORy cATEGORy = db.CATEGORIES.Find(id);
            if (cATEGORy == null)
            {
                return NotFound();
            }

            db.CATEGORIES.Remove(cATEGORy);
            db.SaveChanges();

            return Ok(cATEGORy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CATEGORyExists(string id)
        {
            return db.CATEGORIES.Count(e => e.MA_DANH_MUC == id) > 0;
        }
    }
}