using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Http;
using System.Data.SqlClient;
using ERP.Web.Models.Database;

namespace ERP.Api.Controllers.CCTC
{
    
    public class PhongBanController : ApiController
    {
        [Route("api/PhongBan/GetPhongBan/{macongty}")]
        public List<Prod_CCTC_PhongBan_Result> GetPhongBan(string macongty)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_CCTC_PhongBan_Result>("Prod_CCTC_PhongBan @macongty", new SqlParameter("macongty", macongty));
                var result = query.ToList();
                return result;
            }
        }

    }
}
