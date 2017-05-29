using ERP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class PrintPO
    {
        public GetAll_ThongTinChungDonHangPO_Result ChungPO { get; set; }
        public List<GetAll_ChiTiet_DonHangPO_Result> ChiTietPO { get; set; }
    }
}