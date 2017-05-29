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
    public class Api_LoaichungtuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Loaichungtu
        public List<DM_LOAI_CHUNG_TU> GetDM_LOAI_CHUNG_TU()
        {
            var vData = db.DM_LOAI_CHUNG_TU;
            var result = vData.ToList().Select(x => new DM_LOAI_CHUNG_TU()
            {
                MA_LOAI_CHUNG_TU = x.MA_LOAI_CHUNG_TU,
                TEN_LOAI_CHUNG_TU = x.TEN_LOAI_CHUNG_TU,
            }).ToList();
            return result;
        }

        // GET: api/Api_Loaichungtu/5
        [ResponseType(typeof(DM_LOAI_CHUNG_TU))]
        public IHttpActionResult GetDM_LOAI_CHUNG_TU(string id)
        {
            DM_LOAI_CHUNG_TU dM_LOAI_CHUNG_TU = db.DM_LOAI_CHUNG_TU.Find(id);
            if (dM_LOAI_CHUNG_TU == null)
            {
                return NotFound();
            }

            return Ok(dM_LOAI_CHUNG_TU);
        }

        // PUT: api/Api_Loaichungtu/5
        [ResponseType(typeof(void))]
        public void PutDM_LOAI_CHUNG_TU(string id, DM_LOAI_CHUNG_TU dM_LOAI_CHUNG_TU)
        {
            var check = db.DM_LOAI_CHUNG_TU.Where(x => x.MA_LOAI_CHUNG_TU == id);
            if(check.Count()>0)
            {
                var resultupdate = check.FirstOrDefault();
                resultupdate.TEN_LOAI_CHUNG_TU = dM_LOAI_CHUNG_TU.TEN_LOAI_CHUNG_TU;
                db.SaveChanges();
            }
        }
            

        // POST: api/Api_Loaichungtu
        [ResponseType(typeof(DM_LOAI_CHUNG_TU))]
        public IHttpActionResult PostDM_LOAI_CHUNG_TU(DM_LOAI_CHUNG_TU dM_LOAI_CHUNG_TU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_LOAI_CHUNG_TU.Add(dM_LOAI_CHUNG_TU);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_LOAI_CHUNG_TUExists(dM_LOAI_CHUNG_TU.MA_LOAI_CHUNG_TU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_LOAI_CHUNG_TU.MA_LOAI_CHUNG_TU }, dM_LOAI_CHUNG_TU);
        }

        // DELETE: api/Api_Loaichungtu/5
        [ResponseType(typeof(DM_LOAI_CHUNG_TU))]
        public IHttpActionResult DeleteDM_LOAI_CHUNG_TU(string id)
        {
            DM_LOAI_CHUNG_TU dM_LOAI_CHUNG_TU = db.DM_LOAI_CHUNG_TU.Find(id);
            if (dM_LOAI_CHUNG_TU == null)
            {
                return NotFound();
            }

            db.DM_LOAI_CHUNG_TU.Remove(dM_LOAI_CHUNG_TU);
            db.SaveChanges();

            return Ok(dM_LOAI_CHUNG_TU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_LOAI_CHUNG_TUExists(string id)
        {
            return db.DM_LOAI_CHUNG_TU.Count(e => e.MA_LOAI_CHUNG_TU == id) > 0;
        }
    }
}