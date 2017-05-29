using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class ImportHangHoaController : Controller
    {
        // GET: Inventory/ImportHangHoa
        XuLyNgayThang xulydate = new XuLyNgayThang();
        int so_dong_thanh_cong;
        int dong;
        string result = " ";
        
        ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        #region "Import Hàng Hóa"
        public ActionResult Import_Hanghoa()
        {
            return View();
        }
        public string GenerateMAHANG()
        {
            Regex digitsOnly = new Regex(@"[^\d]");

            string prefixNumber = "MH";
            string SoChungTu = (from hanghoa in db.HHs where hanghoa.MA_HANG.Contains(prefixNumber) select hanghoa.MA_HANG).Max();


            if (SoChungTu == null)
            {
                return "MH00000001";
            }
            SoChungTu = SoChungTu.Substring(2, SoChungTu.Length - 2);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 8 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "MH"  + result;
        }

        [HttpPost]
        public ActionResult Import_Hanghoa(HttpPostedFileBase file)
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

                                HH hh = new HH();
                                hh.MA_HANG = GenerateMAHANG();
                                if (workSheet.Cells[rowIterator, 3].Value != null)
                                    hh.MA_CHUAN = workSheet.Cells[rowIterator, 3].Value.ToString();
                                if (workSheet.Cells[rowIterator, 4].Value != null)
                                    hh.ITEM_CODE = workSheet.Cells[rowIterator, 4].Value.ToString();
                                if (workSheet.Cells[rowIterator, 5].Value != null)
                                    hh.THONG_SO = workSheet.Cells[rowIterator, 5].Value.ToString();
                                if (workSheet.Cells[rowIterator, 6].Value != null)
                                    hh.MA_NHAP_HANG = workSheet.Cells[rowIterator, 6].Value.ToString();
                                if (workSheet.Cells[rowIterator, 7].Value != null)
                                    hh.TEN_HANG = workSheet.Cells[rowIterator, 7].Value.ToString();

                                if (workSheet.Cells[rowIterator, 8].Value != null)
                                    hh.MA_NHOM_HANG = workSheet.Cells[rowIterator, 8].Value.ToString();
                                if (workSheet.Cells[rowIterator, 9].Value != null)
                                    hh.DON_VI_TINH = workSheet.Cells[rowIterator, 9].Value.ToString();
                                if (workSheet.Cells[rowIterator, 10].Value != null)
                                    hh.KHOI_LUONG = Convert.ToInt32(workSheet.Cells[rowIterator, 10].Value);
                                if (workSheet.Cells[rowIterator, 11].Value != null)
                                    hh.XUAT_XU = workSheet.Cells[rowIterator, 11].Value.ToString();
                                if (workSheet.Cells[rowIterator, 12].Value != null)
                                    hh.GIA_NHAP = Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value.ToString());
                                if (workSheet.Cells[rowIterator, 13].Value != null)
                                    hh.GIA_LIST = Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value.ToString());
                                if (workSheet.Cells[rowIterator, 14].Value != null)
                                    hh.BAO_HANH = Convert.ToInt32(workSheet.Cells[rowIterator, 14].Value.ToString());

                                if (workSheet.Cells[rowIterator, 15].Value != null)
                                    hh.THONG_SO_KY_THUAT = workSheet.Cells[rowIterator, 15].Value.ToString();
                                if (workSheet.Cells[rowIterator, 16].Value != null)
                                    hh.QUY_CACH_DONG_GOI = workSheet.Cells[rowIterator, 16].Value.ToString();

                                if (workSheet.Cells[rowIterator, 17].Value != null)
                                    hh.DISCONTINUE = Convert.ToBoolean(workSheet.Cells[rowIterator, 17].Value);
                                if (workSheet.Cells[rowIterator, 18].Value != null)
                                    hh.MA_CHUYEN_DOI = workSheet.Cells[rowIterator, 18].Value.ToString();

                                if (workSheet.Cells[rowIterator, 19].Value != null)
                                    hh.HINH_ANH = workSheet.Cells[rowIterator, 19].Value.ToString();
                                if (workSheet.Cells[rowIterator, 20].Value != null)
                                    hh.GHI_CHU = workSheet.Cells[rowIterator, 20].Value.ToString();


                                if (workSheet.Cells[rowIterator, 21].Value != null)
                                    hh.TK_HACH_TOAN_KHO = workSheet.Cells[rowIterator, 21].Value.ToString();
                                if (workSheet.Cells[rowIterator, 22].Value != null)
                                    hh.TK_DOANH_THU = workSheet.Cells[rowIterator, 22].Value.ToString();
                                if (workSheet.Cells[rowIterator, 23].Value != null)
                                    hh.TK_CHI_PHI = workSheet.Cells[rowIterator, 23].Value.ToString();
                                if (workSheet.Cells[rowIterator, 24].Value != null)
                                    hh.SERIES = workSheet.Cells[rowIterator, 24].Value.ToString();
                                db.HHs.Add(hh);

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

        #region "Update Hàng Hóa"
        [HttpPost]
        public ActionResult Update_Hanghoa(HttpPostedFileBase file)
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

                                var mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                var hanghoa = db.HHs.Where(x => x.MA_HANG == mahang).FirstOrDefault();
                                if (hanghoa != null)
                                {
                                    //if (workSheet.Cells[rowIterator, 3].Value != null)
                                    //    hanghoa.MA_CHUAN = workSheet.Cells[rowIterator, 3].Value.ToString();

                                    //if (workSheet.Cells[rowIterator, 5].Value != null)
                                    //    hanghoa.THONG_SO = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        hanghoa.TEN_HANG = workSheet.Cells[rowIterator, 7].Value.ToString();

                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        hanghoa.MA_NHOM_HANG = workSheet.Cells[rowIterator, 8].Value.ToString();

                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                        hanghoa.DON_VI_TINH = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                        hanghoa.KHOI_LUONG = Convert.ToInt32(workSheet.Cells[rowIterator, 10].Value);
                                    if (workSheet.Cells[rowIterator, 11].Value != null)
                                        hanghoa.XUAT_XU = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    //if (workSheet.Cells[rowIterator, 12].Value != null)
                                    //    hanghoa.GIA_NHAP = Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value.ToString());
                                    if (workSheet.Cells[rowIterator, 13].Value != null)
                                        hanghoa.GIA_LIST = Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value.ToString());
                                    //if (workSheet.Cells[rowIterator, 14].Value != null)
                                    //    hanghoa.BAO_HANH = Convert.ToInt32(workSheet.Cells[rowIterator, 14].Value.ToString());

                                    //if (workSheet.Cells[rowIterator, 15].Value != null)
                                    //    hanghoa.THONG_SO_KY_THUAT = workSheet.Cells[rowIterator, 15].Value.ToString();
                                    //if (workSheet.Cells[rowIterator, 16].Value != null)
                                    //    hanghoa.QUY_CACH_DONG_GOI = workSheet.Cells[rowIterator, 16].Value.ToString();

                                    //if (workSheet.Cells[rowIterator, 17].Value != null)
                                    //    hanghoa.DISCONTINUE = Convert.ToBoolean(workSheet.Cells[rowIterator, 17].Value);
                                    //if (workSheet.Cells[rowIterator, 18].Value != null)
                                    //    hanghoa.MA_CHUYEN_DOI = workSheet.Cells[rowIterator, 18].Value.ToString();

                                    //if (workSheet.Cells[rowIterator, 19].Value != null)
                                    //    hanghoa.HINH_ANH = workSheet.Cells[rowIterator, 19].Value.ToString();
                                    //if (workSheet.Cells[rowIterator, 20].Value != null)
                                    //    hanghoa.GHI_CHU = workSheet.Cells[rowIterator, 20].Value.ToString();


                                    //if (workSheet.Cells[rowIterator, 21].Value != null)
                                    //    hanghoa.TK_HACH_TOAN_KHO = workSheet.Cells[rowIterator, 21].Value.ToString();
                                    //if (workSheet.Cells[rowIterator, 22].Value != null)
                                    //    hanghoa.TK_DOANH_THU = workSheet.Cells[rowIterator, 22].Value.ToString();
                                    //if (workSheet.Cells[rowIterator, 23].Value != null)
                                    //    hanghoa.TK_CHI_PHI = workSheet.Cells[rowIterator, 23].Value.ToString();
                                    //if (workSheet.Cells[rowIterator, 24].Value != null)
                                    //    hanghoa.SERIES = workSheet.Cells[rowIterator, 24].Value.ToString();
                                    so_dong_thanh_cong++;
                                }
                                else
                                {
                                    result = result + ", " + ((rowIterator - 1).ToString());
                                }
                                db.SaveChanges();
                                
                                //dong = rowIterator;
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                //ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
                ViewBag.Dongloi = "Các dòng bị lỗi là: " + result;
            }

            return View("Import_Hanghoa");
        }

        #endregion

        #region "Import hàng tồn kho Hợp Long"

        [HttpPost]
        public ActionResult Import_Hangtonkho(HttpPostedFileBase file)
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

                                TONKHO_HOPLONG tonkho = new TONKHO_HOPLONG();
                                tonkho.MA_HANG = workSheet.Cells[rowIterator, 2].Value.ToString();
                                tonkho.MA_KHO_CON = workSheet.Cells[rowIterator, 3].Value.ToString();
                                tonkho.SL_HOPLONG = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());

                                db.TONKHO_HOPLONG.Add(tonkho);


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

            return View("Import_Hanghoa");
        }

        #endregion

        #region "Update hàng tồn kho hợp long"

        [HttpPost]
        public ActionResult Update_Hangtonkho(HttpPostedFileBase file)
        {
            
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase filetonkho = Request.Files["UpFile"];
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
                                var mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                var makhocon = workSheet.Cells[rowIterator, 3].Value.ToString();
                                var tonkho = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == mahang && x.MA_KHO_CON == makhocon).FirstOrDefault();
                                if(tonkho != null)
                                {
                                    tonkho.SL_HOPLONG = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    so_dong_thanh_cong++;
                                }
                                else
                                {
                                    result = result + " " + ((rowIterator - 1).ToString())+", ";
                                }

                                

                                //db.DM_HANG_TON_KHO.Add(tonkho);

                                db.SaveChanges();
                                
                               // dong = rowIterator;
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
               // ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
                ViewBag.Dongloi = "Các dòng bị lỗi là: "+ result;
                
                    
                
                
            }

            return View("Import_Hanghoa");
        }

        #endregion

        #region "Import kho"
        [HttpPost]
        public ActionResult Import_Kho(HttpPostedFileBase file)
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
                                DM_KHO kho = new DM_KHO();
                                kho.MA_KHO = workSheet.Cells[rowIterator, 1].Value.ToString();
                                kho.TEN_KHO = workSheet.Cells[rowIterator, 2].Value.ToString();
                                kho.DIA_CHI_KHO = workSheet.Cells[rowIterator, 3].Value.ToString();
                                kho.MA_KHO_CHA = workSheet.Cells[rowIterator, 4].Value.ToString();
                                kho.TRUC_THUOC = workSheet.Cells[rowIterator, 5].Value.ToString();
                                kho.GHI_CHU = workSheet.Cells[rowIterator, 6].Value.ToString();

                                db.DM_KHO.Add(kho);

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

            return View("Import_Hanghoa");
        }

        #endregion

        #region "Import hãng"
        public ActionResult Import_Hangsp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import_Hangsp(HttpPostedFileBase file)
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
                                HH_NHOM_VTHH hangsp = new HH_NHOM_VTHH();
                                hangsp.MA_NHOM_HANG_CHI_TIET = workSheet.Cells[rowIterator, 1].Value.ToString();
                                hangsp.CHUNG_LOAI_HANG = workSheet.Cells[rowIterator, 2].Value.ToString();
                                hangsp.MA_NHOM_HANG_CHA = workSheet.Cells[rowIterator, 3].Value.ToString();
                                hangsp.GHI_CHU = workSheet.Cells[rowIterator, 4].Value.ToString();

                                db.HH_NHOM_VTHH.Add(hangsp);

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

            return View("Import_Hanghoa");
        }

        #endregion



        #region "Tồn Kho HCM"

        #region "Import hàng tồn kho HCM"
        public ActionResult Import_TonKhoHCM()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Import_TonKhoHCM(HttpPostedFileBase file)
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

                                TONKHO_TAHCM tonkho = new TONKHO_TAHCM();
                                tonkho.MA_HANG = workSheet.Cells[rowIterator, 2].Value.ToString();
                                tonkho.MA_KHO_CON = workSheet.Cells[rowIterator, 3].Value.ToString();
                                tonkho.SL_TAHCM = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());

                                db.TONKHO_TAHCM.Add(tonkho);


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

            return View("Import_TonKhoHCM");
        }

        #endregion

        #region "Update hàng tồn kho HCM"

        [HttpPost]
        public ActionResult Update_TonKhoHCM(HttpPostedFileBase file)
        {

            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase filetonkho = Request.Files["UpFile"];
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
                                var mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                var makhocon = workSheet.Cells[rowIterator, 3].Value.ToString();
                                var tonkho = db.TONKHO_TAHCM.Where(x => x.MA_HANG == mahang && x.MA_KHO_CON == makhocon).FirstOrDefault();
                                if (tonkho != null)
                                {
                                    tonkho.SL_TAHCM = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    so_dong_thanh_cong++;
                                }
                                else
                                {
                                    result = result + " " + ((rowIterator - 1).ToString()) + ", ";
                                }



                                //db.DM_HANG_TON_KHO.Add(tonkho);

                                db.SaveChanges();

                                // dong = rowIterator;
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                // ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
                ViewBag.Dongloi = "Các dòng bị lỗi là: " + result;




            }

            return View("Import_TonKhoHCM");
        }

        #endregion

        #endregion



        #region "Tồn Kho DN"

        #region "Import hàng tồn kho DN"
        public ActionResult Import_TonKhoDN()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Import_TonKhoDN(HttpPostedFileBase file)
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

                                TONKHO_TADN tonkho = new TONKHO_TADN();
                                tonkho.MA_HANG = workSheet.Cells[rowIterator, 2].Value.ToString();
                                tonkho.MA_KHO_CON = workSheet.Cells[rowIterator, 3].Value.ToString();
                                tonkho.SL_TADN = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());

                                db.TONKHO_TADN.Add(tonkho);


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

            return View("Import_TonKhoDN");
        }

        #endregion

        #region "Update hàng tồn kho DN"

        [HttpPost]
        public ActionResult Update_TonKhoDN(HttpPostedFileBase file)
        {

            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase filetonkho = Request.Files["UpFile"];
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
                                var mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                var makhocon = workSheet.Cells[rowIterator, 3].Value.ToString();
                                var tonkho = db.TONKHO_TADN.Where(x => x.MA_HANG == mahang && x.MA_KHO_CON == makhocon).FirstOrDefault();
                                if (tonkho != null)
                                {
                                    tonkho.SL_TADN = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    so_dong_thanh_cong++;
                                }
                                else
                                {
                                    result = result + " " + ((rowIterator - 1).ToString()) + ", ";
                                }



                                //db.DM_HANG_TON_KHO.Add(tonkho);

                                db.SaveChanges();

                                // dong = rowIterator;
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                // ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
                ViewBag.Dongloi = "Các dòng bị lỗi là: " + result;




            }

            return View("Import_TonKhoDN");
        }

        #endregion

        #endregion


        #region "Tồn Kho DN"

        #region "Import hàng tồn kho DN"
        public ActionResult Import_TonKhoHP()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Import_TonKhoHP(HttpPostedFileBase file)
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

                                TONKHO_TAHP tonkho = new TONKHO_TAHP();
                                tonkho.MA_HANG = workSheet.Cells[rowIterator, 2].Value.ToString();
                                tonkho.MA_KHO_CON = workSheet.Cells[rowIterator, 3].Value.ToString();
                                tonkho.SL_TAHP = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());

                                db.TONKHO_TAHP.Add(tonkho);


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

            return View("Import_TonKhoHP");
        }

        #endregion

        #region "Update hàng tồn kho HP"

        [HttpPost]
        public ActionResult Update_TonKhoHP(HttpPostedFileBase file)
        {

            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase filetonkho = Request.Files["UpFile"];
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
                                var mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                var makhocon = workSheet.Cells[rowIterator, 3].Value.ToString();
                                var tonkho = db.TONKHO_TAHP.Where(x => x.MA_HANG == mahang && x.MA_KHO_CON == makhocon).FirstOrDefault();
                                if (tonkho != null)
                                {
                                    tonkho.SL_TAHP = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value.ToString());
                                    so_dong_thanh_cong++;
                                }
                                else
                                {
                                    result = result + " " + ((rowIterator - 1).ToString()) + ", ";
                                }



                                //db.DM_HANG_TON_KHO.Add(tonkho);

                                db.SaveChanges();

                                // dong = rowIterator;
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                // ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
                ViewBag.Dongloi = "Các dòng bị lỗi là: " + result;




            }

            return View("Import_TonKhoHP");
        }

        #endregion

        #endregion
    }
}