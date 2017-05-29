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

namespace ERP.Web.Api.BanHang
{
    public class Api_PhuongAnKinhDoanhController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        public string GenerateMaSoPAKD()
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
            string prefixNumber = "PAKD" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.BH_PHUONG_AN_KINH_DOANH where nhapkho.MA_SO_PAKD.Contains(prefixNumber) select nhapkho.MA_SO_PAKD).Max();


            if (SoChungTu == null)
            {
                return "PAKD" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(10, SoChungTu.Length - 10);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "PAKD" + year + month + day + result;
        }

        // GET: api/Api_PhuongAnKinhDoanh
        public IQueryable<BH_PHUONG_AN_KINH_DOANH> GetBH_PHUONG_AN_KINH_DOANH()
        {
            return db.BH_PHUONG_AN_KINH_DOANH;
        }

        // GET: api/Api_PhuongAnKinhDoanh/5
        [ResponseType(typeof(BH_PHUONG_AN_KINH_DOANH))]
        public IHttpActionResult GetBH_PHUONG_AN_KINH_DOANH(string id)
        {
            BH_PHUONG_AN_KINH_DOANH bH_PHUONG_AN_KINH_DOANH = db.BH_PHUONG_AN_KINH_DOANH.Find(id);
            if (bH_PHUONG_AN_KINH_DOANH == null)
            {
                return NotFound();
            }

            return Ok(bH_PHUONG_AN_KINH_DOANH);
        }

        // PUT: api/Api_PhuongAnKinhDoanh/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_PHUONG_AN_KINH_DOANH(string id, BH_PHUONG_AN_KINH_DOANH bH_PHUONG_AN_KINH_DOANH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_PHUONG_AN_KINH_DOANH.MA_SO_PAKD)
            {
                return BadRequest();
            }

            db.Entry(bH_PHUONG_AN_KINH_DOANH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_PHUONG_AN_KINH_DOANHExists(id))
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

        // POST: api/Api_PhuongAnKinhDoanh
        [ResponseType(typeof(BH_PHUONG_AN_KINH_DOANH))]
        [Route("api/Api_PhuongAnKinhDoanh/PostBH_PHUONG_AN_KINH_DOANH")]
        public IHttpActionResult PostBH_PHUONG_AN_KINH_DOANH(BH_PHUONG_AN_KINH_DOANH bH_PHUONG_AN_KINH_DOANH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BH_PHUONG_AN_KINH_DOANH newphuongan = new BH_PHUONG_AN_KINH_DOANH();
            newphuongan.MA_SO_PAKD = GenerateMaSoPAKD();
            newphuongan.PHIEU_BAO_GIA = bH_PHUONG_AN_KINH_DOANH.PHIEU_BAO_GIA;
            newphuongan.MA_KHACH_HANG = bH_PHUONG_AN_KINH_DOANH.MA_KHACH_HANG;
            newphuongan.NOI_DUNG_PAKD = bH_PHUONG_AN_KINH_DOANH.NOI_DUNG_PAKD;
            newphuongan.TONG_GIA_TRI_VTHH_DAU_VAO = bH_PHUONG_AN_KINH_DOANH.TONG_GIA_TRI_VTHH_DAU_VAO;
            newphuongan.CHI_PHI_KHAC = bH_PHUONG_AN_KINH_DOANH.CHI_PHI_KHAC;
            newphuongan.TONG_GIA_TRI_DON_HANG_THUC_TE = bH_PHUONG_AN_KINH_DOANH.TONG_GIA_TRI_DON_HANG_THUC_TE;
            newphuongan.TONG_GIA_TRI_DON_HANG_THEO_PHIEU_XUAT_HOP_DONG = bH_PHUONG_AN_KINH_DOANH.TONG_GIA_TRI_DON_HANG_THEO_PHIEU_XUAT_HOP_DONG;
            newphuongan.GIA_TRI_CHENH_LECH = bH_PHUONG_AN_KINH_DOANH.GIA_TRI_CHENH_LECH;
            newphuongan.CHI_PHI_HOA_DON = bH_PHUONG_AN_KINH_DOANH.CHI_PHI_HOA_DON;
            newphuongan.TIEN_CHI_PHI_HOA_DON = bH_PHUONG_AN_KINH_DOANH.TIEN_CHI_PHI_HOA_DON;
            newphuongan.THUE_VAT = bH_PHUONG_AN_KINH_DOANH.THUE_VAT;
            newphuongan.TIEN_THUE_VAT = bH_PHUONG_AN_KINH_DOANH.TIEN_THUE_VAT;
            newphuongan.TONG_GIA_TRI_THU_CUA_KHACH = bH_PHUONG_AN_KINH_DOANH.TONG_GIA_TRI_THU_CUA_KHACH;
            newphuongan.LOI_NHUAN_THUAN = bH_PHUONG_AN_KINH_DOANH.LOI_NHUAN_THUAN;
            newphuongan.CHIET_KHAU_CHO_KHACH = bH_PHUONG_AN_KINH_DOANH.CHIET_KHAU_CHO_KHACH;
            newphuongan.THANH_TOAN_KHI_DAT_HANG = bH_PHUONG_AN_KINH_DOANH.THANH_TOAN_KHI_DAT_HANG;
            newphuongan.THANH_TOAN_SAU_GIAO_HANG = bH_PHUONG_AN_KINH_DOANH.THANH_TOAN_SAU_GIAO_HANG;
            newphuongan.HINH_THUC_THANH_TOAN = bH_PHUONG_AN_KINH_DOANH.HINH_THUC_THANH_TOAN;
            newphuongan.HOA_DON_CHUNG_TU = bH_PHUONG_AN_KINH_DOANH.HOA_DON_CHUNG_TU;
            newphuongan.CONG_NO = bH_PHUONG_AN_KINH_DOANH.CONG_NO;
            newphuongan.TRUC_THUOC = bH_PHUONG_AN_KINH_DOANH.TRUC_THUOC;
            newphuongan.NHAN_VIEN_QUAN_LY = bH_PHUONG_AN_KINH_DOANH.NHAN_VIEN_QUAN_LY;
            newphuongan.MA_SO_PO = bH_PHUONG_AN_KINH_DOANH.MA_SO_PO;
            db.BH_PHUONG_AN_KINH_DOANH.Add(newphuongan);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_PHUONG_AN_KINH_DOANHExists(bH_PHUONG_AN_KINH_DOANH.MA_SO_PAKD))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(newphuongan);
        }

        // DELETE: api/Api_PhuongAnKinhDoanh/5
        [ResponseType(typeof(BH_PHUONG_AN_KINH_DOANH))]
        public IHttpActionResult DeleteBH_PHUONG_AN_KINH_DOANH(string id)
        {
            BH_PHUONG_AN_KINH_DOANH bH_PHUONG_AN_KINH_DOANH = db.BH_PHUONG_AN_KINH_DOANH.Find(id);
            if (bH_PHUONG_AN_KINH_DOANH == null)
            {
                return NotFound();
            }

            db.BH_PHUONG_AN_KINH_DOANH.Remove(bH_PHUONG_AN_KINH_DOANH);
            db.SaveChanges();

            return Ok(bH_PHUONG_AN_KINH_DOANH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_PHUONG_AN_KINH_DOANHExists(string id)
        {
            return db.BH_PHUONG_AN_KINH_DOANH.Count(e => e.MA_SO_PAKD == id) > 0;
        }
    }
}