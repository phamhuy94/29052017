using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NganHang
{
    public class ChiTietThuePhieuChi
    {
        public string DIEN_GIAI_THUE { get; set; }
        public string TK_THUE_GTGT { get; set; }
        public int TIEN_THUE_GTGT { get; set; }
        public int CK_THUE_GTGT { get; set; }
        public int GIA_TRI_HHDV_CHUA_THUE { get; set; }
        public string NGAY_HD { get; set; }
        public string SO_HD { set; get; }
        public string MAU_SO_HD { set; get; }
        public string KY_HIEU_HD { set; get; }
        public string MA_NHA_CUNG_CAP { set; get; }
    }
}