using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels.All;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Api.KhachHang
{
    public class Api_BaoCaoKhachHangController : Controller
    {


        #region "Danh sách khách hàng không lấy hàng trong 1 tháng gần đây"
        [HttpPost]
        [Route("api/Api_BaoCaoKhachHang/KH_Khong_Mua_Gan_Day/{page}")]
        // GET: BaoCaoKhachHang
        public List<Prod_KH_ListKhachKhongMuaGanDay_Result> KH_Khong_Mua_Gan_Day(int page, ThamSo thansotimkiem)
        {
            using(var db = new ERP_DATABASEEntities())
            {
            var query = db.Database.SqlQuery<Prod_KH_ListKhachKhongMuaGanDay_Result>("Prod_KH_ListKhachKhongMuaGanDay @macongty, @isadmin, @username, @sotrang", new SqlParameter("macongty", thansotimkiem.macongty), new SqlParameter("isadmin", thansotimkiem.isadmin), new SqlParameter("username", thansotimkiem.ussername), new SqlParameter("sotrang", page));
            var result = query.ToList();
            return result;
            }
            
        }
        #endregion

    }
}