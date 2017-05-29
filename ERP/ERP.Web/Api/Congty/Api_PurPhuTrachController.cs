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
using ERP.Web.Models;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.Congty
{
    public class Api_PurPhuTrachController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_PurPhuTrach
        public List<NCC_PUR_PHU_TRACH> GetNCC_PUR_PHU_TRACH()
        {
            var vData = db.NCC_PUR_PHU_TRACH;
            var result = vData.ToList().Select(x => new NCC_PUR_PHU_TRACH()
            {
                ID = x.ID,
                ID_LIEN_HE = x.ID_LIEN_HE,
                PUR_PHU_TRACH = x.PUR_PHU_TRACH,
                NGAY_BAT_DAU_PHU_TRACH = x.NGAY_BAT_DAU_PHU_TRACH,
                NGAY_KET_THUC_PHU_TRACH = x.NGAY_KET_THUC_PHU_TRACH,
                TRANG_THAI = x.TRANG_THAI
            }).ToList();
            return result;
        }

        // GET: api/Api_PurPhuTrach/5
        [ResponseType(typeof(NCC_PUR_PHU_TRACH))]
        public IHttpActionResult GetNCC_PUR_PHU_TRACH(int id)
        {
            NCC_PUR_PHU_TRACH nCC_PUR_PHU_TRACH = db.NCC_PUR_PHU_TRACH.Find(id);
            if (nCC_PUR_PHU_TRACH == null)
            {
                return NotFound();
            }

            return Ok(nCC_PUR_PHU_TRACH);
        }

        // PUT: api/Api_PurPhuTrach/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC_PUR_PHU_TRACH(int id, PurPhuTrach pur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pur.ID)
            {
                return BadRequest();
            }
            var nv = db.NCC_PUR_PHU_TRACH.Where(x => x.ID == id).FirstOrDefault();

            nv.ID_LIEN_HE = pur.ID_LIEN_HE;
            nv.PUR_PHU_TRACH = pur.PUR_PHU_TRACH;
            if (pur.NGAY_KET_THUC_PHU_TRACH != null)
                nv.NGAY_KET_THUC_PHU_TRACH = xlnt.Xulydatetime(pur.NGAY_KET_THUC_PHU_TRACH);
            if (pur.NGAY_BAT_DAU_PHU_TRACH != null)
                nv.NGAY_BAT_DAU_PHU_TRACH = xlnt.Xulydatetime(pur.NGAY_BAT_DAU_PHU_TRACH);
            nv.TRANG_THAI = pur.TRANG_THAI;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCC_PUR_PHU_TRACHExists(id))
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

        // POST: api/Api_PurPhuTrach
        [ResponseType(typeof(NCC_PUR_PHU_TRACH))]
        public IHttpActionResult PostNCC_PUR_PHU_TRACH(PurPhuTrach pur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NCC_PUR_PHU_TRACH nv = new NCC_PUR_PHU_TRACH();
            nv.ID_LIEN_HE = pur.ID_LIEN_HE;
            nv.PUR_PHU_TRACH = pur.PUR_PHU_TRACH;
            if (pur.NGAY_BAT_DAU_PHU_TRACH != null)
                nv.NGAY_BAT_DAU_PHU_TRACH = xlnt.Xulydatetime(pur.NGAY_BAT_DAU_PHU_TRACH);
            if (pur.NGAY_KET_THUC_PHU_TRACH != null)
                nv.NGAY_KET_THUC_PHU_TRACH = xlnt.Xulydatetime(pur.NGAY_KET_THUC_PHU_TRACH);
            nv.TRANG_THAI = pur.TRANG_THAI;

            db.NCC_PUR_PHU_TRACH.Add(nv);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NCC_PUR_PHU_TRACHExists(pur.ID_LIEN_HE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nv.ID_LIEN_HE }, nv);
        }

        // DELETE: api/Api_PurPhuTrach/5
        [ResponseType(typeof(NCC_PUR_PHU_TRACH))]
        public IHttpActionResult DeleteNCC_PUR_PHU_TRACH(int id)
        {
            NCC_PUR_PHU_TRACH nCC_PUR_PHU_TRACH = db.NCC_PUR_PHU_TRACH.Find(id);
            if (nCC_PUR_PHU_TRACH == null)
            {
                return NotFound();
            }

            db.NCC_PUR_PHU_TRACH.Remove(nCC_PUR_PHU_TRACH);
            db.SaveChanges();

            return Ok(nCC_PUR_PHU_TRACH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCC_PUR_PHU_TRACHExists(int id)
        {
            return db.NCC_PUR_PHU_TRACH.Count(e => e.ID == id) > 0;
        }
    }
}