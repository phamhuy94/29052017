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
    public class TimKiemKhachHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [Route("api/TimKiemKhachHang/GetKH/{sdt}")]
        public List<TimKiemKhachHang_Result> GetKH(string sdt)
        {
            var query = db.Database.SqlQuery<TimKiemKhachHang_Result>("TimKiemKhachHang @sdt", new SqlParameter("sdt", sdt));
            var result = query.ToList();
           
            return result;
        }
    }
}
