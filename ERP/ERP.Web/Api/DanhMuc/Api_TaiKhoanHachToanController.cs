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
    public class Api_TaiKhoanHachToanController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_TaiKhoanHachToan
        public List<DM_TAI_KHOAN_HACH_TOAN> GetDM_TAI_KHOAN_HACH_TOAN()
        {
            var vData = db.DM_TAI_KHOAN_HACH_TOAN;
            var result = vData.ToList().Select(x => new DM_TAI_KHOAN_HACH_TOAN()
            {
                SO_TK = x.SO_TK,
                TEN_TK = x.TEN_TK,
                TINH_CHAT = x.TINH_CHAT,
                TEN_TA = x.TEN_TA,
                TK_CAP_CHA = x.TK_CAP_CHA,
                DIEN_GIAI = x.DIEN_GIAI,
            }).ToList();
            return result;
        }

        // GET: api/Api_TaiKhoanHachToan/5
        [ResponseType(typeof(DM_TAI_KHOAN_HACH_TOAN))]
        public IHttpActionResult GetDM_TAI_KHOAN_HACH_TOAN(string id)
        {
            DM_TAI_KHOAN_HACH_TOAN dM_TAI_KHOAN_HACH_TOAN = db.DM_TAI_KHOAN_HACH_TOAN.Find(id);
            if (dM_TAI_KHOAN_HACH_TOAN == null)
            {
                return NotFound();
            }

            return Ok(dM_TAI_KHOAN_HACH_TOAN);
        }

        // PUT: api/Api_TaiKhoanHachToan/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDM_TAI_KHOAN_HACH_TOAN(string id, DM_TAI_KHOAN_HACH_TOAN dM_TAI_KHOAN_HACH_TOAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dM_TAI_KHOAN_HACH_TOAN.SO_TK)
            {
                return BadRequest();
            }

            db.Entry(dM_TAI_KHOAN_HACH_TOAN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DM_TAI_KHOAN_HACH_TOANExists(id))
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

        // POST: api/Api_TaiKhoanHachToan
        [ResponseType(typeof(DM_TAI_KHOAN_HACH_TOAN))]
        public IHttpActionResult PostDM_TAI_KHOAN_HACH_TOAN(DM_TAI_KHOAN_HACH_TOAN dM_TAI_KHOAN_HACH_TOAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_TAI_KHOAN_HACH_TOAN.Add(dM_TAI_KHOAN_HACH_TOAN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_TAI_KHOAN_HACH_TOANExists(dM_TAI_KHOAN_HACH_TOAN.SO_TK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_TAI_KHOAN_HACH_TOAN.SO_TK }, dM_TAI_KHOAN_HACH_TOAN);
        }

        // DELETE: api/Api_TaiKhoanHachToan/5
        [ResponseType(typeof(DM_TAI_KHOAN_HACH_TOAN))]
        public IHttpActionResult DeleteDM_TAI_KHOAN_HACH_TOAN(string id)
        {
            DM_TAI_KHOAN_HACH_TOAN dM_TAI_KHOAN_HACH_TOAN = db.DM_TAI_KHOAN_HACH_TOAN.Find(id);
            if (dM_TAI_KHOAN_HACH_TOAN == null)
            {
                return NotFound();
            }

            db.DM_TAI_KHOAN_HACH_TOAN.Remove(dM_TAI_KHOAN_HACH_TOAN);
            db.SaveChanges();

            return Ok(dM_TAI_KHOAN_HACH_TOAN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_TAI_KHOAN_HACH_TOANExists(string id)
        {
            return db.DM_TAI_KHOAN_HACH_TOAN.Count(e => e.SO_TK == id) > 0;
        }
    }
}