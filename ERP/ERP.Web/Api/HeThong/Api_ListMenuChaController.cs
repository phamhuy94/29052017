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
    public class Api_ListMenuChaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [Route("api/Api_ListMenuCha/{id}/{menucon}")]
        public List<MenuHienThi> GetListMenuCha(string id, string menucon)
        {
            var vData = (from t1 in db.MENUs
                         join t2 in db.MENU_USER on t1.MA_MENU equals t2.MA_MENU
                         where t2.USERNAME == id && t1.MENU_CHA == menucon
                         select new { t1.MA_MENU, t1.TEN_MENU, t1.MENU_CHA, t1.LINK, t2.MA_PHONG_BAN, t2.USERNAME, t2.TRANG_THAI });
            var result = vData.ToList().Select(x => new MenuHienThi()
            {
                MA_MENU = x.MA_MENU,
                TEN_MENU = x.TEN_MENU,
                LINK = x.LINK,
                MENU_CHA = x.MENU_CHA,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                USERNAME = x.USERNAME,
                TRANG_THAI = x.TRANG_THAI,
            }).ToList();
            return result;
        }
    }
}
