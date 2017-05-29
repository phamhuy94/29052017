using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NganHang
{
    public class ChiTietHachToanPhieuChi
    {
    
        public string DIEN_GIAI { get; set; }
        public string LOAI_TIEN { get; set; }
        public int TY_GIA { get; set; }
        public string SO_TIEN { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string QUY_DOI { set; get; }
        public string MA_DOI_TUONG { set; get; }
        public string MA_NHA_CUNG_CAP { set; get; }
        public string DON_VI { set; get; }
    }
}
