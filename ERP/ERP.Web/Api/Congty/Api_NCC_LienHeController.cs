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
    public class Api_NCC_LienHeController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NCC_LienHe
        public List<NCC_LIEN_HE> GetNCC_LIEN_HE()
        {
            var vData = db.NCC_LIEN_HE;
            var result = vData.ToList().Select(x => new NCC_LIEN_HE()
            {
                ID_LIEN_HE = x.ID_LIEN_HE,
                MA_NHA_CUNG_CAP = x.MA_NHA_CUNG_CAP,
                NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
                PHONG_BAN = x.PHONG_BAN,
                CHUC_VU = x.CHUC_VU,
                NGAY_SINH = x.NGAY_SINH,
                GIOI_TINH = x.GIOI_TINH,
                EMAIL_CA_NHAN = x.EMAIL_CA_NHAN,
                EMAIL_CONG_TY = x.EMAIL_CONG_TY,
                SKYPE = x.SKYPE,
                FACEBOOK = x.FACEBOOK,
                SO_DIEN_THOAI_1 = x.SO_DIEN_THOAI_1,
                SO_DIEN_THOAI_2 = x.SO_DIEN_THOAI_2,
                GHI_CHU = x.GHI_CHU,
            }).ToList();
            return result;
        }

        // GET: api/Api_NCC_LienHe/5
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

        // PUT: api/Api_NCC_LienHe/5
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

        // POST: api/Api_NCC_LienHe
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult PostNCC_LIEN_HE(NCC_LIEN_HE nCC_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NCC_LIEN_HE.Add(nCC_LIEN_HE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nCC_LIEN_HE.ID_LIEN_HE }, nCC_LIEN_HE);
        }

        // DELETE: api/Api_NCC_LienHe/5
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