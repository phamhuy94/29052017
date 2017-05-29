using ERP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.MuaHang
{
    public class ChiTietPOMuaHang
    {
        public int ID { set; get; }
        public string MA_SO_PO { set; get; }
        public string MA_HANG { set; get; }
        public int SL { set; get; }
        public decimal DON_GIA_CHUA_VAT { set; get; }
        public decimal THANH_TIEN_CHUA_VAT { set; get; }
        public string THOI_GIAN_GIAO_HANG { set; get; }
        public string GHI_CHU { set; get; }
        public bool DA_NHAP_KHO { set; get; }
        public decimal GIA_BAN_RA { set; get; }
        public int ID_BAN_HANG { set; get; }

        public GetAll_ThongTinChungDonHangPO_MuaHang_Result ChungPO { get; set; }
        public List<GetAll_ChiTiet_DonHangPO_MuaHang_Result> ChiTietPO { get; set; }

        public string TK_NO { set; get; }
        public string TK_CO { set; get; }
        public string TK_THUE { set; get; }
        public decimal THUE_GTGT { set; get; }
        public decimal TIEN_THUE_GTGT { set; get; }
        public string DIEN_GIAI_THUE { set; get; }
        public string MA_SO_DN { set; get; }

        public System.DateTime NGAY_DN { get; set; }
        public string NGAY_VE_DU_KIEN { get; set; }
        public string NGUOI_DN { get; set; }
        public string MA_NCC { get; set; }
        public int ID_NGUOI_LIEN_HE { get; set; }
        public string HINH_THUC_VAN_CHUYEN { get; set; }
        public string HINH_THUC_THANH_TOAN { get; set; }
        public string THOI_HAN_THANH_TOAN { get; set; }
        public string DIEN_GIAI { get; set; }
        public decimal TONG_TIEN_HANG { get; set; }
        public decimal TIEN_THUE_VAT { get; set; }
        public decimal TONG_TIEN_DA_BAO_GOM_VAT { get; set; }
        public string TONG_TIEN_BANG_CHU { get; set; }
        public string PHIEU_NHAP_KHO { get; set; }
    }
}