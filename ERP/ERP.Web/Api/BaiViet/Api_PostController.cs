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
using ERP.Web.Models;

namespace ERP.Web.Api.HeThong
{
    public class Api_PostController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Post
        [Route("api/Api_Post/GetPOSTS/{id}")]
        public List<Post> GetPOSTS(string id)
        {
            var query = db.Database.SqlQuery<Post>("BaiVietTheoDanhMuc @tukhoa", new SqlParameter("tukhoa", id));
            var result = query.ToList().Select(x => new Post()
            {
                MA_BAI_VIET=x.MA_BAI_VIET,
                TIEU_DE_BAI_VIET = x.TIEU_DE_BAI_VIET,
                NGAY_DANG_BAI = x.NGAY_DANG_BAI,
                ANH_BAI_VIET = x.ANH_BAI_VIET,
                NOI_DUNG_BAI_VIET = x.NOI_DUNG_BAI_VIET,
                NGUOI_DANG_BAI = x.NGUOI_DANG_BAI,
                TEN_DANH_MUC = x.TEN_DANH_MUC
    }).ToList();
            return result;
        }

        // GET: api/Api_Post/5
        [ResponseType(typeof(POST))]
        public IHttpActionResult GetPOST(int id)
        {
            POST pOST = db.POSTS.Find(id);
            if (pOST == null)
            {
                return NotFound();
            }

            return Ok(pOST);
        }

        // PUT: api/Api_Post/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPOST(int id, POST pOST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pOST.MA_BAI_VIET)
            {
                return BadRequest();
            }

            db.Entry(pOST).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POSTExists(id))
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

        // POST: api/Api_Post
        [ResponseType(typeof(POST))]
        public IHttpActionResult PostPOST(POST pOST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            POST baiviet = new POST();
            baiviet.TIEU_DE_BAI_VIET = pOST.TIEU_DE_BAI_VIET;
            baiviet.NGAY_DANG_BAI = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            baiviet.NOI_DUNG_BAI_VIET = pOST.NOI_DUNG_BAI_VIET;
            baiviet.ANH_BAI_VIET = pOST.ANH_BAI_VIET;
            baiviet.NGUOI_DANG_BAI = pOST.NGUOI_DANG_BAI;

            db.POSTS.Add(baiviet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (POSTExists(baiviet.MA_BAI_VIET))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = baiviet.MA_BAI_VIET }, baiviet);
        }

        // DELETE: api/Api_Post/5
        [ResponseType(typeof(POST))]
        public IHttpActionResult DeletePOST(int id)
        {
            POST pOST = db.POSTS.Find(id);
            if (pOST == null)
            {
                return NotFound();
            }

            db.POSTS.Remove(pOST);
            db.SaveChanges();

            return Ok(pOST);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool POSTExists(int id)
        {
            return db.POSTS.Count(e => e.MA_BAI_VIET == id) > 0;
        }
    }
}