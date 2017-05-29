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

namespace ERP.Web.Api.Kho
{
    public class Api_HangCanXuatKinhDoanhController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // List hang can xuat
        [Route("api/Api_HangCanXuatKinhDoanh/ListHangCanXuat/{isadmin}/{username}")]
        public List<Prod_HangCanXuat_KD_Result> ListHangCanXuat(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuat_KD_Result>("Prod_HangCanXuat_KD @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }
        // List hang can xuat da giu
        [Route("api/Api_HangCanXuatKinhDoanh/ListHangCanXuatDaGiu/{isadmin}/{username}")]
        public List<Prod_HangCanXuat_DaGiu_KD_Result> ListHangCanXuatDaGiu(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuat_DaGiu_KD_Result>("Prod_HangCanXuat_DaGiu_KD @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        // List hang can xuat chua giu
        [Route("api/Api_HangCanXuatKinhDoanh/ListHangCanXuatChuaGiu/{isadmin}/{username}")]
        public List<Prod_HangCanXuat_ChuaGiu_KD_Result> ListHangCanXuatChuaGiu(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuat_ChuaGiu_KD_Result>("Prod_HangCanXuat_ChuaGiu_KD @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        // List hang can xuat da ban
        [Route("api/Api_HangCanXuatKinhDoanh/ListHangCanXuatDaBan/{isadmin}/{username}")]
        public List<Prod_HangCanXuatDaBan_KD_Result> ListHangCanXuatDaBan(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuatDaBan_KD_Result>("Prod_HangCanXuatDaBan_KD @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        // List hang can xuat da dat hang
        [Route("api/Api_HangCanXuatKinhDoanh/ListHangCanXuatDaDatHang/{isadmin}/{username}")]
        public List<Prod_HangCanXuatDaDatHang_KD_Result> ListHangCanXuatDaDatHang(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuatDaDatHang_KD_Result>("Prod_HangCanXuatDaDatHang_KD @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        // List hang can xuat chua dat hang
        [Route("api/Api_HangCanXuatKinhDoanh/ListHangCanXuatChuaDatHang/{isadmin}/{username}")]
        public List<Prod_HangCanXuatCanDatHang_KD_Result> ListHangCanXuatChuaDatHang(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuatCanDatHang_KD_Result>("Prod_HangCanXuatCanDatHang_KD @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_HangCanXuatKinhDoanh/5
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public IHttpActionResult GetBH_CT_BAO_GIA(int id)
        {
            BH_CT_BAO_GIA bH_CT_BAO_GIA = db.BH_CT_BAO_GIA.Find(id);
            if (bH_CT_BAO_GIA == null)
            {
                return NotFound();
            }

            return Ok(bH_CT_BAO_GIA);
        }

        // PUT: api/Api_HangCanXuatKinhDoanh/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_CT_BAO_GIA(int id, BH_CT_BAO_GIA bH_CT_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_CT_BAO_GIA.ID)
            {
                return BadRequest();
            }

            db.Entry(bH_CT_BAO_GIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_CT_BAO_GIAExists(id))
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

        // POST: api/Api_HangCanXuatKinhDoanh
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public IHttpActionResult PostBH_CT_BAO_GIA(BH_CT_BAO_GIA bH_CT_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_CT_BAO_GIA.Add(bH_CT_BAO_GIA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bH_CT_BAO_GIA.ID }, bH_CT_BAO_GIA);
        }

        // DELETE: api/Api_HangCanXuatKinhDoanh/5
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public IHttpActionResult DeleteBH_CT_BAO_GIA(int id)
        {
            BH_CT_BAO_GIA bH_CT_BAO_GIA = db.BH_CT_BAO_GIA.Find(id);
            if (bH_CT_BAO_GIA == null)
            {
                return NotFound();
            }

            db.BH_CT_BAO_GIA.Remove(bH_CT_BAO_GIA);
            db.SaveChanges();

            return Ok(bH_CT_BAO_GIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_CT_BAO_GIAExists(int id)
        {
            return db.BH_CT_BAO_GIA.Count(e => e.ID == id) > 0;
        }
    }
}