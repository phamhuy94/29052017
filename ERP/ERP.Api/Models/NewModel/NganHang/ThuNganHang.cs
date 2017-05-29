using ERP.Web.Models.NewModels.All;
using ERP.Web.Models.NewModels.XuatKho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NganHang
{
    public class ThuNganHang
    {

        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string MA_DOI_TUONG { get; set; }
        public string NOP_VAO_TAI_KHOAN { get; set; }
        public string LY_DO_THU { get; set; }
        public string DIEN_GIAI_LY_DO_THU { get; set; }
        public string NHAN_VIEN_THU { get; set; }
        public decimal TONG_TIEN { get; set; }
        public string NGUOI_LAP_BIEU { get; set; }
        public string TRUC_THUOC { get; set; }
        public List<ChiTietPhieuThuNH> ChiTietPTNH { set; get; }
        public List<ThamChieu> ThamChieu { set; get; }
    }
}
