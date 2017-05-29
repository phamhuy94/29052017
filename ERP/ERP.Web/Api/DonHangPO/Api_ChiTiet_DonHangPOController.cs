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

namespace ERP.Web.Api.DonHangPO
{
    public class Api_ChiTiet_DonHangPOController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ChiTiet_DonHangPO
        public IQueryable<BH_CT_DON_HANG_PO> GetBH_CT_DON_HANG_PO()
        {
            return db.BH_CT_DON_HANG_PO;
        }

        // GET: api/Api_ChiTiet_DonHangPO/5
        [Route("api/Api_ChiTiet_DonHangPO/ThongtinChitiet/{masoPO}")]
        public List<GetAll_ChiTiet_DonHangPO_Result> ThongtinChitiet(string masoPO)
        {
            var query = db.Database.SqlQuery<GetAll_ChiTiet_DonHangPO_Result>("GetAll_ChiTiet_DonHangPO @masoPO", new SqlParameter("masoPO", masoPO));
            var result = query.ToList();
            return result;
        }

        // PUT: api/Api_ChiTiet_DonHangPO/5
        [Route("api/Api_ChiTiet_DonHangPO/PutBH_CT_DON_HANG_PO")]
        public async Task<IHttpActionResult> PutBH_CT_DON_HANG_PO([FromBody] List<ChiTietDonHangPO> bH_CT_DON_HANG_PO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != bH_CT_DON_HANG_PO.ID)
            //{
            //    return BadRequest();
            //}
            foreach (var item in bH_CT_DON_HANG_PO)
            {
                var donhangPO = db.BH_CT_DON_HANG_PO.Where(x => x.ID == item.ID).FirstOrDefault();
                if (donhangPO != null)
                {
                    donhangPO.MA_HANG = item.MA_HANG;
                    donhangPO.SO_LUONG = item.SO_LUONG;
                    donhangPO.DON_GIA = item.DON_GIA;
                    donhangPO.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                    donhangPO.DVT = item.DVT;
                    donhangPO.THANH_TIEN_HANG = item.THANH_TIEN_HANG;
                    donhangPO.THUE_GTGT = item.THUE_GTGT;
                    donhangPO.TIEN_THUE_GTGT = item.TIEN_THUE_GTGT;
                    donhangPO.TIEN_THANH_TOAN = item.TIEN_THANH_TOAN;
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
            return Ok(bH_CT_DON_HANG_PO);
        }

        [Route("api/Api_ChiTiet_DonHangPO/SuaDatHang")]
        public async Task<IHttpActionResult> SuaDatHang([FromBody] List<ChiTietDonHangPO> bH_CT_DON_HANG_PO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != bH_CT_DON_HANG_PO.ID)
            //{
            //    return BadRequest();
            //}
            foreach (var item in bH_CT_DON_HANG_PO)
            {
                var donhangPO = db.MH_HANG_CAN_DAT.Where(x => x.ID == item.ID).FirstOrDefault();
                if (donhangPO != null)
                {
                    donhangPO.DA_LEN_PO_MUA_HANG = true;
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
            return Ok(bH_CT_DON_HANG_PO);
        }

        // POST: api/Api_ChiTiet_DonHangPO
        [ResponseType(typeof(BH_CT_DON_HANG_PO))]
        public IHttpActionResult PostBH_CT_DON_HANG_PO(BH_CT_DON_HANG_PO bH_CT_DON_HANG_PO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_CT_DON_HANG_PO.Add(bH_CT_DON_HANG_PO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bH_CT_DON_HANG_PO.ID }, bH_CT_DON_HANG_PO);
        }

        // DELETE: api/Api_ChiTiet_DonHangPO/5
        [ResponseType(typeof(BH_CT_DON_HANG_PO))]
        public IHttpActionResult DeleteBH_CT_DON_HANG_PO(int id)
        {
            BH_CT_DON_HANG_PO bH_CT_DON_HANG_PO = db.BH_CT_DON_HANG_PO.Find(id);
            if (bH_CT_DON_HANG_PO == null)
            {
                return NotFound();
            }

            db.BH_CT_DON_HANG_PO.Remove(bH_CT_DON_HANG_PO);
            db.SaveChanges();

            return Ok(bH_CT_DON_HANG_PO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_CT_DON_HANG_POExists(int id)
        {
            return db.BH_CT_DON_HANG_PO.Count(e => e.ID == id) > 0;
        }
    }
}