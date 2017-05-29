using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERP.Web.Api.Congty
{
    public class Api_SuaAnhCaNhanController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // PUT: api/Api_SuaAnhCaNhan/5
        [System.Web.Http.Route("api/Api_SuaAnhCaNhan/{id}")]
        public void PutHT_NGUOI_DUNG(string id, HT_NGUOI_DUNG hT_NGUOI_DUNG)
        {
            var nguoidung = db.HT_NGUOI_DUNG.Where(x => x.USERNAME == id).FirstOrDefault();

            if (nguoidung != null)
            {
                nguoidung.AVATAR = hT_NGUOI_DUNG.AVATAR;
            }


            db.SaveChanges();
        }
    }
}
