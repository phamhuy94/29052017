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
    public class Api_DinhkhoantudongController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Dinhkhoantudong
       public List<DM_DINH_KHOAN_TU_DONG> GetDM_DINH_KHOAN_TU_DONG()
        {
            var vData = db.DM_DINH_KHOAN_TU_DONG;
            var result = vData.ToList().Select(x => new DM_DINH_KHOAN_TU_DONG()
            {
                ID = x.ID,
                MA_LOAI_CHUNG_TU = x.MA_LOAI_CHUNG_TU,
                MA_LY_DO = x.MA_LY_DO,
                TEN_LY_DO = x.TEN_LY_DO,
                TK_NO = x.TK_NO,
                TK_CO = x.TK_CO
            }).ToList();
            return result;
        }


        // GET: api/Api_Dinhkhoantudong/5
        [ResponseType(typeof(DM_DINH_KHOAN_TU_DONG))]
        public IHttpActionResult GetDM_DINH_KHOAN_TU_DONG(int id)
        {
            DM_DINH_KHOAN_TU_DONG dM_DINH_KHOAN_TU_DONG = db.DM_DINH_KHOAN_TU_DONG.Find(id);
            if (dM_DINH_KHOAN_TU_DONG == null)
            {
                return NotFound();
            }

            return Ok(dM_DINH_KHOAN_TU_DONG);
        }

        // PUT: api/Api_Dinhkhoantudong/5
        [ResponseType(typeof(void))]
        public void PutDM_DINH_KHOAN_TU_DONG(int id, DM_DINH_KHOAN_TU_DONG dM_DINH_KHOAN_TU_DONG)
        {
            var check = db.DM_DINH_KHOAN_TU_DONG.Where(x => x.ID == id);
            if(check.Count()>0)
            {
                var resultupdate = check.FirstOrDefault();
                resultupdate.MA_LY_DO = dM_DINH_KHOAN_TU_DONG.MA_LY_DO;
                resultupdate.TEN_LY_DO = dM_DINH_KHOAN_TU_DONG.TEN_LY_DO;
                resultupdate.TK_NO = dM_DINH_KHOAN_TU_DONG.TK_NO;
                resultupdate.TK_CO = dM_DINH_KHOAN_TU_DONG.TK_CO;
                db.SaveChanges();
            }
        }
            

        // POST: api/Api_Dinhkhoantudong
        [ResponseType(typeof(DM_DINH_KHOAN_TU_DONG))]
        public IHttpActionResult PostDM_DINH_KHOAN_TU_DONG(DM_DINH_KHOAN_TU_DONG dM_DINH_KHOAN_TU_DONG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_DINH_KHOAN_TU_DONG.Add(dM_DINH_KHOAN_TU_DONG);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dM_DINH_KHOAN_TU_DONG.ID }, dM_DINH_KHOAN_TU_DONG);
        }

        // DELETE: api/Api_Dinhkhoantudong/5
        [ResponseType(typeof(DM_DINH_KHOAN_TU_DONG))]
        public IHttpActionResult DeleteDM_DINH_KHOAN_TU_DONG(int id)
        {
            DM_DINH_KHOAN_TU_DONG dM_DINH_KHOAN_TU_DONG = db.DM_DINH_KHOAN_TU_DONG.Find(id);
            if (dM_DINH_KHOAN_TU_DONG == null)
            {
                return NotFound();
            }

            db.DM_DINH_KHOAN_TU_DONG.Remove(dM_DINH_KHOAN_TU_DONG);
            db.SaveChanges();

            return Ok(dM_DINH_KHOAN_TU_DONG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_DINH_KHOAN_TU_DONGExists(int id)
        {
            return db.DM_DINH_KHOAN_TU_DONG.Count(e => e.ID == id) > 0;
        }
    }
}