using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.NhapKho
{
    public class ChiTietNhapKho
    {

        public string MA_HANG { get; set; }
        public string MA_KHO { get; set; }
        public string DON_GIA { get; set; }
        public string SO_LUONG { get; set; }
        public string DVT { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string TK_KHO { set; get; }
    }
}