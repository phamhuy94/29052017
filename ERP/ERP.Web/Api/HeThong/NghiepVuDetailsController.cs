using ERP.Web.Models;
using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERP.Web.Api.HeThong
{


    public class NghiepVuDetailsController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET : api/NghiepVuDetails/ma_nhom_nghiep_vu
        public List<NghiepVuDetails> GetListNghiepvu(string id)
        {
            var vData = (from t1 in db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU
                         join t3 in db.HT_NGUOI_DUNG on t1.USERNAME equals t3.USERNAME
                         where t1.ID_NHOM_NGHIEP_VU == id
                         select new { t3.HO_VA_TEN, t3.USERNAME });
            var result = vData.ToList().Select(x => new NghiepVuDetails()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                USERNAME = x.USERNAME
            }).ToList();
            return result;
        }


       



        // POST: api/NghiepVuDetails/username
        [ResponseType(typeof(MENU))]
        [Route("api/NghiepVuDetails/{idnhomnghiepvu}/{username}")]
        public void PostNghiepVu(String idnhomnghiepvu, string username)
        {
            
            
            var vData = (from t2 in db.CN_CHI_TIET_NHOM_NGHIEP_VU
                         join t3 in db.CN_CHI_TIET_NGHIEP_VU on t2.ID_CHI_TIET_NGHIEP_VU equals t3.ID
                         where t2.ID_NHOM_NGHIEP_VU == idnhomnghiepvu
                         select new {t3.ID, t3.TEN_CHI_TIET, t3.ID_NGHIEP_VU, t3.MO_TA });

            var result = vData.ToList().Select(x=> new CN_CHI_TIET_NGHIEP_VU()
            {
                ID = x.ID,
                TEN_CHI_TIET = x.TEN_CHI_TIET,
                ID_NGHIEP_VU = x.ID_NGHIEP_VU,
                MO_TA = x.MO_TA
            }).ToList();
            foreach (var item in result)
            {
                CN_NGHIEP_VU_NHAN_VIEN nghiepvunhanvien = new CN_NGHIEP_VU_NHAN_VIEN();
                nghiepvunhanvien.ID_CHI_TIET_NGHIEP_VU = item.ID;
                nghiepvunhanvien.USERNAME = username;
                nghiepvunhanvien.MO_TA = item.MO_TA;

                db.CN_NGHIEP_VU_NHAN_VIEN.Add(nghiepvunhanvien);

            }
            db.SaveChanges();

    
        }

    }
}
