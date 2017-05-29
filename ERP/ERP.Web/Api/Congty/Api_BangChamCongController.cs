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

namespace ERP.Web.Api.HeThong
{
    public class Api_BangChamCongController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_BangChamCong/USERNAME
        public List<BangChamCong> GetCCTC_BANG_CHAM_CONG(string id)
        {
            List<BangChamCong> listchamcong = new List<BangChamCong>();
            var vData = db.CCTC_BANG_CHAM_CONG.Where(x => x.USERNAME == id).ToList();
            foreach (var item in vData)
            {
                BangChamCong cc = new BangChamCong();
                cc.THANG_CHAM_CONG = item.THANG_CHAM_CONG;
                cc.NGAY_CHUAN = String.Format("{0:#,##0.##}",item.NGAY_CHUAN);
                cc.USERNAME = item.USERNAME;
                cc.GIO_DI_MUON = String.Format("{0:N2}", item.GIO_DI_MUON);
                cc.GIO_VE_SOM = String.Format("{0:#,##0.##}", item.GIO_VE_SOM);
                cc.TANG_CA_NGAY_THUONG = String.Format("{0:#,##0.##}", item.TANG_CA_NGAY_THUONG);
                cc.TANG_CA_NGAY_LE = String.Format("{0:#,##0.##}", item.TANG_CA_NGAY_LE);
                cc.SO_LAN_QUEN_CHAM = String.Format("{0:#,##0.##}", item.SO_LAN_QUEN_CHAM);
                cc.SO_NGAY_NGHI = String.Format("{0:#,##0.##}", item.SO_NGAY_NGHI);
                cc.CONG_THUC_TE = String.Format("{0:#,##0.##}", item.CONG_THUC_TE);
                cc.VAY_TIN_DUNG = String.Format("{0:#,##0.##}", item.VAY_TIN_DUNG);
                cc.UNG_LUONG = String.Format("{0:#,##0.##}", item.UNG_LUONG);
                cc.GHI_CHU = item.GHI_CHU;
                listchamcong.Add(cc);
            }
            var result = listchamcong.ToList();
            return result;
        }


        // PUT: api/Api_BangChamCong/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_BANG_CHAM_CONG(string id, CCTC_BANG_CHAM_CONG cCTC_BANG_CHAM_CONG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cCTC_BANG_CHAM_CONG.THANG_CHAM_CONG)
            {
                return BadRequest();
            }

            db.Entry(cCTC_BANG_CHAM_CONG).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_BANG_CHAM_CONGExists(id))
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

        // POST: api/Api_BangChamCong
        [ResponseType(typeof(CCTC_BANG_CHAM_CONG))]
        public IHttpActionResult PostCCTC_BANG_CHAM_CONG(CCTC_BANG_CHAM_CONG cCTC_BANG_CHAM_CONG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CCTC_BANG_CHAM_CONG.Add(cCTC_BANG_CHAM_CONG);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_BANG_CHAM_CONGExists(cCTC_BANG_CHAM_CONG.THANG_CHAM_CONG))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cCTC_BANG_CHAM_CONG.THANG_CHAM_CONG }, cCTC_BANG_CHAM_CONG);
        }

        // DELETE: api/Api_BangChamCong/5
        [ResponseType(typeof(CCTC_BANG_CHAM_CONG))]
        public IHttpActionResult DeleteCCTC_BANG_CHAM_CONG(string id)
        {
            CCTC_BANG_CHAM_CONG cCTC_BANG_CHAM_CONG = db.CCTC_BANG_CHAM_CONG.Find(id);
            if (cCTC_BANG_CHAM_CONG == null)
            {
                return NotFound();
            }

            db.CCTC_BANG_CHAM_CONG.Remove(cCTC_BANG_CHAM_CONG);
            db.SaveChanges();

            return Ok(cCTC_BANG_CHAM_CONG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_BANG_CHAM_CONGExists(string id)
        {
            return db.CCTC_BANG_CHAM_CONG.Count(e => e.THANG_CHAM_CONG == id) > 0;
        }
    }
}