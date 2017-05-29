using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Settings.Controllers
{
    public class ImportBangLuongController : Controller
    {
        // GET: Settings/ImportBangLuong
        XuLyNgayThang xulydate = new XuLyNgayThang();
        int so_dong_thanh_cong;
        int dong;
        ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: HopLong/ImportExcel
        String username, thanglinhluong;
        Decimal luongcoban, luongbaohiem, phucapantrua, phucapdilaidienthoai, phucapthuongdoanhso, phucaptrachnhiem, luongcobanngay, luongcobangio, bhcty, bhnvien, luonglamthucte, luonglamthemngaythuong, luonglamthemngaynghi, luonglamthemngayle, tongtiencong, tamung, vaytindung, phatditre, congdoan, luonglaocong, thuclinh, phatquendongphuc, phatquenthe, phucapthem;
        double congcoban, conglamthuc, conglamthemngaythuong, conglamthemngaynghi, conglamthemngayle, gioditre;



        #region "Import Bảng chấm công"
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        //var usersList = new List<Users>();
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 3].Value.ToString() != "")
                                    username = workSheet.Cells[rowIterator, 3].Value.ToString();
                                else
                                    username = null;
                                if (workSheet.Cells[rowIterator, 4].Value.ToString() != "")
                                    luongcoban = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value);
                                else
                                    luongcoban = 0;
                                if (workSheet.Cells[rowIterator, 5].Value.ToString() != "")
                                    luongbaohiem = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                                else
                                    luongbaohiem = 0;
                                if (workSheet.Cells[rowIterator, 6].Value.ToString() != "")
                                    phucapantrua = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                                else
                                    phucapantrua = 0;
                                if (workSheet.Cells[rowIterator, 7].Value.ToString() != "")
                                    phucapdilaidienthoai = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                                else
                                    phucapdilaidienthoai = 0;
                                if (workSheet.Cells[rowIterator, 8].Value.ToString() != "")
                                    phucapthuongdoanhso = Convert.ToDecimal(workSheet.Cells[rowIterator, 8].Value);
                                else
                                    phucapthuongdoanhso = 0;
                                if (workSheet.Cells[rowIterator, 9].Value.ToString() != "")
                                    phucaptrachnhiem = Convert.ToDecimal(workSheet.Cells[rowIterator, 9].Value);
                                else
                                    phucaptrachnhiem = 0;
                                if (workSheet.Cells[rowIterator, 10].Value.ToString() != "")
                                    congcoban = Convert.ToDouble(workSheet.Cells[rowIterator, 10].Value);
                                else
                                    congcoban = 0;
                                if (workSheet.Cells[rowIterator, 11].Value.ToString() != "")
                                    luongcobanngay = Convert.ToDecimal(workSheet.Cells[rowIterator, 11].Value);
                                else
                                    luongcobanngay = 0;
                                if (workSheet.Cells[rowIterator, 12].Value.ToString() != "")
                                    luongcobangio = Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value);
                                else
                                    luongcobangio = 0;
                                if (workSheet.Cells[rowIterator, 13].Value.ToString() != "")
                                    bhcty = Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value);
                                else
                                    bhcty = 0;
                                if (workSheet.Cells[rowIterator, 14].Value.ToString() != "")
                                    bhnvien = Convert.ToDecimal(workSheet.Cells[rowIterator, 14].Value);
                                else
                                    bhnvien = 0;
                                if (workSheet.Cells[rowIterator, 15].Value.ToString() != "")
                                    conglamthuc = Convert.ToDouble(workSheet.Cells[rowIterator, 15].Value);
                                else
                                    conglamthuc = 0;
                                if (workSheet.Cells[rowIterator, 16].Value.ToString() != "")
                                    luonglamthucte = Convert.ToDecimal(workSheet.Cells[rowIterator, 16].Value);
                                else
                                    luonglamthucte = 0;
                                if (workSheet.Cells[rowIterator, 17].Value.ToString() != "")
                                    conglamthemngaythuong = Convert.ToDouble(workSheet.Cells[rowIterator, 17].Value);
                                else
                                    conglamthemngaythuong = 0;
                                if (workSheet.Cells[rowIterator, 18].Value.ToString() != "")
                                    luonglamthemngaythuong = Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value);
                                else
                                    luonglamthemngaythuong = 0;
                                if (workSheet.Cells[rowIterator, 19].Value.ToString() != "")
                                    conglamthemngaynghi = Convert.ToDouble(workSheet.Cells[rowIterator, 19].Value);
                                else
                                    conglamthemngaynghi = 0;
                                if (workSheet.Cells[rowIterator, 20].Value.ToString() != "")
                                    luonglamthemngaynghi = Convert.ToDecimal(workSheet.Cells[rowIterator, 20].Value);
                                else
                                    luonglamthemngaynghi = 0;
                                if (workSheet.Cells[rowIterator, 21].Value.ToString() != "")
                                    conglamthemngayle = Convert.ToDouble(workSheet.Cells[rowIterator, 21].Value);
                                else
                                    conglamthemngayle = 0;
                                if (workSheet.Cells[rowIterator, 22].Value.ToString() != "")
                                    luonglamthemngayle = Convert.ToDecimal(workSheet.Cells[rowIterator, 22].Value);
                                else
                                    luonglamthemngayle = 0;
                                if (workSheet.Cells[rowIterator, 23].Value.ToString() != "")
                                    tongtiencong = Convert.ToDecimal(workSheet.Cells[rowIterator, 23].Value);
                                else
                                    tongtiencong = 0;
                                if (workSheet.Cells[rowIterator, 24].Value != null)
                                    tamung = Convert.ToDecimal(workSheet.Cells[rowIterator, 24].Value);
                                else
                                    tamung = 0;
                                if (workSheet.Cells[rowIterator, 25].Value != null)
                                    vaytindung = Convert.ToDecimal(workSheet.Cells[rowIterator, 25].Value);
                                else
                                    vaytindung = 0;
                                if (workSheet.Cells[rowIterator, 26].Value!= null)
                                    phatquendongphuc = Convert.ToDecimal(workSheet.Cells[rowIterator, 26].Value);
                                else
                                    phatquendongphuc = 0;
                                if (workSheet.Cells[rowIterator, 27].Value != null)
                                    phatquenthe = Convert.ToDecimal(workSheet.Cells[rowIterator, 27].Value);
                                else
                                    phatquenthe = 0;
                                if (workSheet.Cells[rowIterator, 28].Value != null)
                                    gioditre = Convert.ToDouble(workSheet.Cells[rowIterator, 28].Value);
                                else
                                    gioditre = 0;
                                if (workSheet.Cells[rowIterator, 29].Value != null)
                                    phatditre = Convert.ToDecimal(workSheet.Cells[rowIterator, 29].Value);
                                else
                                    phatditre = 0;
                                if (workSheet.Cells[rowIterator, 30].Value != null)
                                    congdoan = Convert.ToDecimal(workSheet.Cells[rowIterator, 30].Value);
                                else
                                    congdoan = 0;
                                if (workSheet.Cells[rowIterator, 31].Value != null)
                                    luonglaocong = Convert.ToDecimal(workSheet.Cells[rowIterator, 31].Value);
                                else
                                    luonglaocong = 0;
                                if (workSheet.Cells[rowIterator, 32].Value != null)
                                    thuclinh = Convert.ToDecimal(workSheet.Cells[rowIterator, 32].Value);
                                else
                                    thuclinh = 0;
                                if (workSheet.Cells[rowIterator, 33].Value != null)
                                    thanglinhluong = workSheet.Cells[rowIterator, 33].Value.ToString();
                                else
                                    thanglinhluong = null;
                                if (workSheet.Cells[rowIterator, 34].Value != null)
                                    phucapthem = Convert.ToDecimal(workSheet.Cells[rowIterator, 34].Value);
                                else
                                    phucapthem = 0;


                                CCTC_BANG_LUONG bangluong = new CCTC_BANG_LUONG();

                                bangluong.USERNAME = username;
                                bangluong.THANG_LUONG = thanglinhluong;
                                bangluong.LUONG_CO_BAN = luongcoban;
                                bangluong.LUONG_BAO_HIEM = luongbaohiem;
                                bangluong.PHU_CAP_AN_TRUA = phucapantrua;
                                bangluong.PHU_CAP_DI_LAI_DIEN_THOAI = phucapdilaidienthoai;
                                bangluong.PHU_CAP_THUONG_DOANH_SO = phucapthuongdoanhso;
                                bangluong.PHU_CAP_TRACH_NHIEM = phucaptrachnhiem;
                                bangluong.CONG_CO_BAN = congcoban;
                                bangluong.LUONG_CO_BAN_NGAY = luongcobanngay;
                                bangluong.LUONG_CO_BAN_GIO = luongcobangio;
                                bangluong.BAO_HIEM_CONG_TY_DONG = bhcty;
                                bangluong.BAO_HIEM_NHAN_VIEN_DONG = bhnvien;
                                bangluong.LUONG_THUC_TE_CONG_LAM_THUC = conglamthuc;
                                bangluong.LUONG_THUC_TE_SO_TIEN = luonglamthucte;
                                bangluong.LUONG_LAM_THEM_CONG_NGAY_THUONG = conglamthemngaythuong;
                                bangluong.LUONG_LAM_THEM_TIEN_CONG_NGAY_THUONG = luonglamthemngaythuong;
                                bangluong.LUONG_LAM_THEM_CONG_NGAY_NGHI = conglamthemngaynghi;
                                bangluong.LUONG_LAM_THEM_TIEN_CONG_NGAY_NGHI = luonglamthemngaynghi;
                                bangluong.LUONG_LAM_THEM_CONG_NGAY_LE = conglamthemngayle;
                                bangluong.LUONG_LAM_THEM_TIEN_CONG_NGAY_LE = luonglamthemngayle;
                                bangluong.TONG_THU_NHAP = tongtiencong;
                                bangluong.TAM_UNG = tamung;
                                bangluong.VAY_TIN_DUNG = vaytindung;
                                bangluong.GIO_DI_TRE = gioditre;
                                bangluong.PHAT_DI_TRE = phatditre;
                                bangluong.PHAT_QUEN_DONG_PHUC = phatquendongphuc;
                                bangluong.PHAT_QUEN_DEO_THE = phatquenthe;
                                bangluong.PHU_CAP_THEM = phucapthem;
                                bangluong.CONG_DOAN = congdoan;
                                bangluong.LUONG_LAO_CONG = luonglaocong;
                                bangluong.THUC_LINH = thuclinh;
                                db.CCTC_BANG_LUONG.Add(bangluong);
                                db.SaveChanges();






                                so_dong_thanh_cong++;
                                dong = rowIterator;

                            }
                        }
                    }           
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
            }
            return View();
        }
        #endregion



        #region "Update lương"
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(FormCollection formCollection)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        //var usersList = new List<Users>();
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 3].Value.ToString() != "")
                                    username = workSheet.Cells[rowIterator, 3].Value.ToString();
                                else
                                    username = null;
                                if (workSheet.Cells[rowIterator, 4].Value.ToString() != "")
                                    luongcoban = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value);
                                else
                                    luongcoban = 0;
                                if (workSheet.Cells[rowIterator, 5].Value.ToString() != "")
                                    luongbaohiem = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                                else
                                    luongbaohiem = 0;
                                if (workSheet.Cells[rowIterator, 6].Value.ToString() != "")
                                    phucapantrua = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                                else
                                    phucapantrua = 0;
                                if (workSheet.Cells[rowIterator, 7].Value.ToString() != "")
                                    phucapdilaidienthoai = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                                else
                                    phucapdilaidienthoai = 0;
                                if (workSheet.Cells[rowIterator, 8].Value.ToString() != "")
                                    phucapthuongdoanhso = Convert.ToDecimal(workSheet.Cells[rowIterator, 8].Value);
                                else
                                    phucapthuongdoanhso = 0;
                                if (workSheet.Cells[rowIterator, 9].Value.ToString() != "")
                                    phucaptrachnhiem = Convert.ToDecimal(workSheet.Cells[rowIterator, 9].Value);
                                else
                                    phucaptrachnhiem = 0;
                                if (workSheet.Cells[rowIterator, 10].Value.ToString() != "")
                                    congcoban = Convert.ToDouble(workSheet.Cells[rowIterator, 10].Value);
                                else
                                    congcoban = 0;
                                if (workSheet.Cells[rowIterator, 11].Value.ToString() != "")
                                    luongcobanngay = Convert.ToDecimal(workSheet.Cells[rowIterator, 11].Value);
                                else
                                    luongcobanngay = 0;
                                if (workSheet.Cells[rowIterator, 12].Value.ToString() != "")
                                    luongcobangio = Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value);
                                else
                                    luongcobangio = 0;
                                if (workSheet.Cells[rowIterator, 13].Value.ToString() != "")
                                    bhcty = Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value);
                                else
                                    bhcty = 0;
                                if (workSheet.Cells[rowIterator, 14].Value.ToString() != "")
                                    bhnvien = Convert.ToDecimal(workSheet.Cells[rowIterator, 14].Value);
                                else
                                    bhnvien = 0;
                                if (workSheet.Cells[rowIterator, 15].Value.ToString() != "")
                                    conglamthuc = Convert.ToDouble(workSheet.Cells[rowIterator, 15].Value);
                                else
                                    conglamthuc = 0;
                                if (workSheet.Cells[rowIterator, 16].Value.ToString() != "")
                                    luonglamthucte = Convert.ToDecimal(workSheet.Cells[rowIterator, 16].Value);
                                else
                                    luonglamthucte = 0;
                                if (workSheet.Cells[rowIterator, 17].Value.ToString() != "")
                                    conglamthemngaythuong = Convert.ToDouble(workSheet.Cells[rowIterator, 17].Value);
                                else
                                    conglamthemngaythuong = 0;
                                if (workSheet.Cells[rowIterator, 18].Value.ToString() != "")
                                    luonglamthemngaythuong = Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value);
                                else
                                    luonglamthemngaythuong = 0;
                                if (workSheet.Cells[rowIterator, 19].Value.ToString() != "")
                                    conglamthemngaynghi = Convert.ToDouble(workSheet.Cells[rowIterator, 19].Value);
                                else
                                    conglamthemngaynghi = 0;
                                if (workSheet.Cells[rowIterator, 20].Value.ToString() != "")
                                    luonglamthemngaynghi = Convert.ToDecimal(workSheet.Cells[rowIterator, 20].Value);
                                else
                                    luonglamthemngaynghi = 0;
                                if (workSheet.Cells[rowIterator, 21].Value.ToString() != "")
                                    conglamthemngayle = Convert.ToDouble(workSheet.Cells[rowIterator, 21].Value);
                                else
                                    conglamthemngayle = 0;
                                if (workSheet.Cells[rowIterator, 22].Value.ToString() != "")
                                    luonglamthemngayle = Convert.ToDecimal(workSheet.Cells[rowIterator, 22].Value);
                                else
                                    luonglamthemngayle = 0;
                                if (workSheet.Cells[rowIterator, 23].Value.ToString() != "")
                                    tongtiencong = Convert.ToDecimal(workSheet.Cells[rowIterator, 23].Value);
                                else
                                    tongtiencong = 0;
                                if (workSheet.Cells[rowIterator, 24].Value.ToString() != "")
                                    tamung = Convert.ToDecimal(workSheet.Cells[rowIterator, 24].Value);
                                else
                                    tamung = 0;
                                if (workSheet.Cells[rowIterator, 25].Value != null)
                                    vaytindung = Convert.ToDecimal(workSheet.Cells[rowIterator, 25].Value);
                                else
                                    vaytindung = 0;
                                if (workSheet.Cells[rowIterator, 26].Value != null)
                                    phatquendongphuc = Convert.ToDecimal(workSheet.Cells[rowIterator, 26].Value);
                                else
                                    phatquendongphuc = 0;
                                if (workSheet.Cells[rowIterator, 27].Value != null)
                                    phatquenthe = Convert.ToDecimal(workSheet.Cells[rowIterator, 27].Value);
                                else
                                    phatquenthe = 0;
                                if (workSheet.Cells[rowIterator, 28].Value != null)
                                    gioditre = Convert.ToDouble(workSheet.Cells[rowIterator, 28].Value);
                                else
                                    gioditre = 0;
                                if (workSheet.Cells[rowIterator, 29].Value != null)
                                    phatditre = Convert.ToDecimal(workSheet.Cells[rowIterator, 29].Value);
                                else
                                    phatditre = 0;
                                if (workSheet.Cells[rowIterator, 30].Value.ToString() != "")
                                    congdoan = Convert.ToDecimal(workSheet.Cells[rowIterator, 30].Value);
                                else
                                    congdoan = 0;
                                if (workSheet.Cells[rowIterator, 31].Value.ToString() != "")
                                    luonglaocong = Convert.ToDecimal(workSheet.Cells[rowIterator, 31].Value);
                                else
                                    luonglaocong = 0;
                                if (workSheet.Cells[rowIterator, 32].Value.ToString() != "")
                                    thuclinh = Convert.ToDecimal(workSheet.Cells[rowIterator, 32].Value);
                                else
                                    thuclinh = 0;
                                if (workSheet.Cells[rowIterator, 33].Value.ToString() != "")
                                    thanglinhluong = workSheet.Cells[rowIterator, 33].Value.ToString();
                                else
                                    thanglinhluong = null;
                                if (workSheet.Cells[rowIterator, 34].Value != null)
                                    phucapthem = Convert.ToDecimal(workSheet.Cells[rowIterator, 34].Value);
                                else
                                    phucapthem = 0;


                                //CCTC_BANG_LUONG bangluong = new CCTC_BANG_LUONG();
                                var bangluong = db.CCTC_BANG_LUONG.Where(x => x.USERNAME == username && x.THANG_LUONG == thanglinhluong).FirstOrDefault();
                                if(bangluong != null)
                                {
                                    //bangluong.USERNAME = username;
                                    // bangluong.THANG_LUONG = thanglinhluong;
                                    bangluong.LUONG_CO_BAN = luongcoban;
                                    bangluong.LUONG_BAO_HIEM = luongbaohiem;
                                    bangluong.PHU_CAP_AN_TRUA = phucapantrua;
                                    bangluong.PHU_CAP_DI_LAI_DIEN_THOAI = phucapdilaidienthoai;
                                    bangluong.PHU_CAP_THUONG_DOANH_SO = phucapthuongdoanhso;
                                    bangluong.PHU_CAP_TRACH_NHIEM = phucaptrachnhiem;
                                    bangluong.CONG_CO_BAN = congcoban;
                                    bangluong.LUONG_CO_BAN_NGAY = luongcobanngay;
                                    bangluong.LUONG_CO_BAN_GIO = luongcobangio;
                                    bangluong.BAO_HIEM_CONG_TY_DONG = bhcty;
                                    bangluong.BAO_HIEM_NHAN_VIEN_DONG = bhnvien;
                                    bangluong.LUONG_THUC_TE_CONG_LAM_THUC = conglamthuc;
                                    bangluong.LUONG_THUC_TE_SO_TIEN = luonglamthucte;
                                    bangluong.LUONG_LAM_THEM_CONG_NGAY_THUONG = conglamthemngaythuong;
                                    bangluong.LUONG_LAM_THEM_TIEN_CONG_NGAY_THUONG = luonglamthemngaythuong;
                                    bangluong.LUONG_LAM_THEM_CONG_NGAY_NGHI = conglamthemngaynghi;
                                    bangluong.LUONG_LAM_THEM_TIEN_CONG_NGAY_NGHI = luonglamthemngaynghi;
                                    bangluong.LUONG_LAM_THEM_CONG_NGAY_LE = conglamthemngayle;
                                    bangluong.LUONG_LAM_THEM_TIEN_CONG_NGAY_LE = luonglamthemngayle;
                                    bangluong.TONG_THU_NHAP = tongtiencong;
                                    bangluong.TAM_UNG = tamung;
                                    bangluong.VAY_TIN_DUNG = vaytindung;
                                    bangluong.GIO_DI_TRE = gioditre;
                                    bangluong.PHAT_DI_TRE = phatditre;
                                    bangluong.PHAT_QUEN_DONG_PHUC = phatquendongphuc;
                                    bangluong.PHAT_QUEN_DEO_THE = phatquenthe;
                                    bangluong.PHU_CAP_THEM = phucapthem;
                                    bangluong.CONG_DOAN = congdoan;
                                    bangluong.LUONG_LAO_CONG = luonglaocong;
                                    bangluong.THUC_LINH = thuclinh;
                                    db.SaveChanges();
                                }

                               
                                //db.CCTC_BANG_LUONG.Add(bangluong);
                               






                                so_dong_thanh_cong++;
                                dong = rowIterator;

                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
            }
            return View("Index");
        }
        #endregion
    }
}