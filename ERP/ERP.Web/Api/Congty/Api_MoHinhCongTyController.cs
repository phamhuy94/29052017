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
    public class Api_MoHinhCongTyController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_MoHinhCongTy
        public List<CCTC_MO_HINH_CONG_TY> GetCCTC_MO_HINH_CONG_TY()
        {
            var vData = db.CCTC_MO_HINH_CONG_TY;
            var result = vData.ToList().Select(x => new CCTC_MO_HINH_CONG_TY()
            {
                MA_MO_HINH = x.MA_MO_HINH,
                TEN_MO_HINH = x.TEN_MO_HINH,
                GHI_CHU = x.GHI_CHU
            }).ToList();
            return result;
        }

        // GET: api/Api_MoHinhCongTy/5
        [ResponseType(typeof(CCTC_MO_HINH_CONG_TY))]
        public IHttpActionResult GetCCTC_MO_HINH_CONG_TY(string id,string username)
        {
            CCTC_MO_HINH_CONG_TY cCTC_MO_HINH_CONG_TY = db.CCTC_MO_HINH_CONG_TY.Find(id);
            if (cCTC_MO_HINH_CONG_TY == null)
            {
                return NotFound();
            }

            return Ok(cCTC_MO_HINH_CONG_TY);
        }

        // PUT: api/Api_MoHinhCongTy/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_MO_HINH_CONG_TY(string id, CCTC_MO_HINH_CONG_TY cCTC_MO_HINH_CONG_TY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_MO_HINH_CONG_TY.MA_MO_HINH)
            {
                return BadRequest();
            }

            db.Entry(cCTC_MO_HINH_CONG_TY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_MO_HINH_CONG_TYExists(id))
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

        // POST: api/Api_MoHinhCongTy
        [ResponseType(typeof(CCTC_MO_HINH_CONG_TY))]
        public IHttpActionResult PostCCTC_MO_HINH_CONG_TY(CCTC_MO_HINH_CONG_TY cCTC_MO_HINH_CONG_TY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CCTC_MO_HINH_CONG_TY.Add(cCTC_MO_HINH_CONG_TY);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_MO_HINH_CONG_TYExists(cCTC_MO_HINH_CONG_TY.MA_MO_HINH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cCTC_MO_HINH_CONG_TY.MA_MO_HINH }, cCTC_MO_HINH_CONG_TY);
        }

        // DELETE: api/Api_MoHinhCongTy/5
        [ResponseType(typeof(CCTC_MO_HINH_CONG_TY))]
        public IHttpActionResult DeleteCCTC_MO_HINH_CONG_TY(string id)
        {
            CCTC_MO_HINH_CONG_TY cCTC_MO_HINH_CONG_TY = db.CCTC_MO_HINH_CONG_TY.Find(id);
            if (cCTC_MO_HINH_CONG_TY == null)
            {
                return NotFound();
            }

            db.CCTC_MO_HINH_CONG_TY.Remove(cCTC_MO_HINH_CONG_TY);
            db.SaveChanges();

            return Ok(cCTC_MO_HINH_CONG_TY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_MO_HINH_CONG_TYExists(string id)
        {
            return db.CCTC_MO_HINH_CONG_TY.Count(e => e.MA_MO_HINH == id) > 0;
        }
    }
}