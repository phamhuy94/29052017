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
using System.Dynamic;
using System.Web.Routing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using ERP.Web.Models.NewModels.All;

namespace ERP.Web.Areas.HopLong.Api.Kho
{
    public class Api_HanghoaHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        //int page_size = 30;


        // GET: api/Api_HanghoaHL
        [Route("api/Api_HanghoaHL/TimKiemHH/{ma_chuan}")]
        public List<HH> TimKiemHH(string ma_chuan)
        {
            // var vData = db.HHs.Where(x => x.MA_NHOM_HANG == ma_chuan);
            var query = db.Database.SqlQuery<HH>("XL_TimKiemHangHoa @tukhoa", new SqlParameter("tukhoa", ma_chuan));
            var result = query.ToList();
            return result;
        }

        [HttpPost]
        [Route("api/Api_HanghoaHL/GetAllHH")]
        public List<Prod_HH_GetHH_TheoNhom_Result> GetAllHH(ThamSo thamso)
        {
            

            var query = db.Database.SqlQuery<Prod_HH_GetHH_TheoNhom_Result>("Prod_HH_GetHH_TheoNhom @manhomhang, @sotrang, @username, @isadmin", new SqlParameter("manhomhang", thamso.manhomhang), new SqlParameter("sotrang", thamso.sotrang), new SqlParameter("username", thamso.manhomhang), new SqlParameter("isadmin", thamso.isadmin));

            var result = query.ToList();

            return result;
        }

        //Hang hoa bao gia
        [HttpGet]
        [Route("api/Api_HanghoaHL/GetAllHHBaoGia/{machuan}")]
        public List<Prod_KHO_GetAllTon_Result> GetAllHHBaoGia(string machuan)
        {
            var query = db.Database.SqlQuery<Prod_KHO_GetAllTon_Result>("Prod_KHO_GetAllTon @macongty,@machuan", new SqlParameter("macongty","HOPLONG"), new SqlParameter("machuan", machuan));
            var resultHH = query.ToList();

            return resultHH;

        }

        // GET: api/Api_HanghoaHL/5

        [ResponseType(typeof(HH))]
        public IHttpActionResult GetHH(string id)
        {
            HH Hh = db.HHs.Find(id);
            if (Hh == null)
            {
                return NotFound();
            }

            return Ok(Hh);
        }

        // PUT: api/Api_HanghoaHL/5
        [Route("api/Api_HanghoaHL/PutDM_HANG_HOA")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDM_HANG_HOA(HH Hh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var hanghoa = db.HHs.Where(x => x.MA_HANG == Hh.MA_HANG).FirstOrDefault();
            if (hanghoa != null)
            {
                if (Hh.HINH_ANH != "")
                {
                    hanghoa.HINH_ANH = Hh.HINH_ANH;
                }

                hanghoa.TEN_HANG = Hh.TEN_HANG;
                hanghoa.MA_NHOM_HANG = Hh.MA_NHOM_HANG;
                hanghoa.MA_CHUAN = Hh.MA_CHUAN;
                hanghoa.THONG_SO = Hh.THONG_SO;
                hanghoa.MA_NHAP_HANG = Hh.MA_NHAP_HANG;
                hanghoa.DON_VI_TINH = Hh.DON_VI_TINH;
                hanghoa.KHOI_LUONG = Hh.KHOI_LUONG;
                hanghoa.XUAT_XU = Hh.XUAT_XU;
                hanghoa.GIA_NHAP = Hh.GIA_NHAP;
                hanghoa.GIA_LIST = Hh.GIA_LIST;
                hanghoa.BAO_HANH = Hh.BAO_HANH;
                hanghoa.THONG_SO_KY_THUAT = Hh.THONG_SO_KY_THUAT;
                hanghoa.QUY_CACH_DONG_GOI = Hh.QUY_CACH_DONG_GOI;
                hanghoa.DISCONTINUE = Hh.DISCONTINUE;
                hanghoa.GHI_CHU = Hh.GHI_CHU;
                hanghoa.MA_CHUYEN_DOI = Hh.MA_CHUYEN_DOI;
                hanghoa.TK_CHI_PHI = Hh.TK_CHI_PHI;
                hanghoa.TK_DOANH_THU = Hh.TK_DOANH_THU;
                hanghoa.TK_HACH_TOAN_KHO = Hh.TK_HACH_TOAN_KHO;
                hanghoa.SERIES = Hh.SERIES;
            }


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public string AutoMA_HANG()
        {
            Regex digitsOnly = new Regex(@"[^\d]");

            string SoChungTu = (from nhapkho in db.HHs select nhapkho.MA_HANG).Max();


            if (SoChungTu == null)
            {
                return "MH" + "00000001";
            }
            SoChungTu = SoChungTu.Substring(2, SoChungTu.Length - 2);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 8 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "MH" + result;
        }

        // POST: api/Api_HanghoaHL
        [Route("api/Api_HanghoaHL/PostHH")]
        public IHttpActionResult PostHH(HH Hh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            HH hanghoa = new HH();
            hanghoa.MA_HANG = AutoMA_HANG();
            if (Hh.HINH_ANH != "")
            {
                hanghoa.HINH_ANH = Hh.HINH_ANH;
            }

            hanghoa.TEN_HANG = Hh.TEN_HANG;
            hanghoa.MA_NHOM_HANG = Hh.MA_NHOM_HANG;
            hanghoa.MA_CHUAN = Hh.MA_CHUAN;
            hanghoa.THONG_SO = Hh.THONG_SO;
            hanghoa.MA_NHAP_HANG = Hh.MA_NHAP_HANG;
            hanghoa.DON_VI_TINH = Hh.DON_VI_TINH;
            hanghoa.KHOI_LUONG = Hh.KHOI_LUONG;
            hanghoa.XUAT_XU = Hh.XUAT_XU;
            hanghoa.GIA_NHAP = Hh.GIA_NHAP;
            hanghoa.GIA_LIST = Hh.GIA_LIST;
            hanghoa.BAO_HANH = Hh.BAO_HANH;
            hanghoa.THONG_SO_KY_THUAT = Hh.THONG_SO_KY_THUAT;
            hanghoa.QUY_CACH_DONG_GOI = Hh.QUY_CACH_DONG_GOI;
            hanghoa.DISCONTINUE = Hh.DISCONTINUE;
            hanghoa.GHI_CHU = Hh.GHI_CHU;
            hanghoa.MA_CHUYEN_DOI = Hh.MA_CHUYEN_DOI;
            hanghoa.TK_CHI_PHI = Hh.TK_CHI_PHI;
            hanghoa.TK_DOANH_THU = Hh.TK_DOANH_THU;
            hanghoa.TK_HACH_TOAN_KHO = Hh.TK_HACH_TOAN_KHO;
            hanghoa.SERIES = Hh.SERIES;
            hanghoa.MA_DO_SALE_TAO = Hh.MA_DO_SALE_TAO;
            db.HHs.Add(hanghoa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_HANG_HOAExists(Hh.MA_HANG))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(hanghoa.MA_HANG);
        }

        // DELETE: api/Api_HanghoaHL/5
        [Route("api/Api_HanghoaHL/DeleteDM_HANG_HOA")]
        [ResponseType(typeof(HH))]
        public IHttpActionResult DeleteDM_HANG_HOA(string id)
        {
            HH Hh = db.HHs.Find(id);
            if (Hh == null)
            {
                return NotFound();
            }

            db.HHs.Remove(Hh);
            db.SaveChanges();

            return Ok(Hh);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_HANG_HOAExists(string id)
        {
            return db.HHs.Count(e => e.MA_HANG == id) > 0;
        }
    }
}