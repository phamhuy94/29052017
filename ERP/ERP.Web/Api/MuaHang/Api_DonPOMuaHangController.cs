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
using ERP.Web.Models.NewModels.MuaHang;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ERP.Web.Api.MuaHang
{
    public class Api_DonPOMuaHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        List<MH_PO_CT_MUA_HANG> chitietdonPO = new List<MH_PO_CT_MUA_HANG>();

        public string GenerateMaSoPO()
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
            string prefixNumber = "PO" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.MH_PO_MUA_HANG where nhapkho.MA_SO_PO.Contains(prefixNumber) select nhapkho.MA_SO_PO).Max();


            if (SoChungTu == null)
            {
                return "PO" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "PO" + year + month + day + result;
        }

        // GET: api/Api_DonPOMuaHang
        // List PO
        [Route("api/Api_DonPOMuaHang/ListPOMuaHang/{isadmin}/{username}")]
        public List<Prod_ListDonMuaHangPO_Result> ListPOMuaHang(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_ListDonMuaHangPO_Result>("Prod_ListDonMuaHangPO @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }


        // Chi tiet PO mua hang
        [Route("api/Api_DonPOMuaHang/ChiTietPOMuaHang/{masoPO}")]
        public ChiTietPOMuaHang ChiTietPOMuaHang(string masoPO)
        {
            var data = db.Database.SqlQuery<GetAll_ThongTinChungDonHangPO_MuaHang_Result>("GetAll_ThongTinChungDonHangPO_MuaHang @masoPO", new SqlParameter("masoPO", masoPO));
            var resultdata = data.FirstOrDefault();
            var query = db.Database.SqlQuery<GetAll_ChiTiet_DonHangPO_MuaHang_Result>("GetAll_ChiTiet_DonHangPO_MuaHang @masoPO", new SqlParameter("masoPO", masoPO));
            var resultquery = query.ToList();
            ChiTietPOMuaHang baogia = new ChiTietPOMuaHang();
            baogia.ChungPO = resultdata;
            baogia.ChiTietPO = resultquery;
            return baogia;
        }


        // PUT: api/Api_DonPOMuaHang/5
        // Sua thong tin chung PO mua hang
        [Route("api/Api_DonPOMuaHang/EditThongTinChung")]
        public IHttpActionResult EditThongTinChung(MH_PO_MUA_HANG mH_PO_MUA_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = db.MH_PO_MUA_HANG.Where(x => x.MA_SO_PO == mH_PO_MUA_HANG.MA_SO_PO).FirstOrDefault();
            if(query != null)
            {
                query.THUE_VAT = mH_PO_MUA_HANG.THUE_VAT;
                query.DIA_DIEM_GIAO_HANG = mH_PO_MUA_HANG.DIA_DIEM_GIAO_HANG;
                query.HINH_THUC_VAN_CHUYEN = mH_PO_MUA_HANG.HINH_THUC_VAN_CHUYEN;
                query.HINH_THUC_THANH_TOAN = mH_PO_MUA_HANG.HINH_THUC_THANH_TOAN;
                query.THOI_HAN_THANH_TOAN = mH_PO_MUA_HANG.THOI_HAN_THANH_TOAN;
                query.TIEN_THUE_VAT = mH_PO_MUA_HANG.TIEN_THUE_VAT;
                query.TONG_TIEN_BANG_CHU = mH_PO_MUA_HANG.TONG_TIEN_BANG_CHU;
                query.TONG_TIEN_HANG = mH_PO_MUA_HANG.TONG_TIEN_HANG;
                query.THUE_VAT = mH_PO_MUA_HANG.THUE_VAT;
                query.TONG_TIEN_DA_BAO_GOM_VAT = mH_PO_MUA_HANG.TONG_TIEN_DA_BAO_GOM_VAT;
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MH_PO_MUA_HANGExists(mH_PO_MUA_HANG.MA_SO_PO))
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

        // Sua chi tiet don PO mua hang
        [Route("api/Api_DonPOMuaHang/EditChiTietPO")]
        public async Task<IHttpActionResult> EditChiTietPO([FromBody] List<ChiTietPOMuaHang> mH_CT_DON_HANG_PO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != bH_CT_DON_HANG_PO.ID)
            //{
            //    return BadRequest();
            //}
            foreach (var item in mH_CT_DON_HANG_PO)
            {
                var donhangPO = db.MH_PO_CT_MUA_HANG.Where(x => x.ID == item.ID).FirstOrDefault();
                if (donhangPO != null)
                {
                    donhangPO.SL = item.SL;
                    donhangPO.DON_GIA_CHUA_VAT = item.DON_GIA_CHUA_VAT;
                    donhangPO.THANH_TIEN_CHUA_VAT = item.THANH_TIEN_CHUA_VAT;
                    donhangPO.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                    donhangPO.GHI_CHU = item.GHI_CHU;
                }
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw;

            }
            //return this.CreatedAtRoute("GetNH_NTTK", new { id = nH_NTTK.SO_CHUNG_TU }, nH_NTTK);
            return Ok(mH_CT_DON_HANG_PO);
        }

        // POST: api/Api_DonPOMuaHang
        [HttpPost]
        [Route("api/Api_DonPOMuaHang/ThongTinChungPOMuaHang")]
        public IHttpActionResult ThongTinChungPOMuaHang(MH_PO_MUA_HANG mH_PO_MUA_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MH_PO_MUA_HANG newpo = new MH_PO_MUA_HANG();
            newpo.MA_SO_PO = GenerateMaSoPO();
            newpo.NGAY_PO = DateTime.Today.Date;
            newpo.NGUOI_TAO = mH_PO_MUA_HANG.NGUOI_TAO;
            newpo.NHA_CUNG_CAP = mH_PO_MUA_HANG.NHA_CUNG_CAP;
            newpo.NGUOI_LIEN_HE = mH_PO_MUA_HANG.NGUOI_LIEN_HE;
            newpo.DIA_DIEM_GIAO_HANG = mH_PO_MUA_HANG.DIA_DIEM_GIAO_HANG;
            newpo.HINH_THUC_THANH_TOAN = mH_PO_MUA_HANG.HINH_THUC_THANH_TOAN;
            newpo.HINH_THUC_VAN_CHUYEN = mH_PO_MUA_HANG.HINH_THUC_VAN_CHUYEN;
            newpo.THOI_HAN_THANH_TOAN = mH_PO_MUA_HANG.THOI_HAN_THANH_TOAN;
            newpo.TIEN_THUE_VAT = mH_PO_MUA_HANG.TIEN_THUE_VAT;
            newpo.TONG_TIEN_BANG_CHU = mH_PO_MUA_HANG.TONG_TIEN_BANG_CHU;
            newpo.TONG_TIEN_HANG = mH_PO_MUA_HANG.TONG_TIEN_HANG;
            newpo.TONG_TIEN_DA_BAO_GOM_VAT = mH_PO_MUA_HANG.TONG_TIEN_DA_BAO_GOM_VAT;
            newpo.THUE_VAT = mH_PO_MUA_HANG.THUE_VAT;
            db.MH_PO_MUA_HANG.Add(newpo);



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;

            }

            return Ok(newpo);
        }

        //Them Chi tiet PO mua hang
        [HttpPost]
        [Route("api/Api_DonPOMuaHang/ChiTietPOMuaHang")]
        public IHttpActionResult ChiTietPOMuaHang(List<ChiTietPOMuaHang> muahang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(var item in muahang)
            {
                MH_PO_CT_MUA_HANG newpoct = new MH_PO_CT_MUA_HANG();
                newpoct.MA_SO_PO = item.MA_SO_PO;
                newpoct.MA_HANG = item.MA_HANG;
                newpoct.SL = item.SL;
                newpoct.DON_GIA_CHUA_VAT = item.DON_GIA_CHUA_VAT;
                newpoct.THANH_TIEN_CHUA_VAT = item.THANH_TIEN_CHUA_VAT;
                newpoct.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                newpoct.GHI_CHU = item.GHI_CHU;
                newpoct.GIA_BAN_RA = item.GIA_BAN_RA;
                db.MH_PO_CT_MUA_HANG.Add(newpoct);
            }
            db.SaveChanges();

            var masoPO = muahang.FirstOrDefault();

            
            var query = db.MH_PO_CT_MUA_HANG.Where(x =>x.MA_SO_PO == masoPO.MA_SO_PO).ToList();               
            


            try
            {
                 
            }
            catch (DbUpdateException)
            {
               
                    throw;
                
            }

            return Ok(query);
        }

        // DELETE: api/Api_DonPOMuaHang/5
        [ResponseType(typeof(MH_PO_MUA_HANG))]
        public IHttpActionResult DeleteMH_PO_MUA_HANG(string id)
        {
            MH_PO_MUA_HANG mH_PO_MUA_HANG = db.MH_PO_MUA_HANG.Find(id);
            if (mH_PO_MUA_HANG == null)
            {
                return NotFound();
            }

            db.MH_PO_MUA_HANG.Remove(mH_PO_MUA_HANG);
            db.SaveChanges();

            return Ok(mH_PO_MUA_HANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MH_PO_MUA_HANGExists(string id)
        {
            return db.MH_PO_MUA_HANG.Count(e => e.MA_SO_PO == id) > 0;
        }
    }
}