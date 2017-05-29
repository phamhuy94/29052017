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
using System.Data.SqlClient;
using ERP.Web.Models.BusinessModel;
using System.Text.RegularExpressions;
using ERP.Web.Models.NewModels;
using ERP.Web.Models.NewModels.BaoGiaAll;

namespace ERP.Web.Api.BaoGia
{
    public class TongHopBaoGia
    {
        public string username { get; set; }
        public string macongty { get; set; }
        public Boolean isadmin { get; set; }
        public string tukhoa { get; set; }
    }
    public class Api_BaoGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
       


        // GET: api/Api_BaoGia
        //List bao gia kinh doanh
        [Route("api/Api_BaoGia/ListBaoGia/{page}")]
        public List<Prod_BH_ListBaoGia_Result> ListBaoGia(int page,TongHopBaoGia TongHopBaoGia)
        {
            var query = db.Database.SqlQuery<Prod_BH_ListBaoGia_Result>("Prod_BH_ListBaoGia @username,@macongty,@isadmin,@sotrang,@tukhoa", new SqlParameter("username", TongHopBaoGia.username), new SqlParameter("macongty", TongHopBaoGia.macongty),new SqlParameter("isadmin", TongHopBaoGia.isadmin), new SqlParameter("sotrang", page), new SqlParameter("tukhoa", TongHopBaoGia.tukhoa));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_BaoGia/DemTongSoBaoGia")]
        public string DemTongSoBaoGia(TongHopBaoGia TongHopBaoGia)
        {
            var query = db.Database.SqlQuery<int>("Prod_BH_DemTongSoBaoGia @username,@macongty,@isadmin", new SqlParameter("username", TongHopBaoGia.username), new SqlParameter("macongty", TongHopBaoGia.macongty), new SqlParameter("isadmin", TongHopBaoGia.isadmin));
            var result = query.FirstOrDefault();
            string kq = result.ToString();
            return kq;
        }

        [Route("api/Api_BaoGia/ListBaoGiaDaHuy/{isadmin}/{username}")]
        public List<Prod_BH_ListBaoGiaDaHuy_Result> ListBaoGiaDaHuy(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_ListBaoGiaDaHuy_Result>("Prod_BH_ListBaoGiaDaHuy @username,@macongty,@isadmin", new SqlParameter("username", username), new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_BaoGia/ListBaoGiaThatBai/{isadmin}/{username}")]
        public List<Prod_BH_ListBaoGiaThatBai_Result> ListBaoGiaThatBai(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_ListBaoGiaThatBai_Result>("Prod_BH_ListBaoGiaThatBai @username,@macongty,@isadmin", new SqlParameter("username", username), new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_BaoGia/ListBaoGiaDangChoPhanHoi/{isadmin}/{username}")]
        public List<Prod_BH_ListBaoGiaDangChoPhanHoi_Result> ListBaoGiaDangChoPhanHoi(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_ListBaoGiaDangChoPhanHoi_Result>("Prod_BH_ListBaoGiaDangChoPhanHoi @username,@macongty,@isadmin", new SqlParameter("username", username), new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_BaoGia/ListBaoGiaDaLenPO/{isadmin}/{username}")]
        public List<Prod_BH_ListBaoGiaDaLenPO_Result> ListBaoGiaDaLenPO(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_ListBaoGiaDaLenPO_Result>("Prod_BH_ListBaoGiaDaLenPO @username,@macongty,@isadmin", new SqlParameter("username", username), new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_BaoGia/ListBaoGiaThanhCong/{isadmin}/{username}")]
        public List<Prod_BH_ListBaoGiaThanhCong_Result> ListBaoGiaThanhCong(bool isadmin, string username)
        {
            var query = db.Database.SqlQuery<Prod_BH_ListBaoGiaThanhCong_Result>("Prod_BH_ListBaoGiaThanhCong @username,@macongty,@isadmin", new SqlParameter("username", username), new SqlParameter("macongty", "HOPLONG"), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }


        #region "Print Báo giá"
        [Route("api/Api_BaoGia/PrintBaoGia/{sobaogia}")]
        public BaoGia_All PrintBaoGia(string sobaogia)
        {
            var data = db.Database.SqlQuery<Prod_BH_GetThongTinBaoGia_Result>("Prod_BH_GetThongTinBaoGia @so_bao_gia", new SqlParameter("so_bao_gia", sobaogia));
            var resultdata = data.FirstOrDefault();
            var query = db.Database.SqlQuery<Prod_BH_GetThongTin_CT_BaoGia_Result>("Prod_BH_GetThongTin_CT_BaoGia @so_bao_gia", new SqlParameter("so_bao_gia", sobaogia));
            var resultquery = query.ToList();
            BaoGia_All baogia = new BaoGia_All();
            baogia.BG = resultdata;
            baogia.CTBG = resultquery;
            return baogia;
        }
        #endregion

        #region "Lấy khách hàng theo sale"
        [Route("api/Api_BaoGia/KhachHangTheoSale/{username}/{isadmin}")]
        public List<GetAll_KhachCuaSale_Result> KhachHangTheoSale(string username,bool isadmin)
        {
            var query = db.Database.SqlQuery<GetAll_KhachCuaSale_Result>("GetAll_KhachCuaSale @macongty,@sale,@isadmin", new SqlParameter("macongty", "HOPLONG"), new SqlParameter("sale", username), new SqlParameter("isadmin", isadmin));
            var result = query.ToList();
            return result;
        }
        #endregion

        #region "Lấy liên hệ khách"
        [Route("api/Api_BaoGia/GetLienHeKhach/{makhachhang}")]
        public List<GetAll_LienHeTheoKhach_Result> GetLienHeKhach(string makhachhang)
        {
            var query = db.Database.SqlQuery<GetAll_LienHeTheoKhach_Result>("GetAll_LienHeTheoKhach @makhachhang", new SqlParameter("makhachhang", makhachhang));
            var result = query.ToList();
            return result;
        }
        #endregion

        [Route("api/Api_BaoGia/BaoGiaTheoDuKien/{sodukien}")]
        public List<Get_BaoGia_TheoDuKien_Result> GetBaoGiaTheoDuKien(string sodukien)
        {
            var query = db.Database.SqlQuery<Get_BaoGia_TheoDuKien_Result>("Get_BaoGia_TheoDuKien @sodukien,@macongty", new SqlParameter("sodukien", sodukien), new SqlParameter("macongty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_BaoGia/5
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult GetBH_BAO_GIA(string id)
        {
            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            return Ok(bH_BAO_GIA);
        }

        // PUT: api/Api_BaoGia/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_BAO_GIA(string id, BH_BAO_GIA bH_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_BAO_GIA.SO_BAO_GIA)
            {
                return BadRequest();
            }
            var baogia = db.BH_BAO_GIA.Where(x => x.SO_BAO_GIA == id).FirstOrDefault();
            if (baogia != null)
            {
                baogia.PHUONG_THUC_THANH_TOAN = bH_BAO_GIA.PHUONG_THUC_THANH_TOAN;
                baogia.HAN_THANH_TOAN = bH_BAO_GIA.HAN_THANH_TOAN;
                baogia.HIEU_LUC_BAO_GIA = bH_BAO_GIA.HIEU_LUC_BAO_GIA;
                baogia.DIEU_KHOAN_THANH_TOAN = bH_BAO_GIA.DIEU_KHOAN_THANH_TOAN;
                baogia.PHI_VAN_CHUYEN = bH_BAO_GIA.PHI_VAN_CHUYEN;
                baogia.TONG_TIEN = bH_BAO_GIA.TONG_TIEN;
                baogia.THUE_SUAT_GTGT = bH_BAO_GIA.THUE_SUAT_GTGT;
                baogia.TIEN_THUE_GTGT = bH_BAO_GIA.TIEN_THUE_GTGT;
                baogia.TONG_GIA_TRI_DON_HANG_THUC_TE = bH_BAO_GIA.TONG_GIA_TRI_DON_HANG_THUC_TE;
                baogia.GIA_TRI_THUC_THU_TU_KHACH = bH_BAO_GIA.GIA_TRI_THUC_THU_TU_KHACH;
                baogia.TONG_GIA_TRI_CHENH_LECH = bH_BAO_GIA.TONG_GIA_TRI_CHENH_LECH;
                baogia.TONG_CHI_PHI_HOA_DON = bH_BAO_GIA.TONG_CHI_PHI_HOA_DON;
                baogia.THUC_NHAN_CUA_KHACH = bH_BAO_GIA.THUC_NHAN_CUA_KHACH;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_BAO_GIAExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(bH_BAO_GIA);
        }


        public string GenerateSoBaoGia()
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
            string prefixNumber = "BG" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.BH_BAO_GIA where nhapkho.SO_BAO_GIA.Contains(prefixNumber) select nhapkho.SO_BAO_GIA).Max();


            if (SoChungTu == null)
            {
                return "BG" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "BG" + year + month + day + result;
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
            string prefixNumber = "YC" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.BH_DON_HANG_DU_KIEN where nhapkho.MA_DU_KIEN.Contains(prefixNumber) select nhapkho.MA_DU_KIEN).Max();


            if (SoChungTu == null)
            {
                return "YC" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(8, SoChungTu.Length - 8);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "YC" + year + month + day + result;
        }

        // POST: api/Api_BaoGia
        [ResponseType(typeof(BH_BAO_GIA))]
        [Route("api/Api_BaoGia/PostBH_BAO_GIA")]
        public IHttpActionResult PostBH_BAO_GIA(BH_BAO_GIA bH_BAO_GIA)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            BH_BAO_GIA baogia = new BH_BAO_GIA();
            baogia.SO_BAO_GIA = GenerateSoBaoGia();
            baogia.NGAY_BAO_GIA = DateTime.Today.Date;
            baogia.MA_DU_KIEN = bH_BAO_GIA.MA_DU_KIEN;
            baogia.MA_KHACH_HANG = bH_BAO_GIA.MA_KHACH_HANG;
            baogia.LIEN_HE_KHACH_HANG = bH_BAO_GIA.LIEN_HE_KHACH_HANG;
            baogia.PHUONG_THUC_THANH_TOAN = bH_BAO_GIA.PHUONG_THUC_THANH_TOAN;
            baogia.HAN_THANH_TOAN = bH_BAO_GIA.HAN_THANH_TOAN;
            baogia.HIEU_LUC_BAO_GIA = bH_BAO_GIA.HIEU_LUC_BAO_GIA;
            baogia.DIEU_KHOAN_THANH_TOAN = bH_BAO_GIA.DIEU_KHOAN_THANH_TOAN;
            baogia.PHI_VAN_CHUYEN = bH_BAO_GIA.PHI_VAN_CHUYEN;
            baogia.TONG_TIEN = bH_BAO_GIA.TONG_TIEN;
            baogia.TONG_GIA_TRI_DON_HANG_THUC_TE = bH_BAO_GIA.TONG_GIA_TRI_DON_HANG_THUC_TE;
            baogia.GIA_TRI_THUC_THU_TU_KHACH = bH_BAO_GIA.GIA_TRI_THUC_THU_TU_KHACH;
            baogia.TONG_GIA_TRI_CHENH_LECH = bH_BAO_GIA.TONG_GIA_TRI_CHENH_LECH;
            baogia.TONG_CHI_PHI_HOA_DON = bH_BAO_GIA.TONG_CHI_PHI_HOA_DON;
            baogia.THUC_NHAN_CUA_KHACH = bH_BAO_GIA.THUC_NHAN_CUA_KHACH;
            baogia.DA_DUYET = bH_BAO_GIA.DA_DUYET;
            baogia.NGUOI_DUYET = bH_BAO_GIA.NGUOI_DUYET;
            baogia.DA_TRUNG = bH_BAO_GIA.DA_TRUNG;
            baogia.DA_HUY = bH_BAO_GIA.DA_HUY;
            baogia.LY_DO_HUY = bH_BAO_GIA.LY_DO_HUY;
            baogia.SALES_BAO_GIA = bH_BAO_GIA.SALES_BAO_GIA;
            baogia.TRUC_THUOC = bH_BAO_GIA.TRUC_THUOC;
            baogia.DANG_CHO_PHAN_HOI = bH_BAO_GIA.DANG_CHO_PHAN_HOI;
            baogia.THUE_SUAT_GTGT = bH_BAO_GIA.THUE_SUAT_GTGT;
            baogia.TIEN_THUE_GTGT = bH_BAO_GIA.TIEN_THUE_GTGT;
            db.BH_BAO_GIA.Add(baogia);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_BAO_GIAExists(bH_BAO_GIA.SO_BAO_GIA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(baogia);
        }

        // DELETE: api/Api_BaoGia/5
        [ResponseType(typeof(BH_BAO_GIA))]
        public IHttpActionResult DeleteBH_BAO_GIA(string id)
        {
            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(id);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            db.BH_BAO_GIA.Remove(bH_BAO_GIA);
            db.SaveChanges();

            return Ok(bH_BAO_GIA);
        }


        // Gộp báo giá
        [HttpPost]
        [Route("api/Api_BaoGia/GopBaoGia/{baogia1}/{baogia2}")]
        public IHttpActionResult GopBaoGia(string baogia1,string baogia2)
        {
            var thongtinbg1 = db.BH_BAO_GIA.Where(x => x.SO_BAO_GIA == baogia1).FirstOrDefault();
            var thongtinbg2 = db.BH_BAO_GIA.Where(x => x.SO_BAO_GIA == baogia2).FirstOrDefault();
            var chitietbg1 = db.BH_CT_BAO_GIA.Where(x => x.SO_BAO_GIA == baogia1).ToList();
            var chitietbg2 = db.BH_CT_BAO_GIA.Where(x => x.SO_BAO_GIA == baogia2).ToList();

            thongtinbg2.TONG_TIEN = thongtinbg1.TONG_TIEN + thongtinbg2.TONG_TIEN;
            thongtinbg2.TONG_GIA_TRI_DON_HANG_THUC_TE = thongtinbg1.TONG_GIA_TRI_DON_HANG_THUC_TE + thongtinbg2.TONG_GIA_TRI_DON_HANG_THUC_TE;
            thongtinbg2.GIA_TRI_THUC_THU_TU_KHACH = thongtinbg1.GIA_TRI_THUC_THU_TU_KHACH + thongtinbg2.GIA_TRI_THUC_THU_TU_KHACH;
            thongtinbg2.TONG_GIA_TRI_CHENH_LECH = thongtinbg1.TONG_GIA_TRI_CHENH_LECH + thongtinbg2.TONG_GIA_TRI_CHENH_LECH;
            thongtinbg2.TONG_CHI_PHI_HOA_DON = thongtinbg1.TONG_CHI_PHI_HOA_DON + thongtinbg2.TONG_CHI_PHI_HOA_DON;
            thongtinbg2.THUC_NHAN_CUA_KHACH = thongtinbg1.THUC_NHAN_CUA_KHACH + thongtinbg2.THUC_NHAN_CUA_KHACH;
            thongtinbg2.TIEN_THUE_GTGT = thongtinbg1.TIEN_THUE_GTGT + thongtinbg2.TIEN_THUE_GTGT;
            db.SaveChanges();

            foreach (var item in chitietbg1)
            {
                BH_CT_BAO_GIA lienhe = new BH_CT_BAO_GIA();
                lienhe.SO_BAO_GIA = thongtinbg2.SO_BAO_GIA;
                lienhe.MA_HANG = item.MA_HANG;
                lienhe.MA_DIEU_CHINH = item.MA_DIEU_CHINH;
                lienhe.TEN_HANG = item.TEN_HANG;
                lienhe.HANG_SP = item.HANG_SP;
                lienhe.ITEM_CODE = item.ITEM_CODE;
                lienhe.SO_LUONG = item.SO_LUONG;
                lienhe.DVT = item.DVT;
                lienhe.DON_GIA = item.DON_GIA;
                lienhe.THANH_TIEN = item.THANH_TIEN;
                lienhe.THANH_TIEN_NET = item.THANH_TIEN_NET;
                lienhe.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                lienhe.CHIET_KHAU = item.CHIET_KHAU;
                lienhe.GIA_LIST = item.GIA_LIST;
                lienhe.DON_GIA_NHAP = item.DON_GIA_NHAP;
                lienhe.HE_SO_LOI_NHUAN = item.HE_SO_LOI_NHUAN;
                lienhe.DON_GIA_BAO_DI_NET = item.DON_GIA_BAO_DI_NET;
                lienhe.CM = item.CM;
                lienhe.DON_GIA_MOI = item.DON_GIA_MOI;
                lienhe.THUE_TNDN = item.THUE_TNDN;
                lienhe.TIEN_THUE_TNDN = item.TIEN_THUE_TNDN;
                lienhe.KHACH_NHAN_DUOC = item.KHACH_NHAN_DUOC;
                lienhe.GHI_CHU = item.GHI_CHU;
                db.BH_CT_BAO_GIA.Add(lienhe);
            }
            db.SaveChanges();

            foreach (var delete in chitietbg1)
            {
                var xoabaogia = db.BH_CT_BAO_GIA.Where(x => x.SO_BAO_GIA == delete.SO_BAO_GIA).FirstOrDefault();
                if (xoabaogia != null)
                {
                    db.BH_CT_BAO_GIA.Remove(xoabaogia);
                    db.SaveChanges();
                }
            }
            db.SaveChanges();

            BH_BAO_GIA bH_BAO_GIA = db.BH_BAO_GIA.Find(baogia1);
            if (bH_BAO_GIA == null)
            {
                return NotFound();
            }

            db.BH_BAO_GIA.Remove(bH_BAO_GIA);
            db.SaveChanges();


            return Ok(thongtinbg2.SO_BAO_GIA);
        }

        // Tao bao gia tu khach hang
        [HttpPost]
        [Route("api/Api_BaoGia/BaoGiaTuKhach")]
        public IHttpActionResult BaoGiaTuKhach(CopyBaoGia bH_BAO_GIA)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BH_DON_HANG_DU_KIEN newdondukien = new BH_DON_HANG_DU_KIEN();
            newdondukien.MA_DU_KIEN = AutoMA_DU_KIEN();
            newdondukien.NGAY_TAO = DateTime.Today.Date;
            newdondukien.MA_KHACH_HANG = bH_BAO_GIA.MA_KHACH_HANG;
            newdondukien.TRUC_THUOC = bH_BAO_GIA.TRUC_THUOC;
            newdondukien.SALES_QUAN_LY = bH_BAO_GIA.SALES_BAO_GIA;
            newdondukien.ID_LIEN_HE = bH_BAO_GIA.LIEN_HE_KHACH_HANG;
            db.BH_DON_HANG_DU_KIEN.Add(newdondukien);
            db.SaveChanges();

            BH_BAO_GIA baogia = new BH_BAO_GIA();
            baogia.SO_BAO_GIA = GenerateSoBaoGia();
            baogia.NGAY_BAO_GIA = DateTime.Today.Date;
            baogia.MA_DU_KIEN = newdondukien.MA_DU_KIEN;
            baogia.MA_KHACH_HANG = bH_BAO_GIA.MA_KHACH_HANG;
            baogia.LIEN_HE_KHACH_HANG = bH_BAO_GIA.LIEN_HE_KHACH_HANG;
            baogia.PHUONG_THUC_THANH_TOAN = bH_BAO_GIA.PHUONG_THUC_THANH_TOAN;
            baogia.HAN_THANH_TOAN = bH_BAO_GIA.HAN_THANH_TOAN;
            baogia.HIEU_LUC_BAO_GIA = bH_BAO_GIA.HIEU_LUC_BAO_GIA;
            baogia.DIEU_KHOAN_THANH_TOAN = bH_BAO_GIA.DIEU_KHOAN_THANH_TOAN;
            baogia.PHI_VAN_CHUYEN = bH_BAO_GIA.PHI_VAN_CHUYEN;
            baogia.TONG_TIEN = bH_BAO_GIA.TONG_TIEN;
            baogia.TONG_GIA_TRI_DON_HANG_THUC_TE = bH_BAO_GIA.TONG_GIA_TRI_DON_HANG_THUC_TE;
            baogia.GIA_TRI_THUC_THU_TU_KHACH = bH_BAO_GIA.GIA_TRI_THUC_THU_TU_KHACH;
            baogia.TONG_GIA_TRI_CHENH_LECH = bH_BAO_GIA.TONG_GIA_TRI_CHENH_LECH;
            baogia.TONG_CHI_PHI_HOA_DON = bH_BAO_GIA.TONG_CHI_PHI_HOA_DON;
            baogia.THUC_NHAN_CUA_KHACH = bH_BAO_GIA.THUC_NHAN_CUA_KHACH;
            baogia.DA_DUYET = bH_BAO_GIA.DA_DUYET;
            baogia.NGUOI_DUYET = bH_BAO_GIA.NGUOI_DUYET;
            baogia.DA_TRUNG = bH_BAO_GIA.DA_TRUNG;
            baogia.DA_HUY = bH_BAO_GIA.DA_HUY;
            baogia.LY_DO_HUY = bH_BAO_GIA.LY_DO_HUY;
            baogia.SALES_BAO_GIA = bH_BAO_GIA.SALES_BAO_GIA;
            baogia.TRUC_THUOC = bH_BAO_GIA.TRUC_THUOC;
            baogia.DANG_CHO_PHAN_HOI = bH_BAO_GIA.DANG_CHO_PHAN_HOI;
            baogia.THUE_SUAT_GTGT = bH_BAO_GIA.THUE_SUAT_GTGT;
            baogia.TIEN_THUE_GTGT = bH_BAO_GIA.TIEN_THUE_GTGT;
            db.BH_BAO_GIA.Add(baogia);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_BAO_GIAExists(bH_BAO_GIA.SO_BAO_GIA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(baogia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_BAO_GIAExists(string id)
        {
            return db.BH_BAO_GIA.Count(e => e.SO_BAO_GIA == id) > 0;
        }
    }
}