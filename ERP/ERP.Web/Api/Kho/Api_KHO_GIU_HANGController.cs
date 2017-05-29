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
using ERP.Web.Models.NewModels.NhapKho;
using ERP.Web.Common;

namespace ERP.Web.Api.Kho
{
    public class Api_KHO_GIU_HANGController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_KHO_GIU_HANG
        public IQueryable<KHO_GIU_HANG> GetKHO_GIU_HANG()
        {
            return db.KHO_GIU_HANG;
        }

        // GET: api/Api_KHO_GIU_HANG/5
        [ResponseType(typeof(KHO_GIU_HANG))]
        public IHttpActionResult GetKHO_GIU_HANG(int id)
        {
            KHO_GIU_HANG kHO_GIU_HANG = db.KHO_GIU_HANG.Find(id);
            if (kHO_GIU_HANG == null)
            {
                return NotFound();
            }

            return Ok(kHO_GIU_HANG);
        }

        // PUT: api/Api_KHO_GIU_HANG/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKHO_GIU_HANG(int id, KHO_GIU_HANG kHO_GIU_HANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHO_GIU_HANG.ID)
            {
                return BadRequest();
            }

            db.Entry(kHO_GIU_HANG).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHO_GIU_HANGExists(id))
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

        //// POST: api/Api_KHO_GIU_HANG
        //[Route("api/Api_KHO_GIU_HANG/PostKHO_GIU_HANG")]
        //[ResponseType(typeof(KHO_GIU_HANG))]
        //public IHttpActionResult PostKHO_GIU_HANG(KhoGiu khogiuhang)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

            
        //    foreach (ChiTietKhoGiu item in khogiuhang.ChiTiet)
        //    {
        //        KHO_GIU_HANG kg = new KHO_GIU_HANG();
        //        kg.SALES_GIU = item.SALES_GIU;
        //        kg.MA_KHACH_HANG = item.MA_KHACH_HANG;
        //        kg.NGAY_GIU = GeneralFunction.ConvertToTime(item.NGAY_GIU);
        //        kg.MA_HANG = item.MA_HANG;
        //        kg.SL_GIU = item.SL_GIU;
        //        kg.NGAY_XUAT = GeneralFunction.ConvertToTime(item.NGAY_XUAT);
        //        kg.GIU_PO = Convert.ToBoolean(item.GIU_PO);
        //        kg.GHI_CHU = item.GHI_CHU;
        //        kg.TRUC_THUOC = item.TRUC_THUOC;
        //        db.KHO_GIU_HANG.Add(kg);
        //    } 

                
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {

        //            throw;

        //    }

        //    return Ok();
        //}


        // POST: api/Api_KHO_GIU_HANG
        [Route("api/Api_KHO_GIU_HANG/PostKHO_GIU_HANG1")]
        [ResponseType(typeof(KHO_GIU_HANG))]
        public IHttpActionResult PostKHO_GIU_HANG1(KhoGiu khogiuhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
                KHO_GIU_HANG kg = new KHO_GIU_HANG();
                kg.SALES_GIU = khogiuhang.SALES_GIU;
                kg.MA_KHACH_HANG = khogiuhang.MA_KHACH_HANG;
                kg.NGAY_GIU = DateTime.Today.Date;
                kg.MA_HANG = khogiuhang.MA_HANG;
                kg.SL_GIU = khogiuhang.SL_GIU;
                kg.GIU_PO = Convert.ToBoolean(khogiuhang.GIU_PO);
                kg.TRUC_THUOC = khogiuhang.TRUC_THUOC;
                kg.ID_CT_PO = khogiuhang.ID_CT_PO;
                db.KHO_GIU_HANG.Add(kg);


            TONKHO_HOPLONG newhanggiu = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == khogiuhang.MA_HANG && x.MA_KHO_CON == "IVHOPLONG05").FirstOrDefault();
            if (newhanggiu == null)
            {
                TONKHO_HOPLONG soluonggiumoi = new TONKHO_HOPLONG();
                soluonggiumoi.MA_HANG = khogiuhang.MA_HANG;
                soluonggiumoi.MA_KHO_CON = "IVHOPLONG05";
                soluonggiumoi.SL_HOPLONG = Convert.ToInt32(khogiuhang.SL_GIU);
                db.TONKHO_HOPLONG.Add(soluonggiumoi);
                db.SaveChanges();
            }
            else
            {
                newhanggiu.SL_HOPLONG += Convert.ToInt32(khogiuhang.SL_GIU);
            }

            foreach (TonKho item in khogiuhang.TonKho)
            {
                //Cập nhật hàng tồn
                TONKHO_HOPLONG newHangTon = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == khogiuhang.MA_HANG && x.MA_KHO_CON == item.MA_KHO).FirstOrDefault();
                
                if(newHangTon != null)
                {
                    newHangTon.SL_HOPLONG = newHangTon.SL_HOPLONG - Convert.ToInt32(item.TON_TANG_2) - Convert.ToInt32(item.TON_TANG_3) - Convert.ToInt32(item.TON_TANG_4);
                }
                //if (newHangTon == null || newHangTon.SL_HOPLONG < khogiuhang.SL_GIU)
                //{
                //    return Ok("Hàng không có trong kho hoặc SL tồn không đủ");
                //}

               BH_CT_DON_HANG_PO trangthai = db.BH_CT_DON_HANG_PO.Where(x => x.ID == khogiuhang.ID_CT_PO).FirstOrDefault();
                if(trangthai != null)
                {
                    trangthai.CAN_GIU_HANG = true;
                }
                db.SaveChanges();
                var dagiu = db.BH_CT_DON_HANG_PO.Where(x => x.MA_SO_PO == khogiuhang.MA_SO_PO && x.CAN_GIU_HANG == false).ToList().Count();
                if (dagiu == 0)
                {
                    var dagiuhang = db.BH_DON_HANG_PO.Where(x => x.MA_SO_PO == khogiuhang.MA_SO_PO).FirstOrDefault();
                    if(dagiuhang != null)
                    {
                        dagiuhang.DA_GIU = true;
                    }
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

            return Ok();
        }

        // DELETE: api/Api_KHO_GIU_HANG/5
        [ResponseType(typeof(KHO_GIU_HANG))]
        public IHttpActionResult DeleteKHO_GIU_HANG(int id)
        {
            KHO_GIU_HANG kHO_GIU_HANG = db.KHO_GIU_HANG.Find(id);
            if (kHO_GIU_HANG == null)
            {
                return NotFound();
            }

            db.KHO_GIU_HANG.Remove(kHO_GIU_HANG);
            db.SaveChanges();

            return Ok(kHO_GIU_HANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHO_GIU_HANGExists(int id)
        {
            return db.KHO_GIU_HANG.Count(e => e.ID == id) > 0;
        }
    }
}