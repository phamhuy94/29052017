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
using ERP.Web.Common;

namespace ERP.Web.Api.NhaCungCap
{
    public class Api_ArrayLienHeNCCController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ArrayLienHeNCC
        public IQueryable<NCC_LIEN_HE> GetNCC_LIEN_HE()
        {
            return db.NCC_LIEN_HE;
        }

        // GET: api/Api_ArrayLienHeNCC/5
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult GetNCC_LIEN_HE(int id)
        {
            NCC_LIEN_HE nCC_LIEN_HE = db.NCC_LIEN_HE.Find(id);
            if (nCC_LIEN_HE == null)
            {
                return NotFound();
            }

            return Ok(nCC_LIEN_HE);
        }

        // PUT: api/Api_ArrayLienHeNCC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNCC_LIEN_HE(int id, NCC_LIEN_HE nCC_LIEN_HE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nCC_LIEN_HE.ID_LIEN_HE)
            {
                return BadRequest();
            }

            db.Entry(nCC_LIEN_HE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NCC_LIEN_HEExists(id))
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

        // POST: api/Api_ArrayLienHeNCC
        [ResponseType(typeof(NCC_LIEN_HE))]
        public void PostNCC_LIEN_HE(List<LienHeNCC> lh)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            foreach (var item in lh)
            {
                NCC_LIEN_HE lienhe = new NCC_LIEN_HE();
                lienhe.MA_NHA_CUNG_CAP = item.MA_NHA_CUNG_CAP;
                lienhe.NGUOI_LIEN_HE = item.NGUOI_LIEN_HE;
                lienhe.CHUC_VU = item.CHUC_VU;
                lienhe.PHONG_BAN = item.PHONG_BAN;
                if(item.NGAY_SINH != "")
                lienhe.NGAY_SINH = GeneralFunction.ConvertToTime(item.NGAY_SINH);
                lienhe.GIOI_TINH = item.GIOI_TINH;
                lienhe.EMAIL_CA_NHAN = item.EMAIL_CA_NHAN;
                lienhe.EMAIL_CONG_TY = item.EMAIL_CONG_TY;
                lienhe.SKYPE = item.SKYPE;
                lienhe.FACEBOOK = item.FACEBOOK;
                lienhe.GHI_CHU = item.GHI_CHU;
                lienhe.SO_DIEN_THOAI_1 = item.SO_DIEN_THOAI_1;
                lienhe.SO_DIEN_THOAI_2 = item.SO_DIEN_THOAI_2;
                db.NCC_LIEN_HE.Add(lienhe);
                db.SaveChanges();
                var query = db.NCC_LIEN_HE.Where(x => x.SO_DIEN_THOAI_1 == item.SO_DIEN_THOAI_1).ToList();
                var data = query.LastOrDefault();
                NCC_PUR_PHU_TRACH salept = new NCC_PUR_PHU_TRACH();
                salept.ID_LIEN_HE = data.ID_LIEN_HE;
                salept.PUR_PHU_TRACH = item.PUR_PHU_TRACH;
                salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                salept.TRANG_THAI = true;
                db.NCC_PUR_PHU_TRACH.Add(salept);
                db.SaveChanges();
            }
        }

        // DELETE: api/Api_ArrayLienHeNCC/5
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult DeleteNCC_LIEN_HE(int id)
        {
            NCC_LIEN_HE nCC_LIEN_HE = db.NCC_LIEN_HE.Find(id);
            if (nCC_LIEN_HE == null)
            {
                return NotFound();
            }

            db.NCC_LIEN_HE.Remove(nCC_LIEN_HE);
            db.SaveChanges();

            return Ok(nCC_LIEN_HE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NCC_LIEN_HEExists(int id)
        {
            return db.NCC_LIEN_HE.Count(e => e.ID_LIEN_HE == id) > 0;
        }
    }
}