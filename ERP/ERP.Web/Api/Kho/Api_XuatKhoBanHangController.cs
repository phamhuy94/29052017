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
using ERP.Web.Common;
using ERP.Web.Models.NewModels.XuatKho;
using ERP.Web.Models.NewModels.All;
using System.Text.RegularExpressions;

namespace ERP.Web.Api.Kho
{
    public class Api_XuatKhoBanHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_XuatKhoBanHang
        public IQueryable<KHO_XUAT_KHO> GetKHO_XUAT_KHO()
        {
            return db.KHO_XUAT_KHO;
        }

        // GET: api/Api_XuatKhoBanHang/5
        [ResponseType(typeof(KHO_XUAT_KHO))]
        public IHttpActionResult GetKHO_XUAT_KHO(string id)
        {
            KHO_XUAT_KHO kHO_XUAT_KHO = db.KHO_XUAT_KHO.Find(id);
            if (kHO_XUAT_KHO == null)
            {
                return NotFound();
            }

            return Ok(kHO_XUAT_KHO);
        }

        // PUT: api/Api_XuatKhoBanHang/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHO_XUAT_KHO(string id, KHO_XUAT_KHO kHO_XUAT_KHO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHO_XUAT_KHO.SO_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(kHO_XUAT_KHO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHO_XUAT_KHOExists(id))
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

        public string GeneralChungTu()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string SoChungTu = (from nhapkho in db.KHO_XUAT_KHO where nhapkho.SO_CHUNG_TU.Contains("XK") select nhapkho.SO_CHUNG_TU).Max();
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (SoChungTu == null)
            {
                return "XK" + year + month + "00001";
            }
            SoChungTu = SoChungTu.Substring(6, SoChungTu.Length - 6);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 5 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "XK" + year + month + result;
        }
        [Route("api/Api_XuatKhoBanHang/PostKHO_XUAT_KHO")]
        // POST: api/Api_XuatKhoBanHang
        [ResponseType(typeof(KHO_XUAT_KHO))]
        public IHttpActionResult PostKHO_XUAT_KHO(XuatKhoBH kho_xuatkho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Lưu thông tin nhập kho
            KHO_XUAT_KHO xk = new KHO_XUAT_KHO();
            xk.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(kho_xuatkho.NGAY_CHUNG_TU);
            xk.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(kho_xuatkho.NGAY_HACH_TOAN);
            xk.SO_CHUNG_TU = GeneralChungTu();
            xk.NGUOI_NHAN = kho_xuatkho.NGUOI_NHAN;
            xk.KHACH_HANG = kho_xuatkho.KHACH_HANG;
            xk.LY_DO_XUAT = kho_xuatkho.LY_DO_XUAT;
            xk.NHAN_VIEN_BAN_HANG = kho_xuatkho.NHAN_VIEN_BAN_HANG;
            xk.NGUOI_LAP_PHIEU = kho_xuatkho.NGUOI_LAP_PHIEU;
            xk.TRUC_THUOC = "HOPLONG";
            xk.LOAI_XUAT_KHO = kho_xuatkho.LOAI_XUAT_KHO;
            xk.PHIEU_BAN_HANG = kho_xuatkho.PHIEU_BAN_HANG;
            db.KHO_XUAT_KHO.Add(xk);

            // Lưu Đơn bán hàng 
            BH_DON_BAN_HANG dbh = db.BH_DON_BAN_HANG.Where(x => x.MA_SO_BH == xk.PHIEU_BAN_HANG).FirstOrDefault();
            if (dbh.DA_XUAT_KHO == false)
            {
                dbh.DA_XUAT_KHO = true;
            }


            //Lưu thông tin tham chiếu
            if (kho_xuatkho.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in kho_xuatkho.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = xk.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (kho_xuatkho.ChiTiet != null && kho_xuatkho.ChiTiet.Count > 0)
            {
                foreach (ChiTietXuatKhoBH item in kho_xuatkho.ChiTiet)
                {
                    KHO_CT_XUAT_KHO newItem = new KHO_CT_XUAT_KHO();
                    newItem.SO_CHUNG_TU = xk.SO_CHUNG_TU;
                    newItem.MA_HANG = item.MA_HANG;

                    newItem.MA_DIEU_CHINH = item.MA_DIEU_CHINH;

                    newItem.MA_KHO_CON = item.MA_KHO_CON;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.DVT = item.DVT;
                    newItem.DON_GIA_BAN = Convert.ToDecimal(item.DON_GIA);
                    newItem.DON_GIA_VON = Convert.ToDecimal(item.DON_GIA_VON);
                    newItem.SO_LUONG = Convert.ToInt32(item.SO_LUONG);
                    newItem.THANH_TIEN = newItem.DON_GIA_BAN * newItem.SO_LUONG;
                    tongtien += newItem.THANH_TIEN;
                    newItem.TK_KHO = item.TK_HACH_TOAN_KHO;
                    db.KHO_CT_XUAT_KHO.Add(newItem);
                    //Cập nhật hàng tồn
                    TONKHO_HOPLONG newHangTon = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == item.MA_HANG && x.MA_KHO_CON == item.MA_KHO_CON).FirstOrDefault();
                    if (newHangTon == null || newHangTon.SL_HOPLONG < item.SO_LUONG)
                    {
                        return Ok("Hàng không có trong kho hoặc SL tồn không đủ");
                    }
                    newHangTon.SL_HOPLONG -= Convert.ToInt32(item.SO_LUONG);
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
                    // Lưu Nhật ký
                    KT_SO_NHAT_KY_CHUNG sonhatky = new KT_SO_NHAT_KY_CHUNG();
                    sonhatky.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                    sonhatky.NGAY_CHUNG_TU = xk.NGAY_CHUNG_TU;
                    sonhatky.NGAY_HACH_TOAN = xk.NGAY_HACH_TOAN;
                    if (xk.NGUOI_NHAN == null)
                    {
                        sonhatky.DOI_TUONG = xk.KHACH_HANG;
                    }
                    else
                    {
                        sonhatky.DOI_TUONG = xk.NGUOI_NHAN;
                    }

                    sonhatky.TRUC_THUOC = "HOPLONG";
                    sonhatky.DIEN_GIAI_CHUNG = xk.LY_DO_XUAT;
                    sonhatky.DIEN_GIAI_CHI_TIET = xk.LY_DO_XUAT;
                    sonhatky.TAI_KHOAN_HACH_TOAN = newItem.TK_NO;
                    sonhatky.TAI_KHOAN_DOI_UNG = newItem.TK_CO;
                    sonhatky.PHAT_SINH_NO = tongtien;
                    sonhatky.PHAT_SINH_CO = 0;
                    db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky);
                    KT_SO_NHAT_KY_CHUNG sonhatky1 = new KT_SO_NHAT_KY_CHUNG();
                    sonhatky1.SO_CHUNG_TU = newItem.SO_CHUNG_TU;
                    sonhatky1.NGAY_CHUNG_TU = xk.NGAY_CHUNG_TU;
                    sonhatky1.NGAY_HACH_TOAN = xk.NGAY_HACH_TOAN;
                    if (xk.NGUOI_NHAN == null)
                    {
                        sonhatky1.DOI_TUONG = xk.KHACH_HANG;
                    }
                    else
                    {
                        sonhatky1.DOI_TUONG = xk.NGUOI_NHAN;
                    }
                    sonhatky1.TRUC_THUOC = "HOPLONG";
                    sonhatky1.DIEN_GIAI_CHUNG = xk.LY_DO_XUAT;
                    sonhatky1.DIEN_GIAI_CHI_TIET = xk.LY_DO_XUAT;
                    sonhatky1.TAI_KHOAN_HACH_TOAN = newItem.TK_CO;
                    sonhatky1.TAI_KHOAN_DOI_UNG = newItem.TK_NO;
                    sonhatky1.PHAT_SINH_NO = 0;
                    sonhatky1.PHAT_SINH_CO = tongtien;
                    db.KT_SO_NHAT_KY_CHUNG.Add(sonhatky1);


                }
            }

            xk.TONG_TIEN = tongtien;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHO_XUAT_KHOExists(kho_xuatkho.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }

            return Ok(xk.SO_CHUNG_TU);
        }

        // DELETE: api/Api_XuatKhoBanHang/5
        [ResponseType(typeof(KHO_XUAT_KHO))]
        public IHttpActionResult DeleteKHO_XUAT_KHO(string id)
        {
            KHO_XUAT_KHO kHO_XUAT_KHO = db.KHO_XUAT_KHO.Find(id);
            if (kHO_XUAT_KHO == null)
            {
                return NotFound();
            }

            db.KHO_XUAT_KHO.Remove(kHO_XUAT_KHO);
            db.SaveChanges();

            return Ok(kHO_XUAT_KHO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHO_XUAT_KHOExists(string id)
        {
            return db.KHO_XUAT_KHO.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}