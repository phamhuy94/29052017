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
    public class Api_UserDetailsController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_UserDetails/5
        public List<UserDetails> GetDetail(string id)
        {
            var vData = (from t1 in db.CCTC_NHAN_VIEN
                         join t2 in db.HT_NGUOI_DUNG on t1.USERNAME equals t2.USERNAME
                         where t1.USERNAME == id

                         select new { t1.GIOI_TINH, t1.NGAY_SINH, t1.QUE_QUAN,t1.CHUC_VU, t1.TRINH_DO_HOC_VAN,t1.MA_PHONG_BAN, t2.HO_VA_TEN, t2.EMAIL, t2.SDT, t2.AVATAR });


            var result = vData.ToList().Select(x => new UserDetails()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                EMAIL = x.EMAIL,
                SDT = x.SDT,
                CHUC_VU = x.CHUC_VU,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                GIOI_TINH = x.GIOI_TINH,
                NGAY_SINH = x.NGAY_SINH.ToString(),
                QUE_QUAN = x.QUE_QUAN,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                AVATAR = x.AVATAR
            }).ToList();
            return result;
        }
    }
}
