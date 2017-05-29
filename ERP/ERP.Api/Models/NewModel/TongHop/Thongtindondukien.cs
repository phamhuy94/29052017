using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class ThongTinDonDuKien
    {
        public string MA_DU_KIEN { set; get; }
        public string MA_KHACH_HANG { set; get; }
        public string SALES_QUAN_LY { set; get; }
        public int? ID_LIEN_HE { set; get; }

        public string HO_VA_TEN { set; get; }
        public string NGUOI_LIEN_HE { set; get; }
        public string TEN_CONG_TY { set; get; }
    }
}