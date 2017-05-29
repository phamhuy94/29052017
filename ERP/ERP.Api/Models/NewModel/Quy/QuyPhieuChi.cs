using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.Quy
{
    public class QuyPhieuChi
    {
        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string MA_DOI_TUONG { get; set; }
        public string NHAN_VIEN_MUA_HANG { get; set; }
        public string LY_DO_CHI { get; set; }
        public string DIEN_GIAI_LY_DO_CHI { get; set; }
        public string NGUOI_NHAN { get; set; }
        public decimal TONG_TIEN { get; set; }
        public string NGUOI_LAP_BIEU { get; set; }
        public string TRUC_THUOC { get; set; }
        public List<ChiTietQuyPhieuChi> ChiTietQPC { set; get; }
        public List<ThamChieu> ThamChieu { set; get; }
    }
}