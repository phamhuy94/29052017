using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.HeThong
{
    public class Api_CheckChiTietController : ApiController
    {
        bool trangthai;
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [Route("api/Api_CheckChiTiet/{nhomnghiepvu}/{mamota}")]
        public bool Gettrangthai(string nhomnghiepvu,int mamota)
        {

            var vData = db.CN_CHI_TIET_NHOM_NGHIEP_VU.Where(x => x.ID_NHOM_NGHIEP_VU == nhomnghiepvu && x.ID_CHI_TIET_NGHIEP_VU == mamota).ToList();
            if (vData.Count() > 0)
                trangthai = true;
            else
                trangthai = false;
            return trangthai;
        }
    }
}
