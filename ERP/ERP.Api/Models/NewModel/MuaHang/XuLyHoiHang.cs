using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class XuLyHoiHang
    {
        public int ID { set; get; }
        public string USERNAME { set; get; }
        public bool IS_ADMIN { set; get; }

        public int ID_YEU_CAU_HOI_GIA { set; get; }
        public string MA_HANG { set; get; }
        public string MA_CHUAN { set; get; }
        public int? SO_LUONG { set; get; }
        public string MA_NCC { set; get; }
        public decimal? GIA { set; get; }
        public string THOI_GIAN_CAP_HANG { set; get; }
        public DateTime NGAY_HOI_GIA { set; get; }
        public string GHI_CHU { set; get; }
        public string PUR_XU_LY { set; get; }
        public string TRUC_THUOC { set; get; }
        public string MA_KHACH_ORDER { set; get; }
    }
}