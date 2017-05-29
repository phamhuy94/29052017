using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class NHAN_VIEN_MODEL
    {
        public string USERNAME { set; get; }
        public string GIOI_TINH { set; get; }
        public string NGAY_SINH { set; get; }
        public string QUE_QUAN { set; get; }
        public string TRINH_DO_HOC_VAN { set; get; }
        public string MA_PHONG_BAN { set; get; }

        public string THANH_TICH_CONG_TAC { set; get; }
        public string LINH_VUC_CONG_TAC { set; get; }
    }
}