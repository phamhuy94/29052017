using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.Quy
{
    public class ChiTietQuyPhieuThu
    {
        public string DIEN_GIAI { get; set; }
        public string LOAI_TIEN { get; set; }
        public int TY_GIA { get; set; }
        public string SO_TIEN { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string QUY_DOI { set; get; }
        public string DOI_TUONG { set; get; }
        public string TK_NGAN_HANG { set; get; }
    }
}