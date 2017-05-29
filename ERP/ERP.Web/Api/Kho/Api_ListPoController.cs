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
    public class Api_ListPoController : ApiController
    {


        [Route("api/Api_ListPo/GetListPOCanGiuHang/{macongty}")]
        public List<Prod_KHO_ListPoCanGiuHang_Result> GetListPOCanGiuHang(string macongty)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_KHO_ListPoCanGiuHang_Result>("Prod_KHO_ListPoCanGiuHang @macongty", new SqlParameter("macongty", macongty));
                var data = query.ToList();
                return data;

            }
        }

            

    }
}
