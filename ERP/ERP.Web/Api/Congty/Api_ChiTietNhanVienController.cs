using ERP.Web.Areas.HopLong.Models;
using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.HeThong
{
    public class Api_ChiTietNhanVienController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_ChiTietNhanVien
        public List<nhanvien> Get(string id)
        {
            var vData = (from t1 in db.CCTC_NHAN_VIEN
                         join t2 in db.HT_NGUOI_DUNG on t1.USERNAME equals t2.USERNAME
                         join t3 in db.CCTC_PHONG_BAN on t1.MA_PHONG_BAN equals t3.MA_PHONG_BAN
                         where t1.USERNAME == id

                         select new {t2.ALLOWED,t1.LINH_VUC_CONG_TAC,t2.IS_ADMIN, t1.GIOI_TINH,t1.USERNAME,t2.PASSWORD,t2.MA_CONG_TY, t1.NGAY_SINH,t1.MA_PHONG_BAN, t1.CHUC_VU, t1.QUE_QUAN, t1.THANH_TICH_CONG_TAC, t1.TRINH_DO_HOC_VAN, t2.HO_VA_TEN, t2.EMAIL, t2.SDT, t2.AVATAR,t3.TEN_PHONG_BAN });


            var result = vData.ToList().Select(x => new nhanvien()
            {
                HO_VA_TEN = x.HO_VA_TEN,
                PASSWORD = x.PASSWORD,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                EMAIL = x.EMAIL,
                USERNAME = x.USERNAME,
                CHUC_VU = x.CHUC_VU,
                SDT = x.SDT,
                GIOI_TINH = x.GIOI_TINH,
                LINH_VUC_CONG_TAC = x.LINH_VUC_CONG_TAC,
                NGAY_SINH = Convert.ToDateTime(x.NGAY_SINH).ToString("dd/MM/yyyy"),
                QUE_QUAN = x.QUE_QUAN,
                TEN_PHONG_BAN = x.TEN_PHONG_BAN,
                THANH_TICH_CONG_TAC = x.THANH_TICH_CONG_TAC,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                AVATAR = x.AVATAR,
                MA_CONG_TY = x.MA_CONG_TY,
                ALLOWED = x.ALLOWED,
                IS_ADMIN = x.IS_ADMIN
            }).ToList();
            return result;
        }

 

        // POST: api/Api_ChiTietNhanVien
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Api_ChiTietNhanVien/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Api_ChiTietNhanVien/5
        public void Delete(int id)
        {
        }
    }
}
