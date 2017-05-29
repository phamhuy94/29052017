using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.ChuyenKho
{
    public class ChuyenKho
    {
        public string SO_CHUNG_TU { get; set; }
        public string NGAY_CHUNG_TU { get; set; }
        public string NGAY_HACH_TOAN { get; set; }
        public string DIEN_GIAI { get; set; }
        public string NGUOI_LAP_PHIEU { get; set; }
        public string TRUC_THUOC { get; set; }
        public List<ChiTietChuyenKho> ChiTiet { set; get; }
    }
}