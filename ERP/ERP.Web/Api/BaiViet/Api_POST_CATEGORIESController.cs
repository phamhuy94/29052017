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
using ERP.Web.Models.BusinessModel;
using ERP.Web.Models;

namespace ERP.Web.Api.HeThong
{
    public class Api_POST_CATEGORIESController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_POST_CATEGORIES
        public List<POST_CATEGORIES> GetPOST_CATEGORIES()
        {
            var data = db.POST_CATEGORIES.ToList().Select(x => new POST_CATEGORIES()
            {
                ID = x.ID,
                MA_BAI_VIET = x.MA_BAI_VIET,
                MA_DANH_MUC = x.MA_DANH_MUC
            }).ToList();

            return data;
        }

        // GET: api/Api_POST_CATEGORIES/5
        [ResponseType(typeof(POST_CATEGORIES))]
        public List<Post> GetPOST_CATEGORIES(string id)
        {

            List<Post> result = new List<Post>();
            // POST post = db.POSTS.Where(x => x.TIEU_DE_BAI_VIET == id).FirstOrDefault();

            // int ma_bai_viet = post.MA_BAI_VIET;


            var data = db.POST_CATEGORIES.Where(x => x.MA_DANH_MUC == id).ToList();
            foreach (var item in data)
            {
                var kq = (from t1 in db.POSTS
                          join t2 in db.HT_NGUOI_DUNG on t1.NGUOI_DANG_BAI equals t2.USERNAME
                          where t1.MA_BAI_VIET == item.MA_BAI_VIET
                          select (new Post()
                          {
                              MA_BAI_VIET = t1.MA_BAI_VIET,
                              USERNAME = t2.USERNAME,
                              TIEU_DE_BAI_VIET = t1.TIEU_DE_BAI_VIET,
                              NGAY_DANG_BAI = t1.NGAY_DANG_BAI,
                              ANH_BAI_VIET = t1.ANH_BAI_VIET,
                              NOI_DUNG_BAI_VIET = t1.NOI_DUNG_BAI_VIET,
                              NGUOI_DANG_BAI = t1.NGUOI_DANG_BAI,
                              HO_VA_TEN = t2.HO_VA_TEN
                          })
                             );
                var r = kq.FirstOrDefault();
                Post k = new Post();
                k.MA_BAI_VIET = r.MA_BAI_VIET;
                k.USERNAME = r.USERNAME;
                k.TIEU_DE_BAI_VIET = r.TIEU_DE_BAI_VIET;
                k.NGAY_DANG_BAI = r.NGAY_DANG_BAI;
                k.ANH_BAI_VIET = r.ANH_BAI_VIET;
                k.NOI_DUNG_BAI_VIET = r.NOI_DUNG_BAI_VIET;
                k.NGUOI_DANG_BAI = r.NGUOI_DANG_BAI;
                k.HO_VA_TEN = r.HO_VA_TEN;


                result.Add(k);

            }
            var ds = result.ToList().Select(x => new Post
            {
                MA_BAI_VIET = x.MA_BAI_VIET,
                TIEU_DE_BAI_VIET = x.TIEU_DE_BAI_VIET,
                NGAY_DANG_BAI = x.NGAY_DANG_BAI,
                ANH_BAI_VIET = x.ANH_BAI_VIET,
                NOI_DUNG_BAI_VIET = x.NOI_DUNG_BAI_VIET,
                NGUOI_DANG_BAI = x.NGUOI_DANG_BAI,
                HO_VA_TEN = x.HO_VA_TEN
            }).ToList();

            return ds;
        }

        // PUT: api/Api_POST_CATEGORIES/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPOST_CATEGORIES(int id, POST_CATEGORIES pOST_CATEGORIES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pOST_CATEGORIES.ID)
            {
                return BadRequest();
            }

            db.Entry(pOST_CATEGORIES).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POST_CATEGORIESExists(id))
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

        // POST: api/Api_POST_CATEGORIES
        [ResponseType(typeof(POST_CATEGORIES))]
        public IHttpActionResult PostPOST_CATEGORIES(post_categories pOST_CATEGORIES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            POST post = db.POSTS.Where(x => x.TIEU_DE_BAI_VIET == pOST_CATEGORIES.tieu_de_bai_viet).FirstOrDefault();

            int ma_bai_viet = post.MA_BAI_VIET;
            POST_CATEGORIES p_cat = new POST_CATEGORIES();
            p_cat.MA_BAI_VIET = ma_bai_viet;
            p_cat.MA_DANH_MUC = pOST_CATEGORIES.ma_danh_muc;

            db.POST_CATEGORIES.Add(p_cat);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (POST_CATEGORIESExists(p_cat.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = p_cat.ID }, p_cat);
        }

        // DELETE: api/Api_POST_CATEGORIES/5
        [ResponseType(typeof(POST_CATEGORIES))]
        public IHttpActionResult DeletePOST_CATEGORIES(int id)
        {
            POST_CATEGORIES pOST_CATEGORIES = db.POST_CATEGORIES.Find(id);
            if (pOST_CATEGORIES == null)
            {
                return NotFound();
            }

            db.POST_CATEGORIES.Remove(pOST_CATEGORIES);
            db.SaveChanges();

            return Ok(pOST_CATEGORIES);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool POST_CATEGORIESExists(int id)
        {
            return db.POST_CATEGORIES.Count(e => e.ID == id) > 0;
        }
    }
}