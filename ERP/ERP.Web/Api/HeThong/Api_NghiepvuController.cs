using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ERP.Web.Models.Database;

namespace ERP.Web.Areas.HopLong.Api.HeThong
{
    public class Api_NghiepvuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Nghiepvu
        public List<CN_NGHIEP_VU> GetDS_NghiepVu()
        {
            var vData = db.CN_NGHIEP_VU.Where(x => x.TRUC_THUOC == "HOPLONG");
            var result = vData.ToList().Select(x => new CN_NGHIEP_VU()
            {
                ID = x.ID,
                TEN_NGHIEP_VU = x.TEN_NGHIEP_VU
            }).ToList();
            return result;
        }


        // PUT: api/Api_Nghiepvu
        [ResponseType(typeof(void))]
        public void PutCN_NGHIEP_VU(string id, CN_NGHIEP_VU cN_Nghiep_Vu)
        {
            var Dsnv = db.CN_NGHIEP_VU.Where(x => x.ID == id);
            if (Dsnv.Count() > 0)
            {
                var resultupdate = Dsnv.FirstOrDefault();
                resultupdate.TEN_NGHIEP_VU = cN_Nghiep_Vu.TEN_NGHIEP_VU;
                db.SaveChanges();
            }
        }

        private bool CN_CHI_TIET_NGHIEP_VUExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}