using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class nhanvien
    {
        public string USERNAME { set; get; }
        public string HO_VA_TEN { set; get; }

        public string PASSWORD { set; get;}
        public string EMAIL { set; get; }
        public string SDT { set; get; }
        public string GIOI_TINH { set; get; }
        public string NGAY_SINH { set; get; }
        public string QUE_QUAN { set; get; }
        public string TRINH_DO_HOC_VAN { set; get; }

        public string AVATAR { set; get; }
        public string THANH_TICH_CONG_TAC { get; set; }

        public string CHUC_VU { set; get; }
        public string TEN_PHONG_BAN { set; get; }

        public string MA_PHONG_BAN { set; get; }

        public string MA_CONG_TY { set; get; }
       public bool? IS_ADMIN { set; get; }
        public bool ALLOWED { set; get; }
        public string LINH_VUC_CONG_TAC { set; get; }
    }
}