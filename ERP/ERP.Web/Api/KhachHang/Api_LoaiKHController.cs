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
    public class Api_LoaiKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LoaiKH
        public List<KH_LOAI> GetKH_LOAI()
        {
            var vData = db.KH_LOAI;
            var result = vData.ToList().Select(x => new KH_LOAI()
            {
                MA_LOAI_KHACH = x.MA_LOAI_KHACH,
                TEN_LOAI_KHACH = x.TEN_LOAI_KHACH,
                DIEN_GIAI = x.DIEN_GIAI,
                MA_LOAI_KHACH_CHA = x.MA_LOAI_KHACH_CHA,
            }).ToList();
            return result;
        }

        // GET: api/Api_LoaiKH/5
        [ResponseType(typeof(KH_LOAI))]
        public IHttpActionResult GetKH_LOAI(string id)
        {
            KH_LOAI kH_LOAI = db.KH_LOAI.Find(id);
            if (kH_LOAI == null)
            {
                return NotFound();
            }

            return Ok(kH_LOAI);
        }

        // PUT: api/Api_LoaiKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_LOAI(string id, KH_LOAI kH_LOAI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH_LOAI.MA_LOAI_KHACH)
            {
                return BadRequest();
            }

            db.Entry(kH_LOAI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_LOAIExists(id))
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

        // POST: api/Api_LoaiKH
        [ResponseType(typeof(KH_LOAI))]
        public IHttpActionResult PostKH_LOAI(KH_LOAI kH_LOAI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KH_LOAI.Add(kH_LOAI);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KH_LOAIExists(kH_LOAI.MA_LOAI_KHACH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kH_LOAI.MA_LOAI_KHACH }, kH_LOAI);
        }

        // DELETE: api/Api_LoaiKH/5
        [ResponseType(typeof(KH_LOAI))]
        public IHttpActionResult DeleteKH_LOAI(string id)
        {
            KH_LOAI kH_LOAI = db.KH_LOAI.Find(id);
            if (kH_LOAI == null)
            {
                return NotFound();
            }

            db.KH_LOAI.Remove(kH_LOAI);
            db.SaveChanges();

            return Ok(kH_LOAI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_LOAIExists(string id)
        {
            return db.KH_LOAI.Count(e => e.MA_LOAI_KHACH == id) > 0;
        }
    }
}