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
using ERP.Web.Models.NewModels;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ERP.Web.Api.HeThong
{
    public class ThongTinTimKiem
    {
        public string sales { get; set; }
        public string macongty { get; set; }
        public Boolean isadmin { get; set; }
        public string tukhoa { get; set; }
    }
    public class DSTrang
    {
        public int trangso { get; set; }
    }

    public class Api_KHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        //Dem tong so khach hang


        // GET: api/Api_KH
        string mkh;
        public List<GetAll_KhachHang_Result> GetKH()
        {
            var query = db.Database.SqlQuery<GetAll_KhachHang_Result>("GetAll_KhachHang");
            var result = query.ToList();
            return result;
        }

        [Route("api/Search_KH/Search/{mkh}")]
        public List<GetAll_KhachHang_Result> Search_KH(string mkh)
        {

            var query = db.Database.SqlQuery<GetAll_KhachHang_Result>("GetAll_KhachHang");
            var result = query.ToList();
            var kq = result.Where(x => x.TEN_CONG_TY.ToLower().Contains(mkh.ToLower())).Take(10).ToList();
            return kq;
        }

        #region "Tìm kiếm khách"
        [Route("api/Api_KH/KH_THEO_SALES/{page}")]
        public List<HopLong_LocKHTheoSale_Result> KH_THEO_SALES(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_LocKHTheoSale_Result>("HopLong_LocKHTheoSale @sale, @macongty, @isadmin, @tukhoa, @sotrang", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa), new SqlParameter("sotrang", page));
            var result = query.ToList();

            var kq = result.Take(10).ToList();
            return kq;


        }


        [Route("api/Api_KH/TimKhachTheoMa/{page}")]
        public List<HopLong_TimKH_TheoMaKhach_Result> TimKhachTheoMa(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_TimKH_TheoMaKhach_Result>("HopLong_TimKH_TheoMaKhach @sale, @macongty, @isadmin, @tukhoa, @sotrang", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa), new SqlParameter("sotrang", page));
            var result = query.ToList();

            var kq = result.Take(10).ToList();
            return kq;
        }



        [Route("api/Api_KH/TimKhachTheoEmail/{page}")]
        public List<HopLong_TimKH_TheoEmail_Result> TimKhachTheoEmail(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_TimKH_TheoEmail_Result>("HopLong_TimKH_TheoEmail @sale, @macongty, @isadmin, @tukhoa, @sotrang", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa), new SqlParameter("sotrang", page));
            var result = query.ToList();

            var kq = result.Take(10).ToList();
            return kq;
        }



        [Route("api/Api_KH/TimKhachTheoSDT/{page}")]
        public List<HopLong_TimKH_TheoSDT_Result> TimKhachTheoSDT(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_TimKH_TheoSDT_Result>("HopLong_TimKH_TheoSDT @sale, @macongty, @isadmin, @tukhoa, @sotrang", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa), new SqlParameter("sotrang", page));
            var result = query.ToList();

            var kq = result.Take(10).ToList();
            return kq;
        }


        [Route("api/Api_KH/TimKhachTheoTen/{page}")]
        public List<HopLong_TimKH_TheoTen_Result> TimKhachTheoTen(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_TimKH_TheoTen_Result>("HopLong_TimKH_TheoTen @sale, @macongty, @isadmin, @tukhoa, @sotrang", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa), new SqlParameter("sotrang", page));
            var result = query.ToList();

            var kq = result.Take(10).ToList();
            return kq;
        }

        #endregion

        [Route("api/Api_KH/LOC_KH/{page}")]
        public List<HopLong_LocKHTheoSale_Result> LOC_KH(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_LocKHTheoSale_Result>("HopLong_LocKHTheoSale @sale, @macongty, @isadmin, @tukhoa, @sotrang", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa), new SqlParameter("sotrang", page));
            var result = query.ToList();
            return result;

        }
        [Route("api/Api_KH/SoTrangTimKiem")]
        public int SoTrangTimKiem(ThongTinTimKiem timkiem)
        {

            var query = db.Database.SqlQuery<int>("HopLong_LocKHTheoSale_Tongsotrang @sale, @macongty, @isadmin, @tukhoa", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa));
            int kq = query.FirstOrDefault();

            return kq;
        }


        [Route("api/Api_KH/ThongKeMuaHang/{makhach}/{page}")]
        public List<Prod_KH_GetThongKeMuaHang_Result> ThongKeMuaHang(string makhach, int page)
        {
            var query = db.Database.SqlQuery<Prod_KH_GetThongKeMuaHang_Result>("Prod_KH_GetThongKeMuaHang @makhach,@page", new SqlParameter("makhach", makhach), new SqlParameter("page", page));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_KH/LocKH/{username}")]
        public List<GetAll_KhachCuaSale_Result> LocKH(string username)
        {
            var query = db.Database.SqlQuery<GetAll_KhachCuaSale_Result>("GetAll_KhachCuaSale @macongty, @sale", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("sale", username));
            var result = query.ToList();
            return result;
        }

        [HttpPost]
        [Route("api/Api_KH/PhantrangKH/{page}")]
        public List<HopLong_PhanTrangKhachHang_Result> PhantrangKH(int page, ThongTinTimKiem timkiem)
        {
            var query = db.Database.SqlQuery<HopLong_PhanTrangKhachHang_Result>("HopLong_PhanTrangKhachHang @macongty, @sale, @isadmin, @sotrang,@tukhoa", new SqlParameter("macongty", timkiem.macongty), new SqlParameter("sale", timkiem.sales), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("sotrang", page), new SqlParameter("tukhoa", timkiem.tukhoa));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_KH/TongSoTrang/{macongty}")]
        public int TongSoTrang(string macongty)
        {

            var query = db.Database.SqlQuery<int>("Prod_KH_GetTongSoKhach @macongty", new SqlParameter("macongty", macongty));
            int kq = query.FirstOrDefault();

            return kq;
        }

        [Route("api/Api_KH/GET_KHACH_CUA_SALE/{username}/{isadmin}")]
        public List<GetAll_KhachCuaSale_Result> GET_KHACH_CUA_SALE(string username, bool isadmin)
        {
            var query = db.Database.SqlQuery<GetAll_KhachCuaSale_Result>("GetAll_KhachCuaSale  @macongty, @sale,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("sale", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }



        [Route("api/Api_KH/GetAllSale")]
        public List<HopLong_GetAllSale_Result> GetAllSale()
        {
            var query = db.Database.SqlQuery<HopLong_GetAllSale_Result>("HopLong_GetAllSale");
            var result = query.ToList();
            return result;
        }


        // GET: api/Api_KH/5

        [Route("api/Api_KH/GetCT_KH/{makh}")]
        public List<Get_ChiTiet_Tung_KhachHang_Result> GetCT_KH(string makh)
        {
            var query = db.Database.SqlQuery<Get_ChiTiet_Tung_KhachHang_Result>("Get_ChiTiet_Tung_KhachHang @makh,@macongty", new SqlParameter("makh", makh), new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        //Tim kiem thong ke mua hang
        [HttpPost]
        [Route("api/Api_KH/TimKiemThongKeMuaHang/{makhach}/{mahang}")]
        public List<Prod_KH_FindThongKeMuaHang_Result> TimKiemThongKeMuaHang(string makhach, string mahang)
        {
            var query = db.Database.SqlQuery<Prod_KH_FindThongKeMuaHang_Result>("Prod_KH_FindThongKeMuaHang @makhach, @mahang", new SqlParameter("makhach", makhach), new SqlParameter("mahang", mahang));
            var result = query.ToList();
            return result;
        }

        public string GenerateMAKH()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string year = DateTime.Now.Year.ToString().Substring(2, 2);

            string prefixNumber = "KH" + year.ToString();
            string SoChungTu = (from nhapkho in db.KHs where nhapkho.MA_KHACH_HANG.Contains(prefixNumber) select nhapkho.MA_KHACH_HANG).Max();


            if (SoChungTu == null)
            {
                return "KH" + year + "0001";
            }
            SoChungTu = SoChungTu.Substring(4, SoChungTu.Length - 4);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "KH" + year + result;
        }


        // PUT: api/Api_KH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH(string id, KH kH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH.MA_KHACH_HANG)
            {
                return BadRequest();
            }
            var khach = db.KHs.Where(x => x.MA_KHACH_HANG == id).FirstOrDefault();
            if (khach != null)
            {
                if (kH.LOGO != "")
                {
                    khach.LOGO = kH.LOGO;
                }

                khach.TEN_CONG_TY = kH.TEN_CONG_TY;
                khach.VAN_PHONG_GIAO_DICH = kH.VAN_PHONG_GIAO_DICH;
                khach.DIA_CHI_XUAT_HOA_DON = kH.DIA_CHI_XUAT_HOA_DON;
                khach.TINH = kH.TINH;
                khach.QUOC_GIA = kH.QUOC_GIA;
                khach.MST = kH.MST;
                khach.HOTLINE = kH.HOTLINE;
                khach.EMAIL = kH.EMAIL;
                khach.FAX = kH.FAX;
                khach.WEBSITE = kH.WEBSITE;
                khach.DIEU_KHOAN_THANH_TOAN = kH.DIEU_KHOAN_THANH_TOAN;
                khach.SO_NGAY_DUOC_NO = kH.SO_NGAY_DUOC_NO;
                khach.SO_NO_TOI_DA = kH.SO_NO_TOI_DA;
                khach.TINH_TRANG_HOAT_DONG = kH.TINH_TRANG_HOAT_DONG;
                khach.GHI_CHU = kH.GHI_CHU;
                khach.HO_SO_THANH_TOAN = kH.HO_SO_THANH_TOAN;
                khach.TRUC_THUOC = kH.TRUC_THUOC;
                khach.SALES_TAO = kH.SALES_TAO;
                khach.KHACH_DO_MARKETING_TIM_KIEM = kH.KHACH_DO_MARKETING_TIM_KIEM;
                khach.THONG_TIN_DA_DAY_DU = kH.THONG_TIN_DA_DAY_DU;
                khach.KHACH_MUA_SO_LUONG_NHIEU = kH.KHACH_MUA_SO_LUONG_NHIEU;
                khach.KHACH_MUA_DOANH_SO_CAO = kH.KHACH_MUA_DOANH_SO_CAO;
                khach.KHACH_DAC_BIET = kH.KHACH_DAC_BIET;
            }
            //db.Entry(kH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHExists(id))
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


        [Route("api/Api_KH/GetIdKH")]
        public string GetIdKH()
        {
            var query = db.Database.SqlQuery<string>("XL_LayMaKhachMoiNhat");


            if (query.Count() > 0)
            {
                mkh = query.FirstOrDefault();
            }
            return mkh;
        }

        [Route("api/Api_KH/DemTongSoKH_HL")]
        public string DemTongSoKH_HL(ThongTinTimKiem timkiem)
        {

            var query = db.Database.SqlQuery<int>("HopLong_LocKHTheoSale_Tongsotrang @sale, @macongty, @isadmin, @tukhoa", new SqlParameter("sale", timkiem.sales), new SqlParameter("macongty", timkiem.macongty), new SqlParameter("isadmin", timkiem.isadmin), new SqlParameter("tukhoa", timkiem.tukhoa));
            var result = query.FirstOrDefault();
            string kq = result.ToString();
            return kq;
        }

        // POST: api/Api_KH
        [HttpPost]
        [Route("api/Api_KH/ThemMoiKH")]
        public IHttpActionResult ThemMoiKH(KH kH)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            KH khach = new KH();
            khach.MA_KHACH_HANG = GenerateMAKH();
            khach.TEN_CONG_TY = kH.TEN_CONG_TY;
            khach.VAN_PHONG_GIAO_DICH = kH.VAN_PHONG_GIAO_DICH;
            khach.DIA_CHI_XUAT_HOA_DON = kH.DIA_CHI_XUAT_HOA_DON;
            khach.TINH = kH.TINH;
            khach.QUOC_GIA = kH.QUOC_GIA;
            khach.MST = kH.MST;
            khach.HOTLINE = kH.HOTLINE;
            khach.EMAIL = kH.EMAIL;
            khach.FAX = kH.FAX;
            khach.LOGO = kH.LOGO;
            khach.WEBSITE = kH.WEBSITE;
            khach.DIEU_KHOAN_THANH_TOAN = kH.DIEU_KHOAN_THANH_TOAN;
            khach.SO_NGAY_DUOC_NO = kH.SO_NGAY_DUOC_NO;
            khach.SO_NO_TOI_DA = kH.SO_NO_TOI_DA;
            khach.TINH_TRANG_HOAT_DONG = kH.TINH_TRANG_HOAT_DONG;
            khach.GHI_CHU = kH.GHI_CHU;
            khach.HO_SO_THANH_TOAN = kH.HO_SO_THANH_TOAN;
            khach.TRUC_THUOC = kH.TRUC_THUOC;
            khach.SALES_TAO = kH.SALES_TAO;
            khach.KHACH_DO_MARKETING_TIM_KIEM = kH.KHACH_DO_MARKETING_TIM_KIEM;
            khach.THONG_TIN_DA_DAY_DU = kH.THONG_TIN_DA_DAY_DU;
            khach.KHACH_MUA_SO_LUONG_NHIEU = kH.KHACH_MUA_SO_LUONG_NHIEU;
            khach.KHACH_MUA_DOANH_SO_CAO = kH.KHACH_MUA_DOANH_SO_CAO;
            khach.KHACH_DAC_BIET = kH.KHACH_DAC_BIET;
            db.KHs.Add(khach);


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KHExists(kH.MA_KHACH_HANG))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(khach);
        }

        // DELETE: api/Api_KH/5
        [Route("api/Api_KH/DeleteKH/{id}")]
        [ResponseType(typeof(KH))]
        public IHttpActionResult DeleteKH(string id)
        {
            KH khachhang = db.KHs.Find(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            List<KH_TK_NGAN_HANG> tknganhang = new List<KH_TK_NGAN_HANG>();

            tknganhang = db.KH_TK_NGAN_HANG.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in tknganhang)
            {
                db.KH_TK_NGAN_HANG.Remove(item);
            }

            List<KH_THONG_KE_MUA_HANG> thongke = new List<KH_THONG_KE_MUA_HANG>();

            thongke = db.KH_THONG_KE_MUA_HANG.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in thongke)
            {
                db.KH_THONG_KE_MUA_HANG.Remove(item);
            }

            List<KH_LIEN_HE> lienhe = new List<KH_LIEN_HE>();

            lienhe = db.KH_LIEN_HE.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in lienhe)
            {
                var query = db.KH_SALES_PHU_TRACH.Where(x => x.ID_LIEN_HE == item.ID_LIEN_HE).FirstOrDefault();
                if (query != null)
                {
                    db.KH_SALES_PHU_TRACH.Remove(query);
                }
                db.KH_LIEN_HE.Remove(item);
            }

            List<KH_POLICY> policy = new List<KH_POLICY>();

            policy = db.KH_POLICY.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in policy)
            {
                db.KH_POLICY.Remove(item);
            }

            List<KH_PHAN_LOAI_KHACH> phanloai = new List<KH_PHAN_LOAI_KHACH>();

            phanloai = db.KH_PHAN_LOAI_KHACH.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in phanloai)
            {
                db.KH_PHAN_LOAI_KHACH.Remove(item);
            }

            List<KH_CHUYEN_SALES> chuyensale = new List<KH_CHUYEN_SALES>();

            chuyensale = db.KH_CHUYEN_SALES.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in chuyensale)
            {
                db.KH_CHUYEN_SALES.Remove(item);
            }

            List<KH_PHAN_HOI_KHACH_HANG> phanhoi = new List<KH_PHAN_HOI_KHACH_HANG>();

            phanhoi = db.KH_PHAN_HOI_KHACH_HANG.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in phanhoi)
            {
                db.KH_PHAN_HOI_KHACH_HANG.Remove(item);
            }

            List<KH_DC_XUAT_HANG> diachi = new List<KH_DC_XUAT_HANG>();

            diachi = db.KH_DC_XUAT_HANG.Where(x => x.MA_KHACH_HANG == id).ToList();

            foreach (var item in diachi)
            {
                db.KH_DC_XUAT_HANG.Remove(item);
            }

            db.KHs.Remove(khachhang);
            db.SaveChanges();

            return Ok(khachhang);
        }


        [HttpPost]
        [Route("api/Api_KH/CopyNewKH/{mkh}")]
        public IHttpActionResult CopyNewKH(string mkh, KhachHanghl thongtinmoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var thongtinchung = db.KHs.Where(x => x.MA_KHACH_HANG == mkh).FirstOrDefault();
            var diachixuathang = db.KH_DC_XUAT_HANG.Where(x => x.MA_KHACH_HANG == mkh).ToList();
            var lienhe = db.KH_LIEN_HE.Where(x => x.MA_KHACH_HANG == mkh).ToList();
            var phanloai = db.KH_PHAN_LOAI_KHACH.Where(x => x.MA_KHACH_HANG == mkh).FirstOrDefault();
            var taikhoan = db.KH_TK_NGAN_HANG.Where(x => x.MA_KHACH_HANG == mkh).ToList();
            var policy = db.KH_POLICY.Where(x => x.MA_KHACH_HANG == mkh).ToList();
            var phanhoi = db.KH_PHAN_HOI_KHACH_HANG.Where(x => x.MA_KHACH_HANG == mkh).ToList();
            var thongke = db.KH_THONG_KE_MUA_HANG.Where(x => x.MA_KHACH_HANG == mkh).ToList();

            KH newkhachhang = new KH();
            newkhachhang.MA_KHACH_HANG = GenerateMAKH();
            newkhachhang.TEN_CONG_TY = thongtinchung.TEN_CONG_TY;
            newkhachhang.VAN_PHONG_GIAO_DICH = thongtinchung.VAN_PHONG_GIAO_DICH;
            newkhachhang.DIA_CHI_XUAT_HOA_DON = thongtinchung.DIA_CHI_XUAT_HOA_DON;
            newkhachhang.TINH = thongtinchung.TINH;
            newkhachhang.QUOC_GIA = thongtinchung.QUOC_GIA;
            newkhachhang.MST = thongtinchung.MST;
            newkhachhang.HOTLINE = thongtinchung.HOTLINE;
            newkhachhang.EMAIL = thongtinchung.EMAIL;
            newkhachhang.FAX = thongtinchung.FAX;
            newkhachhang.LOGO = thongtinchung.LOGO;
            newkhachhang.WEBSITE = thongtinchung.WEBSITE;
            newkhachhang.DIEU_KHOAN_THANH_TOAN = thongtinchung.DIEU_KHOAN_THANH_TOAN;
            newkhachhang.TINH_TRANG_HOAT_DONG = thongtinchung.TINH_TRANG_HOAT_DONG;
            newkhachhang.SO_NGAY_DUOC_NO = thongtinchung.SO_NGAY_DUOC_NO;
            newkhachhang.SO_NO_TOI_DA = thongtinchung.SO_NO_TOI_DA;
            newkhachhang.GHI_CHU = thongtinchung.GHI_CHU;
            newkhachhang.TRUC_THUOC = thongtinmoi.TRUC_THUOC;
            newkhachhang.SALES_TAO = thongtinmoi.SALES_PHU_TRACH;
            newkhachhang.KHACH_DO_MARKETING_TIM_KIEM = thongtinchung.KHACH_DO_MARKETING_TIM_KIEM;
            newkhachhang.KHACH_MUA_DOANH_SO_CAO = thongtinchung.KHACH_MUA_DOANH_SO_CAO;
            newkhachhang.KHACH_MUA_SO_LUONG_NHIEU = thongtinchung.KHACH_MUA_SO_LUONG_NHIEU;
            newkhachhang.KHACH_DAC_BIET = thongtinchung.KHACH_DAC_BIET;
            newkhachhang.THONG_TIN_DA_DAY_DU = thongtinchung.THONG_TIN_DA_DAY_DU;
            newkhachhang.HO_SO_THANH_TOAN = thongtinchung.HO_SO_THANH_TOAN;
            db.KHs.Add(newkhachhang);
            db.SaveChanges();


            KH_CHUYEN_SALES newchuyensale = new KH_CHUYEN_SALES();
            newchuyensale.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
            newchuyensale.SALE_HIEN_THOI = thongtinmoi.SALES_PHU_TRACH;
            db.KH_CHUYEN_SALES.Add(newchuyensale);
            db.SaveChanges();

            foreach (var item in diachixuathang)
            {
                KH_DC_XUAT_HANG newdiachi = new KH_DC_XUAT_HANG();
                newdiachi.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newdiachi.DIA_CHI_XUAT_HANG = item.DIA_CHI_XUAT_HANG;
                newdiachi.GHI_CHU = item.GHI_CHU;
                db.KH_DC_XUAT_HANG.Add(newdiachi);
                db.SaveChanges();
            }

            foreach (var item in diachixuathang)
            {
                KH_DC_XUAT_HANG newdiachi = new KH_DC_XUAT_HANG();
                newdiachi.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newdiachi.DIA_CHI_XUAT_HANG = item.DIA_CHI_XUAT_HANG;
                newdiachi.GHI_CHU = item.GHI_CHU;
                db.KH_DC_XUAT_HANG.Add(newdiachi);
                db.SaveChanges();
            }

            foreach (var item in lienhe)
            {
                KH_LIEN_HE newlienhe = new KH_LIEN_HE();
                newlienhe.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newlienhe.NGUOI_LIEN_HE = item.NGUOI_LIEN_HE;
                newlienhe.CHUC_VU = item.CHUC_VU;
                newlienhe.PHONG_BAN = item.PHONG_BAN;
                newlienhe.NGAY_SINH = item.NGAY_SINH;
                newlienhe.GIOI_TINH = item.GIOI_TINH;
                newlienhe.EMAIL_CA_NHAN = item.EMAIL_CA_NHAN;
                newlienhe.EMAIL_CONG_TY = item.EMAIL_CONG_TY;
                newlienhe.SKYPE = item.SKYPE;
                newlienhe.FACEBOOK = item.FACEBOOK;
                newlienhe.GHI_CHU = item.GHI_CHU;
                newlienhe.SDT1 = item.SDT1;
                newlienhe.SDT2 = item.SDT2;
                newlienhe.TINH_TRANG_LAM_VIEC = item.TINH_TRANG_LAM_VIEC;
                db.KH_LIEN_HE.Add(newlienhe);
                db.SaveChanges();
            }

            KH_PHAN_LOAI_KHACH newphanloai = new KH_PHAN_LOAI_KHACH();
            newphanloai.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
            newphanloai.MA_LOAI_KHACH = phanloai.MA_LOAI_KHACH;
            newphanloai.NHOM_NGANH = phanloai.NHOM_NGANH;
            db.KH_PHAN_LOAI_KHACH.Add(newphanloai);
            db.SaveChanges();

            foreach (var item in taikhoan)
            {
                KH_TK_NGAN_HANG newtaikhoan = new KH_TK_NGAN_HANG();
                newtaikhoan.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newtaikhoan.SO_TAI_KHOAN = item.SO_TAI_KHOAN;
                newtaikhoan.TEN_TAI_KHOAN = item.TEN_TAI_KHOAN;
                newtaikhoan.TEN_NGAN_HANG = item.TEN_NGAN_HANG;
                newtaikhoan.CHI_NHANH = item.CHI_NHANH;
                newtaikhoan.TINH_TP = item.TINH_TP;
                newtaikhoan.LOAI_TAI_KHOAN = item.LOAI_TAI_KHOAN;
                newtaikhoan.GHI_CHU = item.GHI_CHU;
                db.KH_TK_NGAN_HANG.Add(newtaikhoan);
                db.SaveChanges();
            }

            foreach (var item in policy)
            {
                KH_POLICY newpolicy = new KH_POLICY();
                newpolicy.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newpolicy.MA_NHOM_HANG = item.MA_NHOM_HANG;
                newpolicy.GIA_BAN = item.GIA_BAN;
                newpolicy.CK = item.CK;
                newpolicy.CK_HISTORY_1 = item.CK_HISTORY_1;
                newpolicy.GIA_HISTORY_1 = item.GIA_HISTORY_1;
                newpolicy.CK_HISTORY_2 = item.CK_HISTORY_2;
                newpolicy.GIA_HISTORY_2 = item.GIA_HISTORY_2;
                newpolicy.CK_HISTORY_3 = item.CK_HISTORY_3;
                newpolicy.GIA_HISTORY_3 = item.GIA_HISTORY_3;
                db.KH_POLICY.Add(newpolicy);
                db.SaveChanges();
            }

            foreach (var item in phanhoi)
            {
                KH_PHAN_HOI_KHACH_HANG newphanhoi = new KH_PHAN_HOI_KHACH_HANG();
                newphanhoi.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newphanhoi.NGUOI_PHAN_HOI = item.NGUOI_PHAN_HOI;
                newphanhoi.NGAY_PHAN_HOI = item.NGAY_PHAN_HOI;
                newphanhoi.THONG_TIN_PHAN_HOI = item.THONG_TIN_PHAN_HOI;
                db.KH_PHAN_HOI_KHACH_HANG.Add(newphanhoi);
                db.SaveChanges();
            }

            foreach (var item in thongke)
            {
                KH_THONG_KE_MUA_HANG newthongke = new KH_THONG_KE_MUA_HANG();
                newthongke.MA_KHACH_HANG = newkhachhang.MA_KHACH_HANG;
                newthongke.MA_HANG = item.MA_HANG;
                newthongke.SL_MUA = item.SL_MUA;
                newthongke.DON_GIA_MUA = item.DON_GIA_MUA;
                newthongke.NHAN_VIEN_BAN_HANG = item.NHAN_VIEN_BAN_HANG;
                db.KH_THONG_KE_MUA_HANG.Add(newthongke);
                db.SaveChanges();
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok(newkhachhang.MA_KHACH_HANG);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHExists(string id)
        {
            return db.KHs.Count(e => e.MA_KHACH_HANG == id) > 0;
        }
    }
}