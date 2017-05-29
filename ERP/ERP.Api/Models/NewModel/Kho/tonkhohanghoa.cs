using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Api.Models.NewModel.Kho
{
    public class tonkhohanghoa
    {
        public string MA_HANG { get; set; }
        public string MA_CHUAN { get; set; }
        public string THONG_SO { get; set; }
        public string XUAT_XU { get; set; }
        public Decimal? GIA_LIST { get; set; }
        public bool? DISCONTINUE { get; set; }
        public string MA_CHUYEN_DOI { get; set; }
        public int? SL_HOPLONG { get; set; }
        public int? SL_GIU { get; set; }
        public int? SL_KYGUI_DEN { get; set; }
        public int? SL_KYGUI_DI { get; set; }
        public int? SL_HANG { get; set; }
    }
}