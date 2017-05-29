using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels.NhapKho;
using ERP.Web.Common;
using ERP.Web.Models.NewModels.All;
using System.Data.SqlClient;
using ERP.Web.Models.NewModels.XuatKho;

namespace ERP.Web.Api.Kho
{
    public class Api_NhapKhoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();


        public class GetHangtra
        {
            public GetPhieuXuatKho_Result xuatkho { set; get; }
            public List<GetChiTietPhieuXuatKho_Result> ctxuatkho { set; get; }
        }
        // GET: api/Api_NhapKho
        [Route("api/Api_NhapKho/GetDetailKHO_NHAP_KHO/{ChungTu}")]
        public GetHangtra GetDetailKHO_NHAP_KHO(string ChungTu)
        {

            //Lưu thông tin nhập kho
            GetHangtra xk = new GetHangtra();
            var query = db.Database.SqlQuery<GetPhieuXuatKho_Result>("GetPhieuXuatKho @sochungtu, @macongty", new SqlParameter("sochungtu", ChungTu), new SqlParameter("macongty", "HOPLONG"));
            var data = db.Database.SqlQuery<GetChiTietPhieuXuatKho_Result>("GetChiTietPhieuXuatKho @sochungtu, @macongty", new SqlParameter("sochungtu", ChungTu), new SqlParameter("macongty", "HOPLONG"));
            xk.xuatkho = query.FirstOrDefault();
            xk.ctxuatkho = data.ToList();
            return xk;

        }

        [Route("api/Api_NhapKho/GetDSPhieuNhapKho")]
        public List<GetAll_PhieuNhapKho_Result> GetDSPhieuNhapKho()
        {
            var query = db.Database.SqlQuery<GetAll_PhieuNhapKho_Result>("GetAll_PhieuNhapKho @macongty", new SqlParameter("macongty", "HOPLONG"));
            return query.ToList();
        }

        [Route("api/Api_NhapKho/DemSoBanGhi")]
        public IHttpActionResult DemSoBanGhi()
        {
            var query = db.KHO_NHAP_KHO.Count().ToString();

            return Ok(query);
        }



        // GET: api/Api_NhapKho/5
        [ResponseType(typeof(KHO_NHAP_KHO))]
        public IHttpActionResult GetKHO_NHAP_KHO(string id)
        {
            KHO_NHAP_KHO kHO_NHAP_KHO = db.KHO_NHAP_KHO.Find(id);
            if (kHO_NHAP_KHO == null)
            {
                return NotFound();
            }

            return Ok(kHO_NHAP_KHO);
        }

        // PUT: api/Api_XuatKho/5
        [Route("api/Api_NhapKho/PutKHO_NHAP_KHO")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHO_NHAP_KHO(NhapKho kho_NhapKho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Lưu thông tin nhập kho
            var nk = db.KHO_NHAP_KHO.Where(x => x.SO_CHUNG_TU == kho_NhapKho.SO_CHUNG_TU).FirstOrDefault();
            nk.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(kho_NhapKho.NGAY_CHUNG_TU);
            nk.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(kho_NhapKho.NGAY_HACH_TOAN);
            nk.SO_CHUNG_TU = kho_NhapKho.SO_CHUNG_TU;
            nk.NGUOI_GIAO_HANG = kho_NhapKho.NGUOI_GIAO_HANG;
            nk.NGUOI_LAP_PHIEU = kho_NhapKho.NGUOI_LAP_PHIEU;
            nk.MA_DOI_TUONG = kho_NhapKho.MA_DOI_TUONG;
            nk.DIEN_GIAI = kho_NhapKho.DIEN_GIAI;
            nk.TRUC_THUOC = "HOPLONG";
            nk.LOAI_NHAP_KHO = kho_NhapKho.LOAI_NHAP_KHO;
            //Lưu thông tin tham chiếu
            if (kho_NhapKho.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in kho_NhapKho.ThamChieu)
                {
                    var newItem = db.XL_THAM_CHIEU_CHUNG_TU.Where(x => x.SO_CHUNG_TU_GOC == nk.SO_CHUNG_TU).FirstOrDefault();
                    if (newItem != null)
                    {
                        //newItem.SO_CHUNG_TU_GOC = xk.SO_CHUNG_TU;
                        newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    }

                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (kho_NhapKho.ChiTiet != null && kho_NhapKho.ChiTiet.Count > 0)
            {
                foreach (ChiTietNhapKho item in kho_NhapKho.ChiTiet)
                {
                    var newItem = db.KHO_CT_NHAP_KHO.Where(x => x.SO_CHUNG_TU == nk.SO_CHUNG_TU && x.MA_HANG == item.MA_HANG).FirstOrDefault();
                    int sl_cu = newItem.SO_LUONG;
                    if (newItem != null)
                    {
                        newItem.SO_CHUNG_TU = nk.SO_CHUNG_TU;
                        newItem.MA_HANG = item.MA_HANG;
                        newItem.TK_CO = item.TK_CO;
                        newItem.TK_NO = item.TK_NO;
                        newItem.DVT = item.DVT;
                        newItem.DON_GIA = Convert.ToDecimal(item.DON_GIA);
                        newItem.SO_LUONG = Convert.ToInt32(item.SO_LUONG);
                        newItem.THANH_TIEN = newItem.DON_GIA * newItem.SO_LUONG;
                        tongtien += newItem.THANH_TIEN;
                        newItem.TK_KHO = item.TK_KHO;

                    }

                    //Cập nhật hàng tồn
                    TONKHO_HOPLONG newHangTon = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG).FirstOrDefault();
                    newHangTon.SL_HOPLONG = newHangTon.SL_HOPLONG - sl_cu;
                    newHangTon.SL_HOPLONG += Convert.ToInt32(item.SO_LUONG);
                    //if (newHangTon == null)
                    //{
                    //    db.TONKHO_HOPLONG.Add(newHangTon);
                    //}
                    ////Cập nhật nhóm hàng
                    //TONKHO_HANG hangton = NhomHang.GetNhomHang(item.MA_HANG);
                    //if (hangton != null)
                    //{
                    //    hangton.SL_HANG = Convert.ToInt32(item.SO_LUONG);
                    //}

                }
            }

            nk.TONG_TIEN = tongtien;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {


                throw;

            }

            return Ok(nk.SO_CHUNG_TU);
        }


        public string GeneralChungTu()
        {
            string SoChungTu = (from nhapkho in db.KHO_NHAP_KHO where nhapkho.SO_CHUNG_TU.Contains("NK") select nhapkho.SO_CHUNG_TU).Max();
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (SoChungTu == null)
            {
                return "NK" + year + month + "00001";
            }
            SoChungTu = SoChungTu.Substring(6, SoChungTu.Length - 6);
            string number = (Convert.ToInt32(SoChungTu) + 1).ToString();
            string result = number.ToString();
            int count = 5 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "NK" + year + month + result;
        }

        // POST: api/Api_NhapKho
        [Route("api/Api_NhapKho/PostKHO_NHAP_KHO")]
        [ResponseType(typeof(KHO_NHAP_KHO))]
        public IHttpActionResult PostKHO_NHAP_KHO(NhapKho kho_NhapKho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Lưu thông tin nhập kho
            KHO_NHAP_KHO nk = new KHO_NHAP_KHO();
            nk.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(kho_NhapKho.NGAY_CHUNG_TU);
            nk.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(kho_NhapKho.NGAY_HACH_TOAN);
            nk.SO_CHUNG_TU = GeneralChungTu();
            nk.NGUOI_GIAO_HANG = kho_NhapKho.NGUOI_GIAO_HANG;
            nk.NGUOI_LAP_PHIEU = kho_NhapKho.NGUOI_LAP_PHIEU;
            nk.MA_DOI_TUONG = kho_NhapKho.MA_DOI_TUONG;
            nk.DIEN_GIAI = kho_NhapKho.DIEN_GIAI;
            nk.TRUC_THUOC = "HOPLONG";
            nk.LOAI_NHAP_KHO = kho_NhapKho.LOAI_NHAP_KHO;
            db.KHO_NHAP_KHO.Add(nk);

            //Lưu thông tin tham chiếu
            if (kho_NhapKho.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in kho_NhapKho.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = nk.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //HHTONKHOViewModels HHTon = new HHTONKHOViewModels();
            //NhomHangViewModels NhomHang = new NhomHangViewModels();
            if (kho_NhapKho.ChiTiet != null && kho_NhapKho.ChiTiet.Count > 0)
            {
                foreach (ChiTietNhapKho item in kho_NhapKho.ChiTiet)
                {
                    KHO_CT_NHAP_KHO newItem = new KHO_CT_NHAP_KHO();
                    newItem.SO_CHUNG_TU = nk.SO_CHUNG_TU;
                    newItem.MA_HANG = item.MA_HANG;
                    newItem.MA_KHO_CON = item.MA_KHO;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.DVT = item.DVT;
                    newItem.DON_GIA = Convert.ToDecimal(item.DON_GIA);
                    newItem.SO_LUONG = Convert.ToInt32(item.SO_LUONG);
                    newItem.THANH_TIEN = newItem.DON_GIA * newItem.SO_LUONG;
                    tongtien += newItem.THANH_TIEN;
                    newItem.TK_KHO = item.TK_KHO;
                    db.KHO_CT_NHAP_KHO.Add(newItem);
                    //Cập nhật hàng tồn
                    TONKHO_HOPLONG newHangTon = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.MA_KHO).FirstOrDefault();
                    if (newHangTon == null)
                    {
                        newHangTon = new TONKHO_HOPLONG();
                        newHangTon.MA_HANG = item.MA_HANG;
                        newHangTon.MA_KHO_CON = item.MA_KHO;
                        newHangTon.SL_HOPLONG = Convert.ToInt32(item.SO_LUONG);
                        db.TONKHO_HOPLONG.Add(newHangTon);
                    }
                    else
                    {
                        newHangTon.SL_HOPLONG += Convert.ToInt32(item.SO_LUONG);
                    }

                    // Lưu Nhật ký
                    KT_SO_NHAT_KY_CHUNG sonhatky = new KT_SO_NHAT_KY_CHUNG();
                    sonhatky.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                    sonhatky.NGAY_CHUNG_TU = nk.NGAY_CHUNG_TU;
                    sonhatky.NGAY_HACH_TOAN = nk.NGAY_HACH_TOAN;
                    if(nk.MA_DOI_TUONG == null)
                    {
                        sonhatky.DOI_TUONG = nk.NGUOI_GIAO_HANG;
                    }
                    else
                    {
                        sonhatky.DOI_TUONG = nk.MA_DOI_TUONG;
                    }
                  
                    sonhatky.TRUC_THUOC = "HOPLONG";
                    sonhatky.DIEN_GIAI_CHUNG = nk.DIEN_GIAI;
                    sonhatky.DIEN_GIAI_CHI_TIET = nk.DIEN_GIAI;
                    sonhatky.TAI_KHOAN_HACH_TOAN = newItem.TK_NO;
                    sonhatky.TAI_KHOAN_DOI_UNG = newItem.TK_CO;
                    sonhatky.PHAT_SINH_NO = tongtien;
                    sonhatky.PHAT_SINH_CO = 0;
                    db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky);
                    KT_SO_NHAT_KY_CHUNG sonhatky1 = new KT_SO_NHAT_KY_CHUNG();
                    sonhatky1.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                    sonhatky1.NGAY_CHUNG_TU = nk.NGAY_CHUNG_TU;
                    sonhatky1.NGAY_HACH_TOAN = nk.NGAY_HACH_TOAN;
                    if (nk.MA_DOI_TUONG == null)
                    {
                        sonhatky1.DOI_TUONG = nk.NGUOI_GIAO_HANG;
                    }
                    else
                    {
                        sonhatky1.DOI_TUONG = nk.MA_DOI_TUONG;
                    }
                    sonhatky1.TRUC_THUOC = "HOPLONG";
                    sonhatky1.DIEN_GIAI_CHUNG = nk.DIEN_GIAI;
                    sonhatky1.DIEN_GIAI_CHI_TIET = nk.DIEN_GIAI;
                    sonhatky1.TAI_KHOAN_HACH_TOAN = newItem.TK_CO;
                    sonhatky1.TAI_KHOAN_DOI_UNG = newItem.TK_NO;
                    sonhatky1.PHAT_SINH_NO = 0;
                    sonhatky1.PHAT_SINH_CO = tongtien;
                    db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky1);
                    
               

                }
            }


            nk.TONG_TIEN = tongtien;
            //Cập nhật Hàng tồn kho
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHO_NHAP_KHOExists(kho_NhapKho.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }

            return Ok(nk.SO_CHUNG_TU);
        }

        // DELETE: api/Api_NhapKho/5
        [ResponseType(typeof(KHO_NHAP_KHO))]
        public IHttpActionResult DeleteKHO_NHAP_KHO(string id)
        {
            KHO_NHAP_KHO kHO_NHAP_KHO = db.KHO_NHAP_KHO.Find(id);
            if (kHO_NHAP_KHO == null)
            {
                return NotFound();
            }

            db.KHO_NHAP_KHO.Remove(kHO_NHAP_KHO);
            db.SaveChanges();

            return Ok(kHO_NHAP_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHO_NHAP_KHOExists(string id)
        {
            return db.KHO_NHAP_KHO.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}