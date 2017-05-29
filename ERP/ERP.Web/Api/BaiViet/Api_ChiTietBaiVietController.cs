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
using ERP.Web.Models;

namespace ERP.Web.Api.HeThong
{
    public class Api_ChiTietBaiVietController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ChiTietBaiViet
        public List<Post> GetPost(int id)
        {
            var vData = (from t1 in db.POSTS
                         join t2 in db.POST_CATEGORIES on t1.MA_BAI_VIET equals t2.MA_BAI_VIET
                         join t3 in db.HT_NGUOI_DUNG on t1.NGUOI_DANG_BAI equals t3.USERNAME
                         where t2.MA_BAI_VIET == id
                         select new { t1.MA_BAI_VIET, t1.TIEU_DE_BAI_VIET, t1.NGUOI_DANG_BAI, t1.NOI_DUNG_BAI_VIET, t3.HO_VA_TEN, t1.ANH_BAI_VIET, t1.NGAY_DANG_BAI, t1.NGAY_CAP_NHAT});
            var result = vData.ToList().Select(x => new Post()
            {
                MA_BAI_VIET = x.MA_BAI_VIET,
                TIEU_DE_BAI_VIET = x.TIEU_DE_BAI_VIET,
                NGAY_DANG_BAI = x.NGAY_DANG_BAI,
                NOI_DUNG_BAI_VIET = x.NOI_DUNG_BAI_VIET,
                HO_VA_TEN = x.HO_VA_TEN,
                ANH_BAI_VIET = x.ANH_BAI_VIET,
                NGAY_CAP_NHAT = x.NGAY_CAP_NHAT
            }).ToList();
            return result;
        }

        // GET: api/Api_ChiTietBaiViet/5
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

        // PUT: api/Api_ChiTietBaiViet/5
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

            var baiviet = db.POSTS.Where(x => x.MA_BAI_VIET == id).FirstOrDefault();
            baiviet.TIEU_DE_BAI_VIET = pOST.TIEU_DE_BAI_VIET;
            baiviet.NOI_DUNG_BAI_VIET = pOST.NOI_DUNG_BAI_VIET;
            baiviet.NGAY_CAP_NHAT = DateTime.Today.Date;

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

        // POST: api/Api_ChiTietBaiViet
        [ResponseType(typeof(POST))]
        public IHttpActionResult PostPOST(POST pOST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.POSTS.Add(pOST);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pOST.MA_BAI_VIET }, pOST);
        }

        // DELETE: api/Api_ChiTietBaiViet/5
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