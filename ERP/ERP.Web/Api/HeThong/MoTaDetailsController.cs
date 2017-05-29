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
    public class MoTaDetailsController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET : api/MoTaDetails/ma_nhom_nghiep_vu
        public List<MoTaNghiepVu> GetListMota(string id)
        {
            var vData = (from t2 in db.CN_CHI_TIET_NHOM_NGHIEP_VU
                         join t3 in db.CN_CHI_TIET_NGHIEP_VU on t2.ID_CHI_TIET_NGHIEP_VU equals t3.ID
                         where t2.ID_NHOM_NGHIEP_VU == id
                         select new { t3.MO_TA });
            var result = vData.ToList().Select(x => new MoTaNghiepVu()
            {
                MO_TA = x.MO_TA,
            }).ToList();
            return result;
        }
    }
}
