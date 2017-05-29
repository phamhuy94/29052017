using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ERP.Web.Models.Database;
using System.Data.SqlClient;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.Kho
{
    public class Api_KhoXuatController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        List<GetAll_List_XuatHang_Result> resultDSXuatKho = new List<GetAll_List_XuatHang_Result>();
        public class DataDSXuatKho
        {
            public string ngay { get; set; }
            public string tukhoa { get; set; }
        }
        // GET: api/Api_KhoXuat (List hàng cần đặt)
        [Route("api/Api_KhoXuat/GetHangCanDat/{isadmin}/{username}")]
        public List<Prod_HangCanDat_Result> GetHangCanDat(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanDat_Result>("Prod_HangCanDat @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_KhoXuat (List đơn bán hàng)
        [Route("api/Api_KhoXuat/GetListBanHang/{isadmin}/{username}")]
        public List<GetAll_ListBanHang_Result> GetListBanHang(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<GetAll_ListBanHang_Result>("GetAll_ListBanHang @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_KhoXuat (List xuất hàng)
        [HttpPost]
        [Route("api/Api_KhoXuat/GetListXuatHang/{isadmin}/{username}")]
        public List<GetAll_List_XuatHang_Result> GetListXuatHang(bool isadmin, string username, DataDSXuatKho data)
        {

            if (data.ngay == null && data.tukhoa == null)
            {

                var query = db.Database.SqlQuery<GetAll_List_XuatHang_Result>("GetAll_List_XuatHang @macongty,@isadmin,@username,@tukhoa,@ngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username), new SqlParameter("tukhoa", DBNull.Value), new SqlParameter("ngay", DBNull.Value));
                resultDSXuatKho = query.ToList();
            }
            if (data.ngay == null && data.tukhoa != null)
            {
                
                var query = db.Database.SqlQuery<GetAll_List_XuatHang_Result>("GetAll_List_XuatHang @macongty,@isadmin,@username,@tukhoa,@ngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username), new SqlParameter("tukhoa", data.tukhoa), new SqlParameter("ngay", DBNull.Value));
                resultDSXuatKho = query.ToList();
            }
            if (data.ngay != null && data.tukhoa == null)
            {
                DateTime ngay1 = xlnt.Xulydatetime(data.ngay);
                var query = db.Database.SqlQuery<GetAll_List_XuatHang_Result>("GetAll_List_XuatHang @macongty,@isadmin,@username,@tukhoa,@ngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username), new SqlParameter("tukhoa", DBNull.Value), new SqlParameter("ngay", ngay1));
                resultDSXuatKho = query.ToList();
            }
            if (data.ngay != null && data.tukhoa != null)
            {
                DateTime ngay = xlnt.Xulydatetime(data.ngay);
                var query = db.Database.SqlQuery<GetAll_List_XuatHang_Result>("GetAll_List_XuatHang @macongty,@isadmin,@username,@tukhoa,@ngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username), new SqlParameter("tukhoa", data.tukhoa), new SqlParameter("ngay", ngay));
                resultDSXuatKho = query.ToList();
               
            }
            return resultDSXuatKho;

        }

        

        // GET: api/Api_BanHang (List Bán Hàng Chưa Xuất)
        [Route("api/Api_KhoXuat/Get_DON_BAN_HANG_CHUA_XUAT/{isadmin}/{username}")]
        public List<GetAll_DonBanHangChuaXuat_Result> Get_DON_BAN_HANG_CHUA_XUAT(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<GetAll_DonBanHangChuaXuat_Result>("GetAll_DonBanHangChuaXuat @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_BanHang (List Bán Hàng Đã Xuất)
        [Route("api/Api_KhoXuat/Get_DON_BAN_HANG_DA_XUAT/{isadmin}/{username}")]
        public List<GetAll_DonBanHangDaXuat_Result> Get_DON_BAN_HANG_DA_XUAT(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<GetAll_DonBanHangDaXuat_Result>("GetAll_DonBanHangDaXuat @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang (List Hàng cần Xuất)
        [Route("api/Api_KhoXuat/Get_List_HANG_CAN_XUAT/{isadmin}/{username}")]
        public List<Prod_HangCanXuat_Result> Get_List_HANG_CAN_XUAT(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangCanXuat_Result>("Prod_HangCanXuat @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_BanHang (List Hàng Giữ)
        [Route("api/Api_KhoXuat/Get_List_HANG_GIU/{isadmin}/{username}")]
        public List<Prod_ListHangGiu_Result> Get_List_HANG_GIU(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_ListHangGiu_Result>("Prod_ListHangGiu @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang (List Hàng Chưa Giữ)
        [Route("api/Api_KhoXuat/Get_List_HANG_CHUA_GIU/{isadmin}/{username}")]
        public List<Prod_HangChuaGiu_Result> Get_List_HANG_CHUA_GIU(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangChuaGiu_Result>("Prod_HangChuaGiu @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang (List Hàng Giữ Chưa Bán)
        [Route("api/Api_KhoXuat/Get_List_HANG_GIU_CHUA_BAN/{isadmin}/{username}")]
        public List<Prod_HangGiuChuaBan_Result> Get_List_HANG_GIU_CHUA_BAN(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangGiuChuaBan_Result>("Prod_HangGiuChuaBan @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang (List Hàng Giữ Đã Bán)
        [Route("api/Api_KhoXuat/Get_List_HANG_GIU_DA_BAN/{isadmin}/{username}")]
        public List<Prod_HangGiuDaBan_Result> Get_List_HANG_GIU_DA_BAN(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangGiuDaBan_Result>("Prod_HangGiuDaBan @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang (List Hàng Giữ quá ngày giao hàng)
        [Route("api/Api_KhoXuat/Get_List_HANG_GIU_QUA_NGAY_GIAO/{isadmin}/{username}")]
        public List<Prod_HangGiuQuaNgayGiaoHang_Result> Get_List_HANG_GIU_QUA_NGAY_GIAO(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangGiuQuaNgayGiaoHang_Result>("Prod_HangGiuQuaNgayGiaoHang @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang (List Hàng Giữ quá ngày giao hàng)
        [Route("api/Api_KhoXuat/Get_List_HANG_GIU_SAP_DEN_NGAY_XUAT/{isadmin}/{username}")]
        public List<Prod_HangGiuSapDenNgayXuat_Result> Get_List_HANG_GIU_SAP_DEN_NGAY_XUAT(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_HangGiuSapDenNgayXuat_Result>("Prod_HangGiuSapDenNgayXuat @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }


        // GET: api/Api_BanHang (Cập nhật hàng cần đặt)
        [Route("api/Api_KhoXuat/CanDatHang/{id}")]
        public IHttpActionResult CanDatHang(int id)
        {
            String kq;
            var query = db.BH_CT_DON_HANG_PO.Where(x => x.ID == id).FirstOrDefault();
            if (query != null)
            {
                query.CAN_DAT_HANG = true;
                db.SaveChanges();
                kq = "Thành Công";
            }
            else
                kq = "Thất bại";

            return Ok(kq);
        }

    }
}