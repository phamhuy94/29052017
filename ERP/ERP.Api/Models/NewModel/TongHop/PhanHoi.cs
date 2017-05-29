using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class PhanHoi
    {
        public int ID { set; get; }
        public string NHAN_VIEN_PHAN_HOI { set; get; }
        public string NGAY_PHAN_HOI { set; get; }
        public string THONG_TIN_PHAN_HOI { set; get; }
        public bool THONG_TIN_PHAN_TOT { set; get; }
        public bool THONG_TIN_PHAN_HOI_TRUNG_BINH { set; get; }
        public bool THONG_TIN_PHAN_HOI_KHONG_TOT { set; get; }
        public bool THONG_TIN_PHAN_HOI_LUNG_TUNG { set; get; }
        public string NGUOI_DUYET { set; get; }
        public string NGAY_DUYET { set; get; }
        public string TINH_DIEM { set; get; }


    }
}