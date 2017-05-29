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
    public class Api_XL_YeuCauHoiGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_XL_YeuCauHoiGia
        [Route("api/Api_XL_YeuCauHoiGia/GetMH_XL_YEU_CAU_HOI_GIA/{mahang}")]
        public List<Prod_MH_XL_YeuCauHoiHang_Result> GetMH_XL_YEU_CAU_HOI_GIA(string mahang)
        {
            var query = db.Database.SqlQuery<Prod_MH_XL_YeuCauHoiHang_Result>("Prod_MH_XL_YeuCauHoiHang @mahang", new SqlParameter("mahang", mahang));
            var data = query.ToList();
            return data;
        }

        // GET: api/Api_XL_YeuCauHoiGia/5
        [ResponseType(typeof(MH_XL_YEU_CAU_HOI_GIA))]
        public IHttpActionResult GetMH_XL_YEU_CAU_HOI_GIA(int id)
        {
            MH_XL_YEU_CAU_HOI_GIA mH_XL_YEU_CAU_HOI_GIA = db.MH_XL_YEU_CAU_HOI_GIA.Find(id);
            if (mH_XL_YEU_CAU_HOI_GIA == null)
            {
                return NotFound();
            }

            return Ok(mH_XL_YEU_CAU_HOI_GIA);
        }

        // PUT: api/Api_XL_YeuCauHoiGia/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMH_XL_YEU_CAU_HOI_GIA(int id, MH_XL_YEU_CAU_HOI_GIA mH_XL_YEU_CAU_HOI_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mH_XL_YEU_CAU_HOI_GIA.ID)
            {
                return BadRequest();
            }

            db.Entry(mH_XL_YEU_CAU_HOI_GIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MH_XL_YEU_CAU_HOI_GIAExists(id))
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

        // POST: api/Api_XL_YeuCauHoiGia
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

        // DELETE: api/Api_XL_YeuCauHoiGia/5
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

        private bool MH_XL_YEU_CAU_HOI_GIAExists(int id)
        {
            return db.MH_XL_YEU_CAU_HOI_GIA.Count(e => e.ID == id) > 0;
        }
    }
}