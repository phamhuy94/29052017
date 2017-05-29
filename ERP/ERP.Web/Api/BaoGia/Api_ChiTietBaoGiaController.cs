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
using ERP.Web.Models.NewModels;
using System.Web.Http.Results;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ERP.Web.Api.BaoGia
{
    public class Api_ChiTietBaoGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        decimal? tong_gia_tri_thuc_te_edit = 0;
        decimal tong_gia_tri_theo_hop_dong_edit = 0;
        decimal? tong_chi_phi_hoa_don_edit = 0;
        decimal? tong_khach_nhan_edit = 0;
        decimal? TONG_GIA_TRI_DON_HANG_THUC_TE = 0;
        decimal TONG_TIEN = 0;
        decimal? TONG_CHI_PHI_HOA_DON = 0;
        decimal? THUC_NHAN_CUA_KHACH = 0;
        decimal? TONG_GIA_TRI_CHENH_LECH = 0 ;
        decimal? TIEN_THUE_GTGT = 0 ;
        decimal? GIA_TRI_THUC_THU_TU_KHACH = 0 ;
        // GET: api/Api_ChiTietBaoGia
        [Route("api/Api_ChiTietBaoGia/CT_BAO_GIA/{so_bao_gia}")]
        public List<GetAll_ChiTietBaoGia_Result> CT_BAO_GIA(string so_bao_gia)
        {
            var query = db.Database.SqlQuery<GetAll_ChiTietBaoGia_Result>("GetAll_ChiTietBaoGia  @so_bao_gia, @ma_cong_ty", new SqlParameter("so_bao_gia", so_bao_gia),new SqlParameter("ma_cong_ty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_ChiTietBaoGia/5
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public IHttpActionResult GetBH_CT_BAO_GIA(int id)
        {
            BH_CT_BAO_GIA bH_CT_BAO_GIA = db.BH_CT_BAO_GIA.Find(id);
            if (bH_CT_BAO_GIA == null)
            {
                return NotFound();
            }

            return Ok(bH_CT_BAO_GIA);
        }

        // PUT: api/Api_ChiTietBaoGia/5
        [HttpPost]
        [Route("api/Api_ChiTietBaoGia/PutBH_CT_BAO_GIA")]
        public async Task<IHttpActionResult> PutBH_CT_BAO_GIA([FromBody] List<ChiTietBaoGia> bH_CT_BAO_GIA)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //try
            //{
            foreach (var item in bH_CT_BAO_GIA)
            {
                var baogia = db.BH_CT_BAO_GIA.Where(x => x.ID == item.ID).FirstOrDefault();
                if (baogia != null)
                {
                    baogia.MA_HANG = item.MA_HANG;
                    baogia.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                    baogia.TEN_HANG = item.TEN_HANG;
                    baogia.HANG_SP = item.HANG_SP;
                    baogia.ITEM_CODE = item.ITEM_CODE;
                    baogia.SO_LUONG = item.SO_LUONG;
                    baogia.DVT = item.DVT;
                    baogia.DON_GIA = item.DON_GIA;
                    baogia.THANH_TIEN = item.THANH_TIEN;
                    baogia.THANH_TIEN_NET = item.THANH_TIEN_NET;
                    baogia.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                    baogia.CHIET_KHAU = item.CHIET_KHAU;
                    baogia.GIA_LIST = item.GIA_LIST;
                    baogia.DON_GIA_NHAP = item.DON_GIA_NHAP;
                    baogia.HE_SO_LOI_NHUAN = item.HE_SO_LOI_NHUAN;
                    baogia.DON_GIA_BAO_DI_NET = item.DON_GIA_BAO_DI_NET;
                    baogia.CM = item.CM;
                    baogia.DON_GIA_MOI = item.DON_GIA_MOI;
                    baogia.THUE_TNDN = item.THUE_TNDN;
                    baogia.TIEN_THUE_TNDN = item.TIEN_THUE_TNDN;
                    baogia.KHACH_NHAN_DUOC = item.KHACH_NHAN_DUOC;
                    baogia.GHI_CHU = item.GHI_CHU;
                }
                else if (baogia == null)

                {
                    BH_CT_BAO_GIA newbaogia = new BH_CT_BAO_GIA();
                    newbaogia.SO_BAO_GIA = item.SO_BAO_GIA;
                    newbaogia.MA_HANG = item.MA_HANG;
                    newbaogia.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                    newbaogia.TEN_HANG = item.TEN_HANG;
                    newbaogia.HANG_SP = item.HANG_SP;
                    newbaogia.ITEM_CODE = item.ITEM_CODE;
                    newbaogia.SO_LUONG = item.SO_LUONG;
                    newbaogia.DVT = item.DVT;
                    newbaogia.DON_GIA = item.DON_GIA;
                    newbaogia.THANH_TIEN = item.THANH_TIEN;
                    newbaogia.THANH_TIEN_NET = item.THANH_TIEN_NET;
                    newbaogia.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                    newbaogia.CHIET_KHAU = item.CHIET_KHAU;
                    newbaogia.GIA_LIST = item.GIA_LIST;
                    newbaogia.DON_GIA_NHAP = item.DON_GIA_NHAP;
                    newbaogia.HE_SO_LOI_NHUAN = item.HE_SO_LOI_NHUAN;
                    newbaogia.DON_GIA_BAO_DI_NET = item.DON_GIA_BAO_DI_NET;
                    newbaogia.CM = item.CM;
                    newbaogia.DON_GIA_MOI = item.DON_GIA_MOI;
                    newbaogia.THUE_TNDN = item.THUE_TNDN;
                    newbaogia.TIEN_THUE_TNDN = item.TIEN_THUE_TNDN;
                    newbaogia.KHACH_NHAN_DUOC = item.KHACH_NHAN_DUOC;
                    newbaogia.GHI_CHU = item.GHI_CHU;
                    db.BH_CT_BAO_GIA.Add(newbaogia);
                }

            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw;

            }
            //return this.CreatedAtRoute("GetNH_NTTK", new { id = nH_NTTK.SO_CHUNG_TU }, nH_NTTK);
            return Ok(bH_CT_BAO_GIA);
        }



        // POST: api/Api_ChiTietBaoGia
        [Route("api/ApiChiTietBaoGia/PostKH_LIEN_HE")]
        public IHttpActionResult PostKH_LIEN_HE(List<ChiTietBaoGia> lh)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            foreach (var item in lh)
            {
                BH_CT_BAO_GIA lienhe = new BH_CT_BAO_GIA();
                lienhe.SO_BAO_GIA = item.SO_BAO_GIA;
                lienhe.MA_HANG = item.MA_HANG;
                lienhe.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                lienhe.TEN_HANG = item.TEN_HANG;
                lienhe.HANG_SP = item.HANG_SP;
                lienhe.ITEM_CODE = item.ITEM_CODE;
                lienhe.SO_LUONG = item.SO_LUONG;
                lienhe.DVT = item.DVT;
                lienhe.DON_GIA = item.DON_GIA;
                lienhe.THANH_TIEN = item.THANH_TIEN;
                lienhe.THANH_TIEN_NET = item.THANH_TIEN_NET;
                lienhe.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                lienhe.CHIET_KHAU = item.CHIET_KHAU;
                lienhe.GIA_LIST = item.GIA_LIST;
                lienhe.DON_GIA_NHAP = item.DON_GIA_NHAP;
                lienhe.HE_SO_LOI_NHUAN = item.HE_SO_LOI_NHUAN;
                lienhe.DON_GIA_BAO_DI_NET = item.DON_GIA_BAO_DI_NET;
                lienhe.CM = item.CM;
                lienhe.DON_GIA_MOI = item.DON_GIA_MOI;
                lienhe.THUE_TNDN = item.THUE_TNDN;
                lienhe.TIEN_THUE_TNDN = item.TIEN_THUE_TNDN;
                lienhe.KHACH_NHAN_DUOC = item.KHACH_NHAN_DUOC;
                lienhe.GHI_CHU = item.GHI_CHU;
                db.BH_CT_BAO_GIA.Add(lienhe);                                   
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
               
                    throw;
            }

            return Ok("Thành Công");
        }

        public string GenerateSoBaoGia()
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
            string prefixNumber = "BG" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.BH_BAO_GIA where nhapkho.SO_BAO_GIA.Contains(prefixNumber) select nhapkho.SO_BAO_GIA).Max();


            if (SoChungTu == null)
            {
                return "BG" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "BG" + year + month + day + result;
        }

        // DELETE: api/Api_ChiTietBaoGia/5


        // Tach bao gia
        [HttpPost]
        [Route("api/Api_ChiTietBaoGia/TachBaoGia/{SoBaoGia}")]
        public IHttpActionResult DeleteCT_BAO_GIA(String SoBaoGia, List<ChiTietBaoGia> chitiet)
        {
            var thongtinchung = db.BH_BAO_GIA.Where(x => x.SO_BAO_GIA == SoBaoGia).FirstOrDefault();

            for (var i = 0; i < chitiet.Count; i++) {
                tong_gia_tri_thuc_te_edit = chitiet[i].THANH_TIEN_NET + tong_gia_tri_thuc_te_edit;
                tong_gia_tri_theo_hop_dong_edit = chitiet[i].THANH_TIEN + tong_gia_tri_theo_hop_dong_edit;
                tong_chi_phi_hoa_don_edit = chitiet[i].TIEN_THUE_TNDN + tong_chi_phi_hoa_don_edit;
                tong_khach_nhan_edit = chitiet[i].KHACH_NHAN_DUOC + tong_khach_nhan_edit;
            }
            TONG_GIA_TRI_DON_HANG_THUC_TE = tong_gia_tri_thuc_te_edit;
            TONG_TIEN = tong_gia_tri_theo_hop_dong_edit;
            TONG_CHI_PHI_HOA_DON = tong_chi_phi_hoa_don_edit;
            THUC_NHAN_CUA_KHACH = tong_khach_nhan_edit;
            TONG_GIA_TRI_CHENH_LECH = tong_gia_tri_theo_hop_dong_edit - tong_gia_tri_thuc_te_edit;
            TIEN_THUE_GTGT = tong_gia_tri_theo_hop_dong_edit * (Convert.ToDecimal( thongtinchung.THUE_SUAT_GTGT) / 100);
            GIA_TRI_THUC_THU_TU_KHACH = tong_gia_tri_thuc_te_edit + tong_chi_phi_hoa_don_edit + thongtinchung.TIEN_THUE_GTGT;

            thongtinchung.TONG_GIA_TRI_DON_HANG_THUC_TE = thongtinchung.TONG_GIA_TRI_DON_HANG_THUC_TE - TONG_GIA_TRI_DON_HANG_THUC_TE;
            thongtinchung.TONG_TIEN = thongtinchung.TONG_TIEN - TONG_TIEN;
            thongtinchung.TONG_CHI_PHI_HOA_DON = thongtinchung.TONG_CHI_PHI_HOA_DON - TONG_CHI_PHI_HOA_DON;
            thongtinchung.THUC_NHAN_CUA_KHACH = thongtinchung.THUC_NHAN_CUA_KHACH - THUC_NHAN_CUA_KHACH;
            thongtinchung.TONG_GIA_TRI_CHENH_LECH = thongtinchung.TONG_GIA_TRI_CHENH_LECH - TONG_GIA_TRI_CHENH_LECH;
            thongtinchung.TIEN_THUE_GTGT = thongtinchung.TIEN_THUE_GTGT - TIEN_THUE_GTGT;
            thongtinchung.GIA_TRI_THUC_THU_TU_KHACH = thongtinchung.GIA_TRI_THUC_THU_TU_KHACH - GIA_TRI_THUC_THU_TU_KHACH;


            db.SaveChanges();

            foreach (var item in chitiet)
            {
                var xoabaogia = db.BH_CT_BAO_GIA.Where(x => x.ID == item.ID).FirstOrDefault();
                if (xoabaogia != null)
                {
                    db.BH_CT_BAO_GIA.Remove(xoabaogia);
                }                   
            }
            db.SaveChanges();

            BH_BAO_GIA baogia = new BH_BAO_GIA();
            baogia.SO_BAO_GIA = GenerateSoBaoGia();
            baogia.NGAY_BAO_GIA = DateTime.Today.Date;
            baogia.MA_DU_KIEN = thongtinchung.MA_DU_KIEN;
            baogia.MA_KHACH_HANG = thongtinchung.MA_KHACH_HANG;
            baogia.LIEN_HE_KHACH_HANG = thongtinchung.LIEN_HE_KHACH_HANG;
            baogia.PHUONG_THUC_THANH_TOAN = thongtinchung.PHUONG_THUC_THANH_TOAN;
            baogia.HAN_THANH_TOAN = thongtinchung.HAN_THANH_TOAN;
            baogia.HIEU_LUC_BAO_GIA = thongtinchung.HIEU_LUC_BAO_GIA;
            baogia.DIEU_KHOAN_THANH_TOAN = thongtinchung.DIEU_KHOAN_THANH_TOAN;
            baogia.PHI_VAN_CHUYEN = thongtinchung.PHI_VAN_CHUYEN;
            baogia.SALES_BAO_GIA = thongtinchung.SALES_BAO_GIA;
            baogia.TRUC_THUOC = thongtinchung.TRUC_THUOC;
            baogia.DANG_CHO_PHAN_HOI = thongtinchung.DANG_CHO_PHAN_HOI;
            baogia.THUE_SUAT_GTGT = thongtinchung.THUE_SUAT_GTGT;

            for (var i = 0; i < chitiet.Count; i++)
            {
                tong_gia_tri_thuc_te_edit = chitiet[i].THANH_TIEN_NET + tong_gia_tri_thuc_te_edit;
                tong_gia_tri_theo_hop_dong_edit = chitiet[i].THANH_TIEN + tong_gia_tri_theo_hop_dong_edit;
                tong_chi_phi_hoa_don_edit = chitiet[i].TIEN_THUE_TNDN + tong_chi_phi_hoa_don_edit;
                tong_khach_nhan_edit = chitiet[i].KHACH_NHAN_DUOC + tong_khach_nhan_edit;
            }
            baogia.TONG_GIA_TRI_DON_HANG_THUC_TE = tong_gia_tri_thuc_te_edit;
            baogia.TONG_TIEN = tong_gia_tri_theo_hop_dong_edit;
            baogia.TONG_CHI_PHI_HOA_DON = tong_chi_phi_hoa_don_edit;
            baogia.THUC_NHAN_CUA_KHACH = tong_khach_nhan_edit;
            baogia.TONG_GIA_TRI_CHENH_LECH = tong_gia_tri_theo_hop_dong_edit - tong_gia_tri_thuc_te_edit;
            baogia.TIEN_THUE_GTGT = tong_gia_tri_theo_hop_dong_edit * (Convert.ToDecimal(thongtinchung.THUE_SUAT_GTGT) / 100);
            baogia.GIA_TRI_THUC_THU_TU_KHACH = tong_gia_tri_thuc_te_edit + tong_chi_phi_hoa_don_edit + thongtinchung.TIEN_THUE_GTGT + tong_khach_nhan_edit;

            db.BH_BAO_GIA.Add(baogia);
            db.SaveChanges();

            foreach (var item in chitiet)
            {
                BH_CT_BAO_GIA lienhe = new BH_CT_BAO_GIA();
                lienhe.SO_BAO_GIA = baogia.SO_BAO_GIA;
                lienhe.MA_HANG = item.MA_HANG;
                lienhe.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                lienhe.TEN_HANG = item.TEN_HANG;
                lienhe.HANG_SP = item.HANG_SP;
                lienhe.ITEM_CODE = item.ITEM_CODE;
                lienhe.SO_LUONG = item.SO_LUONG;
                lienhe.DVT = item.DVT;
                lienhe.DON_GIA = item.DON_GIA;
                lienhe.THANH_TIEN = item.THANH_TIEN;
                lienhe.THANH_TIEN_NET = item.THANH_TIEN_NET;
                lienhe.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                lienhe.CHIET_KHAU = item.CHIET_KHAU;
                lienhe.GIA_LIST = item.GIA_LIST;
                lienhe.DON_GIA_NHAP = item.DON_GIA_NHAP;
                lienhe.HE_SO_LOI_NHUAN = item.HE_SO_LOI_NHUAN;
                lienhe.DON_GIA_BAO_DI_NET = item.DON_GIA_BAO_DI_NET;
                lienhe.CM = item.CM;
                lienhe.DON_GIA_MOI = item.DON_GIA_MOI;
                lienhe.THUE_TNDN = item.THUE_TNDN;
                lienhe.TIEN_THUE_TNDN = item.TIEN_THUE_TNDN;
                lienhe.KHACH_NHAN_DUOC = item.KHACH_NHAN_DUOC;
                lienhe.GHI_CHU = item.GHI_CHU;
                db.BH_CT_BAO_GIA.Add(lienhe);
            }
            db.SaveChanges();


            return Ok(baogia.SO_BAO_GIA);
        }

        //Copy báo giá mới

        [Route("api/Api_ChiTietBaoGia/CopyNewBaoGia")]
        public IHttpActionResult PostBH_BAO_GIA(ChiTietBaoGia bH_BAO_GIA)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            BH_BAO_GIA baogia = new BH_BAO_GIA();
            baogia.SO_BAO_GIA = GenerateSoBaoGia();
            baogia.NGAY_BAO_GIA = DateTime.Today.Date;
            baogia.MA_DU_KIEN = bH_BAO_GIA.MA_DU_KIEN;
            baogia.MA_KHACH_HANG = bH_BAO_GIA.MA_KHACH_HANG;
            baogia.LIEN_HE_KHACH_HANG = bH_BAO_GIA.LIEN_HE_KHACH_HANG;
            baogia.PHUONG_THUC_THANH_TOAN = bH_BAO_GIA.PHUONG_THUC_THANH_TOAN;
            baogia.HAN_THANH_TOAN = bH_BAO_GIA.HAN_THANH_TOAN;
            baogia.HIEU_LUC_BAO_GIA = bH_BAO_GIA.HIEU_LUC_BAO_GIA;
            baogia.DIEU_KHOAN_THANH_TOAN = bH_BAO_GIA.DIEU_KHOAN_THANH_TOAN;
            baogia.PHI_VAN_CHUYEN = bH_BAO_GIA.PHI_VAN_CHUYEN;
            baogia.TONG_TIEN = bH_BAO_GIA.TONG_TIEN;
            baogia.TONG_GIA_TRI_DON_HANG_THUC_TE = bH_BAO_GIA.TONG_GIA_TRI_DON_HANG_THUC_TE;
            baogia.GIA_TRI_THUC_THU_TU_KHACH = bH_BAO_GIA.GIA_TRI_THUC_THU_TU_KHACH;
            baogia.TONG_GIA_TRI_CHENH_LECH = bH_BAO_GIA.TONG_GIA_TRI_CHENH_LECH;
            baogia.TONG_CHI_PHI_HOA_DON = bH_BAO_GIA.TONG_CHI_PHI_HOA_DON;
            baogia.THUC_NHAN_CUA_KHACH = bH_BAO_GIA.THUC_NHAN_CUA_KHACH;
            baogia.DA_DUYET = bH_BAO_GIA.DA_DUYET;
            baogia.NGUOI_DUYET = bH_BAO_GIA.NGUOI_DUYET;
            baogia.DA_TRUNG = bH_BAO_GIA.DA_TRUNG;
            baogia.DA_HUY = bH_BAO_GIA.DA_HUY;
            baogia.LY_DO_HUY = bH_BAO_GIA.LY_DO_HUY;
            baogia.SALES_BAO_GIA = bH_BAO_GIA.SALES_BAO_GIA;
            baogia.TRUC_THUOC = bH_BAO_GIA.TRUC_THUOC;
            baogia.DANG_CHO_PHAN_HOI = bH_BAO_GIA.DANG_CHO_PHAN_HOI;
            baogia.THUE_SUAT_GTGT = bH_BAO_GIA.THUE_SUAT_GTGT;
            baogia.TIEN_THUE_GTGT = bH_BAO_GIA.TIEN_THUE_GTGT;
            db.BH_BAO_GIA.Add(baogia);
            db.SaveChanges();

            foreach (var item in bH_BAO_GIA.Chitiet)
            {
                BH_CT_BAO_GIA lienhe = new BH_CT_BAO_GIA();
                lienhe.SO_BAO_GIA = baogia.SO_BAO_GIA;
                lienhe.MA_HANG = item.MA_HANG;
                lienhe.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                lienhe.TEN_HANG = item.TEN_HANG;
                lienhe.HANG_SP = item.HANG_SP;
                lienhe.ITEM_CODE = item.ITEM_CODE;
                lienhe.SO_LUONG = item.SO_LUONG;
                lienhe.DVT = item.DVT;
                lienhe.DON_GIA = item.DON_GIA;
                lienhe.THANH_TIEN = item.THANH_TIEN;
                lienhe.THANH_TIEN_NET = item.THANH_TIEN_NET;
                lienhe.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                lienhe.CHIET_KHAU = item.CHIET_KHAU;
                lienhe.GIA_LIST = item.GIA_LIST;
                lienhe.DON_GIA_NHAP = item.DON_GIA_NHAP;
                lienhe.HE_SO_LOI_NHUAN = item.HE_SO_LOI_NHUAN;
                lienhe.DON_GIA_BAO_DI_NET = item.DON_GIA_BAO_DI_NET;
                lienhe.CM = item.CM;
                lienhe.DON_GIA_MOI = item.DON_GIA_MOI;
                lienhe.THUE_TNDN = item.THUE_TNDN;
                lienhe.TIEN_THUE_TNDN = item.TIEN_THUE_TNDN;
                lienhe.KHACH_NHAN_DUOC = item.KHACH_NHAN_DUOC;
                lienhe.GHI_CHU = item.GHI_CHU;
                db.BH_CT_BAO_GIA.Add(lienhe);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
              throw;
            }

            return Ok(baogia.SO_BAO_GIA);
        }


        // Xóa 1 chi tiết báo giá
        [Route("api/Api_ChiTietBaoGia/XoaCT_BAO_GIA/{id}")]
        public IHttpActionResult DeleteBH_CT_BAO_GIA(int id)
        {
            BH_CT_BAO_GIA chitietbg = db.BH_CT_BAO_GIA.Find(id);
            if (chitietbg == null)
            {
                return NotFound();
            }

            db.BH_CT_BAO_GIA.Remove(chitietbg);
            db.SaveChanges();

            return Ok(chitietbg);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_CT_BAO_GIAExists(int id)
        {
            return db.BH_CT_BAO_GIA.Count(e => e.ID == id) > 0;
        }
    }
}