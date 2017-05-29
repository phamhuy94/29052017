using ERP.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.BaiViet
{
    public class Api_BaiViet_TongHopController : ApiController
    {

        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        string thongbao;


        #region "Get all marketing"
        [Route("api/Api_BaiViet_TongHop/GetAllMarketing/{macongty}")]
        public List<Prod_GetAll_Marketing_Result> GetAllMarketing(string macongty)
        {
            var query = db.Database.SqlQuery<Prod_GetAll_Marketing_Result>("Prod_GetAll_Marketing @macongty", new SqlParameter("macongty", macongty));
            var data = query.ToList();
            return data;
        }
        #endregion



        [Route("api/Api_BaiViet_TongHop/GetThongBaoKinhDoanh/{username}")]
        public List<Prod_GetNotifications_Result> GetThongBaoKinhDoanh(string username)
        {
            var query = db.Database.SqlQuery<Prod_GetNotifications_Result>("Prod_GetNotifications @username", new SqlParameter("username", username));
            var data = query.ToList();
            return data;
        }

        [Route("api/Api_BaiViet_TongHop/GetSoThongBaoKinhDoanh/{username}")]
        public int GetSoThongBaoKinhDoanh(string username)
        {
            var query = db.Database.SqlQuery<Prod_GetNotifications_Result>("Prod_GetNotifications @username", new SqlParameter("username", username));
            var data = query.ToList();
            var sothongbao = data.Count;
            return sothongbao;
        }

        [HttpPut]
        [Route("api/Api_BaiViet_TongHop/ReadNotification/{id}")]
        public string ReadNotification(int id)
        {
            
            var query = db.NOTIFICATIONS.Where(x => x.ID == id).FirstOrDefault();
            if(query != null)
            {
                query.DA_DOC_THONG_BAO = true;
                query.NGAY_DOC_THONG_BAO = DateTime.Now;
                db.SaveChanges();
                
            }
            else
            {
                thongbao = "Lỗi đọc thông báo!";
            }
            return thongbao;
        }


        [HttpPost]
        [Route("api/Api_BaiViet_TongHop/AddNotification")]
        public void AddNotification(List<NOTIFICATION> danhsachnotification)
        {
            foreach (var item in danhsachnotification)
            {
                NOTIFICATION thongbao = new NOTIFICATION();
                thongbao.NGUOI_DUNG = item.NGUOI_DUNG;
                thongbao.NGAY_THONG_BAO = DateTime.Now;
                thongbao.LINK_THONG_BAO = item.LINK_THONG_BAO;
                thongbao.NOI_DUNG_THONG_BAO = item.NOI_DUNG_THONG_BAO;
                db.NOTIFICATIONS.Add(thongbao);
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

        }



        [HttpPost]
        [Route("api/Api_BaiViet_TongHop/GetTongHopBaiViet")]
        public List<GetAll_HomeSales_Result> GetTongHopBaiViet()
        {
            var query = db.Database.SqlQuery<GetAll_HomeSales_Result>("GetAll_HomeSales");
            var result = query.ToList();
            return result;
        }
    }
}
