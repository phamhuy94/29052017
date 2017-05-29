using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    public class Post
    {
        public int MA_BAI_VIET { set; get; }
        public string TIEU_DE_BAI_VIET { set; get; }
        public DateTime NGAY_DANG_BAI { set; get; }
        public string ANH_BAI_VIET { set; get; }
        public string NOI_DUNG_BAI_VIET { set; get; }
        public string NGUOI_DANG_BAI { set; get; }
        public string HO_VA_TEN { set; get; }
        public string TEN_DANH_MUC { set; get; }
        public string MA_DANH_MUC { set; get; }
        public string USERNAME { set; get; }
        public DateTime? NGAY_CAP_NHAT { set; get; }
    }
}