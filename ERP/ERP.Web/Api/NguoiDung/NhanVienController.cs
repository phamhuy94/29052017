using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

using System.Web.Mvc;
using System.Data.SqlClient;

using ERP.Web.Models.Database;
using ERP.Api.Models.CreatedModel.Common;

namespace ERP.Api.Controllers.CCTC
{
    
    public class NhanVienController : ApiController
    {
        //  private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        #region "Get All Kinh Doanh"
        [System.Web.Http.Route("api/NhanVien/GetAllSale")]
        public List<Prod_CCTC_GetAllSale_Result> GetAllSale(TimKiem timkiem)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_CCTC_GetAllSale_Result>("Prod_CCTC_GetAllSale @macongty, @phongsale, @phongmarketing", new SqlParameter("macongty", timkiem.macongty), new SqlParameter("phongsale", timkiem.phongsale), new SqlParameter("phongmarketing", timkiem.phongmarketing));
                var result = query.ToList();
                return result;
            }
        }
        #endregion



        #region "Get All Nhân Viên"
        [System.Web.Http.Route("api/NhanVien/GetAllNhanVien/{macongty}")]
        public List<Prod_CCTC_GetAllNhanVien_Result> GetAllNhanVien(string macongty)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_CCTC_GetAllNhanVien_Result>("Prod_CCTC_GetAllNhanVien @macongty", new SqlParameter("macongty", macongty));
                var result = query.ToList();
                return result;
            }
        }
        #endregion

        #region "Get Nhân Viên Theo Phòng Ban"
        // GET: api/NhanVien
        // [DisableCors]
        [System.Web.Http.Route("api/NhanVien/GetNhanVienPhongBan/{maphongban}")]
        public List<Prod_CCTC_NhanVienPhongBan_Result> GetNhanVienPhongBan(string maphongban)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_CCTC_NhanVienPhongBan_Result>("Prod_CCTC_NhanVienPhongBan @maphongban", new SqlParameter("maphongban", maphongban));
                var result = query.ToList();
                return result;
            }
        }
        #endregion

        #region "Put Nhân Viên"
        // PUT: api/NhanVien/5
        [ResponseType(typeof(void))]
        [System.Web.Http.Route("api/NhanVien/PutNhanVienPhongBan/{id}")]
        public IHttpActionResult PutNhanVienPhongBan(string id, CCTC_NHAN_VIEN cCTC_NHAN_VIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_NHAN_VIEN.USERNAME)
            {
                return BadRequest();
            }
            using (var db = new ERP_DATABASEEntities())
            {
                db.Entry(cCTC_NHAN_VIEN).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CCTC_NHAN_VIENExists(id))
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
        }
        #endregion

        #region "Post Nhân Viên"
        // POST: api/NhanVien
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        [System.Web.Http.Route("api/NhanVien/PostNhanVienPhongBan")]
        public IHttpActionResult PostNhanVienPhongBan(CCTC_NHAN_VIEN cCTC_NHAN_VIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ERP_DATABASEEntities())
            {

                db.CCTC_NHAN_VIEN.Add(cCTC_NHAN_VIEN);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (CCTC_NHAN_VIENExists(cCTC_NHAN_VIEN.USERNAME))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtRoute("DefaultApi", new { id = cCTC_NHAN_VIEN.USERNAME }, cCTC_NHAN_VIEN);
            }
        }
        #endregion

        #region "Delete Nhân Viên"
        // DELETE: api/NhanVien/5
        [System.Web.Http.Route("api/NhanVien/DeleteNhanVienPhongBan/{id}")]
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult DeleteCCTC_NHAN_VIEN(string id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find(id);
                if (cCTC_NHAN_VIEN == null)
                {
                    return NotFound();
                }

                db.CCTC_NHAN_VIEN.Remove(cCTC_NHAN_VIEN);
                db.SaveChanges();

                return Ok(cCTC_NHAN_VIEN);
            }
        }
        #endregion
        

        protected override void Dispose(bool disposing)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        private bool CCTC_NHAN_VIENExists(string id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                return db.CCTC_NHAN_VIEN.Count(e => e.USERNAME == id) > 0;
            }
        }
    }
}