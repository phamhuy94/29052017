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

namespace ERP.Web.Api.DangKyPheDuyet
{
    public class Api_DangKyPheDuyetPOController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_DangKyPheDuyetPO
        public IQueryable<XL_DANG_KY_PHE_DUYET> GetXL_DANG_KY_PHE_DUYET()
        {
            return db.XL_DANG_KY_PHE_DUYET;
        }

        // GET: api/Api_DangKyPheDuyetPO/5
        [ResponseType(typeof(XL_DANG_KY_PHE_DUYET))]
        [Route("api/Api_DangKyPheDuyetPO/DanhsachpheduyetPO")]
        public List<Prod_XL_List_DangKyPheDuyet_Result> DanhsachpheduyetPO()
        {
            var query = db.Database.SqlQuery<Prod_XL_List_DangKyPheDuyet_Result>("Prod_XL_List_DangKyPheDuyet  @macongty,@loaipheduyet", new SqlParameter("macongty","HOPLONG"), new SqlParameter("loaipheduyet", "PO"));
            var result = query.ToList();

            return result;
        }

        [Route("api/Api_DangKyPheDuyetPO/Danhsachduocpheduyet")]
        public List<Prod_CCTC_GetAllSale_Result> Danhsachduocpheduyet()
        {
            var query = db.Database.SqlQuery<Prod_CCTC_GetAllSale_Result>("Prod_CCTC_GetAllSale  @macongty", new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();

            return result;
        }

        // PUT: api/Api_DangKyPheDuyetPO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutXL_DANG_KY_PHE_DUYET(int id, XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != xL_DANG_KY_PHE_DUYET.ID)
            {
                return BadRequest();
            }

            db.Entry(xL_DANG_KY_PHE_DUYET).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XL_DANG_KY_PHE_DUYETExists(id))
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

        // POST: api/Api_DangKyPheDuyetPO
        [ResponseType(typeof(XL_DANG_KY_PHE_DUYET))]
        [Route("api/Api_DangKyPheDuyetPO/PostXL_DANG_KY_PHE_DUYET")]
        public IHttpActionResult PostXL_DANG_KY_PHE_DUYET(XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            XL_DANG_KY_PHE_DUYET newpheduyet = new XL_DANG_KY_PHE_DUYET();
            newpheduyet.MA_PHE_DUYET = xL_DANG_KY_PHE_DUYET.MA_PHE_DUYET;
            newpheduyet.NGUOI_PHE_DUYET = xL_DANG_KY_PHE_DUYET.NGUOI_PHE_DUYET;
            newpheduyet.TRUC_THUOC = xL_DANG_KY_PHE_DUYET.TRUC_THUOC;
            newpheduyet.GHI_CHU = xL_DANG_KY_PHE_DUYET.GHI_CHU;
            db.XL_DANG_KY_PHE_DUYET.Add(newpheduyet);
            db.SaveChanges();

            return Ok(newpheduyet);
        }

        // DELETE: api/Api_DangKyPheDuyetPO/5
        [ResponseType(typeof(XL_DANG_KY_PHE_DUYET))]
        public IHttpActionResult DeleteXL_DANG_KY_PHE_DUYET(int id)
        {
            XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET = db.XL_DANG_KY_PHE_DUYET.Find(id);
            if (xL_DANG_KY_PHE_DUYET == null)
            {
                return NotFound();
            }

            db.XL_DANG_KY_PHE_DUYET.Remove(xL_DANG_KY_PHE_DUYET);
            db.SaveChanges();

            return Ok(xL_DANG_KY_PHE_DUYET);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool XL_DANG_KY_PHE_DUYETExists(int id)
        {
            return db.XL_DANG_KY_PHE_DUYET.Count(e => e.ID == id) > 0;
        }
    }
}