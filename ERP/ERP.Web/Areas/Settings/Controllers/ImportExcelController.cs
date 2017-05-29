using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ERP.Web.Areas.Settings.Controllers
{
        // GET: Settings/ImportExcel
        public class ImportExcelController : Controller
        {
            XuLyNgayThang xulydate = new XuLyNgayThang();
            int so_dong_thanh_cong;
            int dong;
            ERP_DATABASEEntities db = new ERP_DATABASEEntities();
            // GET: HopLong/ImportExcel

        

            #region "Import Tồn kho hãng"
            public ActionResult Import_TonKhoHang()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Import_TonKhoHang(HttpPostedFileBase file)
            {
                try
                {
                    if (Request != null)
                    {
                        HttpPostedFileBase filetonkho = Request.Files["UploadedFile"];
                        if ((filetonkho != null) && (filetonkho.ContentLength > 0) && !string.IsNullOrEmpty(filetonkho.FileName))
                        {
                            string fileName = filetonkho.FileName;
                            string fileContentType = filetonkho.ContentType;
                            byte[] fileBytes = new byte[filetonkho.ContentLength];
                            var data = filetonkho.InputStream.Read(fileBytes, 0, Convert.ToInt32(filetonkho.ContentLength));
                            //var usersList = new List<Users>();
                            using (var package = new ExcelPackage(filetonkho.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    TONKHO_HANG HH = new TONKHO_HANG();
                                    HH.MA_HANG = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    HH.MA_NHOM_HANG = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    HH.SL_HANG = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value.ToString());

                                    db.TONKHO_HANG.Add(HH);

                                    db.SaveChanges();
                                    so_dong_thanh_cong++;
                                    dong = rowIterator - 1;
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

                return View("Import_Hanghoa");
            }

            #endregion

            #region "Import Phòng Ban"
            public ActionResult Import_PhongBan()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Import_PhongBan(FormCollection formCollection)
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
                                    CCTC_PHONG_BAN phongban = new CCTC_PHONG_BAN();
                                    phongban.MA_PHONG_BAN = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    phongban.TEN_PHONG_BAN = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 3].Value != null)
                                        phongban.SDT = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    phongban.MA_CONG_TY = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                        phongban.GHI_CHU = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    db.CCTC_PHONG_BAN.Add(phongban);

                                    db.SaveChanges();
                                    so_dong_thanh_cong++;
                                    dong = rowIterator - 1;
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
                return View("Import_Hanghoa");
            }
            #endregion

            #region "Import nhan vien"
            public ActionResult Import_Nhanvien()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Import_Nhanvien(HttpPostedFileBase file)
            {
                try
                {
                    if (Request != null)
                    {
                        HttpPostedFileBase filetonkho = Request.Files["UploadedFile"];
                        if ((filetonkho != null) && (filetonkho.ContentLength > 0) && !string.IsNullOrEmpty(filetonkho.FileName))
                        {
                            string fileName = filetonkho.FileName;
                            string fileContentType = filetonkho.ContentType;
                            byte[] fileBytes = new byte[filetonkho.ContentLength];
                            var data = filetonkho.InputStream.Read(fileBytes, 0, Convert.ToInt32(filetonkho.ContentLength));
                            //var usersList = new List<Users>();
                            using (var package = new ExcelPackage(filetonkho.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    HT_NGUOI_DUNG user = new HT_NGUOI_DUNG();
                                    user.USERNAME = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    user.PASSWORD = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    user.HO_VA_TEN = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                        user.SDT = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        user.EMAIL = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 11].Value != null)
                                        user.AVATAR = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 15].Value != null)
                                        user.IS_ADMIN = Convert.ToBoolean(workSheet.Cells[rowIterator, 15].Value);
                                    user.ALLOWED = Convert.ToBoolean(workSheet.Cells[rowIterator, 16].Value);
                                    user.MA_CONG_TY = workSheet.Cells[rowIterator, 14].Value.ToString();

                                    db.HT_NGUOI_DUNG.Add(user);


                                    CCTC_NHAN_VIEN nhanvien = new CCTC_NHAN_VIEN();
                                    nhanvien.USERNAME = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                        nhanvien.GIOI_TINH = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 4].Value != null)
                                        nhanvien.NGAY_SINH = xulydate.Xulydatetime(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        nhanvien.QUE_QUAN = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                        nhanvien.TRINH_DO_HOC_VAN = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                        nhanvien.THANH_TICH_CONG_TAC = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 12].Value != null)
                                            nhanvien.MA_PHONG_BAN = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 13].Value != null)
                                            nhanvien.CHUC_VU = workSheet.Cells[rowIterator,13].Value.ToString();
                                    db.CCTC_NHAN_VIEN.Add(nhanvien);

                                    db.SaveChanges();
                                    so_dong_thanh_cong++;
                                    dong = rowIterator - 1;
                                }

                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                    ViewBag.Information = "Lỗi tại dòng thứ: " + (dong+1);

                }
                finally
                {
                    ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
                }

                return View("Import_Hanghoa");
            }

            #endregion

           

            #region "Import công ty"
            public ActionResult Import_Congty()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Import_Congty(HttpPostedFileBase file)
            {
                try
                {
                    if (Request != null)
                    {
                        HttpPostedFileBase filetonkho = Request.Files["UploadedFile"];
                        if ((filetonkho != null) && (filetonkho.ContentLength > 0) && !string.IsNullOrEmpty(filetonkho.FileName))
                        {
                            string fileName = filetonkho.FileName;
                            string fileContentType = filetonkho.ContentType;
                            byte[] fileBytes = new byte[filetonkho.ContentLength];
                            var data = filetonkho.InputStream.Read(fileBytes, 0, Convert.ToInt32(filetonkho.ContentLength));
                            //var usersList = new List<Users>();
                            using (var package = new ExcelPackage(filetonkho.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    CCTC_CONG_TY congty = new CCTC_CONG_TY();
                                    congty.MA_CONG_TY = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    congty.TEN_CONG_TY = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 3].Value != null)
                                        congty.NGAY_THANH_LAP = xulydate.Xulydatetime(workSheet.Cells[rowIterator, 3].Value.ToString());
                                    if (workSheet.Cells[rowIterator, 4].Value != null)
                                        congty.EMAIL = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                        congty.FAX = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                        congty.SDT = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        congty.MST = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        congty.LOGO = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                        congty.DIA_CHI = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                        congty.DIA_CHI_XUAT_HOA_DON = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 11].Value != null)
                                        congty.CONG_TY_ME = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    congty.CAP_TO_CHUC = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 13].Value != null)
                                        congty.GHI_CHU = workSheet.Cells[rowIterator, 13].Value.ToString();

                                    db.CCTC_CONG_TY.Add(congty);

                                    db.SaveChanges();
                                    so_dong_thanh_cong++;
                                    dong = rowIterator - 1;
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

                return View("Import_Hanghoa");
            }

            #endregion

            

            #region "Import hệ thống tài khoản"
            public ActionResult Import_HTtaikhoan()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Import_HTtaikhoan(HttpPostedFileBase file)
            {
                try
                {
                    if (Request != null)
                    {
                        HttpPostedFileBase filetonkho = Request.Files["UploadedFile"];
                        if ((filetonkho != null) && (filetonkho.ContentLength > 0) && !string.IsNullOrEmpty(filetonkho.FileName))
                        {
                            string fileName = filetonkho.FileName;
                            string fileContentType = filetonkho.ContentType;
                            byte[] fileBytes = new byte[filetonkho.ContentLength];
                            var data = filetonkho.InputStream.Read(fileBytes, 0, Convert.ToInt32(filetonkho.ContentLength));
                            //var usersList = new List<Users>();
                            using (var package = new ExcelPackage(filetonkho.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    DM_TAI_KHOAN_HACH_TOAN taikhoan = new DM_TAI_KHOAN_HACH_TOAN();
                                    taikhoan.SO_TK = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    taikhoan.TEN_TK = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    taikhoan.TINH_CHAT = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    taikhoan.TEN_TA = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    taikhoan.TK_CAP_CHA = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    taikhoan.DIEN_GIAI = workSheet.Cells[rowIterator, 6].Value.ToString();

                                    db.DM_TAI_KHOAN_HACH_TOAN.Add(taikhoan);

                                    db.SaveChanges();
                                    so_dong_thanh_cong++;
                                    dong = rowIterator - 1;
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

                return View("Import_Hanghoa");
            }

            #endregion

          
        }

}