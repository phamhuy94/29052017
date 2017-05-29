using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.XuatKho
{
    public class XuatKhoBH
    {
        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string LOAI_XUAT_KHO { get; set; }
        public string NGUOI_NHAN { get; set; }
        public string LY_DO_XUAT { get; set; }
        public string NHAN_VIEN_BAN_HANG { get; set; }
        public string KEM_THEO { get; set; }
        public decimal TONG_TIEN { get; set; }
        public string NGUOI_LAP_PHIEU { get; set; }
        public string TRUC_THUOC { get; set; }
        public string KHACH_HANG { set; get; }
        public string TEN_KHACH_HANG { set; get; }
        public string PHIEU_BAN_HANG { set; get; }
        public List<ChiTietXuatKhoBH> ChiTiet { set; get; }
        public List<ThamChieu> ThamChieu { set; get; }
    }
}