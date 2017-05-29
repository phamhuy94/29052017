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

namespace ERP.Web.Api.BaoGia
{
    public class Api_BaoGia_DonHangDuKienController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_BaoGia_DonHangDuKien
        [Route("api/Api_BaoGia_DonHangDuKien/BH_DON_HANG_DU_KIEN/{madk}")]
        public List<ThongTinDonDuKien> GetBH_DON_HANG_DU_KIEN(string madk)
        {
            var vData = (from t1 in db.BH_DON_HANG_DU_KIEN
                         join t2 in db.KH_LIEN_HE on t1.ID_LIEN_HE equals t2.ID_LIEN_HE
                         join t4 in db.KHs on t1.MA_KHACH_HANG equals t4.MA_KHACH_HANG
                         join t3 in db.HT_NGUOI_DUNG on t1.SALES_QUAN_LY equals t3.USERNAME
                         where t1.MA_DU_KIEN == madk
                         select new
                         {
                             t3.HO_VA_TEN,
                             t1.ID_LIEN_HE,
                             t4.TEN_CONG_TY,
                             t1.SALES_QUAN_LY,
                             t1.MA_KHACH_HANG,
                             t1.MA_DU_KIEN,
                             t2.NGUOI_LIEN_HE,
                         });
            var result = vData.ToList().Select(x => new ThongTinDonDuKien()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                ID_LIEN_HE = x.ID_LIEN_HE,
                NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
                TEN_CONG_TY = x.TEN_CONG_TY,
                MA_DU_KIEN = x.MA_DU_KIEN,
                MA_KHACH_HANG = x.MA_KHACH_HANG,
                SALES_QUAN_LY = x.SALES_QUAN_LY,
            }).ToList();
            return result;
        }

        //// GET: api/Api_BaoGia_DonHangDuKien/5
        //[ResponseType(typeof(BH_DON_HANG_DU_KIEN))]
        //public IHttpActionResult GetBH_DON_HANG_DU_KIEN(string id)
        //{
        //    BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
        //    if (bH_DON_HANG_DU_KIEN == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(bH_DON_HANG_DU_KIEN);
        //}

        // PUT: api/Api_BaoGia_DonHangDuKien/5
        [Route("api/Api_BaoGia_DonHangDuKien/PutBH_DON_HANG_DU_KIEN/{id}")]
        public IHttpActionResult PutBH_DON_HANG_DU_KIEN(string id, BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_DON_HANG_DU_KIEN.MA_DU_KIEN)
            {
                return BadRequest();
            }

            var baogia = db.BH_DON_HANG_DU_KIEN.Where(x => x.MA_DU_KIEN == id).FirstOrDefault();
            if (baogia != null)
            {
                baogia.THANH_CONG = bH_DON_HANG_DU_KIEN.THANH_CONG;
                baogia.THAT_BAI = bH_DON_HANG_DU_KIEN.THAT_BAI;
                baogia.LY_DO_THAT_BAI = bH_DON_HANG_DU_KIEN.LY_DO_THAT_BAI;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_DON_HANG_DU_KIENExists(id))
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

        // POST: api/Api_BaoGia_DonHangDuKien
        [ResponseType(typeof(BH_DON_HANG_DU_KIEN))]
        public IHttpActionResult PostBH_DON_HANG_DU_KIEN(BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_DON_HANG_DU_KIEN.Add(bH_DON_HANG_DU_KIEN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_DON_HANG_DU_KIENExists(bH_DON_HANG_DU_KIEN.MA_DU_KIEN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bH_DON_HANG_DU_KIEN.MA_DU_KIEN }, bH_DON_HANG_DU_KIEN);
        }

        // DELETE: api/Api_BaoGia_DonHangDuKien/5
        [ResponseType(typeof(BH_DON_HANG_DU_KIEN))]
        public IHttpActionResult DeleteBH_DON_HANG_DU_KIEN(string id)
        {
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            if (bH_DON_HANG_DU_KIEN == null)
            {
                return NotFound();
            }

            db.BH_DON_HANG_DU_KIEN.Remove(bH_DON_HANG_DU_KIEN);
            db.SaveChanges();

            return Ok(bH_DON_HANG_DU_KIEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_DON_HANG_DU_KIENExists(string id)
        {
            return db.BH_DON_HANG_DU_KIEN.Count(e => e.MA_DU_KIEN == id) > 0;
        }
    }
}