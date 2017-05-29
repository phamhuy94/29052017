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
using ERP.Web.Models.NewModels.NganHang;
using ERP.Web.Common;
using System.Text.RegularExpressions;
using ERP.Web.Models.NewModels.All;
using ERP.Web.Models.NewModels.XuatKho;
using System.Dynamic;

namespace ERP.Web.Api.NganHang
{
    public class Api_NganHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [HttpGet]
        // GET: api/Api_NganHang
        public ExpandoObject GetNH_NTTKLIST(DateTime? from_day = null, DateTime? to_day = null, string so_tai_khoan = null, int current_page = 1, int page_size = 10)
        {
            //return _context.NH_NTTK;
            IEnumerable<NH_NTTK> value = db.NH_NTTK;
            //  var vData = db.NH_NTTK;
            if (so_tai_khoan != null)
            {
                value = value.Where(c => c.NOP_VAO_TAI_KHOAN == so_tai_khoan);
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
            var result = value.ToList().Select(x => new NH_NTTK()
            {
                SO_CHUNG_TU = x.SO_CHUNG_TU,
                NGAY_HACH_TOAN = x.NGAY_HACH_TOAN,
                NGAY_CHUNG_TU = x.NGAY_CHUNG_TU,
                MA_DOI_TUONG = x.MA_DOI_TUONG,
                NOP_VAO_TAI_KHOAN = x.NOP_VAO_TAI_KHOAN,
                LY_DO_THU = x.LY_DO_THU,
                DIEN_GIAI_LY_DO_THU = x.DIEN_GIAI_LY_DO_THU,
                NHAN_VIEN_THU = x.NHAN_VIEN_THU,
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

        // GET: api/Api_NganHang/5
        [ResponseType(typeof(NH_NTTK))]
        public IHttpActionResult GetNH_NTTK(string id)
        {
            NH_NTTK nH_NTTK = db.NH_NTTK.Find(id);
            if (nH_NTTK == null)
            {
                return NotFound();
            }

            return Ok(nH_NTTK);
        }

        // PUT: api/Api_NganHang/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNH_NTTK(string id, NH_NTTK nH_NTTK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nH_NTTK.SO_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(nH_NTTK).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NH_NTTKExists(id))
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
            string prefixNumber = "NTTK" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.NH_NTTK where nhapkho.SO_CHUNG_TU.Contains(prefixNumber) select nhapkho.SO_CHUNG_TU).Max();


            if (SoChungTu == null)
            {
                return "NTTK" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(10, SoChungTu.Length - 10);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "NTTK" + year + month + day + result;
        }

        // POST: api/Api_NganHang
        [HttpPost]
        [Route("api/Api_NganHang/PostKNH_NTTK")]
        [ResponseType(typeof(NH_NTTK))]

        public IHttpActionResult PostKNH_NTTK(ThuNganHang chi_nganhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Lưu thông tin nhập kho
            NH_NTTK nhnttk = new NH_NTTK();
            nhnttk.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_CHUNG_TU);
            nhnttk.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_HACH_TOAN);
            nhnttk.SO_CHUNG_TU = AutoMA_DU_KIEN();
            nhnttk.MA_DOI_TUONG = chi_nganhang.MA_DOI_TUONG;
            nhnttk.NOP_VAO_TAI_KHOAN = chi_nganhang.NOP_VAO_TAI_KHOAN;
            nhnttk.LY_DO_THU = chi_nganhang.LY_DO_THU;
            nhnttk.DIEN_GIAI_LY_DO_THU = chi_nganhang.DIEN_GIAI_LY_DO_THU;
            nhnttk.NHAN_VIEN_THU = chi_nganhang.NHAN_VIEN_THU;
            nhnttk.NGUOI_LAP_BIEU = chi_nganhang.NGUOI_LAP_BIEU;
            nhnttk.TRUC_THUOC = "HOPLONG";
            db.NH_NTTK.Add(nhnttk);

            //Lưu thông tin tham chiếu
            if (chi_nganhang.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in chi_nganhang.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = nhnttk.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (chi_nganhang.ChiTietPTNH != null && chi_nganhang.ChiTietPTNH.Count > 0)
            {
                foreach (ChiTietPhieuThuNH item in chi_nganhang.ChiTietPTNH)
                {
                    NH_CT_NTTK newItem = new NH_CT_NTTK();
                    newItem.SO_CHUNG_TU = nhnttk.SO_CHUNG_TU;
                    newItem.DIEN_GIAI = item.DIEN_GIAI;
                    newItem.LOAI_TIEN = item.LOAI_TIEN;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.SO_TIEN = Convert.ToDecimal(item.SO_TIEN);
                    newItem.TY_GIA = Convert.ToInt32(item.TY_GIA);
                    newItem.QUY_DOI = newItem.SO_TIEN * newItem.TY_GIA;
                    tongtien += newItem.QUY_DOI;
                    newItem.MA_DOI_TUONG = nhnttk.MA_DOI_TUONG;
                    newItem.DON_VI = item.DON_VI;
                    db.NH_CT_NTTK.Add(newItem);


                }
            }
            //Lưu nhật ký chung

            if (chi_nganhang.ChiTietPTNH != null && chi_nganhang.ChiTietPTNH.Count > 0)
            {
                foreach (ChiTietPhieuThuNH item in chi_nganhang.ChiTietPTNH)
                {
                    KT_SO_NHAT_KY_CHUNG newitem = new KT_SO_NHAT_KY_CHUNG();
                    newitem.SO_CHUNG_TU = nhnttk.SO_CHUNG_TU;
                    newitem.NGAY_CHUNG_TU = nhnttk.NGAY_CHUNG_TU;
                    newitem.NGAY_HACH_TOAN = nhnttk.NGAY_HACH_TOAN;
                    newitem.DOI_TUONG = nhnttk.MA_DOI_TUONG;
                    newitem.TRUC_THUOC = "HOPLONG";
                    newitem.DIEN_GIAI_CHUNG = nhnttk.DIEN_GIAI_LY_DO_THU;
                    newitem.DIEN_GIAI_CHI_TIET = item.DIEN_GIAI;
                    newitem.TAI_KHOAN_HACH_TOAN = item.TK_NO;
                    newitem.TAI_KHOAN_DOI_UNG = item.TK_CO;
                    newitem.PHAT_SINH_NO = tongtien;
                    newitem.PHAT_SINH_CO = 0;
                    db.KT_SO_NHAT_KY_CHUNG.Add(newitem);
                    KT_SO_NHAT_KY_CHUNG newitem1 = new KT_SO_NHAT_KY_CHUNG();
                    newitem1.SO_CHUNG_TU = nhnttk.SO_CHUNG_TU;
                    newitem1.NGAY_CHUNG_TU = nhnttk.NGAY_CHUNG_TU;
                    newitem1.NGAY_HACH_TOAN = nhnttk.NGAY_HACH_TOAN;
                    newitem1.DOI_TUONG = nhnttk.MA_DOI_TUONG;
                    newitem1.TRUC_THUOC = "HOPLONG";
                    newitem1.DIEN_GIAI_CHUNG = nhnttk.DIEN_GIAI_LY_DO_THU;
                    newitem1.DIEN_GIAI_CHI_TIET = item.DIEN_GIAI;
                    newitem1.TAI_KHOAN_HACH_TOAN = item.TK_CO;
                    newitem1.TAI_KHOAN_DOI_UNG = item.TK_NO;
                    newitem1.PHAT_SINH_NO = 0;
                    newitem1.PHAT_SINH_CO = tongtien;
                    db.KT_SO_NHAT_KY_CHUNG.Add(newitem1);
                    db.SaveChanges();
                }
            }


            nhnttk.TONG_TIEN = tongtien;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NH_NTTKExists(chi_nganhang.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }


            return Ok(nhnttk.SO_CHUNG_TU);

        }

        // DELETE: api/Api_NganHang/5
        [ResponseType(typeof(NH_NTTK))]
        public IHttpActionResult DeleteNH_NTTK(string id)
        {
            NH_NTTK nH_NTTK = db.NH_NTTK.Find(id);
            if (nH_NTTK == null)
            {
                return NotFound();
            }

            db.NH_NTTK.Remove(nH_NTTK);
            db.SaveChanges();

            return Ok(nH_NTTK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NH_NTTKExists(string id)
        {
            return db.NH_NTTK.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}