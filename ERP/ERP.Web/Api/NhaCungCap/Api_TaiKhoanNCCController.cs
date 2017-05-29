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
using System.Threading.Tasks;

namespace ERP.Web.Api.NhaCungCap
{
    public class Api_TaiKhoanNCCController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_TaiKhoanNCC
        [Route("api/Api_TaiKhoanNCC/{mancc}")]
        public List<TaiKhoanNhacungcap> GetNCC_TK_NGAN_HANG(string mancc)
        {
            var vData = (from t1 in db.NCC_TK_NGAN_HANG
                         join t2 in db.DM_LOAI_TK_NGAN_HANG on t1.LOAI_TAI_KHOAN equals t2.MA_LOAI
                         where t1.MA_NHA_CUNG_CAP == mancc
                         select new
                         {
                             t1.MA_NHA_CUNG_CAP,
                             t1.SO_TAI_KHOAN,
                             t1.TEN_NGAN_HANG,
                             t1.TEN_TAI_KHOAN,
                             t1.CHI_NHANH,
                             t1.TINH_TP,
                             t1.GHI_CHU,
                             t1.LOAI_TAI_KHOAN,
                             t2.TEN_LOAI
                         });
            var result = vData.ToList().Select(x => new TaiKhoanNhacungcap()
            {
                SO_TAI_KHOAN = x.SO_TAI_KHOAN,
                MA_NHA_CUNG_CAP = x.MA_NHA_CUNG_CAP,
                TEN_TAI_KHOAN = x.TEN_TAI_KHOAN,
                TEN_NGAN_HANG = x.TEN_NGAN_HANG,
                CHI_NHANH = x.CHI_NHANH,
                TINH_TP = x.TINH_TP,
                GHI_CHU = x.GHI_CHU,
                LOAI_TAI_KHOAN = x.LOAI_TAI_KHOAN,
                TEN_LOAI = x.TEN_LOAI
            }).ToList();
            return result;
        }

        // GET: api/Api_TaiKhoanNCC/5
        [ResponseType(typeof(NCC_TK_NGAN_HANG))]
        public IHttpActionResult GetNCC_TK_NGAN_HANG()
        {
            NCC_TK_NGAN_HANG nCC_TK_NGAN_HANG = db.NCC_TK_NGAN_HANG.Find();
            if (nCC_TK_NGAN_HANG == null)
            {
                return NotFound();
            }

            return Ok(nCC_TK_NGAN_HANG);
        }

        // PUT: api/Api_TaiKhoanNCC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC_TK_NGAN_HANG(string id, NCC_TK_NGAN_HANG nCC_TK_NGAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nCC_TK_NGAN_HANG.SO_TAI_KHOAN)
            {
                return BadRequest();
            }

            db.Entry(nCC_TK_NGAN_HANG).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCC_TK_NGAN_HANGExists(id))
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

        // POST: api/Api_TaiKhoanNCC
        [ResponseType(typeof(NCC_TK_NGAN_HANG))]
        public IHttpActionResult PostNCC_TK_NGAN_HANG(NCC_TK_NGAN_HANG nCC_TK_NGAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NCC_TK_NGAN_HANG.Add(nCC_TK_NGAN_HANG);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NCC_TK_NGAN_HANGExists(nCC_TK_NGAN_HANG.SO_TAI_KHOAN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nCC_TK_NGAN_HANG.SO_TAI_KHOAN }, nCC_TK_NGAN_HANG);
        }


        [HttpPost]
        [Route("api/Api_TaiKhoanNCC/{mancc}")]
        public async Task<IHttpActionResult> PostMultiNCC_TK_NGAN_HANG(string mancc, [FromBody] List<NCC_TK_NGAN_HANG> qUY_CHI_TIET_PHIEU_CHI)
        {
            for (int i = 0; i < qUY_CHI_TIET_PHIEU_CHI.Count(); i++)
            {
                //nH_NTTKs[i].ID = (index + i + 1).ToString();
                db.NCC_TK_NGAN_HANG.Add(qUY_CHI_TIET_PHIEU_CHI[i]);
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

        // DELETE: api/Api_TaiKhoanNCC/5
        [ResponseType(typeof(NCC_TK_NGAN_HANG))]
        public IHttpActionResult DeleteNCC_TK_NGAN_HANG(string id)
        {
            NCC_TK_NGAN_HANG nCC_TK_NGAN_HANG = db.NCC_TK_NGAN_HANG.Find(id);
            if (nCC_TK_NGAN_HANG == null)
            {
                return NotFound();
            }

            db.NCC_TK_NGAN_HANG.Remove(nCC_TK_NGAN_HANG);
            db.SaveChanges();

            return Ok(nCC_TK_NGAN_HANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCC_TK_NGAN_HANGExists(string id)
        {
            return db.NCC_TK_NGAN_HANG.Count(e => e.SO_TAI_KHOAN == id) > 0;
        }
    }
}