using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    public class SalesPhuTrach
    {
        public int ID { set; get; }
        public int ID_LIEN_HE { set; get; }
        public string SALES_PHU_TRACH { set; get; }
        public string NGAY_BAT_DAU_PHU_TRACH { set; get; }
        public string NGAY_KET_THUC_PHU_TRACH { set; get; }
        public bool? TRANG_THAI { set; get; }

        public bool SALES_CU { set; get; }
        public bool SALES_MOI { set; get; }
        public string NGUOI_LIEN_HE { set; get; }
        public string EMAIL_CA_NHAN { set; get; }
        public string EMAIL_CONG_TY { set; get; }
        public string HO_VA_TEN { set; get; }
    }
}