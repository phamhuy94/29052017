using ERP.Web.Models;
using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERP.Web.Areas.HopLong.Api.HeThong
{
    public class Api_ChitietnghiepvuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: api/Api_Chitietnghiepvu/5
        public List<ChiTietNghiepVu> Get_Chitietnghiepvu(string id)
        {
            var vData = (from t1 in db.CN_NGHIEP_VU
                         join t2 in db.CN_CHI_TIET_NGHIEP_VU on t1.ID equals t2.ID_NGHIEP_VU
                         where t2.ID_NGHIEP_VU == id
                         select new { t1.TEN_NGHIEP_VU, t2.TEN_CHI_TIET, t2.MO_TA,t2.ID });
            var result = vData.ToList().Select(x => new ChiTietNghiepVu()
            {
                ID = x.ID,
                TEN_NGHIEP_VU = x.TEN_NGHIEP_VU,
                TEN_CHI_TIET = x.TEN_CHI_TIET,
                MO_TA = x.MO_TA,
            }).ToList();
            return result;
        }

        // PUT: api/Api_Chitietnghiepvu/5
        [ResponseType(typeof(void))]
        public void PutCN_CHI_TIET_NGHIEP_VU(int id, CN_CHI_TIET_NGHIEP_VU chiTietNghiepVu)
        {
            var check = db.CN_CHI_TIET_NGHIEP_VU.Where(x => x.ID == id);
            if(check.Count()>0)
            {
                var resultupdate = check.FirstOrDefault();
                resultupdate.MO_TA = chiTietNghiepVu.MO_TA;
                db.SaveChanges();
            }
        }

        private bool CN_CHI_TIET_NGHIEP_VUExists(int id)
        {
            throw new NotImplementedException();
        }
    }   
}