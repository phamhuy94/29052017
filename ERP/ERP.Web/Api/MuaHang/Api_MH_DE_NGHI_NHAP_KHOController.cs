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
using System.Text.RegularExpressions;
using ERP.Web.Models.NewModels.MuaHang;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.MuaHang
{
    public class Api_MH_DE_NGHI_NHAP_KHOController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        public string GenerateMaSoDeNghi()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string prefixNumber = "DN" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.MH_DE_NGHI_NHAP_KHO where nhapkho.MA_SO_DN.Contains(prefixNumber) select nhapkho.MA_SO_DN).Max();


            if (SoChungTu == null)
            {
                return "DN" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "DN" + year + month + day + result;
        }

        // GET: api/Api_MH_DE_NGHI_NHAP_KHO
        [Route("api/Api_MH_DE_NGHI_NHAP_KHO/GetAllMH_DeNghiNhapKho/")]
        public List<GetAll_DeNghiMuaHang_Result> GetAllMH_DeNghiNhapKho()
        {
            var data = db.Database.SqlQuery<GetAll_DeNghiMuaHang_Result>("GetAll_DeNghiMuaHang");
            return data.ToList();
        }


        // GET: api/Api_MH_DE_NGHI_NHAP_KHO/5
        public class GetMH_DeNghiNhapKho
        {
            public GetDeNghiMuaHang_Result mhdenghinhapkho { set; get; }
            public List<GetAll_ChiTiet_DeNghiNhapKho_Result> ctmhdenghinhapkho { set; get; }
        }
        
        [Route("api/Api_MH_DE_NGHI_NHAP_KHO/GetDetailMH_DeNghiNhapKho/{masodenghi}")]
        public GetMH_DeNghiNhapKho GetDetailMH_DeNghiNhapKho(string masodenghi)
        {

            //Lưu thông tin nhập kho
            GetMH_DeNghiNhapKho dnnk = new GetMH_DeNghiNhapKho();
            var query = db.Database.SqlQuery<GetDeNghiMuaHang_Result>("GetDeNghiMuaHang @masodn", new SqlParameter("masodn", masodenghi));
            var data = db.Database.SqlQuery<GetAll_ChiTiet_DeNghiNhapKho_Result>("GetAll_ChiTiet_DeNghiNhapKho @masodn", new SqlParameter("masodn", masodenghi));
            dnnk.mhdenghinhapkho = query.FirstOrDefault();
            dnnk.ctmhdenghinhapkho = data.ToList();
            return dnnk;

        }

        // PUT: api/Api_MH_DE_NGHI_NHAP_KHO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMH_DE_NGHI_NHAP_KHO(string id, MH_DE_NGHI_NHAP_KHO mH_DE_NGHI_NHAP_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mH_DE_NGHI_NHAP_KHO.MA_SO_DN)
            {
                return BadRequest();
            }

            db.Entry(mH_DE_NGHI_NHAP_KHO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MH_DE_NGHI_NHAP_KHOExists(id))
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

        // POST: api/Api_MH_DE_NGHI_NHAP_KHO
        // Them thong tin chung de nghi nhap kho
        [Route("api/Api_MH_DE_NGHI_NHAP_KHO/PostMH_DE_NGHI_NHAP_KHO")]
        public IHttpActionResult PostMH_DE_NGHI_NHAP_KHO(ChiTietPOMuaHang denghinhapkho)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            MH_DE_NGHI_NHAP_KHO newdenghi = new MH_DE_NGHI_NHAP_KHO();
            newdenghi.MA_SO_DN = GenerateMaSoDeNghi();
            newdenghi.NGAY_DN = DateTime.Today.Date;
            if(denghinhapkho.NGAY_VE_DU_KIEN != "")
            newdenghi.NGAY_VE_DU_KIEN =xlnt.Xulydatetime(denghinhapkho.NGAY_VE_DU_KIEN);
            newdenghi.NGUOI_DN = denghinhapkho.NGUOI_DN;
            newdenghi.MA_NCC = denghinhapkho.MA_NCC;
            newdenghi.ID_NGUOI_LIEN_HE = denghinhapkho.ID_NGUOI_LIEN_HE;
            newdenghi.MA_SO_PO = denghinhapkho.MA_SO_PO;
            newdenghi.HINH_THUC_THANH_TOAN = denghinhapkho.HINH_THUC_THANH_TOAN;
            newdenghi.HINH_THUC_VAN_CHUYEN = denghinhapkho.HINH_THUC_VAN_CHUYEN;
            newdenghi.THOI_HAN_THANH_TOAN = denghinhapkho.THOI_HAN_THANH_TOAN;
            newdenghi.DIEN_GIAI = denghinhapkho.DIEN_GIAI;
            newdenghi.TONG_TIEN_HANG = denghinhapkho.TONG_TIEN_HANG;
            newdenghi.THUE_GTGT = denghinhapkho.THUE_GTGT;
            newdenghi.TIEN_THUE_VAT = denghinhapkho.TIEN_THUE_VAT;
            newdenghi.TONG_TIEN_DA_BAO_GOM_VAT = denghinhapkho.TONG_TIEN_DA_BAO_GOM_VAT;
            newdenghi.TONG_TIEN_BANG_CHU = denghinhapkho.TONG_TIEN_BANG_CHU;
            db.MH_DE_NGHI_NHAP_KHO.Add(newdenghi);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MH_DE_NGHI_NHAP_KHOExists(denghinhapkho.MA_SO_DN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(newdenghi);
        }

        [Route("api/Api_MH_DE_NGHI_NHAP_KHO/ChiTietDeNghiNhapKho")]
        public IHttpActionResult ChiTietDeNghiNhapKho(List<ChiTietPOMuaHang> chitietdenghi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in chitietdenghi)
            {
                MH_CT_DE_NGHI_NHAP_KHO newpoct = new MH_CT_DE_NGHI_NHAP_KHO();
                newpoct.MA_SO_DN = item.MA_SO_DN;
                newpoct.MA_HANG = item.MA_HANG;
                newpoct.SL = item.SL;
                newpoct.DON_GIA_CHUA_VAT = item.DON_GIA_CHUA_VAT;
                newpoct.THANH_TIEN_CHUA_VAT = item.THANH_TIEN_CHUA_VAT;
                newpoct.TK_CO = item.TK_CO;
                newpoct.TK_NO = item.TK_NO;
                newpoct.TK_THUE = item.TK_THUE;
                newpoct.DIEN_GIAI_THUE = item.DIEN_GIAI_THUE;
                newpoct.THUE_GTGT = item.THUE_GTGT;
                newpoct.TIEN_THUE_GTGT = item.TIEN_THUE_GTGT;
                db.MH_CT_DE_NGHI_NHAP_KHO.Add(newpoct);
            }
            db.SaveChanges();

            var masoPO = chitietdenghi.FirstOrDefault();


            var query = db.MH_CT_DE_NGHI_NHAP_KHO.Where(x => x.MA_SO_DN == masoPO.MA_SO_DN).ToList();

            try
            {

            }
            catch (DbUpdateException)
            {

                throw;

            }

            return Ok(query);
        }

        // DELETE: api/Api_MH_DE_NGHI_NHAP_KHO/5
        [ResponseType(typeof(MH_DE_NGHI_NHAP_KHO))]
        public IHttpActionResult DeleteMH_DE_NGHI_NHAP_KHO(string id)
        {
            MH_DE_NGHI_NHAP_KHO mH_DE_NGHI_NHAP_KHO = db.MH_DE_NGHI_NHAP_KHO.Find(id);
            if (mH_DE_NGHI_NHAP_KHO == null)
            {
                return NotFound();
            }

            db.MH_DE_NGHI_NHAP_KHO.Remove(mH_DE_NGHI_NHAP_KHO);
            db.SaveChanges();

            return Ok(mH_DE_NGHI_NHAP_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MH_DE_NGHI_NHAP_KHOExists(string id)
        {
            return db.MH_DE_NGHI_NHAP_KHO.Count(e => e.MA_SO_DN == id) > 0;
        }
    }
}