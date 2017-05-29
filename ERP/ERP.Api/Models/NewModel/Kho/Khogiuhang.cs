using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Api.Models.NewModel.Kho
{
    public class Khogiuhang
    {
        public string MA_GIU_KHO { set; get; }
        public string SALES_GIU { set; get; }
        public string MA_KHACH_HANG { set; get; }
        public string NGAY_GIU { set; get; }
        public string HO_VA_TEN { set; get; }
        public string TEN_CONG_TY { set; get; }
        public bool HUY_DON_GIU { set; get; }
        public bool GIU_PO { set; get; }
        public bool DON_DANG_XUAT { set; get; }
        public bool DON_DA_HOAN_THANH { set; get; }
        public string TRUC_THUOC { set; get; }


        public int ID { set; get; }
        public string NGAY_XUAT { set; get; }
        public string MA_HANG { set; get; }
        public int SL_GIU { set; get; }
        public string DVT { set; get; }
        public string XUAT_XU { set; get; }
        public decimal DON_GIA { set; get; }
        public decimal THANH_TIEN { set; get; }
        public bool DA_XUAT { set; get; }
        public string GHI_CHU { set; get; }
       
    }
}