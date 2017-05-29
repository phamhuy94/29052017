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

namespace ERP.Web.Api.MuaHang
{
    public class Api_XuLyYeuCauHoiGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_XuLyYeuCauHoiGia
        [HttpPost]
        [Route("api/Api_XuLyYeuCauHoiGia/GetMH_XL_YEU_CAU_HOI_GIA")]
        public List<Prod_MH_XuLyYeuCauHoiGia_Result> GetMH_XL_YEU_CAU_HOI_GIA(XuLyHoiHang item)
        {
            var query = db.Database.SqlQuery<Prod_MH_XuLyYeuCauHoiGia_Result>("Prod_MH_XuLyYeuCauHoiGia @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", item.IS_ADMIN), new SqlParameter("username", item.USERNAME));
            var data = query.ToList();
            return data;
        }

        [Route("api/Api_XuLyYeuCauHoiGia/GetNhatKyHoiGia/{id}/{macongty}")]
        [ResponseType(typeof(MH_XL_YEU_CAU_HOI_GIA))]
        public List<Prod_MH_NhatKyHoiGia_Result> GetNhatKyHoiGia(string id, string macongty)
        {
            var query = db.Database.SqlQuery<Prod_MH_NhatKyHoiGia_Result>("Prod_MH_NhatKyHoiGia @mahang, @tructhuoc", new SqlParameter("mahang", id), new SqlParameter("tructhuoc", macongty));
            var data = query.ToList();
            return data;
        }

        // PUT: api/Api_XuLyYeuCauHoiGia/5
        [Route("api/Api_XuLyYeuCauHoiGia/PutMH_YEU_CAU_HOI_GIA/{id}")]
        public IHttpActionResult PutMH_YEU_CAU_HOI_GIA(int id, MH_YEU_CAU_HOI_GIA mH_YEU_CAU_HOI_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mH_YEU_CAU_HOI_GIA.ID)
            {
                return BadRequest();
            }

            var xuly = db.MH_YEU_CAU_HOI_GIA.Where(x => x.ID == id).FirstOrDefault();
            if (xuly != null) {
                xuly.TRANG_THAI = mH_YEU_CAU_HOI_GIA.TRANG_THAI;
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Api_XuLyYeuCauHoiGia
        [ResponseType(typeof(MH_XL_YEU_CAU_HOI_GIA))]
        public IHttpActionResult PostMH_XL_YEU_CAU_HOI_GIA(MH_XL_YEU_CAU_HOI_GIA mH_XL_YEU_CAU_HOI_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MH_XL_YEU_CAU_HOI_GIA.Add(mH_XL_YEU_CAU_HOI_GIA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mH_XL_YEU_CAU_HOI_GIA.ID }, mH_XL_YEU_CAU_HOI_GIA);
        }

        // DELETE: api/Api_XuLyYeuCauHoiGia/5
        [ResponseType(typeof(MH_XL_YEU_CAU_HOI_GIA))]
        public IHttpActionResult DeleteMH_XL_YEU_CAU_HOI_GIA(int id)
        {
            MH_XL_YEU_CAU_HOI_GIA mH_XL_YEU_CAU_HOI_GIA = db.MH_XL_YEU_CAU_HOI_GIA.Find(id);
            if (mH_XL_YEU_CAU_HOI_GIA == null)
            {
                return NotFound();
            }

            db.MH_XL_YEU_CAU_HOI_GIA.Remove(mH_XL_YEU_CAU_HOI_GIA);
            db.SaveChanges();

            return Ok(mH_XL_YEU_CAU_HOI_GIA);
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
            return db.MH_XL_YEU_CAU_HOI_GIA.Count(e => e.ID == id) > 0;
        }
    }
}