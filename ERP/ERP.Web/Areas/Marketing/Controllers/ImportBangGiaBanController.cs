using ERP.Web.Models.Database;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Marketing.Controllers
{
    public class ImportBangGiaBanController : Controller
    {
        int so_dong_thanh_cong, dong;
        // GET: Marketing/ImportBangGiaBan
        string mahang, manhomhang, series;
        decimal cksp1, cksp2, cksp3, giasp1, giasp2, giasp3, slmoq1, slmoq2, slmoq3, ckmoq1, ckmoq2, ckmoq3, giamoq1, giamoq2, giamoq3, ckseries1, ckseries2, ckseries3, giaseries1, giaseries2, giaseries3,
            cklistncc1_1, cklistncc1_2, cklistncc1_3, cklistncc2_1, cklistncc2_2, cklistncc2_3, cklistncc3_1, cklistncc3_2, cklistncc3_3, cklistncc4_1, cklistncc4_2, cklistncc4_3,
             gialistncc1_1, gialistncc1_2, gialistncc1_3, gialistncc2_1, gialistncc2_2, gialistncc2_3, gialistncc3_1, gialistncc3_2, gialistncc3_3, gialistncc4_1, gialistncc4_2, gialistncc4_3;
        decimal gialistncc1, gialistncc2, gialistncc3;

        #region "ImportBangGia"
        public ActionResult ImportBangGia()
        {
            return View();
        }


        [HttpPost]

        public ActionResult ImportBangGia(HttpPostedFileBase file)
        {
            using (var db = new ERP_DATABASEEntities())
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
                                for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                                {

                                    mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 3].Value != null)
                                        manhomhang = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    else
                                        manhomhang = null;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 4].Value != null)
                                        series = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    else
                                        series = null;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                         cksp1= Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                                    else
                                        cksp1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                       giasp1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                                    else
                                        giasp1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        cksp2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                                    else
                                        cksp2 = 0;
                                    //========================================
                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        giasp2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 8].Value);
                                    else
                                        giasp2 = 0;
                                    //=======================================

                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                        cksp3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 9].Value);
                                    else
                                        cksp3 = 0;
                                    //==============================================
                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                        giasp3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 10].Value);
                                    else
                                        giasp3= 0;






                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 11].Value != null)
                                        slmoq1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 11].Value);
                                    else
                                        slmoq1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 12].Value != null)
                                        ckmoq1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value);
                                    else
                                        ckmoq1 = 0;
                                    //=======================================

                                    if (workSheet.Cells[rowIterator, 13].Value != null)
                                        giamoq1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value);
                                    else
                                        giamoq1 = 0;
                                    //======================================

                                    if (workSheet.Cells[rowIterator, 14].Value != null)
                                        slmoq2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 14].Value);
                                    else
                                        slmoq2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 15].Value != null)
                                        ckmoq2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 15].Value);
                                    else
                                        ckmoq2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 16].Value != null)
                                        giamoq2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 16].Value);
                                    else
                                        giamoq2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 17].Value != null)
                                        slmoq3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 17].Value);
                                    else
                                        slmoq3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 18].Value != null)
                                        ckmoq3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value);
                                    else
                                        ckmoq3 = 0;
                                    //=======================================

                                    if (workSheet.Cells[rowIterator, 19].Value != null)
                                        giamoq3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 19].Value);
                                    else
                                        giamoq3 = 0;


                                    //======================================
                                    if (workSheet.Cells[rowIterator, 26].Value != null)
                                        ckseries1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 26].Value);
                                    else
                                        ckseries1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 27].Value != null)
                                        giaseries1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 27].Value);
                                    else
                                        giaseries1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 28].Value != null)
                                        ckseries2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 28].Value);
                                    else
                                        ckseries2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 29].Value != null)
                                        giaseries2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 29].Value);
                                    else
                                        giaseries2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 30].Value != null)
                                        ckseries3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 30].Value);
                                    else
                                        ckseries3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 31].Value != null)
                                        giaseries3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 31].Value);
                                    else
                                        giaseries3 = 0;




                                    //======================================
                                    if (workSheet.Cells[rowIterator, 20].Value != null)
                                        cklistncc1_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 20].Value);
                                    else
                                        cklistncc1_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 21].Value != null)
                                        gialistncc1_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 21].Value);
                                    else
                                        gialistncc1_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 22].Value != null)
                                        cklistncc1_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 22].Value);
                                    else
                                        cklistncc1_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator,23].Value != null)
                                        gialistncc1_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 23].Value);
                                    else
                                        gialistncc1_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 24].Value != null)
                                        cklistncc1_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 24].Value);
                                    else
                                        cklistncc1_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 25].Value != null)
                                        gialistncc1_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 25].Value);
                                    else
                                        gialistncc1_3 = 0;
                                    






                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 32].Value != null)
                                        cklistncc2_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 32].Value);
                                    else
                                        cklistncc2_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 33].Value != null)
                                        gialistncc2_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 33].Value);
                                    else
                                        gialistncc2_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 34].Value != null)
                                        cklistncc2_2 = Convert.ToDecimal(workSheet.Cells[rowIterator,34].Value);
                                    else
                                        cklistncc2_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 35].Value != null)
                                        gialistncc2_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 35].Value);
                                    else
                                        gialistncc2_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 36].Value != null)
                                        cklistncc2_3 = Convert.ToDecimal(workSheet.Cells[rowIterator,36].Value);
                                    else
                                        cklistncc2_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 37].Value != null)
                                        gialistncc2_3 = Convert.ToDecimal(workSheet.Cells[rowIterator,37].Value);
                                    else
                                        gialistncc2_3 = 0;







                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 38].Value != null)
                                        cklistncc3_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 38].Value);
                                    else
                                        cklistncc3_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 39].Value != null)
                                        gialistncc3_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 39].Value);
                                    else
                                        gialistncc3_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 40].Value != null)
                                        cklistncc3_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 40].Value);
                                    else
                                        cklistncc3_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 41].Value != null)
                                        gialistncc3_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 41].Value);
                                    else
                                        gialistncc3_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 42].Value != null)
                                        cklistncc3_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 42].Value);
                                    else
                                        cklistncc3_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 43].Value != null)
                                        gialistncc3_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 43].Value);
                                    else
                                        gialistncc3_3 = 0;




                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 44].Value != null)
                                        cklistncc4_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 44].Value);
                                    else
                                        cklistncc4_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 45].Value != null)
                                        gialistncc4_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 45].Value);
                                    else
                                        gialistncc4_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 46].Value != null)
                                        cklistncc4_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 46].Value);
                                    else
                                        cklistncc4_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 47].Value != null)
                                        gialistncc4_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 47].Value);
                                    else
                                        gialistncc4_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 48].Value != null)
                                        cklistncc4_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 48].Value);
                                    else
                                        cklistncc4_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator,49].Value != null)
                                        gialistncc4_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 49].Value);
                                    else
                                        gialistncc4_3 = 0;

                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 50].Value != null)
                                        cklistncc4_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 50].Value);
                                    else
                                        cklistncc4_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 51].Value != null)
                                        gialistncc4_2 = Convert.ToDecimal(workSheet.Cells[rowIterator,51].Value);
                                    else
                                        gialistncc4_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 52].Value != null)
                                        cklistncc4_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 52].Value);
                                    else
                                        cklistncc4_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 53].Value != null)
                                        gialistncc4_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 53].Value);
                                    else
                                        gialistncc4_3 = 0;



                                    

                                    //======================================
                                    if (workSheet.Cells[rowIterator, 51].Value != null)
                                        gialistncc1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 51].Value);
                                    else
                                        gialistncc1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 52].Value != null)
                                        gialistncc2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 52].Value);
                                    else
                                        gialistncc2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 53].Value != null)
                                        gialistncc3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 53].Value);
                                    else
                                        gialistncc3 = 0;



                                    //===============================================


                                    HH_BANG_GIA_BAN banggia = new HH_BANG_GIA_BAN();

                                    banggia.NGAY_CAP_NHAT = DateTime.Today.Date;
                                    banggia.MA_HANG = mahang;
                                    banggia.MA_NHOM_HANG = manhomhang;
                                    banggia.SERIES = series;


                                    //------------------------------
                                    banggia.GIA_LIST_NCC1 = gialistncc1;
                                    banggia.GIA_LIST_NCC2 = gialistncc2;
                                    banggia.GIA_LIST_NCC3 = gialistncc3;


                                    //------------------------------
                                    banggia.CK_SP_1 = cksp1;
                                    banggia.GIA_SP_1 = giasp1;
                                    banggia.CK_SP_2 = cksp2;
                                    banggia.GIA_SP_2 = giasp2;
                                    banggia.CK_SP_3 = cksp3;
                                    banggia.GIA_SP_3 = giasp3;

                                    //-----------------------------
                                    banggia.SL_MOQ_1 = slmoq1;
                                    banggia.CK_MOQ_1 = ckmoq1;
                                    banggia.GIA_MOQ_1 = giamoq1;
                                    banggia.SL_MOQ_2 = slmoq2;
                                    banggia.CK_MOQ_2 = ckmoq2;
                                    banggia.GIA_MOQ_2 = giamoq2;
                                    banggia.SL_MOQ_3 = slmoq3;
                                    banggia.CK_MOQ_3 = ckmoq3;
                                    banggia.GIA_MOQ_3 = giamoq3;

                                    //------------------------------
                                    banggia.CK_SERIES_1 = ckseries1;
                                    banggia.GIA_SERIES_1 = giaseries1;
                                    banggia.CK_SERIES_2 = ckseries2;
                                    banggia.GIA_SERIES_2 = giaseries2;
                                    banggia.CK_SERIES_3 = ckseries3;
                                    banggia.GIA_SERIES_3 = giaseries3;

                                    //---------------------------------
                                    banggia.CK_LIST_NCC1_1 = cklistncc1_1;
                                    banggia.GIA_LIST_NCC1_1 = gialistncc1_1;
                                    banggia.CK_LIST_NCC1_2 = cklistncc1_2;
                                    banggia.GIA_LIST_NCC1_2 = gialistncc1_2;
                                    banggia.CK_LIST_NCC1_3 = cklistncc1_3;
                                    banggia.GIA_LIST_NCC1_3 = gialistncc1_3;

                                    //---------------------------------
                                    banggia.CK_LIST_NCC2_1 = cklistncc2_1;
                                    banggia.GIA_LIST_NCC2_1 = gialistncc2_1;
                                    banggia.CK_LIST_NCC2_2 = cklistncc2_2;
                                    banggia.GIA_LIST_NCC2_2 = gialistncc2_2;
                                    banggia.CK_LIST_NCC2_3 = cklistncc2_3;
                                    banggia.GIA_LIST_NCC2_3 = gialistncc2_3;


                                    //---------------------------------
                                    banggia.CK_LIST_NCC3_1 = cklistncc3_1;
                                    banggia.GIA_LIST_NCC3_1 = gialistncc3_1;
                                    banggia.CK_LIST_NCC3_2 = cklistncc3_2;
                                    banggia.GIA_LIST_NCC3_2 = gialistncc3_2;
                                    banggia.CK_LIST_NCC3_3 = cklistncc3_3;
                                    banggia.GIA_LIST_NCC3_3 = gialistncc3_3;



                                    //---------------------------------
                                    banggia.CK_LIST_NCC4_1 = cklistncc4_1;
                                    banggia.GIA_LIST_NCC4_1 = gialistncc4_1;
                                    banggia.CK_LIST_NCC4_2 = cklistncc4_2;
                                    banggia.GIA_LIST_NCC4_2 = gialistncc4_2;
                                    banggia.CK_LIST_NCC4_3 = cklistncc4_3;
                                    banggia.GIA_LIST_NCC4_3 = gialistncc4_3;


                                    db.HH_BANG_GIA_BAN.Add(banggia);

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

                return View();

            }
               
        }
        #endregion



        #region "Update bảng giá bán"
        [HttpPost]
        public ActionResult UpdateBangGia()
        {
            using (var db = new ERP_DATABASEEntities())
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

                                    mahang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 3].Value != null)
                                        manhomhang = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    else
                                        manhomhang = null;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 4].Value != null)
                                        series = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    else
                                        series = null;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                        cksp1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                                    else
                                        cksp1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                        giasp1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                                    else
                                        giasp1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        cksp2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                                    else
                                        cksp2 = 0;
                                    //========================================
                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        giasp2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 8].Value);
                                    else
                                        giasp2 = 0;
                                    //=======================================

                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                        cksp3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 9].Value);
                                    else
                                        cksp3 = 0;
                                    //==============================================
                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                        giasp3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 10].Value);
                                    else
                                        giasp3 = 0;






                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        slmoq1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        slmoq1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        ckmoq1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        ckmoq1 = 0;
                                    //=======================================

                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        giamoq1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        giamoq1 = 0;
                                    //======================================

                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        slmoq2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        slmoq2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        ckmoq2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        ckmoq2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        giamoq2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        giamoq2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        slmoq3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        slmoq3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        ckmoq3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        ckmoq3 = 0;
                                    //=======================================

                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        giamoq3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        giamoq3 = 0;




                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        ckseries1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        ckseries1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        giaseries1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        giaseries1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        ckseries2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        ckseries2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        giaseries2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        giaseries2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        ckseries3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        ckseries3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        giaseries3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        giaseries3 = 0;







                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc1_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc1_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc1_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc1_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc1_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc1_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc1_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc1_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc1_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc1_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc1_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc1_3 = 0;






                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc2_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc2_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc2_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc2_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc2_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc2_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc2_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc2_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc2_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc2_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc2_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc2_3 = 0;







                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc3_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc3_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc3_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc3_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc3_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc3_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc3_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc3_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc3_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc3_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc3_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc3_3 = 0;





                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc4_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc4_1 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc4_1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc4_1 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc4_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc4_2 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc4_2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc4_2 = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        cklistncc4_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        cklistncc4_3 = 0;
                                    //======================================
                                    if (workSheet.Cells[rowIterator, 1].Value != null)
                                        gialistncc4_3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 1].Value);
                                    else
                                        gialistncc4_3 = 0;
                                    //===============================================

                                    var banggia = db.HH_BANG_GIA_BAN.Where(x => x.MA_HANG == mahang).FirstOrDefault();
                                    if(banggia != null)
                                    {
                                        // HH_BANG_GIA_BAN banggia = new HH_BANG_GIA_BAN();
                                        banggia.NGAY_CAP_NHAT = DateTime.Today.Date;
                                        //banggia.MA_HANG = mahang;
                                        banggia.MA_NHOM_HANG = manhomhang;
                                        banggia.SERIES = series;

                                        //------------------------------
                                        banggia.CK_SP_1 = cksp1;
                                        banggia.GIA_SP_1 = giasp1;
                                        banggia.CK_SP_2 = cksp2;
                                        banggia.GIA_SP_2 = giasp2;
                                        banggia.CK_SP_3 = cksp3;
                                        banggia.GIA_SP_3 = giasp3;

                                        //-----------------------------
                                        banggia.SL_MOQ_1 = slmoq1;
                                        banggia.CK_MOQ_1 = ckmoq1;
                                        banggia.GIA_MOQ_1 = giamoq1;
                                        banggia.SL_MOQ_2 = slmoq2;
                                        banggia.CK_MOQ_2 = ckmoq2;
                                        banggia.GIA_MOQ_2 = giamoq2;
                                        banggia.SL_MOQ_3 = slmoq3;
                                        banggia.CK_MOQ_3 = ckmoq3;
                                        banggia.GIA_MOQ_3 = giamoq3;

                                        //------------------------------
                                        banggia.CK_SERIES_1 = ckseries1;
                                        banggia.GIA_SERIES_1 = giaseries1;
                                        banggia.CK_SERIES_2 = ckseries2;
                                        banggia.GIA_SERIES_2 = giaseries2;
                                        banggia.CK_SERIES_3 = ckseries3;
                                        banggia.GIA_SERIES_3 = giaseries3;

                                        //---------------------------------
                                        banggia.CK_LIST_NCC1_1 = cklistncc1_1;
                                        banggia.GIA_LIST_NCC1_1 = gialistncc1_1;
                                        banggia.CK_LIST_NCC1_2 = cklistncc1_2;
                                        banggia.GIA_LIST_NCC1_2 = gialistncc1_2;
                                        banggia.CK_LIST_NCC1_3 = cklistncc1_3;
                                        banggia.GIA_LIST_NCC1_3 = gialistncc1_3;

                                        //---------------------------------
                                        banggia.CK_LIST_NCC2_1 = cklistncc2_1;
                                        banggia.GIA_LIST_NCC2_1 = gialistncc2_1;
                                        banggia.CK_LIST_NCC2_2 = cklistncc2_2;
                                        banggia.GIA_LIST_NCC2_2 = gialistncc2_2;
                                        banggia.CK_LIST_NCC2_3 = cklistncc2_3;
                                        banggia.GIA_LIST_NCC2_3 = gialistncc2_3;


                                        //---------------------------------
                                        banggia.CK_LIST_NCC3_1 = cklistncc3_1;
                                        banggia.GIA_LIST_NCC3_1 = gialistncc3_1;
                                        banggia.CK_LIST_NCC3_2 = cklistncc3_2;
                                        banggia.GIA_LIST_NCC3_2 = gialistncc3_2;
                                        banggia.CK_LIST_NCC3_3 = cklistncc3_3;
                                        banggia.GIA_LIST_NCC3_3 = gialistncc3_3;



                                        //---------------------------------
                                        banggia.CK_LIST_NCC4_1 = cklistncc4_1;
                                        banggia.GIA_LIST_NCC4_1 = gialistncc4_1;
                                        banggia.CK_LIST_NCC4_2 = cklistncc4_2;
                                        banggia.GIA_LIST_NCC4_2 = gialistncc4_2;
                                        banggia.CK_LIST_NCC4_3 = cklistncc4_3;
                                        banggia.GIA_LIST_NCC4_3 = gialistncc4_3;


                                       // db.HH_BANG_GIA_BAN.Add(banggia);

                                        db.SaveChanges();
                                        so_dong_thanh_cong++;
                                        dong = rowIterator - 1;
                                    }
                                   
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

                return View("ImportBangGia");
            }
        }
        #endregion




        #region "IMPORT POLICY"
        public ActionResult ImportPolicy()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ImportPolicy(HttpPostedFileBase file)
        {
            string makhachhang, manhomhangcha, mark, purc;
            decimal ck, giaban;


            using (var db = new ERP_DATABASEEntities())
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

                                    makhachhang = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    manhomhang = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 4].Value != null)
                                        manhomhangcha = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    else
                                        manhomhangcha = null;
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                        ck = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                                    else
                                        ck = 0;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                        giaban = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                                    else
                                        giaban = 0;
                                   
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        mark = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    else
                                        mark = null;
                                    //=======================================
                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        purc = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    else
                                        purc = null;
                                    //=======================================

                                    var query = db.HH_NHOM_VTHH.Where(x => x.MA_NHOM_HANG_CHI_TIET == manhomhang).FirstOrDefault();
                                    if (query != null)
                                    {
                                        KH_POLICY policykh = new KH_POLICY();

                                        policykh.MA_KHACH_HANG = makhachhang;
                                        policykh.MA_NHOM_HANG = manhomhang;
                                        policykh.NGAY_CAP_NHAT = DateTime.Now;
                                        policykh.NGUOI_CAP_NHAT = Session["USERNAME"].ToString();
                                        policykh.CK = ck;
                                        policykh.GIA_BAN = giaban;

                                        db.KH_POLICY.Add(policykh);

                                        db.SaveChanges();
                                        so_dong_thanh_cong++;
                                        dong = rowIterator - 1;
                                    }
                                    else
                                    {
                                        HH_NHOM_VTHH nhomhang = new HH_NHOM_VTHH();
                                        nhomhang.MA_NHOM_HANG_CHI_TIET = manhomhang;
                                        nhomhang.MA_NHOM_HANG_CHA = manhomhangcha;
                                        db.HH_NHOM_VTHH.Add(nhomhang);

                                        HH_NHOM_VTHH newvthh = new HH_NHOM_VTHH();
                                        newvthh.MA_NHOM_HANG_CHI_TIET = manhomhang;
                                        newvthh.MA_NHOM_HANG_CHA = manhomhangcha;
                                        newvthh.PURC_PHU_TRACH = purc;
                                        newvthh.MARK_PHU_TRACH = mark;
                                        db.HH_NHOM_VTHH.Add(newvthh);
                                        db.SaveChanges();

                                        KH_POLICY policykh = new KH_POLICY();

                                        policykh.MA_KHACH_HANG = makhachhang;
                                        policykh.MA_NHOM_HANG = manhomhang;
                                        policykh.NGAY_CAP_NHAT = DateTime.Now;
                                        policykh.NGUOI_CAP_NHAT = Session["USERNAME"].ToString();
                                        policykh.CK = ck;
                                        policykh.GIA_BAN = giaban;

                                        db.KH_POLICY.Add(policykh);

                                        db.SaveChanges();
                                        so_dong_thanh_cong++;
                                        dong = rowIterator - 1;
                                    }


                                    

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

        }
        #endregion


        #region "UPDATE POLICY"
     
        #endregion
    }
}