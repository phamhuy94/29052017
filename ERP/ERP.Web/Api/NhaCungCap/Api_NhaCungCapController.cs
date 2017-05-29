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
using ERP.Web.Models.NewModels;
using System.Data.SqlClient;

namespace ERP.Web.Api.NhaCungCap
{
    
    public class Api_NhaCungCapController : ApiController
    {
        public class ThongTinTimKiem
        {
            public string manv { get; set; }
            public string macongty { get; set; }
            public Boolean isadmin { get; set; }
            public string tukhoa { get; set; }
        }
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NhaCungCap
        [HttpPost]
        [Route("api/Api_NhaCungCap/GetNCCTheoTuKhoa")]
        public List<HopLong_LocNCCtheotukhoa_Result> GetNCCTheoTuKhoa(ThongTinTimKiem timkiem)
        {
            if(timkiem.tukhoa == null)
            {
                var query = db.Database.SqlQuery<HopLong_LocNCCtheotukhoa_Result>("HopLong_LocNCCtheotukhoa @manv, @macongty, @isadmin, @tukhoa", new SqlParameter("manv", timkiem.manv), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", DBNull.Value));
                var result = query.ToList();
                return result;
            }
            else
            {
                var query = db.Database.SqlQuery<HopLong_LocNCCtheotukhoa_Result>("HopLong_LocNCCtheotukhoa @manv, @macongty, @isadmin, @tukhoa", new SqlParameter("manv", timkiem.manv), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa));
                var result = query.ToList();
                return result;

            }

        }

        // GET: api/Api_NhaCungCap/5
        [ResponseType(typeof(NCC))]
        public IHttpActionResult GetNCC(string id)
        {
            NCC nCC = db.NCCs.Find(id);
            if (nCC == null)
            {
                return NotFound();
            }

            return Ok(nCC);
        }

        // PUT: api/Api_NhaCungCap/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC(string id, NCC nCC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nCC.MA_NHA_CUNG_CAP)
            {
                return BadRequest();
            }

            db.Entry(nCC).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCCExists(id))
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

        // POST: api/Api_NhaCungCap
        [ResponseType(typeof(NCC))]
        public IHttpActionResult PostNCC(NCC nCC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NCC newnhacungcap = new NCC();
            newnhacungcap.MA_NHA_CUNG_CAP = nCC.MA_NHA_CUNG_CAP;
            newnhacungcap.TEN_NHA_CUNG_CAP = nCC.TEN_NHA_CUNG_CAP;
            newnhacungcap.PHAN_LOAI_NCC = nCC.PHAN_LOAI_NCC;
            newnhacungcap.VAN_PHONG_GIAO_DICH = nCC.VAN_PHONG_GIAO_DICH;
            newnhacungcap.DIA_CHI_XUAT_HOA_DON = nCC.DIA_CHI_XUAT_HOA_DON;
            newnhacungcap.MST = nCC.MST;
            newnhacungcap.SDT = nCC.SDT;
            newnhacungcap.EMAIL = nCC.EMAIL;
            newnhacungcap.FAX = nCC.FAX;
            newnhacungcap.WEBSITE = nCC.WEBSITE;
            newnhacungcap.DIEU_KHOAN_THANH_TOAN = nCC.DIEU_KHOAN_THANH_TOAN;
            newnhacungcap.SO_NGAY_DUOC_NO = nCC.SO_NGAY_DUOC_NO;
            newnhacungcap.SO_NO_TOI_DA = nCC.SO_NO_TOI_DA;
            newnhacungcap.DANH_GIA = nCC.DANH_GIA;
            newnhacungcap.LOGO = nCC.LOGO;
            newnhacungcap.TRUC_THUOC = nCC.TRUC_THUOC;
           db.NCCs.Add(newnhacungcap);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NCCExists(nCC.MA_NHA_CUNG_CAP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nCC.MA_NHA_CUNG_CAP }, nCC);
        }

        // DELETE: api/Api_NhaCungCap/5
        [ResponseType(typeof(NCC))]
        public IHttpActionResult DeleteNCC(string id)
        {
            NCC nCC = db.NCCs.Find(id);
            if (nCC == null)
            {
                return NotFound();
            }

            db.NCCs.Remove(nCC);
            db.SaveChanges();

            return Ok(nCC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCCExists(string id)
        {
            return db.NCCs.Count(e => e.MA_NHA_CUNG_CAP == id) > 0;
        }
    }
}