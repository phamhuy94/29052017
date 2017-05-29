using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    public class PurPhuTrach
    {
        public int ID { set; get; }
        public int ID_LIEN_HE { set; get; }
        public string PUR_PHU_TRACH { set; get; }
        public string NGAY_BAT_DAU_PHU_TRACH { set; get; }
        public string NGAY_KET_THUC_PHU_TRACH { set; get; }
        public bool TRANG_THAI { set; get; }
    }
}