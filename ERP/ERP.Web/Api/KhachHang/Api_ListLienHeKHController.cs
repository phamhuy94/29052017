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

namespace ERP.Web.Api.KhachHang
{
    public class Api_ListLienHeKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ListLienHeKH
        public List<KH_LIEN_HE> GetKH_LIEN_HE()
        {
            var vData = db.KH_LIEN_HE;
            var result = vData.ToList().Select(x => new KH_LIEN_HE()
            {
                ID_LIEN_HE = x.ID_LIEN_HE,
                NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
                EMAIL_CA_NHAN = x.EMAIL_CA_NHAN,
                EMAIL_CONG_TY = x.EMAIL_CONG_TY,
            }).ToList();
            return result;
        }

        // GET: api/Api_ListLienHeKH/5
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult GetKH_LIEN_HE(int id)
        {
            KH_LIEN_HE kH_LIEN_HE = db.KH_LIEN_HE.Find(id);
            if (kH_LIEN_HE == null)
            {
                return NotFound();
            }

            return Ok(kH_LIEN_HE);
        }

        // PUT: api/Api_ListLienHeKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_LIEN_HE(int id, KH_LIEN_HE kH_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH_LIEN_HE.ID_LIEN_HE)
            {
                return BadRequest();
            }

            db.Entry(kH_LIEN_HE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_LIEN_HEExists(id))
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

        // POST: api/Api_ListLienHeKH
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult PostKH_LIEN_HE(KH_LIEN_HE kH_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KH_LIEN_HE.Add(kH_LIEN_HE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kH_LIEN_HE.ID_LIEN_HE }, kH_LIEN_HE);
        }

        // DELETE: api/Api_ListLienHeKH/5
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult DeleteKH_LIEN_HE(int id)
        {
            KH_LIEN_HE kH_LIEN_HE = db.KH_LIEN_HE.Find(id);
            if (kH_LIEN_HE == null)
            {
                return NotFound();
            }

            db.KH_LIEN_HE.Remove(kH_LIEN_HE);
            db.SaveChanges();

            return Ok(kH_LIEN_HE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_LIEN_HEExists(int id)
        {
            return db.KH_LIEN_HE.Count(e => e.ID_LIEN_HE == id) > 0;
        }
    }
}