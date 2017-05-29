using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace ERP.Web.Api.HeThong
{
    public class DoiMatKhauController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // PUT: api/DoiMatKhau/5
        [System.Web.Http.Route("api/DoiMatKhau/{id}/{oldpw}")]
        public void PutHT_NGUOI_DUNG(string id,string oldpw, HT_NGUOI_DUNG hT_NGUOI_DUNG)
        {
            var nguoidung = db.HT_NGUOI_DUNG.Where(x => x.USERNAME == id).ToList();

            if(nguoidung.Count >0)
            {
                var nd = nguoidung.FirstOrDefault();
                if(nd.PASSWORD == oldpw)
                {
                    nd.PASSWORD = hT_NGUOI_DUNG.PASSWORD;
                }
                
            }
            
            db.SaveChanges();
        }
    }
}
