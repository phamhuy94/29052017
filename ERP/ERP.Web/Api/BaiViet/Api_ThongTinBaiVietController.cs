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
    public class Api_ThongTinBaiVietController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: api/Api_ThongTinBaiViet
        public List<Post> GetPost(int id)
        {
            var vData = (from t1 in db.POSTS
                         join t2 in db.POST_CATEGORIES on t1.MA_BAI_VIET equals t2.MA_BAI_VIET
                         join t3 in db.HT_NGUOI_DUNG on t1.NGUOI_DANG_BAI equals t3.USERNAME
                         where t2.MA_BAI_VIET == id
                         select new { t1.MA_BAI_VIET, t1.TIEU_DE_BAI_VIET, t1.NGUOI_DANG_BAI, t1.NOI_DUNG_BAI_VIET, t3.HO_VA_TEN, t1.ANH_BAI_VIET, t1.NGAY_DANG_BAI });
            var result = vData.ToList().Select(x => new Post()
            {
                MA_BAI_VIET = x.MA_BAI_VIET,
                NGUOI_DANG_BAI = x.NGUOI_DANG_BAI,
                TIEU_DE_BAI_VIET = x.TIEU_DE_BAI_VIET,
                NGAY_DANG_BAI = x.NGAY_DANG_BAI,
                NOI_DUNG_BAI_VIET = x.NOI_DUNG_BAI_VIET,
                HO_VA_TEN = x.HO_VA_TEN,
                ANH_BAI_VIET = x.ANH_BAI_VIET,
            }).ToList();
            return result;
        }
    }
}
