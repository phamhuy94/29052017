using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.BusinessModel
{
    public class PermissionAction
    {
        
        
            public int ID { get; set; }
            public String TEN_CHI_TIET { get; set; }
            public String MO_TA { get; set; }
            public bool IS_GRANTED { get; set; }
        
    }
}