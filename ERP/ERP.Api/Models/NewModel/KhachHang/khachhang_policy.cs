using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class khachhang_policy
    {
        public string MA_NHOM_HANG { set; get; }
        public string MA_NHOM_HANG_CHA { set; get; }
        public string MA_KHACH_HANG { set; get; }
        public decimal CK { set; get; }
        public decimal GIA_BAN { set; get; }
        public string NGAY_CAP_NHAT { set; get; }
        public string NGUOI_CAP_NHAT { set; get; }

        public string MARK_PHU_TRACH { set; get; }
        public string PURC_PHU_TRACH { set; get; }

    }
}