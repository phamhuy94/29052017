using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Api.Models.NewModel.Kho
{
    public class ChiTietKhoGiu
    {

        public string SALES_GIU { get; set; }
        public string MA_KHACH_HANG { get; set; }
        public string NGAY_GIU { get; set; }
        public string MA_HANG { get; set; }
        public string NGAY_XUAT { get; set; }
        public int SL_GIU { get; set; }
        public Boolean GIU_PO { get; set; }
        public string GHI_CHU { get; set; }
        public string TRUC_THUOC { get; set; }

    }
}