using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.KhachHang
{
    public class Api_KH_CongNoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [Route("api/Api_KH_CongNo/CongNoTheoNhanVien/{macongty}/{manhanvien}/{isadmin}/{tuancongno}")]
        public List<Prod_KH_Cong_no_Result> CongNoTheoNhanVien(string macongty, string manhanvien, string isadmin, string tuancongno)
        {
            
            var query = db.Database.SqlQuery<Prod_KH_Cong_no_Result>("Prod_KH_Cong_no @macongty, @isadmin, @manhanvien,  @tuancongno", new SqlParameter("macongty", macongty), new SqlParameter("isadmin", isadmin), new SqlParameter("manhanvien", manhanvien) , new SqlParameter("tuancongno", tuancongno));
            var result = query.ToList();
            return result;

        }
    }
}
