using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class LienHeKH
    {
        public int ID_LIEN_HE { set; get; }
        public int ID { set; get; }
        public string SALES_PHU_TRACH { set; get; }
        public string HO_VA_TEN { set; get; }
        public DateTime NGAY_BAT_DAU_PHU_TRACH { set; get; }
        public DateTime? NGAY_KET_THUC_PHU_TRACH { set; get; }
        public bool? TRANG_THAI { set; get; }
        public string MA_KHACH_HANG { set; get; }
        public string NGUOI_LIEN_HE { set; get; }
        public string CHUC_VU { set; get; }
        public string FACEBOOK { set; get; }
        public string PHONG_BAN { set; get; }
        public string EMAIL { set; get; }
        public string GIOI_TINH { set; get; }
        public string SKYPE { set; get; }
        public string NGAY_SINH { set; get; }
        public string EMAIL_CA_NHAN { set; get; }
        public string EMAIL_CONG_TY { set; get; }
        public string SDT1 { set; get; }
        public string SDT2 { set; get; }
        public string GHI_CHU { set; get; }
        public string TEN_CONG_TY { set; get; }
        public bool SALES_MOI { set; get; }
        public string TINH_TRANG_LAM_VIEC { set; get; }
        public bool SALES_CU { set; get; }
    }
}