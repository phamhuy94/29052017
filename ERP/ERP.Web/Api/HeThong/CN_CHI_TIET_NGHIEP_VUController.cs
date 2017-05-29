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
    public class CN_CHI_TIET_NGHIEP_VUController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/CN_CHI_TIET_NGHIEP_VU
        public List<CN_CHI_TIET_NGHIEP_VU> Get_Chitietnghiepvu()
        {
            var vData = db.CN_CHI_TIET_NGHIEP_VU;
            var result = vData.ToList().Select(x => new CN_CHI_TIET_NGHIEP_VU()
            {
                ID = x.ID,
                ID_NGHIEP_VU = x.ID_NGHIEP_VU,
                TEN_CHI_TIET = x.TEN_CHI_TIET,
                MO_TA = x.MO_TA,
            }).ToList();
            return result;
        }

        // GET: api/CN_CHI_TIET_NGHIEP_VU/5
        [ResponseType(typeof(CN_CHI_TIET_NGHIEP_VU))]
        public IHttpActionResult GetCN_CHI_TIET_NGHIEP_VU(int id)
        {
            CN_CHI_TIET_NGHIEP_VU cN_CHI_TIET_NGHIEP_VU = db.CN_CHI_TIET_NGHIEP_VU.Find(id);
            if (cN_CHI_TIET_NGHIEP_VU == null)
            {
                return NotFound();
            }

            return Ok(cN_CHI_TIET_NGHIEP_VU);
        }

        // PUT: api/CN_CHI_TIET_NGHIEP_VU/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCN_CHI_TIET_NGHIEP_VU(int id, CN_CHI_TIET_NGHIEP_VU cN_CHI_TIET_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cN_CHI_TIET_NGHIEP_VU.ID)
            {
                return BadRequest();
            }

            db.Entry(cN_CHI_TIET_NGHIEP_VU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CN_CHI_TIET_NGHIEP_VUExists(id))
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

        // POST: api/CN_CHI_TIET_NGHIEP_VU
        [ResponseType(typeof(CN_CHI_TIET_NGHIEP_VU))]
        public IHttpActionResult PostCN_CHI_TIET_NGHIEP_VU(CN_CHI_TIET_NGHIEP_VU cN_CHI_TIET_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CN_CHI_TIET_NGHIEP_VU.Add(cN_CHI_TIET_NGHIEP_VU);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cN_CHI_TIET_NGHIEP_VU.ID }, cN_CHI_TIET_NGHIEP_VU);
        }

        // DELETE: api/CN_CHI_TIET_NGHIEP_VU/5
        [ResponseType(typeof(CN_CHI_TIET_NGHIEP_VU))]
        public IHttpActionResult DeleteCN_CHI_TIET_NGHIEP_VU(int id)
        {
            CN_CHI_TIET_NGHIEP_VU cN_CHI_TIET_NGHIEP_VU = db.CN_CHI_TIET_NGHIEP_VU.Find(id);
            if (cN_CHI_TIET_NGHIEP_VU == null)
            {
                return NotFound();
            }

            db.CN_CHI_TIET_NGHIEP_VU.Remove(cN_CHI_TIET_NGHIEP_VU);
            db.SaveChanges();

            return Ok(cN_CHI_TIET_NGHIEP_VU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CN_CHI_TIET_NGHIEP_VUExists(int id)
        {
            return db.CN_CHI_TIET_NGHIEP_VU.Count(e => e.ID == id) > 0;
        }
    }
}