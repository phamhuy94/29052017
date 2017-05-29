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
    public class Api_LoaidoituongController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Loaidoituong
        public List<DM_LOAI_DOI_TUONG> GetDM_LOAI_DOI_TUONG()
        {
            var vData = db.DM_LOAI_DOI_TUONG;
            var result = vData.ToList().Select(x => new DM_LOAI_DOI_TUONG()
            {
                MA_LOAI_DOI_TUONG = x.MA_LOAI_DOI_TUONG,
                TEN_LOAI_DOI_TUONG = x.TEN_LOAI_DOI_TUONG,

            }).ToList();
            return result;
        }

        // GET: api/Api_Loaidoituong/5
        [ResponseType(typeof(DM_LOAI_DOI_TUONG))]
        public IHttpActionResult GetDM_LOAI_DOI_TUONG(string id)
        {
            DM_LOAI_DOI_TUONG dM_LOAI_DOI_TUONG = db.DM_LOAI_DOI_TUONG.Find(id);
            if (dM_LOAI_DOI_TUONG == null)
            {
                return NotFound();
            }

            return Ok(dM_LOAI_DOI_TUONG);
        }

        // PUT: api/Api_Loaidoituong/5
        [ResponseType(typeof(void))]
        public void PutDM_LOAI_DOI_TUONG(string id, DM_LOAI_DOI_TUONG dM_LOAI_DOI_TUONG)
        {
            var check = db.DM_LOAI_DOI_TUONG.Where(x => x.MA_LOAI_DOI_TUONG == id);
            if(check.Count()>0)
            {
                var resultupdate = check.FirstOrDefault();
                resultupdate.TEN_LOAI_DOI_TUONG = dM_LOAI_DOI_TUONG.TEN_LOAI_DOI_TUONG;
                db.SaveChanges();
            }
        }
           

        // POST: api/Api_Loaidoituong
        [ResponseType(typeof(DM_LOAI_DOI_TUONG))]
        public IHttpActionResult PostDM_LOAI_DOI_TUONG(DM_LOAI_DOI_TUONG dM_LOAI_DOI_TUONG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_LOAI_DOI_TUONG.Add(dM_LOAI_DOI_TUONG);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_LOAI_DOI_TUONGExists(dM_LOAI_DOI_TUONG.MA_LOAI_DOI_TUONG))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_LOAI_DOI_TUONG.MA_LOAI_DOI_TUONG }, dM_LOAI_DOI_TUONG);
        }

        // DELETE: api/Api_Loaidoituong/5
        [ResponseType(typeof(DM_LOAI_DOI_TUONG))]
        public IHttpActionResult DeleteDM_LOAI_DOI_TUONG(string id)
        {
            DM_LOAI_DOI_TUONG dM_LOAI_DOI_TUONG = db.DM_LOAI_DOI_TUONG.Find(id);
            if (dM_LOAI_DOI_TUONG == null)
            {
                return NotFound();
            }

            db.DM_LOAI_DOI_TUONG.Remove(dM_LOAI_DOI_TUONG);
            db.SaveChanges();

            return Ok(dM_LOAI_DOI_TUONG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_LOAI_DOI_TUONGExists(string id)
        {
            return db.DM_LOAI_DOI_TUONG.Count(e => e.MA_LOAI_DOI_TUONG == id) > 0;
        }
    }
}