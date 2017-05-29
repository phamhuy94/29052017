using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Api.Models.CreatedModel.Common
{
    public class TimKiem
    {
        public string ma { get; set; }
        public string macongty { get; set; }
        public string tukhoa { get; set; }
        public bool isadmin { get; set; }
        public string sotrang { get; set; }
        public string phongsale { get; set; }
        public string phongmarketing { get; set; }
        public bool ismarketing { get; set; }

        public string maphongban { set; get; }

    }
}