using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.Kho
{
    public class Api_XuatNhapKhoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        List<Search_SearchByType_Result> result = new List<Search_SearchByType_Result>();
        List<GetChungTuFromDoiTuong_Result> resultDoiTuong = new List<GetChungTuFromDoiTuong_Result>();
        List<GetChungTu_ByMa_Result> resulByMa = new List<GetChungTu_ByMa_Result>();
        List<GetAll_DS_PhieuXuatKho_NoDate_Result> resultDSXuatKho = new List<GetAll_DS_PhieuXuatKho_NoDate_Result>();
        List<GetChungTuTra_Result> resultCTTra = new List<GetChungTuTra_Result>();
        List<GetChungTuTra_ByKhachHang_Result> resultCTTraByKhach = new List<GetChungTuTra_ByKhachHang_Result>();
        List<GetAll_PhieuNhapKho_Result> resultDSNhap = new List<GetAll_PhieuNhapKho_Result>();
        List<Prod_KHO_GetHHTonKho_Result> resultHH = new List<Prod_KHO_GetHHTonKho_Result>();
        #region "SearchByType"


        public class DataCondition
        {
            public string GiaTriChungTu { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
        }
        public class DataDSXuatKho
        {
            public string tungay { get; set; }
            public string denngay { get; set; }
        }
        public class DataCTByKH
        {
            public string makh { get; set; }
            public string tungay { get; set; }
            public string denngay { get; set; }
        }
        [Route("api/Api_XuatNhapKho/SearchByTypeWithDate")]
        public List<Search_SearchByType_Result> SearchByTypeWithDate(DataCondition data)
        {
            if (data.ToTime == "" && data.FromTime == "")
            {
                var query = db.Database.SqlQuery<Search_SearchByType_Result>("Search_SearchByType @LoaiChungTu,@macongty", new SqlParameter("LoaiChungTu", data.GiaTriChungTu), new SqlParameter("macongty", "HOPLONG"));
                result = query.ToList();
            }
            else
            {
                DateTime FromDate = xlnt.Xulydatetime(data.FromTime);
                DateTime ToDate = xlnt.Xulydatetime(data.ToTime);
                var query = db.Database.SqlQuery<Search_SearchByType_Result>("Search_SearchByTypeWithDate @LoaiChungTu,@FromDate,@ToDate, @macongty", new SqlParameter("LoaiChungTu", data.GiaTriChungTu), new SqlParameter("FromDate", FromDate), new SqlParameter("ToDate", ToDate), new SqlParameter("macongty", "HOPLONG"));
                result = query.ToList();
            }
            return result;
        }

        #endregion


        #region "Search by Object"
        [Route("api/Api_XuatNhapKho/SearchByDoiTuongWithDate")]
        public List<GetChungTuFromDoiTuong_Result> SearchByDoiTuongWithDate(DataCondition data)
        {

            if (data.FromTime == "" && data.ToTime == "")
            {
                var query = db.Database.SqlQuery<GetChungTuFromDoiTuong_Result>("GetChungTuFromDoiTuong @MaDoiTuong,@macongty", new SqlParameter("MaDoiTuong", data.GiaTriChungTu), new SqlParameter("macongty", "HOPLONG"));
                resultDoiTuong = query.ToList();
            }
            else

            {
                DateTime FromDate = xlnt.Xulydatetime(data.FromTime);
                DateTime ToDate = xlnt.Xulydatetime(data.ToTime);
                var query = db.Database.SqlQuery<GetChungTuFromDoiTuong_Result>("GetChungTuFromDoiTuong_WithDate @MaDoiTuong,@FromDate,@ToDate, @macongty", new SqlParameter("MaDoiTuong", data.GiaTriChungTu), new SqlParameter("FromDate", FromDate), new SqlParameter("ToDate", ToDate), new SqlParameter("macongty", "HOPLONG"));
                resultDoiTuong = query.ToList();
            }

            return resultDoiTuong;
        }
        #endregion


        #region "Get Chung tu theo ma"

        [Route("api/Api_XuatNhapKho/GetbyMa/{data}")]
        public List<GetChungTu_ByMa_Result> GetbyMa(string data)
        {
            var query = db.Database.SqlQuery<GetChungTu_ByMa_Result>("GetChungTu_ByMa @sochungtu,@macongty", new SqlParameter("sochungtu", data), new SqlParameter("macongty", "HOPLONG"));
            resulByMa = query.ToList();
            return resulByMa;
        }

        #endregion


        #region "Get All Doi Tuong"
        [HttpPost]
        [Route("api/Api_XuatNhapKho/GetAllDoiTuong/{mdt}")]
        public List<GetAllDoiTuong_Result> GetAllDoiTuong(string mdt)
        {
            var query = db.Database.SqlQuery<GetAllDoiTuong_Result>("GetAllDoiTuong @macongty, @tendoituong", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("tendoituong", mdt));
            var resultAllDT = query.ToList();
            return resultAllDT;
           
        }
        #endregion


        #region "Get All Danh Sach Phieu Xuat Kho"
        [HttpPost]
        [Route("api/Api_XuatNhapKho/GetAllDSPhieuXuatKho/{sotrang}")]

        public List<GetAll_DS_PhieuXuatKho_NoDate_Result> GetAllDSPhieuXuatKho(int sotrang, DataDSXuatKho data)

        {
            if (data.tungay == "" && data.denngay == "")
            {
                var query = db.Database.SqlQuery<GetAll_DS_PhieuXuatKho_NoDate_Result>("GetAll_DS_PhieuXuatKho_NoDate @macongty,@sotrang", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("sotrang", sotrang));
                resultDSXuatKho = query.ToList();
            }
            else
            {
                DateTime FromDate = xlnt.Xulydatetime(data.tungay);
                DateTime ToDate = xlnt.Xulydatetime(data.denngay);
                var query = db.Database.SqlQuery<GetAll_DS_PhieuXuatKho_NoDate_Result>("GetAll_DS_PhieuXuatKho @macongty, @tungay,@denngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("tungay", FromDate), new SqlParameter("denngay", ToDate));
                resultDSXuatKho = query.ToList();
            }
            return resultDSXuatKho;

        }

        #endregion

        #region "Get Chứng Từ trả"

        [Route("api/Api_XuatNhapKho/GetCTTra")]
        public List<GetChungTuTra_Result> GetCTTra()
        {
            var query = db.Database.SqlQuery<GetChungTuTra_Result>("GetChungTuTra");
            resultCTTra = query.ToList();
            return resultCTTra;
        }

        #endregion

        #region "Get Chứng Từ trả theo khách hàng"
        [HttpPost]
        [Route("api/Api_XuatNhapKho/GetCTTraByKhach")]
        public List<GetChungTuTra_ByKhachHang_Result> GetCTTraByKhach(DataCTByKH data)
        {
            if (data.tungay == "" && data.denngay == "")
            {
                var query = db.Database.SqlQuery<GetChungTuTra_ByKhachHang_Result>("GetChungTuTra_ByKhachHang @makhachhang", new SqlParameter("makhachhang", data.makh));
                resultCTTraByKhach = query.ToList();
            }
            else
            {
                DateTime FromDate = xlnt.Xulydatetime(data.tungay);
                DateTime ToDate = xlnt.Xulydatetime(data.denngay);
                var query = db.Database.SqlQuery<GetChungTuTra_ByKhachHang_Result>("GetChungTuTra_ByKhachHangWithDate @makhachhang,@tungay,@denngay", new SqlParameter("makhachhang", data.makh), new SqlParameter("tungay", FromDate), new SqlParameter("denngay", ToDate));
                resultCTTraByKhach = query.ToList();
            }
            return resultCTTraByKhach;
        }

        #endregion

        #region "Get All Danh Sach Phieu nhập Kho with date"
        [HttpPost]
        [Route("api/Api_XuatNhapKho/GetAllDSPhieuNhapKho/{sotrang}")]
        public List<GetAll_PhieuNhapKho_Result> GetAllDSPhieuNhapKho(int sotrang, DataDSXuatKho data)
        {
            if (data.tungay == "" && data.denngay == "")
            {
                var query = db.Database.SqlQuery<GetAll_PhieuNhapKho_Result>("GetAll_PhieuNhapKho @macongty,@sotrang", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("sotrang", sotrang));
                resultDSNhap = query.ToList();
            }
            else
            {
                DateTime FromDate = xlnt.Xulydatetime(data.tungay);
                DateTime ToDate = xlnt.Xulydatetime(data.denngay);
                var query = db.Database.SqlQuery<GetAll_PhieuNhapKho_Result>("GetAll_PhieuNhapKho_WithDate @macongty,@tungay,@denngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("tungay", FromDate), new SqlParameter("denngay", ToDate));
                resultDSNhap = query.ToList();
                return resultDSNhap;
            }
            return resultDSNhap;

        }
        #endregion

        #region "Get All Thông tin hàng hóa"
        [HttpGet]
        [Route("api/Api_XuatNhapKho/GetAllHH/{macongty}/{key}/{machuan}")]
        public List<Prod_KHO_GetHHTonKho_Result> GetAllHH(string macongty, string key, string machuan)
        {
            var query = db.Database.SqlQuery<Prod_KHO_GetHHTonKho_Result>("Prod_KHO_GetHHTonKho @macongty,@key,@machuan", new SqlParameter("macongty", macongty), new SqlParameter("key", key), new SqlParameter("machuan", machuan));
            resultHH = query.ToList();
            
            return resultHH;
            
        }
        #endregion

        #region "Get hàng tồn theo từng kho"
        [HttpGet]
        [Route("api/Api_XuatNhapKho/GetHHTon/{macongty}/{machuan}")]
        public List<Prod_KHO_GetHangTonKho_Result> GetHHTon(string macongty, string machuan)
        {
            var query = db.Database.SqlQuery<Prod_KHO_GetHangTonKho_Result>("Prod_KHO_GetHangTonKho @macongty,@machuan", new SqlParameter("macongty", macongty), new SqlParameter("machuan", machuan));
            var resultHHTon = query.ToList();

            return resultHHTon;

        }
        #endregion
    }

}