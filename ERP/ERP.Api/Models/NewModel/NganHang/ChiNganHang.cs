using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NganHang
{
    public class ChiNganHang
    {

        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string TAI_KHOAN_CHI { get; set; }
        public string MA_DOI_TUONG { get; set; }
        public string NOI_DUNG_THANH_TOAN { get; set; }
        public string DIEN_GIAI_NOI_DUNG_THANH_TOAN { get; set; }
        public string TAI_KHOAN_NHAN { get; set; }
        public string NHAN_VIEN_CHUYEN_KHOAN { get; set; }
        public decimal TONG_TIEN { get; set; }
        public string NGUOI_LAP_BIEU { get; set; }
        public string TRUC_THUOC { get; set; }
        public List<ChiTietHachToanPhieuChi> ChiTietHachToan { set; get; }
        public List<ChiTietThuePhieuChi> ChiTietThue { set; get; }
        public List<ThamChieu> ThamChieu { set; get; }
        public object ChiTietPCNH { get; internal set; }
    }
}