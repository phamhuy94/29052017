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
using System.Threading.Tasks;
using ERP.Web.Models.NewModels;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.MuaHang
{
    public class Api_LoadXuLyYeuCauHoiGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_LoadXuLyYeuCauHoiGia
        [HttpPost]
        [Route("api/Api_LoadXuLyYeuCauHoiGia/HoiGia/{id_YCHG}")]
        public List<Prod_MH_LoadXuLyYeuCauHoiGia_Result> HoiGia(int id_YCHG)
        {
            var query = db.Database.SqlQuery<Prod_MH_LoadXuLyYeuCauHoiGia_Result>("Prod_MH_LoadXuLyYeuCauHoiGia @id_YCHG", new SqlParameter("id_YCHG", id_YCHG));
            var data = query.ToList();
            return data;
        }

        // GET: api/Api_LoadXuLyYeuCauHoiGia/5
        [ResponseType(typeof(MH_YEU_CAU_HOI_GIA))]
        public IHttpActionResult GetMH_YEU_CAU_HOI_GIA()
        {
            MH_YEU_CAU_HOI_GIA mH_YEU_CAU_HOI_GIA = db.MH_YEU_CAU_HOI_GIA.Find();
            if (mH_YEU_CAU_HOI_GIA == null)
            {
                return NotFound();
            }

            return Ok(mH_YEU_CAU_HOI_GIA);
        }

        // PUT: api/Api_LoadXuLyYeuCauHoiGia/5
        [HttpPost]
        [Route("api/Api_LoadXuLyYeuCauHoiGia/XuLyHoiHang/{id}")]
        public async Task<IHttpActionResult> PutXuLyHoiHang(int id,[FromBody] List<XuLyHoiHang> bH_CT_BAO_GIA)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //try
            //{
            foreach (var item in bH_CT_BAO_GIA)
            {
                var baogia = db.MH_XL_YEU_CAU_HOI_GIA.Where(x => x.ID == item.ID).FirstOrDefault();
                if (baogia != null)
                {
                    baogia.MA_HANG = item.MA_HANG;
                    baogia.MA_KHACH_ORDER = item.MA_KHACH_ORDER;
                    baogia.SO_LUONG = item.SO_LUONG;
                    baogia.MA_NCC = item.MA_NCC;
                    baogia.GIA = item.GIA;
                    baogia.THOI_GIAN_CAP_HANG = item.THOI_GIAN_CAP_HANG;
                    if (item.NGAY_HOI_GIA != null)
                        baogia.NGAY_HOI_GIA = item.NGAY_HOI_GIA;
                    baogia.TRUC_THUOC = item.TRUC_THUOC;
                    baogia.PUR_XU_LY = item.PUR_XU_LY;
                    baogia.GHI_CHU = item.GHI_CHU;
                    db.SaveChanges();
                }
                else if (baogia == null)

                {
                    MH_XL_YEU_CAU_HOI_GIA newxuly = new MH_XL_YEU_CAU_HOI_GIA();
                    newxuly.ID_YEU_CAU_HOI_GIA = id;
                    newxuly.MA_HANG = item.MA_HANG;
                    newxuly.MA_KHACH_ORDER = item.MA_KHACH_ORDER;
                    newxuly.SO_LUONG = item.SO_LUONG;
                    newxuly.MA_NCC = item.MA_NCC;
                    newxuly.GIA = item.GIA;
                    newxuly.THOI_GIAN_CAP_HANG = item.THOI_GIAN_CAP_HANG;
                    newxuly.NGAY_HOI_GIA = DateTime.Today.Date;
                    newxuly.TRUC_THUOC = item.TRUC_THUOC;
                    newxuly.PUR_XU_LY = item.PUR_XU_LY;
                    newxuly.GHI_CHU = item.GHI_CHU;
                    db.MH_XL_YEU_CAU_HOI_GIA.Add(newxuly);
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
            return Ok(bH_CT_BAO_GIA);
        }

        // POST: api/Api_LoadXuLyYeuCauHoiGia
        [ResponseType(typeof(MH_YEU_CAU_HOI_GIA))]
        public IHttpActionResult PostMH_YEU_CAU_HOI_GIA(MH_YEU_CAU_HOI_GIA mH_YEU_CAU_HOI_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MH_YEU_CAU_HOI_GIA.Add(mH_YEU_CAU_HOI_GIA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mH_YEU_CAU_HOI_GIA.ID }, mH_YEU_CAU_HOI_GIA);
        }

        // DELETE: api/Api_LoadXuLyYeuCauHoiGia/5
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