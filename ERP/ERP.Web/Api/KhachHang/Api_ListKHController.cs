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
    public class Api_ListKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ListKH
        public List<KH> GetKHs()
        {
            var vData = db.KHs;
            var result = vData.ToList().Select(x => new KH()
            {
                MA_KHACH_HANG = x.MA_KHACH_HANG,
                TEN_CONG_TY = x.TEN_CONG_TY,
                VAN_PHONG_GIAO_DICH = x.VAN_PHONG_GIAO_DICH,
                DIA_CHI_XUAT_HOA_DON = x.DIA_CHI_XUAT_HOA_DON,
                TINH = x.TINH,
                QUOC_GIA = x.QUOC_GIA,
                MST = x.MST,
                HOTLINE = x.HOTLINE,
                EMAIL = x.EMAIL,
                FAX = x.FAX,
                LOGO = x.LOGO,
                WEBSITE = x.WEBSITE,
                DIEU_KHOAN_THANH_TOAN = x.DIEU_KHOAN_THANH_TOAN,
                SO_NGAY_DUOC_NO = x.SO_NGAY_DUOC_NO,
                SO_NO_TOI_DA = x.SO_NO_TOI_DA,
                GHI_CHU = x.GHI_CHU,
                TRUC_THUOC = x.TRUC_THUOC,
            }).ToList();
            return result;
        }

        // GET: api/Api_ListKH/5
        [ResponseType(typeof(KH))]
        public IHttpActionResult GetKH(string id)
        {
            KH kH = db.KHs.Find(id);
            if (kH == null)
            {
                return NotFound();
            }

            return Ok(kH);
        }

        // PUT: api/Api_ListKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH(string id, KH kH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH.MA_KHACH_HANG)
            {
                return BadRequest();
            }

            db.Entry(kH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHExists(id))
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

        // POST: api/Api_ListKH
        [ResponseType(typeof(KH))]
        public IHttpActionResult PostKH(KH kH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KHs.Add(kH);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHExists(kH.MA_KHACH_HANG))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kH.MA_KHACH_HANG }, kH);
        }

        // DELETE: api/Api_ListKH/5
        [ResponseType(typeof(KH))]
        public IHttpActionResult DeleteKH(string id)
        {
            KH kH = db.KHs.Find(id);
            if (kH == null)
            {
                return NotFound();
            }

            db.KHs.Remove(kH);
            db.SaveChanges();

            return Ok(kH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHExists(string id)
        {
            return db.KHs.Count(e => e.MA_KHACH_HANG == id) > 0;
        }
    }
}