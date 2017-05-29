using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.Quy
{
    public class ChiTietQuyPhieuChi
    {
        public string DIEN_GIAI { get; set; }
        public string LOAI_TIEN { get; set; }
        public int TY_GIA { get; set; }
        public int SO_TIEN { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string QUY_DOI { set; get; }
        public string MA_DOI_TUONG { set; get; }
        public string TK_NGAN_HANG { set; get; }
        public string DIEN_GIAI_THUE { set; get; }
        public string TK_THUE_GTGT { set; get; }
        public int TIEN_THUE_GTGT { set; get; }
        public int CK_THUE_GTGT { set; get; }
        public int GIA_TRI_HHDV_CHUA_THUE { set; get; }
        public string NGAY_HD { set; get; }
        public string SO_HD { set; get; }
        public string MAU_SO_HD { set; get; }
        public string KY_HIEU_HD { set; get; }
        public string MA_NHA_CUNG_CAP { set; get; }
    }
}