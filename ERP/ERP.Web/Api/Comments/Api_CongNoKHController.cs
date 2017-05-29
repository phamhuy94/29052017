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

namespace ERP.Web.Api.Comments
{
    public class Api_CongNoKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_CongNoKH
        [Route("api/Api_CongNoKH/DanhSachTuanCongNo/{id}")]
        public List<KH_CONG_NO> GetDM_KHo(string id)
        {
            var vData = db.KH_CONG_NO.Where(x=>x.MA_KHACH_HANG == id);
            var result = vData.ToList().Select(x => new KH_CONG_NO()
            {
                TUAN_CONG_NO = x.TUAN_CONG_NO,
                
            }).ToList();
            return result;
        }

        


        // PUT: api/Api_CongNoKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_CONG_NO(int id, KH_CONG_NO kH_CONG_NO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH_CONG_NO.ID)
            {
                return BadRequest();
            }

            db.Entry(kH_CONG_NO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_CONG_NOExists(id))
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

        // POST: api/Api_CongNoKH
        [ResponseType(typeof(KH_CONG_NO))]
        public IHttpActionResult PostKH_CONG_NO(KH_CONG_NO kH_CONG_NO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KH_CONG_NO.Add(kH_CONG_NO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kH_CONG_NO.ID }, kH_CONG_NO);
        }

        // DELETE: api/Api_CongNoKH/5
        [ResponseType(typeof(KH_CONG_NO))]
        public IHttpActionResult DeleteKH_CONG_NO(int id)
        {
            KH_CONG_NO kH_CONG_NO = db.KH_CONG_NO.Find(id);
            if (kH_CONG_NO == null)
            {
                return NotFound();
            }

            db.KH_CONG_NO.Remove(kH_CONG_NO);
            db.SaveChanges();

            return Ok(kH_CONG_NO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_CONG_NOExists(int id)
        {
            return db.KH_CONG_NO.Count(e => e.ID == id) > 0;
        }
    }
}