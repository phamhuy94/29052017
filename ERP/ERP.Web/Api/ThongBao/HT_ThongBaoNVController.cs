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

namespace ERP.Web.Api.ThongBao
{
    public class HT_ThongBaoNVController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/HT_ThongBaoNV/Mark
        [Route("api/HT_ThongBaoNV/GetThongBaoMark/{username}")]
        public List<GetHTThongBaoMark_Result> GetThongBaoMark(string username)
        {
            var query = db.Database.SqlQuery<GetHTThongBaoMark_Result>("GetHTThongBaoMark @username", new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }

        // GET: api/HT_ThongBaoNV/PhongBan
        [Route("api/HT_ThongBaoNV/GetThongBaoPhongBan/{maphongban}")]
        public List<GetHTNhiemVuPhongBan_Result> GetThongBaoPhongBan(string maphongban)
        {
            var query = db.Database.SqlQuery<GetHTNhiemVuPhongBan_Result>("GetHTNhiemVuPhongBan @maphongban",new SqlParameter("maphongban", maphongban));
            var result = query.ToList();
            return result;
        }
        // GET: api/HT_ThongBaoNV/PhongBan
        [Route("api/HT_ThongBaoNV/GetThongBaoNV/{username}")]
        public List<GetHTCongViecNV_Result> GetThongBaoNV(string username)
        {
            var query = db.Database.SqlQuery<GetHTCongViecNV_Result>("GetHTCongViecNV @username", new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }

        // GET: api/HT_ThongBaoNV/5
        [ResponseType(typeof(HT_NHIEM_VU_PHONG_BAN))]
        public IHttpActionResult GetHT_NHIEM_VU_PHONG_BAN(int id)
        {
            HT_NHIEM_VU_PHONG_BAN hT_NHIEM_VU_PHONG_BAN = db.HT_NHIEM_VU_PHONG_BAN.Find(id);
            if (hT_NHIEM_VU_PHONG_BAN == null)
            {
                return NotFound();
            }

            return Ok(hT_NHIEM_VU_PHONG_BAN);
        }

        // PUT: api/HT_ThongBaoNV/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHT_NHIEM_VU_PHONG_BAN(int id, HT_NHIEM_VU_PHONG_BAN hT_NHIEM_VU_PHONG_BAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hT_NHIEM_VU_PHONG_BAN.ID)
            {
                return BadRequest();
            }

            db.Entry(hT_NHIEM_VU_PHONG_BAN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HT_NHIEM_VU_PHONG_BANExists(id))
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

        // POST: api/HT_ThongBaoNV
        [ResponseType(typeof(HT_NHIEM_VU_PHONG_BAN))]
        public IHttpActionResult PostHT_NHIEM_VU_PHONG_BAN(HT_NHIEM_VU_PHONG_BAN hT_NHIEM_VU_PHONG_BAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HT_NHIEM_VU_PHONG_BAN.Add(hT_NHIEM_VU_PHONG_BAN);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hT_NHIEM_VU_PHONG_BAN.ID }, hT_NHIEM_VU_PHONG_BAN);
        }

        // DELETE: api/HT_ThongBaoNV/5
        [ResponseType(typeof(HT_NHIEM_VU_PHONG_BAN))]
        public IHttpActionResult DeleteHT_NHIEM_VU_PHONG_BAN(int id)
        {
            HT_NHIEM_VU_PHONG_BAN hT_NHIEM_VU_PHONG_BAN = db.HT_NHIEM_VU_PHONG_BAN.Find(id);
            if (hT_NHIEM_VU_PHONG_BAN == null)
            {
                return NotFound();
            }

            db.HT_NHIEM_VU_PHONG_BAN.Remove(hT_NHIEM_VU_PHONG_BAN);
            db.SaveChanges();

            return Ok(hT_NHIEM_VU_PHONG_BAN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HT_NHIEM_VU_PHONG_BANExists(int id)
        {
            return db.HT_NHIEM_VU_PHONG_BAN.Count(e => e.ID == id) > 0;
        }
    }
}