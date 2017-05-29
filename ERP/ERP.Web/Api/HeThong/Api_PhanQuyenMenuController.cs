using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.HeThong
{
    public class Api_PhanQuyenMenuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: api/Api_PhanQuyenMenu
        public List<MENU> GetMenu()
        {
            var vData = db.MENUs.Where(x => x.MUC_TRUC_THUOC == "TONG_CONG_TY");
            var result = vData.ToList().Select(x => new MENU()
            {
                TEN_MENU = x.TEN_MENU,
                LINK = x.LINK,
                MA_MENU = x.MA_MENU,
                MENU_CHA = x.MENU_CHA
            }).ToList();
            return result;
        }
    }
}
