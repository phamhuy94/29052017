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

namespace ERP.Web.Api.BaoGia
{
    public class Api_DuyetBaoGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_DuyetBaoGia
        [Route("api/Api_DuyetBaoGia/GetDuyet_Bao_Gia/{so_bao_gia}")]
        public List<ChiTietBaoGia> GetDuyet_Bao_Gia(string so_bao_gia)
        {
            var vData = (from t1 in db.BH_BAO_GIA
                         join t3 in db.KHs on t1.MA_KHACH_HANG equals t3.MA_KHACH_HANG
                         join t4 in db.BH_DON_HANG_DU_KIEN on t1.MA_DU_KIEN equals t4.MA_DU_KIEN
                         join t5 in db.HT_NGUOI_DUNG on t1.SALES_BAO_GIA equals t5.USERNAME
                         join t6 in db.KH_LIEN_HE on t1.LIEN_HE_KHACH_HANG equals t6.ID_LIEN_HE
                         where t1.SO_BAO_GIA == so_bao_gia
                         select new
                         {
                             t1.SO_BAO_GIA,t1.NGAY_BAO_GIA,t1.MA_DU_KIEN,t1.MA_KHACH_HANG,t1.LIEN_HE_KHACH_HANG,t1.PHUONG_THUC_THANH_TOAN,t1.HAN_THANH_TOAN,t1.HIEU_LUC_BAO_GIA,t1.DIEU_KHOAN_THANH_TOAN,t4.NGAY_TAO,
                             t1.PHI_VAN_CHUYEN,t1.TONG_TIEN,t1.DA_DUYET,t1.NGUOI_DUYET,t1.DA_TRUNG,t1.DA_HUY,t1.LY_DO_HUY,t1.SALES_BAO_GIA,t1.TRUC_THUOC,t6.NGUOI_LIEN_HE,t5.HO_VA_TEN,t3.TEN_CONG_TY,t3.DIA_CHI_XUAT_HOA_DON,t3.VAN_PHONG_GIAO_DICH,
                             t1.THUE_SUAT_GTGT,t1.TIEN_THUE_GTGT,t1.TONG_GIA_TRI_DON_HANG_THUC_TE,t1.GIA_TRI_THUC_THU_TU_KHACH,t1.TONG_GIA_TRI_CHENH_LECH,t1.TONG_CHI_PHI_HOA_DON,t1.THUC_NHAN_CUA_KHACH
                         });
            var result = vData.ToList().Select(x => new ChiTietBaoGia()
            {
                SO_BAO_GIA = x.SO_BAO_GIA,
                NGAY_BAO_GIA = x.NGAY_BAO_GIA,
                MA_DU_KIEN = x.MA_DU_KIEN,
                MA_KHACH_HANG = x.MA_KHACH_HANG,
                LIEN_HE_KHACH_HANG = x.LIEN_HE_KHACH_HANG,
                PHUONG_THUC_THANH_TOAN = x.PHUONG_THUC_THANH_TOAN,
                HAN_THANH_TOAN = x.HAN_THANH_TOAN,
                HIEU_LUC_BAO_GIA = x.HIEU_LUC_BAO_GIA,
                DIEU_KHOAN_THANH_TOAN = x.DIEU_KHOAN_THANH_TOAN,
                PHI_VAN_CHUYEN = x.PHI_VAN_CHUYEN,
                TONG_TIEN = x.TONG_TIEN,
                TONG_GIA_TRI_DON_HANG_THUC_TE = x.TONG_GIA_TRI_DON_HANG_THUC_TE,
                GIA_TRI_THUC_THU_TU_KHACH = x.GIA_TRI_THUC_THU_TU_KHACH,
                DA_DUYET = x.DA_DUYET,
                NGUOI_DUYET = x.NGUOI_DUYET,
                DA_TRUNG = x.DA_TRUNG,
                DA_HUY = x.DA_HUY,
                LY_DO_HUY = x.LY_DO_HUY,
                SALES_BAO_GIA = x.SALES_BAO_GIA,
                TRUC_THUOC = x.TRUC_THUOC,
                NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
                HO_VA_TEN = x.HO_VA_TEN,
                TEN_CONG_TY = x.TEN_CONG_TY,
                DIA_CHI_XUAT_HOA_DON = x.DIA_CHI_XUAT_HOA_DON,
                VAN_PHONG_GIAO_DICH = x.VAN_PHONG_GIAO_DICH,
                NGAY_TAO = x.NGAY_TAO,
                THUE_SUAT_GTGT = x.THUE_SUAT_GTGT,
                TIEN_THUE_GTGT = x.TIEN_THUE_GTGT,
                TONG_GIA_TRI_CHENH_LECH = x.TONG_GIA_TRI_CHENH_LECH,
                TONG_CHI_PHI_HOA_DON = x.TONG_CHI_PHI_HOA_DON,
                THUC_NHAN_CUA_KHACH = x.THUC_NHAN_CUA_KHACH,
            }).ToList();
            return result;
        }

        // GET: api/Api_DuyetBaoGia/5
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult GetBH_BAO_GIA(string id)
        {
            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            return Ok(bH_BAO_GIA);
        }

        // PUT: api/Api_DuyetBaoGia/5
        [ResponseType(typeof(void))]
        [Route("api/Api_DuyetBaoGia/{id}")]
        public IHttpActionResult PutBH_BAO_GIA(string id, BH_BAO_GIA bH_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_BAO_GIA.SO_BAO_GIA)
            {
                return BadRequest();
            }
            var query = db.BH_BAO_GIA.Where(x => x.SO_BAO_GIA == id).FirstOrDefault();
            if(query != null)
            {
                query.DA_DUYET = bH_BAO_GIA.DA_DUYET;
                query.NGUOI_DUYET = bH_BAO_GIA.NGUOI_DUYET;
                query.DA_HUY = bH_BAO_GIA.DA_HUY;
                query.LY_DO_HUY = bH_BAO_GIA.LY_DO_HUY;
            }

           // db.Entry(bH_BAO_GIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_BAO_GIAExists(id))
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

        // POST: api/Api_DuyetBaoGia
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult PostBH_BAO_GIA(BH_BAO_GIA bH_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_BAO_GIA.Add(bH_BAO_GIA);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_BAO_GIAExists(bH_BAO_GIA.SO_BAO_GIA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bH_BAO_GIA.SO_BAO_GIA }, bH_BAO_GIA);
        }

        // DELETE: api/Api_DuyetBaoGia/5
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult DeleteBH_BAO_GIA(string id)
        {
            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            db.BH_BAO_GIA.Remove(bH_BAO_GIA);
            db.SaveChanges();

            return Ok(bH_BAO_GIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_BAO_GIAExists(string id)
        {
            return db.BH_BAO_GIA.Count(e => e.SO_BAO_GIA == id) > 0;
        }
    }
}