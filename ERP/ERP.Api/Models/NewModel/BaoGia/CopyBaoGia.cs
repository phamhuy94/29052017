using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class CopyBaoGia
    {
        public int ID_LIEN_HE { set; get; }
        public string SALES_QUAN_LY { set; get; }
        public int ID { set; get; }
        public string SO_BAO_GIA { set; get; }
        public DateTime NGAY_BAO_GIA { set; get; }
        public string MA_DU_KIEN { set; get; }
        public string MA_KHACH_HANG { set; get; }
        public int LIEN_HE_KHACH_HANG { set; get; }
        public string PHUONG_THUC_THANH_TOAN { set; get; }
        public string HAN_THANH_TOAN { set; get; }
        public int? HIEU_LUC_BAO_GIA { set; get; }
        public string DIEU_KHOAN_THANH_TOAN { set; get; }
        public decimal? PHI_VAN_CHUYEN { set; get; }
        public decimal TONG_TIEN { set; get; }
        public bool? DA_DUYET { set; get; }
        public string NGUOI_DUYET { set; get; }
        public bool? DA_TRUNG { set; get; }
        public bool? DA_HUY { set; get; }
        public string LY_DO_HUY { set; get; }
        public string SALES_BAO_GIA { set; get; }
        public string TRUC_THUOC { set; get; }
        public string MA_HANG { set; get; }
        public string MA_DIEU_CHINH { set; get; }
        public string TEN_HANG { set; get; }
        public string HANG_SP { set; get; }
        public string ITEM_CODE { set; get; }

        public int SO_LUONG { set; get; }
        public string DVT { set; get; }
        public decimal DON_GIA { set; get; }
        public double CHIET_KHAU { set; get; }
        public decimal THANH_TIEN { set; get; }
        public decimal THANH_TIEN_NET { set; get; }
        public double? CK_VAT { set; get; }
        public decimal? TIEN_VAT { set; get; }
        public string THOI_GIAN_GIAO_HANG { set; get; }
        public string NGAY_GIAO_HANG { set; get; }
        public string DIA_DIEM_GIAO_HANG { set; get; }
        public string GHI_CHU { set; get; }
        public string HO_VA_TEN { set; get; }
        public string VAN_PHONG_GIAO_DICH { set; get; }
        public string DIA_CHI_XUAT_HOA_DON { set; get; }
        public string NGUOI_LIEN_HE { set; get; }
        public string TEN_CONG_TY { set; get; }
        public DateTime NGAY_TAO { set; get; }
        public decimal? GIA_LIST { set; get; }
        public decimal? DON_GIA_NHAP { set; get; }
        public float? HE_SO_LOI_NHUAN { set; get; }
        public float? THUE_TNDN { set; get; }
        public decimal? DON_GIA_BAO_DI_NET { set; get; }
        public decimal? CM { set; get; }
        public decimal? DON_GIA_MOI { set; get; }
        public decimal? TIEN_THUE_TNDN { set; get; }

        public decimal? CHIET_KHAU_CHO_KHACH { set; get; }
        public decimal? CHI_PHI_XU_LY_HOA_DON { set; get; }
        public decimal? TONG_CHI_PHI_XU_LY_HOA_DON { set; get; }
        public double? THUE_SUAT_GTGT { set; get; }
        public decimal? TIEN_THUE_GTGT { set; get; }
        public decimal? TONG_GIA_TRI_DON_HANG_THUC_TE { set; get; }
        public decimal? GIA_TRI_THUC_THU_TU_KHACH { set; get; }
        public decimal? TONG_GIA_TRI_CHENH_LECH { set; get; }
        public decimal? TONG_CHI_PHI_HOA_DON { set; get; }
        public decimal? THUC_NHAN_CUA_KHACH { set; get; }
        public decimal? KHACH_NHAN_DUOC { set; get; }
        public bool DANG_CHO_PHAN_HOI { set; get; }
    }
}