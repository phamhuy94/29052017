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
using ERP.Web.Models.NewModels.NganHang;
using ERP.Web.Common;
using ERP.Web.Models.NewModels.All;
using System.Dynamic;

namespace ERP.Web.Api.NganHang
{
    public class Api_NH_UNCController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [HttpGet]
        // GET: api/Api_NH_UNC
        public ExpandoObject GetNH_UNC(DateTime? from_day = null, DateTime? to_day = null, string so_tai_khoan = null, int current_page = 1, int page_size = 10)
        {

            IEnumerable<NH_UNC> value = db.NH_UNC;

            if (so_tai_khoan != null)
            {
                value = value.Where(c => c.TAI_KHOAN_CHI == so_tai_khoan);
            }

            if (to_day != null)
            {
                value = value.Where(c => c.NGAY_CHUNG_TU <= to_day);
            }

            if (from_day != null)
            {
                value = value.Where(c => c.NGAY_CHUNG_TU >= from_day);
            }

            int count = value.Count();
            value = value.Skip(page_size * (current_page - 1)).Take(page_size);
            int max_page = (count + page_size - 1) / page_size;

            if (max_page < current_page)
            {
                current_page = max_page;
            }

            // List<NH_NTTK> Listtest = value.ToList();
            var result = value.ToList().Select(x => new NH_UNC()
            {
                SO_CHUNG_TU = x.SO_CHUNG_TU,
                NGAY_HACH_TOAN = x.NGAY_HACH_TOAN,
                NGAY_CHUNG_TU = x.NGAY_CHUNG_TU,
                MA_DOI_TUONG = x.MA_DOI_TUONG,
                TAI_KHOAN_CHI = x.TAI_KHOAN_CHI,
                DIEN_GIAI_NOI_DUNG_THANH_TOAN = x.DIEN_GIAI_NOI_DUNG_THANH_TOAN,
                NOI_DUNG_THANH_TOAN = x.NOI_DUNG_THANH_TOAN,
                NHAN_VIEN_CHUYEN_KHOAN = x.NHAN_VIEN_CHUYEN_KHOAN,
                TONG_TIEN = x.TONG_TIEN,
                NGUOI_LAP_BIEU = x.NGUOI_LAP_BIEU,
                TRUC_THUOC = x.TRUC_THUOC

            }).ToList();

            dynamic res_data = new ExpandoObject();
            res_data.current_page = current_page;
            res_data.page_size = page_size;
            res_data.max_page = max_page;
            res_data.data = result;
            return res_data;
        }

        // GET: api/Api_NH_UNC/5
        [ResponseType(typeof(NH_UNC))]
        public IHttpActionResult GetNH_UNC(string id)
        {
            NH_UNC nH_UNC = db.NH_UNC.Find(id);
            if (nH_UNC == null)
            {
                return NotFound();
            }

            return Ok(nH_UNC);
        }

        // PUT: api/Api_NH_UNC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNH_UNC(string id, NH_UNC nH_UNC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nH_UNC.SO_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(nH_UNC).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NH_UNCExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public string AutoMA_DU_KIEN()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string prefixNumber = "UNC" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.NH_UNC where nhapkho.SO_CHUNG_TU.Contains(prefixNumber) select nhapkho.SO_CHUNG_TU).Max();


            if (SoChungTu == null)
            {
                return "UNC" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(9, SoChungTu.Length - 9);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "UNC" + year + month + day + result;
        }

        // POST: api/Api_NH_UNC
        [HttpPost]
        [Route("api/Api_NH_UNC/PostNH_UNC")]
        [ResponseType(typeof(NH_UNC))]

        public IHttpActionResult PostNH_UNC(ChiNganHang chi_nganhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Lưu thông tin nhập kho
            NH_UNC unc = new NH_UNC();
            unc.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_CHUNG_TU);
            unc.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_HACH_TOAN);
            unc.SO_CHUNG_TU = AutoMA_DU_KIEN();
            unc.TAI_KHOAN_CHI = chi_nganhang.TAI_KHOAN_CHI;
            unc.MA_DOI_TUONG = chi_nganhang.MA_DOI_TUONG;
            unc.NOI_DUNG_THANH_TOAN = chi_nganhang.NOI_DUNG_THANH_TOAN;
            unc.DIEN_GIAI_NOI_DUNG_THANH_TOAN = chi_nganhang.DIEN_GIAI_NOI_DUNG_THANH_TOAN;
            unc.TAI_KHOAN_NHAN = chi_nganhang.TAI_KHOAN_NHAN;
            unc.NHAN_VIEN_CHUYEN_KHOAN = chi_nganhang.NHAN_VIEN_CHUYEN_KHOAN;
            unc.NGUOI_LAP_BIEU = chi_nganhang.NGUOI_LAP_BIEU;
            unc.TRUC_THUOC = "HOPLONG";
            db.NH_UNC.Add(unc);

            //Lưu thông tin tham chiếu
            if (chi_nganhang.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in chi_nganhang.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = unc.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (chi_nganhang.ChiTietHachToan != null && chi_nganhang.ChiTietHachToan.Count > 0)
            {
                foreach (ChiTietHachToanPhieuChi item in chi_nganhang.ChiTietHachToan)
                {
                    NH_CT_UNC newItem = new NH_CT_UNC();
                    newItem.SO_CHUNG_TU = unc.SO_CHUNG_TU;
                    newItem.DIEN_GIAI = item.DIEN_GIAI;
                    newItem.LOAI_TIEN = item.LOAI_TIEN;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.SO_TIEN = Convert.ToDecimal(item.SO_TIEN);
                    newItem.TY_GIA = Convert.ToInt32(item.TY_GIA);
                    newItem.QUY_DOI = newItem.SO_TIEN * newItem.TY_GIA;
                    tongtien += newItem.QUY_DOI;
                    newItem.MA_DOI_TUONG = unc.MA_DOI_TUONG;
                    newItem.DON_VI = item.DON_VI;
                    if (chi_nganhang.ChiTietThue != null && chi_nganhang.ChiTietThue.Count > 0)
                    {
                        var thue = chi_nganhang.ChiTietThue.Where(x => x.MA_NHA_CUNG_CAP == unc.MA_DOI_TUONG).FirstOrDefault();
                        newItem.DIEN_GIAI_THUE = thue.DIEN_GIAI_THUE;
                        newItem.TK_THUE_GTGT = thue.TK_THUE_GTGT;
                        newItem.TIEN_THUE_GTGT = thue.TIEN_THUE_GTGT;
                        newItem.CK_THUE_GTGT = thue.CK_THUE_GTGT;
                        newItem.GIA_TRI_HHDV_CHUA_THUE = thue.GIA_TRI_HHDV_CHUA_THUE;
                        newItem.NGAY_HD = Convert.ToDateTime(thue.NGAY_HD);
                        newItem.SO_HD = thue.SO_HD;
                        newItem.MAU_SO_HD = thue.MAU_SO_HD;
                        newItem.KY_HIEU_HD = thue.KY_HIEU_HD;
                        newItem.MA_NHA_CUNG_CAP = thue.MA_NHA_CUNG_CAP;
                    }

                    db.NH_CT_UNC.Add(newItem);

                    // Lưu Nhật ký
                    KT_SO_NHAT_KY_CHUNG sonhatky = new KT_SO_NHAT_KY_CHUNG();
                    sonhatky.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                    sonhatky.NGAY_CHUNG_TU = unc.NGAY_CHUNG_TU;
                    sonhatky.NGAY_HACH_TOAN = unc.NGAY_HACH_TOAN;
                    sonhatky.DOI_TUONG = unc.MA_DOI_TUONG;
                    sonhatky.TRUC_THUOC = "HOPLONG";
                    sonhatky.DIEN_GIAI_CHUNG = unc.NOI_DUNG_THANH_TOAN;
                    sonhatky.DIEN_GIAI_CHI_TIET = newItem.DIEN_GIAI;
                    sonhatky.TAI_KHOAN_HACH_TOAN = newItem.TK_NO;
                    sonhatky.TAI_KHOAN_DOI_UNG = newItem.TK_CO;
                    sonhatky.PHAT_SINH_NO = tongtien;
                    sonhatky.PHAT_SINH_CO = 0;
                    db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky);
                    KT_SO_NHAT_KY_CHUNG sonhatky1 = new KT_SO_NHAT_KY_CHUNG();
                    sonhatky1.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                    sonhatky1.NGAY_CHUNG_TU = unc.NGAY_CHUNG_TU;
                    sonhatky1.NGAY_HACH_TOAN = unc.NGAY_HACH_TOAN;
                    sonhatky1.DOI_TUONG = unc.MA_DOI_TUONG;
                    sonhatky1.TRUC_THUOC = "HOPLONG";
                    sonhatky1.DIEN_GIAI_CHUNG = unc.NOI_DUNG_THANH_TOAN;
                    sonhatky1.DIEN_GIAI_CHI_TIET = newItem.DIEN_GIAI;
                    sonhatky1.TAI_KHOAN_HACH_TOAN = newItem.TK_CO;
                    sonhatky1.TAI_KHOAN_DOI_UNG = newItem.TK_NO;
                    sonhatky1.PHAT_SINH_NO = 0;
                    sonhatky1.PHAT_SINH_CO = tongtien;
                    db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky1);
                    if (newItem.TK_THUE_GTGT != null)
                    {
                        KT_SO_NHAT_KY_CHUNG sonhatky3 = new KT_SO_NHAT_KY_CHUNG();
                        sonhatky3.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                        sonhatky3.NGAY_CHUNG_TU = unc.NGAY_CHUNG_TU;
                        sonhatky3.NGAY_HACH_TOAN = unc.NGAY_HACH_TOAN;
                        sonhatky3.DOI_TUONG = unc.MA_DOI_TUONG;
                        sonhatky3.TRUC_THUOC = "HOPLONG";
                        sonhatky3.DIEN_GIAI_CHUNG = unc.NOI_DUNG_THANH_TOAN;
                        sonhatky3.DIEN_GIAI_CHI_TIET = newItem.DIEN_GIAI;
                        sonhatky3.TAI_KHOAN_HACH_TOAN = newItem.TK_THUE_GTGT;
                        sonhatky3.TAI_KHOAN_DOI_UNG = newItem.TK_NO;
                        sonhatky3.PHAT_SINH_NO = Convert.ToDecimal(newItem.TIEN_THUE_GTGT);
                        sonhatky3.PHAT_SINH_CO = 0;
                        db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky3);
                        KT_SO_NHAT_KY_CHUNG sonhatky4 = new KT_SO_NHAT_KY_CHUNG();
                        sonhatky4.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                        sonhatky4.NGAY_CHUNG_TU = unc.NGAY_CHUNG_TU;
                        sonhatky4.NGAY_HACH_TOAN = unc.NGAY_HACH_TOAN;
                        sonhatky4.DOI_TUONG = unc.MA_DOI_TUONG;
                        sonhatky4.TRUC_THUOC = "HOPLONG";
                        sonhatky4.DIEN_GIAI_CHUNG = unc.NOI_DUNG_THANH_TOAN;
                        sonhatky4.DIEN_GIAI_CHI_TIET = newItem.DIEN_GIAI;
                        sonhatky4.TAI_KHOAN_HACH_TOAN = newItem.TK_NO;
                        sonhatky4.TAI_KHOAN_DOI_UNG = newItem.TK_THUE_GTGT;
                        sonhatky4.PHAT_SINH_NO = 0;
                        sonhatky4.PHAT_SINH_CO = Convert.ToDecimal(newItem.TIEN_THUE_GTGT);
                        db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky4);
                        db.SaveChanges();


                    }
                }


            }


            unc.TONG_TIEN = tongtien;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NH_UNCExists(chi_nganhang.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }


            return Ok(unc.SO_CHUNG_TU);

        }

        // DELETE: api/Api_NH_UNC/5
        [ResponseType(typeof(NH_UNC))]
        public IHttpActionResult DeleteNH_UNC(string id)
        {
            NH_UNC nH_UNC = db.NH_UNC.Find(id);
            if (nH_UNC == null)
            {
                return NotFound();
            }

            db.NH_UNC.Remove(nH_UNC);
            db.SaveChanges();

            return Ok(nH_UNC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NH_UNCExists(string id)
        {
            return db.NH_UNC.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}