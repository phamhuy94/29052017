using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.Kho
{
    public class Api_ChecktonkhoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: api/Checktonkho

            [Route("api/Api_Checktonkho/Get/{pg}")]
        public List<HopLong_DS_TONKHO_Result> Get(string pg)
        {
            int sotrang = Convert.ToInt32(pg);

            var query = db.Database.SqlQuery<HopLong_DS_TONKHO_Result>("HopLong_DS_TONKHO @sotrang", new SqlParameter("@sotrang", sotrang));

            var result = query.ToList();
            return result;
        }
    

    }
}
