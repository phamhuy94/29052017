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

namespace ERP.Web.Api.Comments
{
    public class Api_Comments_CongNo_KHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Comments_CongNo_KH
        [Route("api/Api_Comments_CongNo_KH/GetData_Comments_CongNo/{makh}/{tuancongno}")]
        public List<Prod_Comments_CongNoKH_Result> GetData_Comments_CongNo(string makh, string tuancongno)
        {
            var query = db.Database.SqlQuery<Prod_Comments_CongNoKH_Result>("Prod_Comments_CongNoKH @makh, @tuancongno", new SqlParameter("makh", makh), new SqlParameter("tuancongno", tuancongno));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_Comments_CongNo_KH/5
        [ResponseType(typeof(COMMENTS_CONG_NO_KH))]
        public IHttpActionResult GetCOMMENTS_CONG_NO_KH(int id)
        {
            COMMENTS_CONG_NO_KH cOMMENTS_CONG_NO_KH = db.COMMENTS_CONG_NO_KH.Find(id);
            if (cOMMENTS_CONG_NO_KH == null)
            {
                return NotFound();
            }

            return Ok(cOMMENTS_CONG_NO_KH);
        }

        // PUT: api/Api_Comments_CongNo_KH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCOMMENTS_CONG_NO_KH(int id, COMMENTS_CONG_NO_KH cOMMENTS_CONG_NO_KH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cOMMENTS_CONG_NO_KH.ID)
            {
                return BadRequest();
            }

            db.Entry(cOMMENTS_CONG_NO_KH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!COMMENTS_CONG_NO_KHExists(id))
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

        // POST: api/Api_Comments_CongNo_KH
        [ResponseType(typeof(COMMENTS_CONG_NO_KH))]
        public IHttpActionResult PostCOMMENTS_CONG_NO_KH(COMMENTS_CONG_NO_KH cOMMENTS_CONG_NO_KH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            COMMENTS_CONG_NO_KH congno = new COMMENTS_CONG_NO_KH();
            congno.NGAY_COMMENTS = DateTime.Now;
            congno.NGUOI_COMMENTS = cOMMENTS_CONG_NO_KH.NGUOI_COMMENTS;
            congno.MA_KHACH_HANG = cOMMENTS_CONG_NO_KH.MA_KHACH_HANG;
            congno.NOI_DUNG_COMMENTS = cOMMENTS_CONG_NO_KH.NOI_DUNG_COMMENTS;
            congno.TUAN_CONG_NO = cOMMENTS_CONG_NO_KH.TUAN_CONG_NO;
            db.COMMENTS_CONG_NO_KH.Add(congno);
            db.SaveChanges();

            return Ok(congno);
        }

        // DELETE: api/Api_Comments_CongNo_KH/5
        [ResponseType(typeof(COMMENTS_CONG_NO_KH))]
        public IHttpActionResult DeleteCOMMENTS_CONG_NO_KH(int id)
        {
            COMMENTS_CONG_NO_KH cOMMENTS_CONG_NO_KH = db.COMMENTS_CONG_NO_KH.Find(id);
            if (cOMMENTS_CONG_NO_KH == null)
            {
                return NotFound();
            }

            db.COMMENTS_CONG_NO_KH.Remove(cOMMENTS_CONG_NO_KH);
            db.SaveChanges();

            return Ok(cOMMENTS_CONG_NO_KH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool COMMENTS_CONG_NO_KHExists(int id)
        {
            return db.COMMENTS_CONG_NO_KH.Count(e => e.ID == id) > 0;
        }
    }
}