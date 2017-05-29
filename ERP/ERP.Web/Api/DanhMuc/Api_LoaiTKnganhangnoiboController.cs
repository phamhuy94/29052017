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
    public class Api_LoaiTKnganhangnoiboController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LoaiTKnganhangnoibo
        public List<DM_TK_NGAN_HANG_NOI_BO> GetDM_TK_NGAN_HANG_NOI_BO()
        {
            var vData = db.DM_TK_NGAN_HANG_NOI_BO;
            var result = vData.ToList().Select(x => new DM_TK_NGAN_HANG_NOI_BO()
            {
                SO_TAI_KHOAN = x.SO_TAI_KHOAN,
                MA_CONG_TY = x.MA_CONG_TY,
                TEN_TAI_KHOAN = x.TEN_TAI_KHOAN,
                LOAI_TAI_KHOAN = x.LOAI_TAI_KHOAN,
                TEN_NGAN_HANG = x.TEN_NGAN_HANG,
                CHI_NHANH = x.CHI_NHANH,
                TINH_TP = x.TINH_TP,
                GHI_CHU = x.GHI_CHU,
            }).ToList();
            return result;
        }

        // GET: api/Api_LoaiTKnganhangnoibo/5
        [ResponseType(typeof(DM_TK_NGAN_HANG_NOI_BO))]
        public IHttpActionResult GetDM_TK_NGAN_HANG_NOI_BO(string id)
        {
            DM_TK_NGAN_HANG_NOI_BO dM_TK_NGAN_HANG_NOI_BO = db.DM_TK_NGAN_HANG_NOI_BO.Find(id);
            if (dM_TK_NGAN_HANG_NOI_BO == null)
            {
                return NotFound();
            }

            return Ok(dM_TK_NGAN_HANG_NOI_BO);
        }

        // PUT: api/Api_LoaiTKnganhangnoibo/5
        [ResponseType(typeof(void))]
        public void PutDM_TK_NGAN_HANG_NOI_BO(string id, DM_TK_NGAN_HANG_NOI_BO dM_TK_NGAN_HANG_NOI_BO)
        {
            var check = db.DM_TK_NGAN_HANG_NOI_BO.Where(x => x.SO_TAI_KHOAN == id);
            if (check.Count() > 0)
            {
                var resultupadate = check.FirstOrDefault();
                resultupadate.TEN_TAI_KHOAN = dM_TK_NGAN_HANG_NOI_BO.TEN_TAI_KHOAN;
                resultupadate.LOAI_TAI_KHOAN = dM_TK_NGAN_HANG_NOI_BO.LOAI_TAI_KHOAN;
                resultupadate.TEN_NGAN_HANG = dM_TK_NGAN_HANG_NOI_BO.TEN_NGAN_HANG;
                resultupadate.CHI_NHANH = dM_TK_NGAN_HANG_NOI_BO.CHI_NHANH;
                resultupadate.TINH_TP = dM_TK_NGAN_HANG_NOI_BO.TINH_TP;
                resultupadate.GHI_CHU = dM_TK_NGAN_HANG_NOI_BO.GHI_CHU;
                db.SaveChanges();
            }
        }
            

        // POST: api/Api_LoaiTKnganhangnoibo
        [ResponseType(typeof(DM_TK_NGAN_HANG_NOI_BO))]
        public IHttpActionResult PostDM_TK_NGAN_HANG_NOI_BO(DM_TK_NGAN_HANG_NOI_BO dM_TK_NGAN_HANG_NOI_BO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_TK_NGAN_HANG_NOI_BO.Add(dM_TK_NGAN_HANG_NOI_BO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_TK_NGAN_HANG_NOI_BOExists(dM_TK_NGAN_HANG_NOI_BO.SO_TAI_KHOAN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_TK_NGAN_HANG_NOI_BO.SO_TAI_KHOAN }, dM_TK_NGAN_HANG_NOI_BO);
        }

        // DELETE: api/Api_LoaiTKnganhangnoibo/5
        [ResponseType(typeof(DM_TK_NGAN_HANG_NOI_BO))]
        public IHttpActionResult DeleteDM_TK_NGAN_HANG_NOI_BO(string id)
        {
            DM_TK_NGAN_HANG_NOI_BO dM_TK_NGAN_HANG_NOI_BO = db.DM_TK_NGAN_HANG_NOI_BO.Find(id);
            if (dM_TK_NGAN_HANG_NOI_BO == null)
            {
                return NotFound();
            }

            db.DM_TK_NGAN_HANG_NOI_BO.Remove(dM_TK_NGAN_HANG_NOI_BO);
            db.SaveChanges();

            return Ok(dM_TK_NGAN_HANG_NOI_BO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_TK_NGAN_HANG_NOI_BOExists(string id)
        {
            return db.DM_TK_NGAN_HANG_NOI_BO.Count(e => e.SO_TAI_KHOAN == id) > 0;
        }
    }
}