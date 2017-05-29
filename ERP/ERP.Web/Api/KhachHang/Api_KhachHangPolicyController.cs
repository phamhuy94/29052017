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
using ERP.Web.Models.NewModels;

namespace ERP.Web.Api.KhachHang
{
    public class Api_KhachHangPolicyController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_KhachHangPolicy
        [Route("api/Api_KhachHangPolicy/GetNvMuaHang")]
        public List<HopLong_GetAll_Nhanvien_MuaHang_Result> GetNvMuaHang()
        {
            var query = db.Database.SqlQuery<HopLong_GetAll_Nhanvien_MuaHang_Result>("HopLong_GetAll_Nhanvien_MuaHang @macongty", new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_KhachHangPolicy/GetNvMarketing")]
        public List<Prod_GetAll_Marketing_Result> GetNvMarketing()
        {
            var query = db.Database.SqlQuery<Prod_GetAll_Marketing_Result>("Prod_GetAll_Marketing @macongty", new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_KhachHangPolicy/5
        [Route("api/Api_KhachHangPolicy/{makhachhang}")]
        public List<Prod_KH_GetPolicy_Result> GetKH(string makhachhang)
        {
            var query = db.Database.SqlQuery<Prod_KH_GetPolicy_Result>("Prod_KH_GetPolicy @makhachhang", new SqlParameter("makhachhang", makhachhang));
            var result = query.ToList();
            return result;
        }

        // PUT: api/Api_KhachHangPolicy/5
        [Route("api/Api_KhachHangPolicy/PutKH_POLICY/{id}")]
        public IHttpActionResult PutKH_POLICY(int id, KH_POLICY policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.ID)
            {
                return BadRequest();
            }
            var query = db.KH_POLICY.Where(x => x.ID == id).FirstOrDefault();
            if (query != null)
            {
                query.CK_HISTORY_3 = query.CK_HISTORY_2;
                query.GIA_HISTORY_3 = query.GIA_HISTORY_2;
                query.CK_HISTORY_2 = query.CK_HISTORY_1;
                query.GIA_HISTORY_2 = query.GIA_HISTORY_1;
                query.CK_HISTORY_1 = query.CK;
                query.GIA_HISTORY_1 = query.GIA_BAN;
                query.CK = policy.CK;
                query.GIA_BAN = policy.GIA_BAN;
                query.NGUOI_CAP_NHAT = policy.NGUOI_CAP_NHAT;
                query.NGAY_CAP_NHAT = DateTime.Now;
            }
            
            // db.Entry(kH_POLICY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_POLICYExists(id))
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

        // POST: api/Api_KhachHangPolicy
        [ResponseType(typeof(KH_POLICY))]
        public IHttpActionResult PostKH_POLICY(khachhang_policy kH_POLICY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = db.HH_NHOM_VTHH.Where(x => x.MA_NHOM_HANG_CHI_TIET == kH_POLICY.MA_NHOM_HANG).FirstOrDefault();
            if(query != null)
            {
                KH_POLICY newpolicy = new KH_POLICY();
                newpolicy.MA_NHOM_HANG = kH_POLICY.MA_NHOM_HANG;
                newpolicy.MA_KHACH_HANG = kH_POLICY.MA_KHACH_HANG;
                newpolicy.NGAY_CAP_NHAT = DateTime.Today.Date;
                newpolicy.GIA_BAN = kH_POLICY.GIA_BAN;
                newpolicy.CK = kH_POLICY.CK;
                newpolicy.NGUOI_CAP_NHAT = kH_POLICY.NGUOI_CAP_NHAT;
                db.KH_POLICY.Add(newpolicy);
                db.SaveChanges();
            }
            else
            {
                HH_NHOM_VTHH newvthh = new HH_NHOM_VTHH();
                newvthh.MA_NHOM_HANG_CHI_TIET = kH_POLICY.MA_NHOM_HANG;
                newvthh.MA_NHOM_HANG_CHA = kH_POLICY.MA_NHOM_HANG_CHA;
                newvthh.PURC_PHU_TRACH = kH_POLICY.PURC_PHU_TRACH;
                newvthh.MARK_PHU_TRACH = kH_POLICY.MARK_PHU_TRACH;
                db.HH_NHOM_VTHH.Add(newvthh);
                db.SaveChanges();

                KH_POLICY newpolicy = new KH_POLICY();
                newpolicy.MA_NHOM_HANG = newvthh.MA_NHOM_HANG_CHI_TIET;
                newpolicy.MA_KHACH_HANG = kH_POLICY.MA_KHACH_HANG;
                newpolicy.GIA_BAN = kH_POLICY.GIA_BAN;
                newpolicy.CK = kH_POLICY.CK;
                newpolicy.NGUOI_CAP_NHAT = kH_POLICY.NGUOI_CAP_NHAT;
                newpolicy.NGAY_CAP_NHAT = DateTime.Now;
                db.KH_POLICY.Add(newpolicy);
                db.SaveChanges();
            }

            return Ok(kH_POLICY);
        }

        // DELETE: api/Api_KhachHangPolicy/5
        [ResponseType(typeof(KH_POLICY))]
        public IHttpActionResult DeleteKH_POLICY(int id)
        {
            var query = db.KH_POLICY.Where(x => x.ID == id).FirstOrDefault();
            if(query != null)
            {
                db.KH_POLICY.Remove(query);
                db.SaveChanges();
            }

            return Ok(query);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_POLICYExists(int id)
        {
            return db.KH_POLICY.Count(e => e.ID == id) > 0;
        }
    }
}