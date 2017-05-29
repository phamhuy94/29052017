using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NhapKho
{
    public class ChiTietNhapKhoMH
    {

        public string MA_HANG { get; set; }
        public string MA_KHO_CON { get; set; }
        public string DON_GIA_CHUA_VAT { get; set; }
        public string SL { get; set; }
        public string DVT { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string TK_KHO { set; get; }
        public string THANH_TIEN_CHUA_VAT { set; get; }
    }
}