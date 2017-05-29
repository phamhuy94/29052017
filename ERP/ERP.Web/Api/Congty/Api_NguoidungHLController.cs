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
    public class Api_NguoidungHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NguoidungHL
        public List<HT_NGUOI_DUNG> GetHT_NGUOI_DUNG()
        {
            var vData = db.HT_NGUOI_DUNG.Where(x => x.MA_CONG_TY == "HOPLONG");
            var result = vData.ToList().Select(x => new HT_NGUOI_DUNG()
            {
                USERNAME = x.USERNAME,
                PASSWORD = x.PASSWORD,
                HO_VA_TEN = x.HO_VA_TEN,
                SDT = x.SDT,
                EMAIL = x.EMAIL,
                AVATAR = x.AVATAR,
                IS_ADMIN = x.IS_ADMIN,
                ALLOWED = x.ALLOWED,
                MA_CONG_TY = x.MA_CONG_TY,
            }).ToList();
            return result;
        }

        // GET: api/Api_NguoidungHL/5
        [ResponseType(typeof(HT_NGUOI_DUNG))]
        public IHttpActionResult GetHT_NGUOI_DUNG(string id)
        {
            HT_NGUOI_DUNG hT_NGUOI_DUNG = db.HT_NGUOI_DUNG.Find(id);
            if (hT_NGUOI_DUNG == null)
            {
                return NotFound();
            }

            return Ok(hT_NGUOI_DUNG);
        }

        // PUT: api/Api_NguoidungHL/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHT_NGUOI_DUNG(string id, HT_NGUOI_DUNG hT_NGUOI_DUNG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hT_NGUOI_DUNG.USERNAME)
            {
                return BadRequest();
            }
            var query = db.HT_NGUOI_DUNG.Where(x => x.USERNAME == id).FirstOrDefault();
                query.USERNAME = hT_NGUOI_DUNG.USERNAME;
                query.PASSWORD = hT_NGUOI_DUNG.PASSWORD;
                query.HO_VA_TEN = hT_NGUOI_DUNG.HO_VA_TEN;
                query.SDT = hT_NGUOI_DUNG.SDT;
                query.EMAIL = hT_NGUOI_DUNG.EMAIL;
                query.IS_ADMIN = hT_NGUOI_DUNG.IS_ADMIN;
                query.ALLOWED = hT_NGUOI_DUNG.ALLOWED;
                query.AVATAR = hT_NGUOI_DUNG.AVATAR;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HT_NGUOI_DUNGExists(id))
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

        // POST: api/Api_NguoidungHL
        [ResponseType(typeof(HT_NGUOI_DUNG))]
        public IHttpActionResult PostHT_NGUOI_DUNG(HT_NGUOI_DUNG hT_NGUOI_DUNG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HT_NGUOI_DUNG.Add(hT_NGUOI_DUNG);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HT_NGUOI_DUNGExists(hT_NGUOI_DUNG.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hT_NGUOI_DUNG.USERNAME }, hT_NGUOI_DUNG);
        }

        // DELETE: api/Api_NguoidungHL/5
        [ResponseType(typeof(HT_NGUOI_DUNG))]
        public IHttpActionResult DeleteHT_NGUOI_DUNG(string id)
        {
            HT_NGUOI_DUNG hT_NGUOI_DUNG = db.HT_NGUOI_DUNG.Find(id);
            if (hT_NGUOI_DUNG == null)
            {
                return NotFound();
            }

            db.HT_NGUOI_DUNG.Remove(hT_NGUOI_DUNG);
            db.SaveChanges();

            return Ok(hT_NGUOI_DUNG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HT_NGUOI_DUNGExists(string id)
        {
            return db.HT_NGUOI_DUNG.Count(e => e.USERNAME == id) > 0;
        }
    }
}