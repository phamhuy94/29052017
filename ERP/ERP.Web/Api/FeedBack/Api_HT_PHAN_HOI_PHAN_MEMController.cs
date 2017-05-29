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
using ERP.Web.Common;
using ERP.Web.Models.NewModels;
using System.Data.SqlClient;

namespace ERP.Web.Api.FeedBack
{
    public class Api_HT_PHAN_HOI_PHAN_MEMController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_HT_PHAN_HOI_PHAN_MEM
        [HttpGet]
        [Route("api/Api_HT_PHAN_HOI_PHAN_MEM/GetHT_PHAN_HOI_PHAN_MEM/{admin}")]
        public List<Prod_HT_GetListPhanHoi_Result> GetHT_PHAN_HOI_PHAN_MEM(Boolean admin)
        {
            var query = db.Database.SqlQuery<Prod_HT_GetListPhanHoi_Result>("Prod_HT_GetListPhanHoi @isadmin", new SqlParameter("@isadmin", admin));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_HT_PHAN_HOI_PHAN_MEM/5
        [ResponseType(typeof(HT_PHAN_HOI_PHAN_MEM))]
        public IHttpActionResult GetHT_PHAN_HOI_PHAN_MEM(int id)
        {
            HT_PHAN_HOI_PHAN_MEM hT_PHAN_HOI_PHAN_MEM = db.HT_PHAN_HOI_PHAN_MEM.Find(id);
            if (hT_PHAN_HOI_PHAN_MEM == null)
            {
                return NotFound();
            }

            return Ok(hT_PHAN_HOI_PHAN_MEM);
        }

        // PUT: api/Api_HT_PHAN_HOI_PHAN_MEM/5
        [Route("api/Api_HT_PHAN_HOI_PHAN_MEM/PutHT_PHAN_HOI_PHAN_MEM")]
        [ResponseType(typeof(HT_PHAN_HOI_PHAN_MEM))]
        public IHttpActionResult PutHT_PHAN_HOI_PHAN_MEM(PhanHoi phanhoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Lưu thông tin nhập kho
            //HT_PHAN_HOI_PHAN_MEM ph = new HT_PHAN_HOI_PHAN_MEM();
            var ph = db.HT_PHAN_HOI_PHAN_MEM.Where(x => x.ID == phanhoi.ID).FirstOrDefault();          
            ph.NHAN_VIEN_PHAN_HOI = phanhoi.NHAN_VIEN_PHAN_HOI;
            ph.THONG_TIN_PHAN_HOI = phanhoi.THONG_TIN_PHAN_HOI;
            ph.THONG_TIN_PHAN_HOI_TOT = phanhoi.THONG_TIN_PHAN_TOT;
            ph.THONG_TIN_PHAN_HOI_TRUNG_BINH = phanhoi.THONG_TIN_PHAN_HOI_TRUNG_BINH;
            ph.THONG_TIN_PHAN_HOI_KHONG_TOT = phanhoi.THONG_TIN_PHAN_HOI_KHONG_TOT;
            ph.THONG_TIN_PHAN_HOI_LUNG_TUNG = phanhoi.THONG_TIN_PHAN_HOI_LUNG_TUNG;
            ph.NGUOI_DUYET = phanhoi.NGUOI_DUYET;
            ph.NGAY_DUYET = DateTime.Today.Date;
            ph.TINH_DIEM = Convert.ToInt32(phanhoi.TINH_DIEM);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {


                throw;
            }


            return Ok(ph.THONG_TIN_PHAN_HOI);

        }

        // POST: api/Api_HT_PHAN_HOI_PHAN_MEM
        [Route("api/Api_HT_PHAN_HOI_PHAN_MEM/PostHT_PHAN_HOI_PHAN_MEM")]
        [ResponseType(typeof(HT_PHAN_HOI_PHAN_MEM))]
        public IHttpActionResult PostHT_PHAN_HOI_PHAN_MEM(PhanHoi phanhoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Lưu thông tin nhập kho
            HT_PHAN_HOI_PHAN_MEM ph = new HT_PHAN_HOI_PHAN_MEM();
            ph.NGAY_PHAN_HOI = DateTime.Today.Date;
            ph.NHAN_VIEN_PHAN_HOI = phanhoi.NHAN_VIEN_PHAN_HOI;
            ph.THONG_TIN_PHAN_HOI = phanhoi.THONG_TIN_PHAN_HOI;
            db.HT_PHAN_HOI_PHAN_MEM.Add(ph);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {


                throw;
            }
          

            return Ok(ph.THONG_TIN_PHAN_HOI);

        }


        // DELETE: api/Api_HT_PHAN_HOI_PHAN_MEM/5
        [ResponseType(typeof(HT_PHAN_HOI_PHAN_MEM))]
        public IHttpActionResult DeleteHT_PHAN_HOI_PHAN_MEM(int id)
        {
            HT_PHAN_HOI_PHAN_MEM hT_PHAN_HOI_PHAN_MEM = db.HT_PHAN_HOI_PHAN_MEM.Find(id);
            if (hT_PHAN_HOI_PHAN_MEM == null)
            {
                return NotFound();
            }

            db.HT_PHAN_HOI_PHAN_MEM.Remove(hT_PHAN_HOI_PHAN_MEM);
            db.SaveChanges();

            return Ok(hT_PHAN_HOI_PHAN_MEM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HT_PHAN_HOI_PHAN_MEMExists(int id)
        {
            return db.HT_PHAN_HOI_PHAN_MEM.Count(e => e.ID == id) > 0;
        }
    }
}