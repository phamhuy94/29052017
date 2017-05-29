using ERP.Web.Models;
using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.HeThong
{
    public class Api_TrangThaiMenuController : ApiController
    {
       
        bool trangthai;
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_TrangThaiMenu/5
        [Route("api/Api_TrangThaiMenu/{id}/{mamenu}")]
        public bool Gettrangthai(string id,string mamenu)
        {
            
            var vData = db.MENU_USER.Where(x => x.USERNAME == id && x.MA_MENU == mamenu).ToList();
            if(vData.Count() >0)
            {
                var result = vData.FirstOrDefault();
                trangthai = result.TRANG_THAI;
            }
            
            
            return trangthai;
        }

        
    }
}
