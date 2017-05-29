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
using ERP.Web.Models.NewModels.Quy;
using ERP.Web.Common;
using ERP.Web.Models.NewModels.All;
using System.Dynamic;

namespace ERP.Web.Api.Quy
{
    public class Api_QUY_PHIEU_THUController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        [HttpGet]
        // GET: api/Api_QUY_PHIEU_THU
        public ExpandoObject GetQUY_PHIEU_THU(DateTime? from_day = null, DateTime? to_day = null, int current_page = 1, int page_size = 10)
        {

            IEnumerable<QUY_PHIEU_THU> value = db.QUY_PHIEU_THU;



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

            var result = value.ToList().Select(x => new QUY_PHIEU_THU()
            {
                SO_CHUNG_TU = x.SO_CHUNG_TU,
                NGAY_HACH_TOAN = x.NGAY_HACH_TOAN,
                NGAY_CHUNG_TU = x.NGAY_CHUNG_TU,
                MA_DOI_TUONG = x.MA_DOI_TUONG,
                DIEN_GIAI_LY_DO_NOP = x.DIEN_GIAI_LY_DO_NOP,
                LY_DO_NOP = x.LY_DO_NOP,
                NGUOI_NOP = x.NGUOI_NOP,
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

        // GET: api/Api_QUY_PHIEU_THU/5
        [ResponseType(typeof(QUY_PHIEU_THU))]
        public IHttpActionResult GetQUY_PHIEU_THU(string id)
        {
            QUY_PHIEU_THU qUY_PHIEU_THU = db.QUY_PHIEU_THU.Find(id);
            if (qUY_PHIEU_THU == null)
            {
                return NotFound();
            }

            return Ok(qUY_PHIEU_THU);
        }

        // PUT: api/Api_QUY_PHIEU_THU/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQUY_PHIEU_THU(string id, QUY_PHIEU_THU qUY_PHIEU_THU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qUY_PHIEU_THU.SO_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(qUY_PHIEU_THU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QUY_PHIEU_THUExists(id))
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
            string prefixNumber = "PT" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.QUY_PHIEU_THU where nhapkho.SO_CHUNG_TU.Contains(prefixNumber) select nhapkho.SO_CHUNG_TU).Max();


            if (SoChungTu == null)
            {
                return "PT" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "PT" + year + month + day + result;
        }

        // POST: api/Api_QUY_PHIEU_THU
        [HttpPost]
        [Route("api/Api_QUY_PHIEU_THU/PostQUY_PHIEUTHU")]
        [ResponseType(typeof(QUY_PHIEU_THU))]

        public IHttpActionResult PostQUY_PHIEUTHU(QuyPhieuThu quy_phieuthu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Lưu thông tin nhập kho
            QUY_PHIEU_THU qpt = new QUY_PHIEU_THU();
            qpt.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(quy_phieuthu.NGAY_CHUNG_TU);
            qpt.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(quy_phieuthu.NGAY_HACH_TOAN);
            qpt.SO_CHUNG_TU = AutoMA_DU_KIEN();
            qpt.MA_DOI_TUONG = quy_phieuthu.MA_DOI_TUONG;
            qpt.LY_DO_NOP = quy_phieuthu.LY_DO_NOP;
            qpt.DIEN_GIAI_LY_DO_NOP = quy_phieuthu.DIEN_GIAI_LY_DO_NOP;
            qpt.NGUOI_NOP = quy_phieuthu.NGUOI_NOP;
            qpt.NHAN_VIEN_THU = quy_phieuthu.NHAN_VIEN_THU;
            qpt.NGUOI_LAP_BIEU = quy_phieuthu.NGUOI_LAP_BIEU;
            qpt.TRUC_THUOC = "HOPLONG";
            db.QUY_PHIEU_THU.Add(qpt);

            //Lưu thông tin tham chiếu
            if (quy_phieuthu.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in quy_phieuthu.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = qpt.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (quy_phieuthu.ChiTietQPT != null && quy_phieuthu.ChiTietQPT.Count > 0)
            {
                foreach (ChiTietQuyPhieuThu item in quy_phieuthu.ChiTietQPT)
                {
                    QUY_CT_PHIEU_THU newItem = new QUY_CT_PHIEU_THU();
                    newItem.SO_CHUNG_TU = qpt.SO_CHUNG_TU;
                    newItem.DIEN_GIAI = item.DIEN_GIAI;
                    newItem.LOAI_TIEN = item.LOAI_TIEN;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.SO_TIEN = Convert.ToDecimal(item.SO_TIEN);
                    newItem.TY_GIA = Convert.ToInt32(item.TY_GIA);
                    newItem.QUY_DOI = newItem.SO_TIEN * newItem.TY_GIA;
                    tongtien += newItem.QUY_DOI;
                    newItem.DOI_TUONG = qpt.MA_DOI_TUONG;
                    newItem.TK_NGAN_HANG = item.TK_NGAN_HANG;
                    db.QUY_CT_PHIEU_THU.Add(newItem);


                }
            }
            //Lưu nhật ký chung

            if (quy_phieuthu.ChiTietQPT != null && quy_phieuthu.ChiTietQPT.Count > 0)
            {
                foreach (ChiTietQuyPhieuThu item in quy_phieuthu.ChiTietQPT)
                {
                    KT_SO_NHAT_KY_CHUNG newitem = new KT_SO_NHAT_KY_CHUNG();
                    newitem.SO_CHUNG_TU = qpt.SO_CHUNG_TU;
                    newitem.NGAY_CHUNG_TU = qpt.NGAY_CHUNG_TU;
                    newitem.NGAY_HACH_TOAN = qpt.NGAY_HACH_TOAN;
                    newitem.DOI_TUONG = qpt.MA_DOI_TUONG;
                    newitem.TRUC_THUOC = "HOPLONG";
                    newitem.DIEN_GIAI_CHUNG = qpt.DIEN_GIAI_LY_DO_NOP;
                    newitem.DIEN_GIAI_CHI_TIET = item.DIEN_GIAI;
                    newitem.TAI_KHOAN_HACH_TOAN = item.TK_NO;
                    newitem.TAI_KHOAN_DOI_UNG = item.TK_CO;
                    newitem.PHAT_SINH_NO = tongtien;
                    newitem.PHAT_SINH_CO = 0;
                    db.KT_SO_NHAT_KY_CHUNG.Add(newitem);
                    KT_SO_NHAT_KY_CHUNG newitem1 = new KT_SO_NHAT_KY_CHUNG();
                    newitem1.SO_CHUNG_TU = qpt.SO_CHUNG_TU;
                    newitem1.NGAY_CHUNG_TU = qpt.NGAY_CHUNG_TU;
                    newitem1.NGAY_HACH_TOAN = qpt.NGAY_HACH_TOAN;
                    newitem1.DOI_TUONG = qpt.MA_DOI_TUONG;
                    newitem1.TRUC_THUOC = "HOPLONG";
                    newitem1.DIEN_GIAI_CHUNG = qpt.DIEN_GIAI_LY_DO_NOP;
                    newitem1.DIEN_GIAI_CHI_TIET = item.DIEN_GIAI;
                    newitem1.TAI_KHOAN_HACH_TOAN = item.TK_CO;
                    newitem1.TAI_KHOAN_DOI_UNG = item.TK_NO;
                    newitem1.PHAT_SINH_NO = 0;
                    newitem1.PHAT_SINH_CO = tongtien;
                    db.KT_SO_NHAT_KY_CHUNG.Add(newitem1);
                    db.SaveChanges();
                }
            }

            qpt.TONG_TIEN = tongtien;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (QUY_PHIEU_THUExists(quy_phieuthu.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }


            return Ok(qpt.SO_CHUNG_TU);

        }

        // DELETE: api/Api_QUY_PHIEU_THU/5
        [ResponseType(typeof(QUY_PHIEU_THU))]
        public IHttpActionResult DeleteQUY_PHIEU_THU(string id)
        {
            QUY_PHIEU_THU qUY_PHIEU_THU = db.QUY_PHIEU_THU.Find(id);
            if (qUY_PHIEU_THU == null)
            {
                return NotFound();
            }

            db.QUY_PHIEU_THU.Remove(qUY_PHIEU_THU);
            db.SaveChanges();

            return Ok(qUY_PHIEU_THU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QUY_PHIEU_THUExists(string id)
        {
            return db.QUY_PHIEU_THU.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}