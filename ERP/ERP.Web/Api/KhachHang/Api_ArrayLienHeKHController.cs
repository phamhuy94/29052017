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
using System.Threading.Tasks;
using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.NewModels;

namespace ERP.Web.Api.KhachHang
{
    public class Api_ArrayLienHeKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_ArrayLienHeKH
        public IQueryable<KH_LIEN_HE> GetKH_LIEN_HE()
        {
            return db.KH_LIEN_HE;
        }

        // GET: api/Api_ArrayLienHeKH/5
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult GetKH_LIEN_HE(int id)
        {
            KH_LIEN_HE kH_LIEN_HE = db.KH_LIEN_HE.Find(id);
            if (kH_LIEN_HE == null)
            {
                return NotFound();
            }

            return Ok(kH_LIEN_HE);
        }

        // PUT: api/Api_ArrayLienHeKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_LIEN_HE(int id, KH_LIEN_HE kH_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH_LIEN_HE.ID_LIEN_HE)
            {
                return BadRequest();
            }

            db.Entry(kH_LIEN_HE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_LIEN_HEExists(id))
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

        // POST: api/Api_ArrayLienHeKH
        //[HttpPost]
        //[Route("api/Api_ArrayLienHeKH/{makh}")]
        //public async Task<IHttpActionResult> PostMultiArrayLienHeKH(string makh, [FromBody] List<KH_LIEN_HE> qUY_CHI_TIET_PHIEU_CHI)
        //{
        //    for (int i = 0; i < qUY_CHI_TIET_PHIEU_CHI.Count(); i++)
        //    {
        //        //nH_NTTKs[i].ID = (index + i + 1).ToString();
        //        db.KH_LIEN_HE.Add(qUY_CHI_TIET_PHIEU_CHI[i]);
        //    }
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //    return Ok(qUY_CHI_TIET_PHIEU_CHI);
        //}

        public void PostKH_LIEN_HE(List<LienHeKH> lh)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            foreach (var item in lh)
            {
                KH_LIEN_HE lienhe = new KH_LIEN_HE();
                lienhe.MA_KHACH_HANG = item.MA_KHACH_HANG;
                lienhe.NGUOI_LIEN_HE = item.NGUOI_LIEN_HE;
                lienhe.CHUC_VU = item.CHUC_VU;
                lienhe.PHONG_BAN = item.PHONG_BAN;
                //if (item.NGAY_SINH != null)
                //    lienhe.NGAY_SINH = xlnt.Xulydatetime(item.NGAY_SINH);
                lienhe.GIOI_TINH = item.GIOI_TINH;
                lienhe.EMAIL_CA_NHAN = item.EMAIL_CA_NHAN;
                lienhe.EMAIL_CONG_TY = item.EMAIL_CONG_TY;
                lienhe.SKYPE = item.SKYPE;
                lienhe.FACEBOOK = item.FACEBOOK;
                lienhe.GHI_CHU = item.GHI_CHU;
                lienhe.SDT1 = item.SDT1;
                lienhe.SDT2 = item.SDT2;
                lienhe.TINH_TRANG_LAM_VIEC = item.TINH_TRANG_LAM_VIEC;
                db.KH_LIEN_HE.Add(lienhe);
                db.SaveChanges();
                var query = db.KH_LIEN_HE.Where(x => x.SDT1 == item.SDT1).ToList();
                var data = query.LastOrDefault();
                KH_SALES_PHU_TRACH salept = new KH_SALES_PHU_TRACH();
                salept.ID_LIEN_HE = data.ID_LIEN_HE;
                salept.SALES_PHU_TRACH = item.SALES_PHU_TRACH;
                salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                salept.TRANG_THAI = true;
                if (item.SALES_CU == false && item.SALES_MOI == false)
                {
                    salept.SALES_MOI = true;
                    salept.SALES_CU = false;
                } else 
                {
                    salept.SALES_CU = item.SALES_CU;
                    salept.SALES_MOI = item.SALES_MOI;
                }               
                db.KH_SALES_PHU_TRACH.Add(salept);
                db.SaveChanges();

                //KH_CHUYEN_SALES chuyensale = new KH_CHUYEN_SALES();
                //chuyensale.MA_KHACH_HANG = item.MA_KHACH_HANG;
                //chuyensale.SALE_HIEN_THOI = item.SALES_PHU_TRACH;
                //db.KH_CHUYEN_SALES.Add(chuyensale);
                //db.SaveChanges();
            }
        }

        // DELETE: api/Api_ArrayLienHeKH/5
        [ResponseType(typeof(KH_LIEN_HE))]
        public IHttpActionResult DeleteKH_LIEN_HE(int id)
        {
            KH_LIEN_HE kH_LIEN_HE = db.KH_LIEN_HE.Find(id);
            if (kH_LIEN_HE == null)
            {
                return NotFound();
            }

            db.KH_LIEN_HE.Remove(kH_LIEN_HE);
            db.SaveChanges();

            return Ok(kH_LIEN_HE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_LIEN_HEExists(int id)
        {
            return db.KH_LIEN_HE.Count(e => e.ID_LIEN_HE == id) > 0;
        }
    }
}