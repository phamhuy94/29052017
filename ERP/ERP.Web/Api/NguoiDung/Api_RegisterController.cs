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
using ERP.Web.Models.NewModels;
using ERP.Web.Models.BusinessModel;
using System.Net.Mail;

namespace ERP.Web.Api.NguoiDung
{
    public class Api_RegisterController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        RandomTextAndString rd = new RandomTextAndString();

        // GET: api/Api_Register
        public IQueryable<HT_NGUOI_DUNG> GetHT_NGUOI_DUNG()
        {
            return db.HT_NGUOI_DUNG;
        }

        // GET: api/Api_Register/5
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

        // PUT: api/Api_Register/5
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

            db.Entry(hT_NGUOI_DUNG).State = EntityState.Modified;

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


        // DELETE: api/Api_Register/5
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