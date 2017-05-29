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
    public class Api_GiuKhoKinhDoanhController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // List PO da len don ban hang
        [Route("api/Api_DonHangPO/ListHangGiuTheoSale/{isadmin}/{username}")]
        public List<Prod_ListHangGiuTheoSale_Result> ListHangGiuTheoSale(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_ListHangGiuTheoSale_Result>("Prod_ListHangGiuTheoSale @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_DonHangPO/ListHangGiuTongHop")]
        public List<Prod_ListHangGiuTongHop_KD_Result> ListHangGiuTongHop()
        {
            var query = db.Database.SqlQuery<Prod_ListHangGiuTongHop_KD_Result>("Prod_ListHangGiuTongHop_KD @macongty", new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_GiuKhoKinhDoanh/5
        [ResponseType(typeof(BH_CT_DON_HANG_PO))]
        public IHttpActionResult GetBH_CT_DON_HANG_PO(int id)
        {
            BH_CT_DON_HANG_PO bH_CT_DON_HANG_PO = db.BH_CT_DON_HANG_PO.Find(id);
            if (bH_CT_DON_HANG_PO == null)
            {
                return NotFound();
            }

            return Ok(bH_CT_DON_HANG_PO);
        }

        // PUT: api/Api_GiuKhoKinhDoanh/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_CT_DON_HANG_PO(int id, BH_CT_DON_HANG_PO bH_CT_DON_HANG_PO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_CT_DON_HANG_PO.ID)
            {
                return BadRequest();
            }

            db.Entry(bH_CT_DON_HANG_PO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_CT_DON_HANG_POExists(id))
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

        // POST: api/Api_GiuKhoKinhDoanh
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

        // DELETE: api/Api_GiuKhoKinhDoanh/5
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