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

namespace ERP.Web.Areas.HopLong.Api.HeThong
{
    public class Api_PhongbanHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_PhongbanHL
        public List<CCTC_PHONG_BAN> GetCCTC_PHONG_BAN()
        {
            var vData = db.CCTC_PHONG_BAN.Where(x => x.MA_CONG_TY == "HOPLONG");
            var result = vData.ToList().Select(x => new CCTC_PHONG_BAN()
            {
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                TEN_PHONG_BAN = x.TEN_PHONG_BAN,
                SDT = x.SDT,
                GHI_CHU = x.GHI_CHU,
                MA_CONG_TY = x.MA_CONG_TY
            }).ToList();
            return result;
        }

        // GET: api/Api_PhongbanHL/5
        [ResponseType(typeof(CCTC_PHONG_BAN))]
        public IHttpActionResult GetCCTC_PHONG_BAN(string id)
        {
            CCTC_PHONG_BAN cCTC_PHONG_BAN = db.CCTC_PHONG_BAN.Find(id);
            if (cCTC_PHONG_BAN == null)
            {
                return NotFound();
            }

            return Ok(cCTC_PHONG_BAN);
        }

        // PUT: api/Api_PhongbanHL/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_PHONG_BAN(string id, CCTC_PHONG_BAN cCTC_PHONG_BAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_PHONG_BAN.MA_PHONG_BAN)
            {
                return BadRequest();
            }

            db.Entry(cCTC_PHONG_BAN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_PHONG_BANExists(id))
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

        // POST: api/Api_PhongbanHL
        [ResponseType(typeof(CCTC_PHONG_BAN))]
        public IHttpActionResult PostCCTC_PHONG_BAN(CCTC_PHONG_BAN cCTC_PHONG_BAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CCTC_PHONG_BAN.Add(cCTC_PHONG_BAN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_PHONG_BANExists(cCTC_PHONG_BAN.MA_PHONG_BAN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cCTC_PHONG_BAN.MA_PHONG_BAN }, cCTC_PHONG_BAN);
        }

        // DELETE: api/Api_PhongbanHL/5
        [ResponseType(typeof(CCTC_PHONG_BAN))]
        public IHttpActionResult DeleteCCTC_PHONG_BAN(string id)
        {
            CCTC_PHONG_BAN cCTC_PHONG_BAN = db.CCTC_PHONG_BAN.Find(id);
            if (cCTC_PHONG_BAN == null)
            {
                return NotFound();
            }

            db.CCTC_PHONG_BAN.Remove(cCTC_PHONG_BAN);
            db.SaveChanges();

            return Ok(cCTC_PHONG_BAN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_PHONG_BANExists(string id)
        {
            return db.CCTC_PHONG_BAN.Count(e => e.MA_PHONG_BAN == id) > 0;
        }
    }
}