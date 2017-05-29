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
using System.Data.SqlClient;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.NhaCungCap
{
    public class Api_LienHeNhaCungCapController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_LienHeNhaCungCap
        [Route("api/Api_LienHeNhaCungCap/GetNCC_LIEN_HE/{manhacungcap}")]
        public List<GetAll_LienHeTheoNCC_Result> GetNCC_LIEN_HE(string manhacungcap)
        {
            var query = db.Database.SqlQuery<GetAll_LienHeTheoNCC_Result>("GetAll_LienHeTheoNCC @manhacungcap", new SqlParameter("manhacungcap", manhacungcap));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_LienHeNhaCungCap/5
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

        // PUT: api/Api_LienHeNhaCungCap/5
        [Route("api/Api_LienHeNhaCungCap/PuttNCC_LIEN_HE")]
        public IHttpActionResult PuttNCC_LIEN_HE(LienHeNCC lh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var lienhe = db.NCC_LIEN_HE.Where(x => x.ID_LIEN_HE == lh.ID_LIEN_HE).FirstOrDefault();
            lienhe.MA_NHA_CUNG_CAP = lh.MA_NHA_CUNG_CAP;
            lienhe.NGUOI_LIEN_HE = lh.NGUOI_LIEN_HE;
            lienhe.CHUC_VU = lh.CHUC_VU;
            lienhe.PHONG_BAN = lh.PHONG_BAN;
            lienhe.NGAY_SINH = xlnt.Xulydatetime(lh.NGAY_SINH);
            lienhe.GIOI_TINH = lh.GIOI_TINH;
            lienhe.EMAIL_CA_NHAN = lh.EMAIL_CA_NHAN;
            lienhe.EMAIL_CONG_TY = lh.EMAIL_CONG_TY;
            lienhe.SKYPE = lh.SKYPE;
            lienhe.FACEBOOK = lh.FACEBOOK;
            lienhe.GHI_CHU = lh.GHI_CHU;
            lienhe.SO_DIEN_THOAI_1 = lh.SO_DIEN_THOAI_1;
            lienhe.SO_DIEN_THOAI_2 = lh.SO_DIEN_THOAI_2;
          
            db.SaveChanges();

            return Ok() ;
        }

        // POST: api/Api_LienHeNhaCungCap
        [ResponseType(typeof(NCC_LIEN_HE))]
        public IHttpActionResult PostNCC_LIEN_HE(LienHeNCC lh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            NCC_LIEN_HE lienhe = new NCC_LIEN_HE();
            lienhe.MA_NHA_CUNG_CAP = lh.MA_NHA_CUNG_CAP;
            lienhe.NGUOI_LIEN_HE = lh.NGUOI_LIEN_HE;
            lienhe.CHUC_VU = lh.CHUC_VU;
            lienhe.PHONG_BAN = lh.PHONG_BAN;
            lienhe.NGAY_SINH = GeneralFunction.ConvertToTime(lh.NGAY_SINH);
            lienhe.GIOI_TINH = lh.GIOI_TINH;
            lienhe.EMAIL_CA_NHAN = lh.EMAIL_CA_NHAN;
            lienhe.EMAIL_CONG_TY = lh.EMAIL_CONG_TY;
            lienhe.SKYPE = lh.SKYPE;
            lienhe.FACEBOOK = lh.FACEBOOK;
            lienhe.GHI_CHU = lh.GHI_CHU;
            lienhe.SO_DIEN_THOAI_1 = lh.SO_DIEN_THOAI_1;
            lienhe.SO_DIEN_THOAI_2 = lh.SO_DIEN_THOAI_2;
            db.NCC_LIEN_HE.Add(lienhe);
            db.SaveChanges();
            var query = db.NCC_LIEN_HE.Where(x => x.SO_DIEN_THOAI_1 == lh.SO_DIEN_THOAI_1).ToList();
            var data = query.LastOrDefault();
            NCC_PUR_PHU_TRACH salept = new NCC_PUR_PHU_TRACH();
            salept.ID_LIEN_HE = data.ID_LIEN_HE;
            salept.PUR_PHU_TRACH = lh.PUR_PHU_TRACH;
            salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
            salept.TRANG_THAI = true;
            db.NCC_PUR_PHU_TRACH.Add(salept);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lh.ID_LIEN_HE }, lh);
        }

        // DELETE: api/Api_LienHeNhaCungCap/5
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