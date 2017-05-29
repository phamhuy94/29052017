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
using ERP.Web.Models.BusinessModel;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using ERP.Web.Models.NewModels.BaoGiaAll;

namespace ERP.Web.Api.BanHang
{
    public class TongHopDonBanHang
    {
        public string username { get; set; }
        public string macongty { get; set; }
        public Boolean isadmin { get; set; }
        public string tukhoa { get; set; }
    }
    public class Api_BanHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        List<GetDonBanHang_ByKhachHang_Result> resultDBHByKhach = new List<GetDonBanHang_ByKhachHang_Result>();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_BanHang

        // List don ban hang
        [Route("api/Api_BanHang/ListDonBanHang/{page}")]
        public List<Prod_BH_List_DonBanHang_Result> ListDonBanHang(int page,TongHopDonBanHang TongHopDonBanHang)
        {
            var query = db.Database.SqlQuery<Prod_BH_List_DonBanHang_Result>("Prod_BH_List_DonBanHang @macongty,@username,@isadmin,@sotrang,@tukhoa", new SqlParameter("macongty", TongHopDonBanHang.macongty), new SqlParameter("username", TongHopDonBanHang.username), new SqlParameter("isadmin", TongHopDonBanHang.isadmin), new SqlParameter("sotrang", page), new SqlParameter("tukhoa", TongHopDonBanHang.tukhoa));
            var result = query.ToList();
            return result;
        }

        // Dem tong so don ban hang
        [Route("api/Api_BanHang/DemTongSoDonBanHang")]
        public string DemTongSoDonBanHang(TongHopDonBanHang TongHopDonBanHang)
        {
            var query = db.Database.SqlQuery<int>("Prod_BH_DemTongSoDonBanHang @username,@macongty,@isadmin", new SqlParameter("username", TongHopDonBanHang.username), new SqlParameter("macongty", TongHopDonBanHang.macongty), new SqlParameter("isadmin", TongHopDonBanHang.isadmin));
            var result = query.FirstOrDefault();
            string kq = result.ToString();
            return kq;
        }

        // List don ban hang chua xuat kho
        [Route("api/Api_BanHang/ListDonBanHangChuaXuatKho/{isadmin}/{username}")]
        public List<Prod_BH_List_DonBanHangChuaXuatKho_Result> ListDonBanHangChuaXuatKho(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_List_DonBanHangChuaXuatKho_Result>("Prod_BH_List_DonBanHangChuaXuatKho @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        // List don ban hang da xuat kho
        [Route("api/Api_BanHang/ListDonBanHangDaXuatKho/{isadmin}/{username}")]
        public List<Prod_BH_List_DonBanHangDaXuatKho_Result> ListDonBanHangDaXuatKho(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_List_DonBanHangDaXuatKho_Result>("Prod_BH_List_DonBanHangDaXuatKho @macongty,@username,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("username", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }


        [Route("api/Api_BanHang/Get_DON_BAN_HANG")]
        public List<GetAll_DonBanHang_Result> Get_DON_BAN_HANG()
        {
            var query = db.Database.SqlQuery<GetAll_DonBanHang_Result>("GetAll_DonBanHang @macongty", new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_BanHang đã Xuất
        [Route("api/Api_BanHang/Get_DON_BAN_HANG_DA_XUAT/{isadmin}/{username}")]
        public List<GetAll_DonBanHangDaXuat_Result> Get_DON_BAN_HANG_DA_XUAT(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<GetAll_DonBanHangDaXuat_Result>("GetAll_DonBanHangDaXuat @macongty,@isadmin,@username", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin), new SqlParameter("username", username));
            var result = query.ToList();
            return result;
        }
        // GET: api/Api_BanHang
        public class GetDonBanHang
        {
            public GetDonBanHang_Result donbanhang { set; get; }
            public List<GetAll_ChiTiet_DonBanHang_Result> ctdonbanhang { set; get; }
        }
        public class DataDBHByKH
        {
            public string makh { get; set; }
           
        }
        [Route("api/Api_BanHang/GetDetailDON_BAN_HANG/{masobanhang}")]
        public GetDonBanHang GetDetailDON_BAN_HANG(string masobanhang)
        {

            //Lưu thông tin nhập kho
            GetDonBanHang dbh = new GetDonBanHang();
            var query = db.Database.SqlQuery<GetDonBanHang_Result>("GetDonBanHang @masobanhang, @macongty", new SqlParameter("masobanhang", masobanhang), new SqlParameter("macongty", "HOPLONG"));
            var data = db.Database.SqlQuery<GetAll_ChiTiet_DonBanHang_Result>("GetAll_ChiTiet_DonBanHang @masoBH", new SqlParameter("masoBH", masobanhang));
            dbh.donbanhang = query.FirstOrDefault();
            dbh.ctdonbanhang = data.ToList();
            return dbh;

        }

        #region "Get ĐƠN BÁN HÀNG theo khách hàng"
        [HttpPost]
        [Route("api/Api_BanHang/GetDBHByKhach")]
        public List<GetDonBanHang_ByKhachHang_Result> GetDBHByKhach(DataDBHByKH data)
        {
           
            var query = db.Database.SqlQuery<GetDonBanHang_ByKhachHang_Result>("GetDonBanHang_ByKhachHang @macongty, @makhachhang", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("makhachhang", data.makh));
            resultDBHByKhach = query.ToList();
            return resultDBHByKhach;
        }

        #endregion

        // GET: api/Api_BanHang/5
        [HttpPost]
        [Route("api/Api_BanHang/GetThongtinChung/{masoBH}")]
        public List<GetAll_ThongTinChungDonBanHang_Result> GetThongtinChung(string masoBH)
        {
            var query = db.Database.SqlQuery<GetAll_ThongTinChungDonBanHang_Result>("GetAll_ThongTinChungDonBanHang @masoBH", new SqlParameter("masoBH", masoBH));
            var result = query.ToList();
            return result;
        }


        // PUT: api/Api_BanHang/5
        [Route("api/Api_BanHang/PutBH_DON_BAN_HANG/{masobh}")]
        public IHttpActionResult PutBH_DON_BAN_HANG(string masobh, DonBanHang bH_DON_BAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (masobh != bH_DON_BAN_HANG.MA_SO_BH)
            {
                return BadRequest();
            }

            var query = db.BH_DON_BAN_HANG.Where(x => x.MA_SO_BH == masobh).FirstOrDefault();
            if (query != null)
            {
                query.NGAY_BH =xlnt.Xulydatetime(bH_DON_BAN_HANG.NGAY_BH.ToString());
                query.MA_KHACH_HANG = bH_DON_BAN_HANG.MA_KHACH_HANG;
                query.TEN_LIEN_HE = bH_DON_BAN_HANG.TEN_LIEN_HE;
                query.HINH_THUC_THANH_TOAN = bH_DON_BAN_HANG.HINH_THUC_THANH_TOAN;
                query.TONG_TIEN_HANG = bH_DON_BAN_HANG.TONG_TIEN_HANG;
                query.SO_TIEN_VIET_BANG_CHU = bH_DON_BAN_HANG.SO_TIEN_VIET_BANG_CHU;
                query.NGAY_GIAO_HANG = xlnt.Xulydatetime(bH_DON_BAN_HANG.NGAY_GIAO_HANG.ToString());
                query.DIA_DIEM_GIAO_HANG = bH_DON_BAN_HANG.DIA_DIEM_GIAO_HANG;
                query.DA_XUAT_KHO = bH_DON_BAN_HANG.DA_XUAT_KHO;
                query.TONG_TIEN_THANH_TOAN = bH_DON_BAN_HANG.TONG_TIEN_THANH_TOAN;
                query.TONG_TIEN_THUE_GTGT = bH_DON_BAN_HANG.TONG_TIEN_THUE_GTGT;
                query.DA_LAP_HOA_DON = bH_DON_BAN_HANG.DA_LAP_HOA_DON;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_DON_BAN_HANGExists(masobh))
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

        // POST: api/Api_BanHang
        [ResponseType(typeof(BH_DON_BAN_HANG))]
        public IHttpActionResult PostBH_DON_BAN_HANG(BH_DON_BAN_HANG bH_DON_BAN_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_DON_BAN_HANG.Add(bH_DON_BAN_HANG);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_DON_BAN_HANGExists(bH_DON_BAN_HANG.MA_SO_BH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bH_DON_BAN_HANG.MA_SO_BH }, bH_DON_BAN_HANG);
        }

        public string GenerateSoBanHang()
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
            string prefixNumber = "BH" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.BH_DON_BAN_HANG where nhapkho.MA_SO_BH.Contains(prefixNumber) select nhapkho.MA_SO_BH).Max();


            if (SoChungTu == null)
            {
                return "BH" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "BH" + year + month + day + result;
        }

        // Them phieu ban hang
        [HttpPost]
        [Route("api/Api_BanHang/PostThemPhieuBanHang")]
        public IHttpActionResult PostThemPhieuBanHang(ThongTinDonPO thongtinPO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BH_DON_BAN_HANG baogia = new BH_DON_BAN_HANG();
            baogia.MA_SO_BH = GenerateSoBanHang();
            baogia.NGAY_BH = DateTime.Today.Date;
            baogia.MA_KHACH_HANG = thongtinPO.MA_KHACH_HANG;
            baogia.TEN_LIEN_HE = thongtinPO.TEN_LIEN_HE;
            baogia.HINH_THUC_THANH_TOAN = thongtinPO.HINH_THUC_THANH_TOAN;
            baogia.TONG_TIEN_THANH_TOAN = thongtinPO.TONG_TIEN_THANH_TOAN;
            baogia.TONG_TIEN_HANG = thongtinPO.TONG_TIEN_HANG;
            baogia.TONG_TIEN_THUE_GTGT = thongtinPO.TONG_TIEN_THUE_GTGT;
            baogia.SO_TIEN_VIET_BANG_CHU = thongtinPO.SO_TIEN_VIET_BANG_CHU;
            baogia.TRUC_THUOC = thongtinPO.TRUC_THUOC;
            baogia.DA_LAP_HOA_DON = thongtinPO.DA_LAP_HOA_DON;
            baogia.NHAN_VIEN_QUAN_LY = thongtinPO.NHAN_VIEN_QUAN_LY;
            if (thongtinPO.NGAY_GIAO_HANG != null)
                baogia.NGAY_GIAO_HANG = xlnt.Xulydatetime(thongtinPO.NGAY_GIAO_HANG.ToString());
            baogia.DIA_DIEM_GIAO_HANG = thongtinPO.DIA_DIEM_GIAO_HANG;
            baogia.DA_XUAT_KHO = thongtinPO.DA_XUAT_KHO;
            baogia.MA_SO_PO = thongtinPO.MA_SO_PO;
            db.BH_DON_BAN_HANG.Add(baogia);
            db.SaveChanges();

            foreach (var item in thongtinPO.ChiTietPO)
            {
                BH_CT_DON_BAN_HANG lienhe = new BH_CT_DON_BAN_HANG();
                lienhe.MA_SO_BH = baogia.MA_SO_BH;
                lienhe.MA_HANG = item.MA_HANG;
                lienhe.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                lienhe.MA_PO = item.MA_SO_PO;
                lienhe.TK_CO = item.TK_CO;
                lienhe.TK_NO = item.TK_NO;
                lienhe.TK_THUE = item.TK_THUE;
                lienhe.SO_LUONG = item.SO_LUONG;
                lienhe.DVT = item.DVT;
                lienhe.DON_GIA = item.DON_GIA;
                lienhe.THANH_TIEN_HANG = item.THANH_TIEN_HANG;
                lienhe.THUE_GTGT = item.THUE_GTGT;
                lienhe.TIEN_THUE_GTGT = ((Convert.ToDouble(item.THANH_TIEN_HANG) * (item.THUE_GTGT / 100)));
                lienhe.TIEN_THANH_TOAN = Convert.ToDouble(lienhe.THANH_TIEN_HANG) + lienhe.TIEN_THUE_GTGT;
                lienhe.DIEN_GIAI_THUE = item.DIEN_GIAI_THUE;
                db.BH_CT_DON_BAN_HANG.Add(lienhe);
            }

            foreach (var item in thongtinPO.ChiTietPO)
            {
                var query = db.BH_CT_DON_HANG_PO.Where(x => x.ID == item.ID).FirstOrDefault();
                if (query != null) {
                    query.DA_BAN = true;
                    db.SaveChanges();
                }
            }

            var data = db.BH_CT_DON_HANG_PO.Where(x => x.MA_SO_PO == thongtinPO.MA_SO_PO && x.DA_BAN == false).ToList();
            if(data.Count() == 0)
            {
                var data1 = db.BH_DON_HANG_PO.Where(x => x.MA_SO_PO == thongtinPO.MA_SO_PO).FirstOrDefault();
                if(data1 != null)
                {
                    data1.DA_BAN_HANG = true;
                }
            }
                try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok(baogia.MA_SO_BH);
        }

        // DELETE: api/Api_BanHang/5
        [Route("api/Api_BanHang/DeleteBH_DON_BAN_HANG/{id}")]
        public IHttpActionResult DeleteBH_DON_BAN_HANG(string id)
        {
            BH_DON_BAN_HANG donbanhang = db.BH_DON_BAN_HANG.Find(id);
            if (donbanhang == null)
            {
                return NotFound();
            }
            List<BH_CT_DON_BAN_HANG> listChiTiet = new List<BH_CT_DON_BAN_HANG>();

            listChiTiet = db.BH_CT_DON_BAN_HANG.Where(x => x.MA_SO_BH == id).ToList();

            foreach (var item in listChiTiet)
            {
                db.BH_CT_DON_BAN_HANG.Remove(item);
            }


            db.BH_DON_BAN_HANG.Remove(donbanhang);
            db.SaveChanges();

            return Ok(donbanhang);
        }

        [HttpPost]
        [Route("api/Api_BanHang/PrintBanHang/{masobh}")]
        public BaoGia_All PrintBanHang(string masobh)
        {
            var data = db.Database.SqlQuery<GetAll_ThongTinChungDonBanHang_Result>("GetAll_ThongTinChungDonBanHang @masoBH", new SqlParameter("masoBH", masobh));
            var resultdata = data.FirstOrDefault();
            var query = db.Database.SqlQuery<GetAll_ChiTiet_DonBanHang_Result>("GetAll_ChiTiet_DonBanHang @masoBH", new SqlParameter("masoBH", masobh));
            var resultquery = query.ToList();
            BaoGia_All banhang = new BaoGia_All();
            banhang.BanHang = resultdata;
            banhang.CTBanHang = resultquery;
            return banhang;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_DON_BAN_HANGExists(string id)
        {
            return db.BH_DON_BAN_HANG.Count(e => e.MA_SO_BH == id) > 0;
        }
    }
}