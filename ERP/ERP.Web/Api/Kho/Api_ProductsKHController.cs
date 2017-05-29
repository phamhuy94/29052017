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
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.Kho
{
    public class Api_ProductsKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ProductsKH
        public List<HH> GetHH(string id)
        {
            var vData = db.HHs.Where(x => x.MA_HANG == id);
            var result = vData.ToList().Select(x => new HH()
            {
                MA_HANG = x.MA_HANG,
                TEN_HANG = x.TEN_HANG,
                MA_NHOM_HANG = x.MA_NHOM_HANG,
                DON_VI_TINH = x.DON_VI_TINH,
                KHOI_LUONG = x.KHOI_LUONG,
                XUAT_XU = x.XUAT_XU,
                GIA_LIST = x.GIA_LIST,
                BAO_HANH = x.BAO_HANH,
                THONG_SO_KY_THUAT = x.THONG_SO_KY_THUAT,
                QUY_CACH_DONG_GOI = x.QUY_CACH_DONG_GOI,
                HINH_ANH = x.HINH_ANH,
                GHI_CHU = x.GHI_CHU,
                TK_HACH_TOAN_KHO = x.TK_HACH_TOAN_KHO,
                TK_CHI_PHI = x.TK_CHI_PHI,
                TK_DOANH_THU = x.TK_DOANH_THU,
            }).ToList();
            return result;
        }

        // GET: api/Api_ProductsKH/5
        [ResponseType(typeof(HH))]
        

        // PUT: api/Api_ProductsKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHH(string id, HH hH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hH.MA_HANG)
            {
                return BadRequest();
            }

            db.Entry(hH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HHExists(id))
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

        // POST: api/Api_ProductsKH
        [ResponseType(typeof(HH))]
        public IHttpActionResult PostHH(HH hH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HHs.Add(hH);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HHExists(hH.MA_HANG))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hH.MA_HANG }, hH);
        }

        // DELETE: api/Api_ProductsKH/5
        [ResponseType(typeof(HH))]
        public IHttpActionResult DeleteHH(string id)
        {
            HH hH = db.HHs.Find(id);
            if (hH == null)
            {
                return NotFound();
            }

            db.HHs.Remove(hH);
            db.SaveChanges();

            return Ok(hH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HHExists(string id)
        {
            return db.HHs.Count(e => e.MA_HANG == id) > 0;
        }
    }
}