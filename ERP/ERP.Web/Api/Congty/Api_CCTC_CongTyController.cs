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
using ERP.Web.Models;
using System.Data.SqlClient;

namespace ERP.Web.Api.Congty
{
    public class Api_CCTC_CongTyController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_CCTC_CongTy
        public List<CCTC_CONG_TY> GetCCTC_CONG_TY()
        {
            var vData = db.CCTC_CONG_TY;
            var result = vData.ToList().Select(x => new CCTC_CONG_TY()
            {
                MA_CONG_TY = x.MA_CONG_TY,
                TEN_CONG_TY = x.TEN_CONG_TY,
                NGAY_THANH_LAP = x.NGAY_THANH_LAP,
                EMAIL = x.EMAIL,
                FAX = x.FAX,
                SDT = x.SDT,
                MST = x.MST,
                LOGO = x.LOGO,
                DIA_CHI = x.DIA_CHI,
                DIA_CHI_XUAT_HOA_DON = x.DIA_CHI_XUAT_HOA_DON,
                CONG_TY_ME = x.CONG_TY_ME,
                CAP_TO_CHUC = x.CAP_TO_CHUC,
                GHI_CHU = x.GHI_CHU
            }).ToList();
            return result;
        }

        // GET: api/Api_CCTC_CongTy/5
        [Route("api/Api_CCTC_CongTy/DanhsachCongty")]
        public List<GetAll_CongTy_Result> DanhsachCongty()
        {
            var query = db.Database.SqlQuery<GetAll_CongTy_Result>("GetAll_CongTy");
            var result = query.ToList();
            return result;
        }

        [HttpPost]
        [Route("api/Api_CCTC_CongTy/Get_SALE_CHI_NHANH/{macongty}")]
        public List<Prod_CCTC_GetAllSale_Result> Get_SALE_CHI_NHANH(string macongty)
        {
            var query = db.Database.SqlQuery<Prod_CCTC_GetAllSale_Result>("Prod_CCTC_GetAllSale  @macongty", new SqlParameter("macongty", macongty));
            var result = query.ToList();
            return result;
        }

        // PUT: api/Api_CCTC_CongTy/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_CONG_TY(string id, CongTy CONGTY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CONGTY.MA_CONG_TY)
            {
                return BadRequest();
            }
            var nv = db.CCTC_CONG_TY.Where(x => x.MA_CONG_TY == id).FirstOrDefault();

            nv.MA_CONG_TY = CONGTY.MA_CONG_TY;
            nv.TEN_CONG_TY = CONGTY.TEN_CONG_TY;
            if (CONGTY.NGAY_THANH_LAP != null)
                nv.NGAY_THANH_LAP = xlnt.Xulydatetime(CONGTY.NGAY_THANH_LAP);
            nv.EMAIL = CONGTY.EMAIL;
            nv.FAX = CONGTY.FAX;
            nv.SDT = CONGTY.SDT;
            nv.MST = CONGTY.MST;
            nv.LOGO = CONGTY.LOGO;
            nv.DIA_CHI = CONGTY.DIA_CHI;
            nv.DIA_CHI_XUAT_HOA_DON = CONGTY.DIA_CHI_XUAT_HOA_DON;
            nv.CONG_TY_ME = CONGTY.CONG_TY_ME;
            nv.CAP_TO_CHUC = CONGTY.CAP_TO_CHUC;
            nv.GHI_CHU = CONGTY.GHI_CHU;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_CONG_TYExists(id))
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

        // POST: api/Api_CCTC_CongTy
        [ResponseType(typeof(CCTC_CONG_TY))]
        public IHttpActionResult PostCCTC_CONG_TY(CongTy CONGTY )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CCTC_CONG_TY nv = new CCTC_CONG_TY();
            nv.MA_CONG_TY = CONGTY.MA_CONG_TY;
            nv.TEN_CONG_TY = CONGTY.TEN_CONG_TY;
            if (CONGTY.NGAY_THANH_LAP != null)
                nv.NGAY_THANH_LAP = xlnt.Xulydatetime(CONGTY.NGAY_THANH_LAP);
            nv.EMAIL = CONGTY.EMAIL;
            nv.FAX = CONGTY.FAX;
            nv.SDT = CONGTY.SDT;
            nv.MST = CONGTY.MST;
            nv.LOGO = CONGTY.LOGO;
            nv.DIA_CHI = CONGTY.DIA_CHI;
            nv.DIA_CHI_XUAT_HOA_DON = CONGTY.DIA_CHI_XUAT_HOA_DON;
            nv.CONG_TY_ME = CONGTY.CONG_TY_ME;
            nv.CAP_TO_CHUC = CONGTY.CAP_TO_CHUC;
            nv.GHI_CHU = CONGTY.GHI_CHU;

            db.CCTC_CONG_TY.Add(nv);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_CONG_TYExists(CONGTY.MA_CONG_TY))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nv.MA_CONG_TY }, nv);
        }

        // DELETE: api/Api_CCTC_CongTy/5
        [ResponseType(typeof(CCTC_CONG_TY))]
        public IHttpActionResult DeleteCCTC_CONG_TY(string id)
        {
            CCTC_CONG_TY cCTC_CONG_TY = db.CCTC_CONG_TY.Find(id);
            if (cCTC_CONG_TY == null)
            {
                return NotFound();
            }

            db.CCTC_CONG_TY.Remove(cCTC_CONG_TY);
            db.SaveChanges();

            return Ok(cCTC_CONG_TY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_CONG_TYExists(string id)
        {
            return db.CCTC_CONG_TY.Count(e => e.MA_CONG_TY == id) > 0;
        }
    }
}