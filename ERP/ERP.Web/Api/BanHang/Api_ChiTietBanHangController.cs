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
using System.Data.SqlClient;
using ERP.Web.Models.NewModels;
using System.Threading.Tasks;

namespace ERP.Web.Api.BanHang
{
    public class Api_ChiTietBanHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ChiTietBanHang
        public IQueryable<BH_CT_DON_BAN_HANG> GetBH_CT_DON_BAN_HANG()
        {
            return db.BH_CT_DON_BAN_HANG;
        }

        // GET: api/Api_ChiTietBanHang/5
        [HttpPost]
        [Route("api/Api_ChiTietBanHang/ThongtinChitiet/{masoBH}")]
        public List<GetAll_ChiTiet_DonBanHang_Result> ThongtinChitiet(string masoBH)
        {
            var query = db.Database.SqlQuery<GetAll_ChiTiet_DonBanHang_Result>("GetAll_ChiTiet_DonBanHang @masoBH", new SqlParameter("masoBH", masoBH));
            var result = query.ToList();
            return result;
        }

        // PUT: api/Api_ChiTietBanHang/5
        [ResponseType(typeof(void))]
        [Route("api/Api_ChiTietBanHang/PUTBH_CT_BAN_HANG")]
        public async Task<IHttpActionResult> PUTBH_CT_BAN_HANG([FromBody] List<ChiTietDonBanHang> chitietbanhang)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != bH_CT_DON_HANG_PO.ID)
            //{
            //    return BadRequest();
            //}
            foreach (var item in chitietbanhang)
            {
                var banhang = db.BH_CT_DON_BAN_HANG.Where(x => x.ID == item.ID).FirstOrDefault();
                if (banhang != null)
                {
                    banhang.MA_HANG = item.MA_HANG;
                    banhang.SO_LUONG = item.SO_LUONG;
                    banhang.DON_GIA = item.DON_GIA;
                    banhang.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                    banhang.DVT = item.DVT;
                    banhang.THANH_TIEN_HANG = item.THANH_TIEN_HANG;
                    banhang.THUE_GTGT = item.THUE_GTGT;
                    banhang.TIEN_THUE_GTGT = item.TIEN_THUE_GTGT;
                    banhang.TIEN_THANH_TOAN = item.TIEN_THANH_TOAN;
                    banhang.DIEN_GIAI_THUE = item.DIEN_GIAI_THUE;
                    banhang.TK_THUE = item.TK_THUE;
                    banhang.TK_NO = item.TK_NO;
                    banhang.TK_CO = item.TK_CO;
                }
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw;

            }
            //return this.CreatedAtRoute("GetNH_NTTK", new { id = nH_NTTK.SO_CHUNG_TU }, nH_NTTK);
            return Ok(chitietbanhang);
        }

        // POST: api/Api_ChiTietBanHang
        [ResponseType(typeof(BH_CT_DON_BAN_HANG))]
        public IHttpActionResult PostBH_CT_DON_BAN_HANG(BH_CT_DON_BAN_HANG bH_CT_DON_BAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_CT_DON_BAN_HANG.Add(bH_CT_DON_BAN_HANG);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bH_CT_DON_BAN_HANG.ID }, bH_CT_DON_BAN_HANG);
        }

        // DELETE: api/Api_ChiTietBanHang/5
        [ResponseType(typeof(BH_CT_DON_BAN_HANG))]
        public IHttpActionResult DeleteBH_CT_DON_BAN_HANG(int id)
        {
            BH_CT_DON_BAN_HANG bH_CT_DON_BAN_HANG = db.BH_CT_DON_BAN_HANG.Find(id);
            if (bH_CT_DON_BAN_HANG == null)
            {
                return NotFound();
            }

            db.BH_CT_DON_BAN_HANG.Remove(bH_CT_DON_BAN_HANG);
            db.SaveChanges();

            return Ok(bH_CT_DON_BAN_HANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_CT_DON_BAN_HANGExists(int id)
        {
            return db.BH_CT_DON_BAN_HANG.Count(e => e.ID == id) > 0;
        }
    }
}