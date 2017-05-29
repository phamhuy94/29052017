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
using ERP.Web.Models.BusinessModel;
using System.Data.SqlClient;

namespace ERP.Web.Api.KhachHang
{
    public class Api_LienHeKhachHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_LienHeKhachHang
        [Route("api/Api_LienHeKhachHang/{makh}")]
        public List<Prod_KH_ShowLienHe_Result> LocKH(string makh)
        {
            var query = db.Database.SqlQuery<Prod_KH_ShowLienHe_Result>("Prod_KH_ShowLienHe @makhachhang", new SqlParameter("makhachhang", makh));
            var result = query.ToList();
            return result;
        }

        //public List<LienHeKH> GetLienHeKH(string makh)
        //{
        //    var vData = (from t1 in db.KH_LIEN_HE
        //                 join t2 in db.KH_SALES_PHU_TRACH on t1.ID_LIEN_HE equals t2.ID_LIEN_HE
        //                 join t3 in db.HT_NGUOI_DUNG on t2.SALES_PHU_TRACH equals t3.USERNAME
        //                 join t4 in db.KHs on t1.MA_KHACH_HANG equals t4.MA_KHACH_HANG
        //                 where t1.MA_KHACH_HANG == makh
        //                 select new
        //                 {
        //                     t1.MA_KHACH_HANG,
        //                     t1.NGUOI_LIEN_HE,
        //                     t1.CHUC_VU,
        //                     t1.PHONG_BAN,
        //                     t1.NGAY_SINH,
        //                     t1.GIOI_TINH,
        //                     t1.EMAIL_CA_NHAN,
        //                     t1.EMAIL_CONG_TY,
        //                     t1.SKYPE,
        //                     t1.SDT1,
        //                     t1.SDT2,
        //                     t1.GHI_CHU,
        //                     t1.FACEBOOK,
        //                     t1.TINH_TRANG_LAM_VIEC
        //                 ,
        //                     t2.ID,
        //                     t2.ID_LIEN_HE,
        //                     t2.SALES_PHU_TRACH,
        //                     t2.NGAY_BAT_DAU_PHU_TRACH,
        //                     t2.NGAY_KET_THUC_PHU_TRACH,
        //                     t2.TRANG_THAI,
        //                     t2.SALES_MOI,
        //                     t2.SALES_CU,
        //                     t3.HO_VA_TEN,
        //                     t4.TEN_CONG_TY
        //                 });
        //    var result = vData.ToList().Select(x => new LienHeKH()
        //    {
        //        ID_LIEN_HE = x.ID_LIEN_HE,
        //        HO_VA_TEN = x.HO_VA_TEN,
        //        MA_KHACH_HANG = x.MA_KHACH_HANG,
        //        NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
        //        CHUC_VU = x.CHUC_VU,
        //        PHONG_BAN = x.PHONG_BAN,
        //        NGAY_SINH = x.NGAY_SINH.ToString(),
        //        GIOI_TINH = x.GIOI_TINH,
        //        EMAIL_CA_NHAN = x.EMAIL_CA_NHAN,
        //        EMAIL_CONG_TY = x.EMAIL_CONG_TY,
        //        SKYPE = x.SKYPE,
        //        FACEBOOK = x.FACEBOOK,
        //        TINH_TRANG_LAM_VIEC = x.TINH_TRANG_LAM_VIEC,
        //        SDT1 = x.SDT1,
        //        SDT2 = x.SDT2,
        //        GHI_CHU = x.GHI_CHU,
        //        ID = x.ID,
        //        SALES_PHU_TRACH = x.SALES_PHU_TRACH,
        //        TRANG_THAI = x.TRANG_THAI,
        //        NGAY_KET_THUC_PHU_TRACH = x.NGAY_KET_THUC_PHU_TRACH,
        //        NGAY_BAT_DAU_PHU_TRACH = x.NGAY_BAT_DAU_PHU_TRACH,
        //        TEN_CONG_TY = x.TEN_CONG_TY,
        //        SALES_CU = x.SALES_CU,
        //        SALES_MOI = x.SALES_MOI,
        //    }).ToList();
        //    return result;
        //}


        // PUT: api/Api_LienHeKhachHang/5
        [Route("api/Api_LienHeKhachHang/{id}")]
        public IHttpActionResult PutKH_LIEN_HE(int id, KH_LIEN_HE kH_LIEN_HE)
        {
            var query = db.KH_LIEN_HE.Where(x => x.ID_LIEN_HE == id).FirstOrDefault();
            if(query != null)
            {
                query.NGUOI_LIEN_HE = kH_LIEN_HE.NGUOI_LIEN_HE;
                query.CHUC_VU = kH_LIEN_HE.CHUC_VU;
                query.PHONG_BAN = kH_LIEN_HE.PHONG_BAN;
                if(kH_LIEN_HE.NGAY_SINH != null)
                {
                    query.NGAY_SINH = kH_LIEN_HE.NGAY_SINH;
                }
                query.GIOI_TINH = kH_LIEN_HE.GIOI_TINH;
                query.EMAIL_CA_NHAN = kH_LIEN_HE.EMAIL_CA_NHAN;
                query.EMAIL_CONG_TY = kH_LIEN_HE.EMAIL_CONG_TY;
                query.SKYPE = kH_LIEN_HE.SKYPE;
                query.FACEBOOK = kH_LIEN_HE.FACEBOOK;
                query.GHI_CHU = kH_LIEN_HE.GHI_CHU;
                query.SDT1 = kH_LIEN_HE.SDT1;
                query.SDT2 = kH_LIEN_HE.SDT2;
                query.TINH_TRANG_LAM_VIEC = kH_LIEN_HE.TINH_TRANG_LAM_VIEC;
            }

            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_LIEN_HEExists(id))
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

        // POST: api/Api_LienHeKhachHang
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult PostKH_LIEN_HE(LienHeKH lh)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //if(lh.NGUOI_LIEN_HE != "")
            //{
                KH_LIEN_HE lienhe = new KH_LIEN_HE();
                lienhe.MA_KHACH_HANG = lh.MA_KHACH_HANG;
                lienhe.NGUOI_LIEN_HE = lh.NGUOI_LIEN_HE;
                lienhe.CHUC_VU = lh.CHUC_VU;
                lienhe.PHONG_BAN = lh.PHONG_BAN;
                if (lh.NGAY_SINH != null)
                    lienhe.NGAY_SINH = xlnt.Xulydatetime(lh.NGAY_SINH);
                lienhe.GIOI_TINH = lh.GIOI_TINH;
                lienhe.EMAIL_CA_NHAN = lh.EMAIL_CA_NHAN;
                lienhe.EMAIL_CONG_TY = lh.EMAIL_CONG_TY;
                lienhe.SKYPE = lh.SKYPE;
                lienhe.FACEBOOK = lh.FACEBOOK;
                lienhe.GHI_CHU = lh.GHI_CHU;
                lienhe.SDT1 = lh.SDT1;
                lienhe.SDT2 = lh.SDT2;
                lienhe.TINH_TRANG_LAM_VIEC = lh.TINH_TRANG_LAM_VIEC;
                db.KH_LIEN_HE.Add(lienhe);
                db.SaveChanges();
                var query = db.KH_LIEN_HE.Where(x => x.SDT1 == lh.SDT1).ToList();
                var data = query.LastOrDefault();
                KH_SALES_PHU_TRACH salept = new KH_SALES_PHU_TRACH();
                salept.ID_LIEN_HE = data.ID_LIEN_HE;
                salept.SALES_PHU_TRACH = lh.SALES_PHU_TRACH;
                salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                salept.TRANG_THAI = true;
                if (lh.SALES_CU == false && lh.SALES_MOI == false)
                {
                    salept.SALES_MOI = true;
                    salept.SALES_CU = false;
                }
                else
                {
                    salept.SALES_CU = lh.SALES_CU;
                    salept.SALES_MOI = lh.SALES_MOI;
                }
                db.KH_SALES_PHU_TRACH.Add(salept);
                db.SaveChanges();
            //}
           

            return Ok(lienhe);
        }

        // DELETE: api/Api_LienHeKhachHang/5
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult DeleteKH_LIEN_HE(int id)
        {
            KH_LIEN_HE kH_LIEN_HE = db.KH_LIEN_HE.Find(id);
            if (kH_LIEN_HE == null)
            {
                return NotFound();
            }

            db.KH_LIEN_HE.Remove(kH_LIEN_HE);
            db.SaveChanges();

            return Ok(kH_LIEN_HE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_LIEN_HEExists(int id)
        {
            return db.KH_LIEN_HE.Count(e => e.ID_LIEN_HE == id) > 0;
        }
    }
}