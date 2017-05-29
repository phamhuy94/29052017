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
using System.Data.SqlClient;

namespace ERP.Web.Api.Kho
{
    public class Api_KhoGiuHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        String magiuhang;
        // GET: api/Api_KhoGiuHang
        [Route("api/Api_KhoGiuHang/GetDataGiuKho/{macongty}/{mahang}")]
        public List<Prod_Kho_GiuHang_TheoMa_Result> GetDataGiuKho( string macongty, string mahang)
        {
            var query = db.Database.SqlQuery<Prod_Kho_GiuHang_TheoMa_Result>("Prod_Kho_GiuHang_TheoMa @macongty, @mahang", new SqlParameter("macongty", macongty), new SqlParameter("mahang", mahang));
            var result = query.ToList();

            return result;
        }


        // GET: api/Api_KhoGiuHang
        [Route("api/Api_KhoGiuHang/GetAllListDataGiuKho/{macongty}")]
        public List<Prod_Kho_ListGiuHang_Result> GetAllListDataGiuKho(string macongty)
        {
            var query = db.Database.SqlQuery<Prod_Kho_ListGiuHang_Result>("Prod_Kho_ListGiuHang @macongty", new SqlParameter("macongty", macongty));
            var result = query.ToList();

            return result;
        }


        // GET: api/Api_KhoGiuHang/5
        [ResponseType(typeof(KHO_GIU_HANG))]
        public IHttpActionResult GetKHO_GIU_HANG(string id)
        {
            KHO_GIU_HANG kHO_GIU_HANG = db.KHO_GIU_HANG.Find(id);
            if (kHO_GIU_HANG == null)
            {
                return NotFound();
            }

            return Ok(kHO_GIU_HANG);
        }

        // PUT: api/Api_KhoGiuHang/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutKHO_GIU_HANG(string id, KHO_GIU_HANG kHO_GIU_HANG)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != kHO_GIU_HANG.MA_GIU_KHO)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(kHO_GIU_HANG).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!KHO_GIU_HANGExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Api_KhoGiuHang
        //[ResponseType(typeof(KHO_GIU_HANG))]
        //public IHttpActionResult PostKHO_GIU_HANG(Khogiuhang kHO_GIU_HANG)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    String nam = DateTime.Today.Year.ToString();
        //    String nam2so = nam.Substring(2);
        //    var query = db.Database.SqlQuery<string>("XL_LayMaGiuHangMoiNhat");


        //    if (query.Count() > 0)
        //    {
        //        string prefixID = "GH" + nam2so;
        //        var data = query.FirstOrDefault();
        //        string LastID = data;

        //        int nextID = int.Parse(LastID.Remove(0, prefixID.Length)) + 1;
        //        int lengthNumerID = LastID.Length - prefixID.Length;
        //        string zeroNumber = "";
        //        for (int i = 1; i <= lengthNumerID; i++)
        //        {
        //            if (nextID < Math.Pow(10, i))
        //            {
        //                for (int j = 1; j <= lengthNumerID - i; i++)
        //                {
        //                    zeroNumber += "0";
        //                }
        //            }
        //        }
        //        // int ma = Convert.ToInt32(makhach.Substring(4));
        //        magiuhang = prefixID + zeroNumber + nextID.ToString();
        //    }
        //    else
        //        magiuhang = "GH" + nam2so + "000001";

        //    KHO_GIU_HANG nv = new KHO_GIU_HANG();
        //    nv.MA_GIU_KHO = magiuhang;
        //    nv.SALES_GIU = kHO_GIU_HANG.SALES_GIU;
        //    if (kHO_GIU_HANG.NGAY_GIU != null)
        //        nv.NGAY_GIU = xlnt.Xulydatetime(kHO_GIU_HANG.NGAY_GIU);
        //    nv.MA_KHACH_HANG = kHO_GIU_HANG.MA_KHACH_HANG;
        //    nv.HUY_DON_GIU = kHO_GIU_HANG.HUY_DON_GIU;
        //    nv.DON_DANG_XUAT = kHO_GIU_HANG.DON_DANG_XUAT;
        //    nv.DON_DA_HOAN_THANH = kHO_GIU_HANG.DON_DA_HOAN_THANH;
        //    nv.GIU_PO = kHO_GIU_HANG.GIU_PO;
        //    nv.TRUC_THUOC = kHO_GIU_HANG.TRUC_THUOC;
        //    db.KHO_GIU_HANG.Add(nv);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (KHO_GIU_HANGExists(kHO_GIU_HANG.MA_GIU_KHO))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(nv);
        //}

        //// DELETE: api/Api_KhoGiuHang/5
        //[ResponseType(typeof(KHO_GIU_HANG))]
        //public IHttpActionResult DeleteKHO_GIU_HANG(string id)
        //{
        //    KHO_GIU_HANG kHO_GIU_HANG = db.KHO_GIU_HANG.Find(id);
        //    if (kHO_GIU_HANG == null)
        //    {
        //        return NotFound();
        //    }

        //    db.KHO_GIU_HANG.Remove(kHO_GIU_HANG);
        //    db.SaveChanges();

        //    return Ok(kHO_GIU_HANG);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool KHO_GIU_HANGExists(string id)
        //{
        //    return db.KHO_GIU_HANG.Count(e => e.MA_GIU_KHO == id) > 0;
        //}
    }
}