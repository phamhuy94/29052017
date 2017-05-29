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
    public class Api_LoaiTaiKhoanController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LoaiTaiKhoan
        public List<DM_LOAI_TK_NGAN_HANG> GetDM_LOAI_TK_NGAN_HANG()
        {
            var vData = db.DM_LOAI_TK_NGAN_HANG;
            var result = vData.ToList().Select(x => new DM_LOAI_TK_NGAN_HANG()
            {
                MA_LOAI = x.MA_LOAI,
                TEN_LOAI = x.TEN_LOAI,
            }).ToList();
            return result;
        }

        // GET: api/Api_LoaiTaiKhoan/5
        [ResponseType(typeof(DM_LOAI_TK_NGAN_HANG))]
        public IHttpActionResult GetDM_LOAI_TK_NGAN_HANG(string id)
        {
            DM_LOAI_TK_NGAN_HANG dM_LOAI_TK_NGAN_HANG = db.DM_LOAI_TK_NGAN_HANG.Find(id);
            if (dM_LOAI_TK_NGAN_HANG == null)
            {
                return NotFound();
            }

            return Ok(dM_LOAI_TK_NGAN_HANG);
        }

        // PUT: api/Api_LoaiTaiKhoan/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDM_LOAI_TK_NGAN_HANG(string id, DM_LOAI_TK_NGAN_HANG dM_LOAI_TK_NGAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dM_LOAI_TK_NGAN_HANG.MA_LOAI)
            {
                return BadRequest();
            }

            db.Entry(dM_LOAI_TK_NGAN_HANG).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DM_LOAI_TK_NGAN_HANGExists(id))
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

        // POST: api/Api_LoaiTaiKhoan
        [ResponseType(typeof(DM_LOAI_TK_NGAN_HANG))]
        public IHttpActionResult PostDM_LOAI_TK_NGAN_HANG(DM_LOAI_TK_NGAN_HANG dM_LOAI_TK_NGAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_LOAI_TK_NGAN_HANG.Add(dM_LOAI_TK_NGAN_HANG);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_LOAI_TK_NGAN_HANGExists(dM_LOAI_TK_NGAN_HANG.MA_LOAI))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_LOAI_TK_NGAN_HANG.MA_LOAI }, dM_LOAI_TK_NGAN_HANG);
        }

        // DELETE: api/Api_LoaiTaiKhoan/5
        [ResponseType(typeof(DM_LOAI_TK_NGAN_HANG))]
        public IHttpActionResult DeleteDM_LOAI_TK_NGAN_HANG(string id)
        {
            DM_LOAI_TK_NGAN_HANG dM_LOAI_TK_NGAN_HANG = db.DM_LOAI_TK_NGAN_HANG.Find(id);
            if (dM_LOAI_TK_NGAN_HANG == null)
            {
                return NotFound();
            }

            db.DM_LOAI_TK_NGAN_HANG.Remove(dM_LOAI_TK_NGAN_HANG);
            db.SaveChanges();

            return Ok(dM_LOAI_TK_NGAN_HANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_LOAI_TK_NGAN_HANGExists(string id)
        {
            return db.DM_LOAI_TK_NGAN_HANG.Count(e => e.MA_LOAI == id) > 0;
        }
    }
}