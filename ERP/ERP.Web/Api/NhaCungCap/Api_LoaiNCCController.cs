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

namespace ERP.Web.Api.NhaCungCap
{
    public class Api_LoaiNCCController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LoaiNCC
        public List<NCC_LOAI> GetNCC_LOAI()
        {
            var vData = db.NCC_LOAI;
            var result = vData.ToList().Select(x => new NCC_LOAI()
            {
                MA_LOAI_NCC = x.MA_LOAI_NCC,
                TEN_LOAI_NCC = x.TEN_LOAI_NCC,
                GHI_CHU = x.GHI_CHU,
            }).ToList();
            return result;
        }

        // GET: api/Api_LoaiNCC/5
        [ResponseType(typeof(NCC_LOAI))]
        public IHttpActionResult GetNCC_LOAI(string id)
        {
            NCC_LOAI nCC_LOAI = db.NCC_LOAI.Find(id);
            if (nCC_LOAI == null)
            {
                return NotFound();
            }

            return Ok(nCC_LOAI);
        }

        // PUT: api/Api_LoaiNCC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC_LOAI(string id, NCC_LOAI nCC_LOAI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nCC_LOAI.MA_LOAI_NCC)
            {
                return BadRequest();
            }

            db.Entry(nCC_LOAI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCC_LOAIExists(id))
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

        // POST: api/Api_LoaiNCC
        [ResponseType(typeof(NCC_LOAI))]
        public IHttpActionResult PostNCC_LOAI(NCC_LOAI nCC_LOAI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NCC_LOAI.Add(nCC_LOAI);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NCC_LOAIExists(nCC_LOAI.MA_LOAI_NCC))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nCC_LOAI.MA_LOAI_NCC }, nCC_LOAI);
        }

        // DELETE: api/Api_LoaiNCC/5
        [ResponseType(typeof(NCC_LOAI))]
        public IHttpActionResult DeleteNCC_LOAI(string id)
        {
            NCC_LOAI nCC_LOAI = db.NCC_LOAI.Find(id);
            if (nCC_LOAI == null)
            {
                return NotFound();
            }

            db.NCC_LOAI.Remove(nCC_LOAI);
            db.SaveChanges();

            return Ok(nCC_LOAI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCC_LOAIExists(string id)
        {
            return db.NCC_LOAI.Count(e => e.MA_LOAI_NCC == id) > 0;
        }
    }
}