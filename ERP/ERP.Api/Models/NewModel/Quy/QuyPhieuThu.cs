using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.Quy
{
    public class QuyPhieuThu
    {

        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string MA_DOI_TUONG { get; set; }
        public string LY_DO_NOP { get; set; }
        public string DIEN_GIAI_LY_DO_NOP { get; set; }
        public string NGUOI_NOP { get; set; }
        public string NHAN_VIEN_THU { get; set; }
        public decimal TONG_TIEN { get; set; }
        public string NGUOI_LAP_BIEU { get; set; }
        public string TRUC_THUOC { get; set; }
        public List<ChiTietQuyPhieuThu> ChiTietQPT { set; get; }
        public List<ThamChieu> ThamChieu { set; get; }
    }
}