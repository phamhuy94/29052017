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

namespace ERP.Web.Api.MuaHang
{
    public class Api_YeuCauHoiGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_YeuCauHoiGia
        [Route("api/Api_YeuCauHoiGia/GetMH_YEU_CAU_HOI_GIA")]
        public List<Prod_MH_YeuCauHoiHang_Result> GetMH_YEU_CAU_HOI_GIA()
        {
            var query = db.Database.SqlQuery<Prod_MH_YeuCauHoiHang_Result>("Prod_MH_YeuCauHoiHang");
            var data = query.ToList();
            return data;
        }

        // GET: api/Api_YeuCauHoiGia/5
        [ResponseType(typeof(MH_YEU_CAU_HOI_GIA))]
        public IHttpActionResult GetMH_YEU_CAU_HOI_GIA(int id)
        {
            MH_YEU_CAU_HOI_GIA mH_YEU_CAU_HOI_GIA = db.MH_YEU_CAU_HOI_GIA.Find(id);
            if (mH_YEU_CAU_HOI_GIA == null)
            {
                return NotFound();
            }

            return Ok(mH_YEU_CAU_HOI_GIA);
        }

        // PUT: api/Api_YeuCauHoiGia/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMH_YEU_CAU_HOI_GIA(int id, MH_YEU_CAU_HOI_GIA ychg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ychg.ID)
            {
                return BadRequest();
            }

            var query = db.MH_YEU_CAU_HOI_GIA.Where(x => x.ID == id).FirstOrDefault();
            if(query!= null)
            {
                query.PUR_XU_LY = ychg.PUR_XU_LY;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MH_YEU_CAU_HOI_GIAExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ychg);
        }

        // POST: api/Api_YeuCauHoiGia
        [ResponseType(typeof(MH_YEU_CAU_HOI_GIA))]
        public IHttpActionResult PostMH_YEU_CAU_HOI_GIA(MH_YEU_CAU_HOI_GIA yeucau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            MH_YEU_CAU_HOI_GIA YCHG = new MH_YEU_CAU_HOI_GIA();
            YCHG.MA_HANG = yeucau.MA_HANG;
            YCHG.MA_CHUAN = yeucau.MA_CHUAN;
            YCHG.MA_KHACH_ORDER = yeucau.MA_KHACH_ORDER;
            YCHG.THONG_SO = yeucau.THONG_SO;
            YCHG.HANG = yeucau.HANG;
            YCHG.SALES_YEU_CAU = yeucau.SALES_YEU_CAU;
            YCHG.SO_LUONG = yeucau.SO_LUONG;
            YCHG.NGAY_HOI_GIA = DateTime.Today.Date ;
            YCHG.GHI_CHU = yeucau.GHI_CHU;
            YCHG.TRANG_THAI = false;
            YCHG.TRUC_THUOC = yeucau.TRUC_THUOC;
            db.MH_YEU_CAU_HOI_GIA.Add(YCHG);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = YCHG.ID }, YCHG);
        }

        // DELETE: api/Api_YeuCauHoiGia/5
        [ResponseType(typeof(MH_YEU_CAU_HOI_GIA))]
        public IHttpActionResult DeleteMH_YEU_CAU_HOI_GIA(int id)
        {
            MH_YEU_CAU_HOI_GIA mH_YEU_CAU_HOI_GIA = db.MH_YEU_CAU_HOI_GIA.Find(id);
            if (mH_YEU_CAU_HOI_GIA == null)
            {
                return NotFound();
            }

            db.MH_YEU_CAU_HOI_GIA.Remove(mH_YEU_CAU_HOI_GIA);
            db.SaveChanges();

            return Ok(mH_YEU_CAU_HOI_GIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MH_YEU_CAU_HOI_GIAExists(int id)
        {
            return db.MH_YEU_CAU_HOI_GIA.Count(e => e.ID == id) > 0;
        }
    }
}