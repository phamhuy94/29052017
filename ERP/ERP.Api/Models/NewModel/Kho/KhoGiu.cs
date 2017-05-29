using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Api.Models.NewModel.Kho
{
    public class KhoGiu
    {
        public string MA_SO_PO { set; get; }
        public string SALES_GIU { get; set; }
        public string MA_KHACH_HANG { get; set; }
        public string NGAY_GIU { get; set; }
        public string MA_HANG { get; set; }
        public string NGAY_XUAT { get; set; }
        public int SL_GIU { get; set; }
        public Boolean GIU_PO { get; set; }
        public string GHI_CHU { get; set; }
        public string TRUC_THUOC { get; set; }
        public int ID_CT_PO { get; set; }
        public List<ChiTietKhoGiu> ChiTiet { set; get; }
        public List<TonKho> TonKho { set; get; }
    }
}