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
    public class Api_LichSuDangNhapController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LichSuDangNhap
        public List<HT_LICH_SU_DANG_NHAP> GetHT_LICH_SU_DANG_NHAP(string id)
        {
            var vData = db.HT_LICH_SU_DANG_NHAP.Where(x => x.USERNAME == id);
            var result = vData.ToList().Select(x => new HT_LICH_SU_DANG_NHAP()
            {
                USERNAME = x.USERNAME,
                THOI_GIAN_DANG_NHAP = x.THOI_GIAN_DANG_NHAP,
                THOI_GIAN_DANG_XUAT = x.THOI_GIAN_DANG_XUAT ,
            }).ToList();
            return result;
        }

        // GET: api/Api_LichSuDangNhap/5
        [ResponseType(typeof(HT_LICH_SU_DANG_NHAP))]
        public IHttpActionResult GetHT_LICH_SU_DANG_NHAP()
        {
            HT_LICH_SU_DANG_NHAP hT_LICH_SU_DANG_NHAP = db.HT_LICH_SU_DANG_NHAP.Find();
            if (hT_LICH_SU_DANG_NHAP == null)
            {
                return NotFound();
            }

            return Ok(hT_LICH_SU_DANG_NHAP);
        }

        // PUT: api/Api_LichSuDangNhap/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHT_LICH_SU_DANG_NHAP(int id, HT_LICH_SU_DANG_NHAP hT_LICH_SU_DANG_NHAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hT_LICH_SU_DANG_NHAP.ID)
            {
                return BadRequest();
            }

            db.Entry(hT_LICH_SU_DANG_NHAP).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HT_LICH_SU_DANG_NHAPExists(id))
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

        // POST: api/Api_LichSuDangNhap
        [ResponseType(typeof(HT_LICH_SU_DANG_NHAP))]
        public IHttpActionResult PostHT_LICH_SU_DANG_NHAP(HT_LICH_SU_DANG_NHAP hT_LICH_SU_DANG_NHAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HT_LICH_SU_DANG_NHAP.Add(hT_LICH_SU_DANG_NHAP);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hT_LICH_SU_DANG_NHAP.ID }, hT_LICH_SU_DANG_NHAP);
        }

        // DELETE: api/Api_LichSuDangNhap/5
        [ResponseType(typeof(HT_LICH_SU_DANG_NHAP))]
        public IHttpActionResult DeleteHT_LICH_SU_DANG_NHAP(int id)
        {
            HT_LICH_SU_DANG_NHAP hT_LICH_SU_DANG_NHAP = db.HT_LICH_SU_DANG_NHAP.Find(id);
            if (hT_LICH_SU_DANG_NHAP == null)
            {
                return NotFound();
            }

            db.HT_LICH_SU_DANG_NHAP.Remove(hT_LICH_SU_DANG_NHAP);
            db.SaveChanges();

            return Ok(hT_LICH_SU_DANG_NHAP);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HT_LICH_SU_DANG_NHAPExists(int id)
        {
            return db.HT_LICH_SU_DANG_NHAP.Count(e => e.ID == id) > 0;
        }
    }
}