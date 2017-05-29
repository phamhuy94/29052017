using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NhapKho
{
    public class NhapKho
    {
        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string LOAI_NHAP_KHO { get; set; }
        public string MA_DOI_TUONG { get; set; }
        public string NGUOI_GIAO_HANG { get; set; }
        public string DIEN_GIAI { get; set; }
        public string NHAN_VIEN_BAN_HANG { get; set; }
        public decimal TONG_TIEN { get; set; }
        public string TRUC_THUOC { get; set; }
        public string NGUOI_LAP_PHIEU { get; set; }
        public List<ChiTietNhapKho> ChiTiet { set; get; }
        public List<ThamChieu> ThamChieu { set; get; }
    }
}