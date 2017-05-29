using ERP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.XuatKho
{
    public class PrintPhieuXuatKho
    {
        public GetThongTinChungPhieuXuatKho_Result ChungPhieuXuatKho { get; set; }
        public List<GetChiTietPhieuXuatKho_Result> ChiTietPhieuXuatKho { get; set; }
    }
}