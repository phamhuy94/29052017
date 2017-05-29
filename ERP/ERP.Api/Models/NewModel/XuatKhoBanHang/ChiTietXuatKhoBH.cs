using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.XuatKho
{
    public class ChiTietXuatKhoBH
    {
        public string MA_HANG { get; set; }

        public string MA_DIEU_CHINH { get; set; }

        public string MA_KHO_CON { get; set; }
        public decimal DON_GIA { get; set; }
        public int SO_LUONG { get; set; }
        public string DVT { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string TK_HACH_TOAN_KHO { set; get; }
        public string DON_GIA_VON { set; get; }
    }
}