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

namespace ERP.Web.Api.Congty
{
    public class Api_LienHeController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LienHe
        public List<KH_LIEN_HE> GetKH_LIEN_HE()
        {
            var vData = db.KH_LIEN_HE;
            var result = vData.ToList().Select(x => new KH_LIEN_HE()
            {
                ID_LIEN_HE = x.ID_LIEN_HE,
                MA_KHACH_HANG = x.MA_KHACH_HANG,
                NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
                PHONG_BAN = x.PHONG_BAN,
                CHUC_VU = x.CHUC_VU,
                NGAY_SINH = x.NGAY_SINH,
                GIOI_TINH = x.GIOI_TINH,
                EMAIL_CA_NHAN = x.EMAIL_CA_NHAN,
                EMAIL_CONG_TY = x.EMAIL_CONG_TY,
                SKYPE = x.SKYPE,
                FACEBOOK = x.FACEBOOK,
                SDT1 = x.SDT1,
                SDT2 = x.SDT2,
                GHI_CHU = x.GHI_CHU,
            }).ToList();
            return result;
        }

        // GET: api/Api_LienHe/5
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

        // PUT: api/Api_LienHe/5
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

        // POST: api/Api_LienHe
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

        // DELETE: api/Api_LienHe/5
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