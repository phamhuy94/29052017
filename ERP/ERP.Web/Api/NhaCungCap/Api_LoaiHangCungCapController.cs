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
using System.Threading.Tasks;
using ERP.Web.Models.NewModels;

namespace ERP.Web.Api.NhaCungCap
{
    public class Api_LoaiHangCungCapController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_LoaiHangCungCap
        public List<NCC_HL> GetNCC_LOAI_HANG_CUNG_CAP(string ma_ncc)
        {
            var vData = (from t1 in db.NCCs
                         join t2 in db.NCC_LOAI_HANG_CUNG_CAP on t1.MA_NHA_CUNG_CAP equals t2.MA_NHA_CUNG_CAP
                         join t3 in db.HH_NHOM_VTHH on t2.MA_NHOM_HANG equals t3.MA_NHOM_HANG_CHI_TIET
                         where t1.MA_NHA_CUNG_CAP == ma_ncc
                         select new
                         {
                             t3.CHUNG_LOAI_HANG,t2.MA_NHOM_HANG
                         });
            var result = vData.ToList().Select(x => new NCC_HL()
            {
                CHUNG_LOAI_HANG = x.CHUNG_LOAI_HANG,
                MA_NHOM_HANG = x.MA_NHOM_HANG,
            }).ToList();
            return result;
        }

        // GET: api/Api_LoaiHangCungCap/5
        [ResponseType(typeof(NCC_LOAI_HANG_CUNG_CAP))]
        public IHttpActionResult GetNCC_LOAI_HANG_CUNG_CAP()
        {
            NCC_LOAI_HANG_CUNG_CAP nCC_LOAI_HANG_CUNG_CAP = db.NCC_LOAI_HANG_CUNG_CAP.Find();
            if (nCC_LOAI_HANG_CUNG_CAP == null)
            {
                return NotFound();
            }

            return Ok(nCC_LOAI_HANG_CUNG_CAP);
        }

        // PUT: api/Api_LoaiHangCungCap/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC_LOAI_HANG_CUNG_CAP(int id, NCC_LOAI_HANG_CUNG_CAP nCC_LOAI_HANG_CUNG_CAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nCC_LOAI_HANG_CUNG_CAP.ID)
            {
                return BadRequest();
            }

            db.Entry(nCC_LOAI_HANG_CUNG_CAP).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCC_LOAI_HANG_CUNG_CAPExists(id))
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

        // POST: api/Api_LoaiHangCungCap
        //[ResponseType(typeof(NCC_LOAI_HANG_CUNG_CAP))]
        //public IHttpActionResult PostNCC_LOAI_HANG_CUNG_CAP(NCC_LOAI_HANG_CUNG_CAP nCC_LOAI_HANG_CUNG_CAP)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.NCC_LOAI_HANG_CUNG_CAP.Add(nCC_LOAI_HANG_CUNG_CAP);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = nCC_LOAI_HANG_CUNG_CAP.ID }, nCC_LOAI_HANG_CUNG_CAP);
        //}

        [HttpPost]
        [Route("api/Api_LoaiHangCungCap/{mancc}")]
        public async Task<IHttpActionResult> PostMultiNCC_LOAI_HANG_CUNG_CAP(string mancc, [FromBody] List<NCC_LOAI_HANG_CUNG_CAP> qUY_CHI_TIET_PHIEU_CHI)
        {
            for (int i = 0; i < qUY_CHI_TIET_PHIEU_CHI.Count(); i++)
            {
                //nH_NTTKs[i].ID = (index + i + 1).ToString();
                db.NCC_LOAI_HANG_CUNG_CAP.Add(qUY_CHI_TIET_PHIEU_CHI[i]);
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok(qUY_CHI_TIET_PHIEU_CHI);
        }

        // DELETE: api/Api_LoaiHangCungCap/5
        [ResponseType(typeof(NCC_LOAI_HANG_CUNG_CAP))]
        public IHttpActionResult DeleteNCC_LOAI_HANG_CUNG_CAP(int id)
        {
            NCC_LOAI_HANG_CUNG_CAP nCC_LOAI_HANG_CUNG_CAP = db.NCC_LOAI_HANG_CUNG_CAP.Find(id);
            if (nCC_LOAI_HANG_CUNG_CAP == null)
            {
                return NotFound();
            }

            db.NCC_LOAI_HANG_CUNG_CAP.Remove(nCC_LOAI_HANG_CUNG_CAP);
            db.SaveChanges();

            return Ok(nCC_LOAI_HANG_CUNG_CAP);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCC_LOAI_HANG_CUNG_CAPExists(int id)
        {
            return db.NCC_LOAI_HANG_CUNG_CAP.Count(e => e.ID == id) > 0;
        }
    }
}