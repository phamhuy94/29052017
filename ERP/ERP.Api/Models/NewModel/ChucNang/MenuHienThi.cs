using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models
{
    public class MenuHienThi
    {
        public string MA_MENU { set; get; }
        public string TEN_MENU { set; get; }
        public string LINK { set; get; }

        public string MA_PHONG_BAN { set; get; }

        public bool TRANG_THAI { set; get; }

        public string MENU_CHA { set; get; }
        public string USERNAME { set; get; }
    }
}