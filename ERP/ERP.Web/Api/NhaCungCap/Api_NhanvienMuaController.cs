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

namespace ERP.Web.Api.NhaCungCap
{
    public class Api_NhanvienMuaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NhanvienMua
        public List<nhanvien> GetCCTC_NHAN_VIEN()
        {
            var vData = (from t1 in db.HT_NGUOI_DUNG
                         join t2 in db.CCTC_NHAN_VIEN on t1.USERNAME equals t2.USERNAME
                         where t2.MA_PHONG_BAN == "PURC_HL"
                         select new
                         {
                             t1.USERNAME,
                             t1.HO_VA_TEN,
                         });
            var result = vData.ToList().Select(x => new nhanvien()
            {
                USERNAME = x.USERNAME,
                HO_VA_TEN = x.HO_VA_TEN,
            }).ToList();
            return result;
        }

        // GET: api/Api_NhanvienMua/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult GetCCTC_NHAN_VIEN(string id)
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find(id);
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            return Ok(cCTC_NHAN_VIEN);
        }

        // PUT: api/Api_NhanvienMua/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_NHAN_VIEN(string id, CCTC_NHAN_VIEN cCTC_NHAN_VIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_NHAN_VIEN.USERNAME)
            {
                return BadRequest();
            }

            db.Entry(cCTC_NHAN_VIEN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_NHAN_VIENExists(id))
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

        // POST: api/Api_NhanvienMua
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult PostCCTC_NHAN_VIEN(CCTC_NHAN_VIEN cCTC_NHAN_VIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CCTC_NHAN_VIEN.Add(cCTC_NHAN_VIEN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_NHAN_VIENExists(cCTC_NHAN_VIEN.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cCTC_NHAN_VIEN.USERNAME }, cCTC_NHAN_VIEN);
        }

        // DELETE: api/Api_NhanvienMua/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult DeleteCCTC_NHAN_VIEN(string id)
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find(id);
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            db.CCTC_NHAN_VIEN.Remove(cCTC_NHAN_VIEN);
            db.SaveChanges();

            return Ok(cCTC_NHAN_VIEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_NHAN_VIENExists(string id)
        {
            return db.CCTC_NHAN_VIEN.Count(e => e.USERNAME == id) > 0;
        }
    }
}