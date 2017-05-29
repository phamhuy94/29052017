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
using System.Text.RegularExpressions;
using ERP.Web.Common;
using ERP.Web.Models.NewModels.ChuyenKho;
using System.Data.SqlClient;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.Kho
{
    public class Api_ChuyenKhoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        List<GetAll_PhieuChuyenKho_Result> resultDSChuyen = new List<GetAll_PhieuChuyenKho_Result>();
        public class DataDSChuyenKho
        {
            public string tungay { get; set; }
            public string denngay { get; set; }
        }
        // GET: api/Api_ChuyenKho
        [HttpPost]
        [Route("api/Api_ChuyenKho/GetKHO_CHUYEN_KHO/")]
        public List<GetAll_PhieuChuyenKho_Result> GetKHO_CHUYEN_KHO(DataDSChuyenKho data)
        {
            
            if (data.tungay == null && data.denngay == null)
            {
                
                var query = db.Database.SqlQuery<GetAll_PhieuChuyenKho_Result>("GetAll_PhieuChuyenKho @macongty,@tungay,@denngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("tungay", DBNull.Value), new SqlParameter("denngay", DBNull.Value));
                resultDSChuyen = query.ToList();
            }
            else
            {
                DateTime FromDate = xlnt.Xulydatetime(data.tungay);
                DateTime ToDate = xlnt.Xulydatetime(data.denngay);
                var query = db.Database.SqlQuery<GetAll_PhieuChuyenKho_Result>("GetAll_PhieuChuyenKho @macongty,@tungay,@denngay", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("tungay", FromDate), new SqlParameter("denngay", ToDate));
                resultDSChuyen = query.ToList();
                return resultDSChuyen;
            }
            return resultDSChuyen;

        }

        // GET: api/Api_ChuyenKho
        [Route("api/Api_ChuyenKho/GetCTPhieuChuyenKho/{sct}")]
        public List<GetCTChuyenKho_Result> GetCTPhieuChuyenKho(string sct)
        {
            var query = db.Database.SqlQuery<GetCTChuyenKho_Result>("GetCTChuyenKho @sochungtu,@macongty ", new SqlParameter("sochungtu", sct), new SqlParameter("macongty", "HOPLONG"));

            return query.ToList();
        }


        // PUT: api/Api_ChuyenKho/5
        [Route("api/Api_ChuyenKho/PutKHO_CHUYEN_KHO")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHO_CHUYEN_KHO(ChuyenKho chuyenkho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var kho = db.KHO_CHUYEN_KHO.Where(x => x.SO_CHUNG_TU == chuyenkho.SO_CHUNG_TU).FirstOrDefault();
            kho.SO_CHUNG_TU = chuyenkho.SO_CHUNG_TU;
            kho.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(chuyenkho.NGAY_CHUNG_TU);
            kho.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(chuyenkho.NGAY_HACH_TOAN);
            kho.NGUOI_LAP_PHIEU = chuyenkho.NGUOI_LAP_PHIEU;
            kho.TRUC_THUOC = chuyenkho.TRUC_THUOC;
            kho.DIEN_GIAI = chuyenkho.DIEN_GIAI;
            // Chi tiết chuyển kho
            foreach (ChiTietChuyenKho item in chuyenkho.ChiTiet)
            {
                var newItem = db.KHO_CT_CHUYEN_KHO.Where(x => x.SO_CHUNG_TU == chuyenkho.SO_CHUNG_TU).FirstOrDefault();
                int sl_cu = newItem.SO_LUONG;
                newItem.SO_CHUNG_TU = kho.SO_CHUNG_TU;
                newItem.MA_HANG = item.MA_HANG;
                newItem.XUAT_TAI_KHO = item.MA_KHO_CON;
                newItem.NHAP_TAI_KHO = item.NHAP_TAI_KHO;
                newItem.SO_LUONG = item.SO_LUONG;
                newItem.DVT = item.DVT;
               
                //Chuyển hàng vào kho
                TONKHO_HOPLONG newkhoxuat = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.MA_KHO_CON).FirstOrDefault();
                newkhoxuat.SL_HOPLONG = newkhoxuat.SL_HOPLONG + sl_cu;
                TONKHO_HOPLONG newkhonhap = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.NHAP_TAI_KHO).FirstOrDefault();
                newkhonhap.SL_HOPLONG = newkhonhap.SL_HOPLONG - sl_cu;
                if (newkhoxuat == null || newkhoxuat.SL_HOPLONG < item.SO_LUONG)
                {
                    return Ok("Hàng không có trong kho hoặc SL tồn không đủ");
                }
                newkhoxuat.SL_HOPLONG -= Convert.ToInt32(item.SO_LUONG);

                
                if (newkhonhap == null)
                {
                    newkhonhap = new TONKHO_HOPLONG();
                    newkhonhap.MA_HANG = item.MA_HANG;
                    newkhonhap.MA_KHO_CON = item.NHAP_TAI_KHO;
                    newkhonhap.SL_HOPLONG = Convert.ToInt32(item.SO_LUONG);
                    
                }
                else
                {
                    newkhonhap.MA_HANG = item.MA_HANG;
                    newkhonhap.SL_HOPLONG += Convert.ToInt32(item.SO_LUONG);
                }



            }



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHO_CHUYEN_KHOExists(chuyenkho.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(kho.SO_CHUNG_TU);
        }

        public string GeneralChungTu()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string SoChungTu = (from nhapkho in db.KHO_CHUYEN_KHO where nhapkho.SO_CHUNG_TU.Contains("CK") select nhapkho.SO_CHUNG_TU).Max();
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (SoChungTu == null)
            {
                return "CK" + year + month + "00001";
            }
            SoChungTu = SoChungTu.Substring(6, SoChungTu.Length - 6);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 5 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "CK" + year + month + result;
        }

        // POST: api/Api_ChuyenKho
        [Route("api/Api_ChuyenKho/PostKHO_CHUYEN_KHO")]
        [ResponseType(typeof(KHO_CHUYEN_KHO))]
        public IHttpActionResult PostKHO_CHUYEN_KHO(ChuyenKho chuyenkho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            KHO_CHUYEN_KHO kho = new KHO_CHUYEN_KHO();
            kho.SO_CHUNG_TU = GeneralChungTu();
            kho.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(chuyenkho.NGAY_CHUNG_TU);
            kho.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(chuyenkho.NGAY_HACH_TOAN);
            kho.NGUOI_LAP_PHIEU = chuyenkho.NGUOI_LAP_PHIEU;
            kho.TRUC_THUOC = chuyenkho.TRUC_THUOC;
            kho.DIEN_GIAI = chuyenkho.DIEN_GIAI;
            db.KHO_CHUYEN_KHO.Add(kho);
            // Chi tiết chuyển kho
            foreach (ChiTietChuyenKho item in chuyenkho.ChiTiet)
            {
                KHO_CT_CHUYEN_KHO newItem = new KHO_CT_CHUYEN_KHO();
                newItem.SO_CHUNG_TU = kho.SO_CHUNG_TU;
                newItem.MA_HANG = item.MA_HANG;
                newItem.XUAT_TAI_KHO = item.MA_KHO_CON;
                newItem.NHAP_TAI_KHO = item.NHAP_TAI_KHO;
                newItem.SO_LUONG = item.SO_LUONG;
                newItem.DVT = item.DVT;
                db.KHO_CT_CHUYEN_KHO.Add(newItem);
                //Chuyển hàng vào kho
                TONKHO_HOPLONG newkhoxuat = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.MA_KHO_CON).FirstOrDefault();
                
                if (newkhoxuat == null || newkhoxuat.SL_HOPLONG < item.SO_LUONG)
                {
                    return Ok("Hàng không có trong kho hoặc SL tồn không đủ");
                }
                newkhoxuat.SL_HOPLONG -= Convert.ToInt32(item.SO_LUONG);
                
                TONKHO_HOPLONG newkhonhap = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.NHAP_TAI_KHO).FirstOrDefault();
                if (newkhonhap == null)
                {
                    newkhonhap = new TONKHO_HOPLONG();
                    newkhonhap.MA_HANG = item.MA_HANG;
                    newkhonhap.MA_KHO_CON = item.NHAP_TAI_KHO;
                    newkhonhap.SL_HOPLONG = Convert.ToInt32(item.SO_LUONG);
                    db.TONKHO_HOPLONG.Add(newkhonhap);
                }
                else
                {
                    newkhonhap.MA_HANG = item.MA_HANG;
                    newkhonhap.SL_HOPLONG += Convert.ToInt32(item.SO_LUONG);
                }
               
                
                
            }
            


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHO_CHUYEN_KHOExists(chuyenkho.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(kho.SO_CHUNG_TU);
        }


        #region "Chuyển kho giữ hàng"
        // POST: api/Api_ChuyenKho
        [Route("api/Api_ChuyenKho/ChuyenKhoGiuHang/{id}")]
        [ResponseType(typeof(KHO_CHUYEN_KHO))]
        public IHttpActionResult ChuyenKhoGiuHang(int id,ChuyenKho chuyenkho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            KHO_CHUYEN_KHO kho = new KHO_CHUYEN_KHO();
            kho.SO_CHUNG_TU = GeneralChungTu();
            kho.NGAY_CHUNG_TU = DateTime.Today.Date;
            kho.NGAY_HACH_TOAN = DateTime.Today.Date;
            kho.NGUOI_LAP_PHIEU = chuyenkho.NGUOI_LAP_PHIEU;
            kho.TRUC_THUOC = chuyenkho.TRUC_THUOC;
            kho.DIEN_GIAI = chuyenkho.DIEN_GIAI;
            db.KHO_CHUYEN_KHO.Add(kho);
            // Chi tiết chuyển kho
            foreach (ChiTietChuyenKho item in chuyenkho.ChiTiet)
            {
                KHO_CT_CHUYEN_KHO newItem = new KHO_CT_CHUYEN_KHO();
                newItem.SO_CHUNG_TU = kho.SO_CHUNG_TU;
                newItem.MA_HANG = item.MA_HANG;
                newItem.XUAT_TAI_KHO = item.MA_KHO_CON;
                newItem.NHAP_TAI_KHO = item.NHAP_TAI_KHO;
                newItem.SO_LUONG = item.SO_LUONG;
                newItem.DVT = item.DVT;
                db.KHO_CT_CHUYEN_KHO.Add(newItem);
                //Chuyển hàng vào kho
                TONKHO_HOPLONG newkhoxuat = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.MA_KHO_CON).FirstOrDefault();

                if (newkhoxuat == null || newkhoxuat.SL_HOPLONG < item.SO_LUONG)
                {
                    return Ok("Hàng không có trong kho hoặc SL tồn không đủ");
                }
                newkhoxuat.SL_HOPLONG -= Convert.ToInt32(item.SO_LUONG);

                TONKHO_HOPLONG newkhonhap = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.NHAP_TAI_KHO).FirstOrDefault();
                if (newkhonhap == null)
                {
                    newkhonhap = new TONKHO_HOPLONG();
                    newkhonhap.MA_HANG = item.MA_HANG;
                    newkhonhap.MA_KHO_CON = item.NHAP_TAI_KHO;
                    newkhonhap.SL_HOPLONG = Convert.ToInt32(item.SO_LUONG);
                    db.TONKHO_HOPLONG.Add(newkhonhap);
                }
                else
                {
                    newkhonhap.MA_HANG = item.MA_HANG;
                    newkhonhap.SL_HOPLONG += Convert.ToInt32(item.SO_LUONG);
                }



            }


            var query = db.BH_CT_DON_HANG_PO.Where(x => x.ID == id).FirstOrDefault();
            query.CAN_GIU_HANG = true;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHO_CHUYEN_KHOExists(chuyenkho.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(kho.SO_CHUNG_TU);
        }

        #endregion





        // DELETE: api/Api_ChuyenKho/5
        [ResponseType(typeof(KHO_CHUYEN_KHO))]
        public IHttpActionResult DeleteKHO_CHUYEN_KHO(string id)
        {
            KHO_CHUYEN_KHO kHO_CHUYEN_KHO = db.KHO_CHUYEN_KHO.Find(id);
            if (kHO_CHUYEN_KHO == null)
            {
                return NotFound();
            }

            db.KHO_CHUYEN_KHO.Remove(kHO_CHUYEN_KHO);
            db.SaveChanges();

            return Ok(kHO_CHUYEN_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHO_CHUYEN_KHOExists(string id)
        {
            return db.KHO_CHUYEN_KHO.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}