using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class NCC_HL
    {
        public int ID { set; get; }
        public string MA_NHA_CUNG_CAP { set; get; }
        public string TEN_NHA_CUNG_CAP { set; get; }
        public string VAN_PHONG_GIAO_DICH { set; get; }
        public string DIA_CHI_XUAT_HOA_DON { set; get; }
        public string PHAN_LOAI_NCC { set; get; }
        public string MST { set; get; }
        public string EMAIL { set; get; }
        public string SDT { set; get; }
        public string FAX { set; get; }
        public string LOGO { set; get; }
        public string DANH_GIA { set; get; }
        public string WEBSITE { set; get; }
        public string DIEU_KHOAN_THANH_TOAN { set; get; }
        public int? SO_NGAY_DUOC_NO { set; get; }
        public int? SO_NO_TOI_DA { set; get; }
        public string GHI_CHU { set; get; }
        public string TEN_LOAI_NCC { set; get; }
        public string MA_LOAI_NCC { set; get; }
        public string MA_NHOM_HANG { set; get; }
        public string MA_HANG { set; get; }
        public string TEN_HANG { set; get; }
        public string CHUNG_LOAI_HANG { set; get; }
    }
}