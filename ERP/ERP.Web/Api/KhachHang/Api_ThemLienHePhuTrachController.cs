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

namespace ERP.Web.Api.KhachHang
{
    public class Api_ThemLienHePhuTrachController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_ThemLienHePhuTrach
        public IQueryable<KH_LIEN_HE> GetKH_LIEN_HE()
        {
            return db.KH_LIEN_HE;
        }

        // GET: api/Api_ThemLienHePhuTrach/5
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

        // PUT: api/Api_ThemLienHePhuTrach/5
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

        // POST: api/Api_ThemLienHePhuTrach
        [ResponseType(typeof(KH_LIEN_HE))]
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
                if (item.NGAY_SINH != null)
                    lienhe.NGAY_SINH = xlnt.Xulydatetime(item.NGAY_SINH);
                lienhe.GIOI_TINH = item.GIOI_TINH;
                lienhe.EMAIL_CA_NHAN = item.EMAIL_CA_NHAN;
                lienhe.EMAIL_CONG_TY = item.EMAIL_CONG_TY;
                lienhe.SKYPE = item.SKYPE;
                lienhe.FACEBOOK = item.FACEBOOK;
                lienhe.GHI_CHU = item.GHI_CHU;
                lienhe.SDT1 = item.SDT1;
                lienhe.SDT2 = item.SDT2;
                db.KH_LIEN_HE.Add(lienhe);
                db.SaveChanges();
                var query = db.KH_LIEN_HE.Where(x => x.SDT1 == item.SDT1).ToList();
                var data = query.LastOrDefault();
                KH_SALES_PHU_TRACH salept = new KH_SALES_PHU_TRACH();
                salept.ID_LIEN_HE = data.ID_LIEN_HE;
                salept.SALES_PHU_TRACH = item.SALES_PHU_TRACH;
                salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                salept.TRANG_THAI = true;
                db.KH_SALES_PHU_TRACH.Add(salept);
                db.SaveChanges();
            }


        }

        // DELETE: api/Api_ThemLienHePhuTrach/5
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