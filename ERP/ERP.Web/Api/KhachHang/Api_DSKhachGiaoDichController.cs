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
    public class Api_DSKhachGiaoDichController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [Route("api/Api_DSKhachGiaoDich/GetKHChuaPhatSinh/{username}")]
        public List<Query_DS_KhachHang_ChuaPhatSinhGiaoDich_Result> GetKHChuaPhatSinh( string username)
        {
            var query = db.Database.SqlQuery<Query_DS_KhachHang_ChuaPhatSinhGiaoDich_Result>("Query_DS_KhachHang_ChuaPhatSinhGiaoDich @macongty, @sale", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("sale", username));
            var result = query.ToList();
            return result;
        }
    }
}
