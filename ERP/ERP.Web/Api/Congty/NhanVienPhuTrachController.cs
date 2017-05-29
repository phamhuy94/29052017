using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.Congty
{
    public class NhanVienPhuTrachController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/NhanVienPhuTrach/5
        public List<nhanvien> Getnhanvien()
        {
            var vData = (from t1 in db.CCTC_NHAN_VIEN
                         join t2 in db.HT_NGUOI_DUNG on t1.USERNAME equals t2.USERNAME
                         where t1.MA_PHONG_BAN == "SALE_HL" 
                         select new { t2.HO_VA_TEN,t2.USERNAME});
            var result = vData.ToList().Select(x => new nhanvien()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                USERNAME = x.USERNAME
            }).ToList();
            return result;
        }
    }
}
