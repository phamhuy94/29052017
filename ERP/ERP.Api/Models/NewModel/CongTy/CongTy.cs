using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    public class CongTy
    {
        public string MA_CONG_TY { set; get; }
        public string TEN_CONG_TY { set; get; }
        public string NGAY_THANH_LAP { set; get; }
        public string EMAIL { set; get; }
        public string FAX { set; get; }
        public string SDT { set; get; }
        public string MST { set; get; }

        public string LOGO { set; get; }
        public string DIA_CHI { get; set; }

        public string DIA_CHI_XUAT_HOA_DON { set; get; }
        public string CONG_TY_ME { set; get; }

        public string CAP_TO_CHUC { set; get; }
        public string GHI_CHU { get; set; }

    }
}
