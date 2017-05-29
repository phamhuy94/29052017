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
using ERP.Web.Models;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Api.HeThong
{
    public class Api_SalePhuTrachController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_SalePhuTrach
        public List<SalesPhuTrach> GetSalesPhuTrach()
        {
            var vData = (from t1 in db.KH_SALES_PHU_TRACH
                         join t2 in db.KH_LIEN_HE on t1.ID_LIEN_HE equals t2.ID_LIEN_HE
                         join t3 in db.HT_NGUOI_DUNG on t1.SALES_PHU_TRACH equals t3.USERNAME
                         select new
                         {
                            t3.HO_VA_TEN,t1.ID,t1.ID_LIEN_HE,t1.SALES_PHU_TRACH,t1.NGAY_BAT_DAU_PHU_TRACH,t1.NGAY_KET_THUC_PHU_TRACH,t1.TRANG_THAI,t1.SALES_CU,t1.SALES_MOI,t2.NGUOI_LIEN_HE,t2.EMAIL_CA_NHAN,t2.EMAIL_CONG_TY,
                         });
            var result = vData.ToList().Select(x => new SalesPhuTrach()
            {
                ID = x.ID,
                ID_LIEN_HE = x.ID_LIEN_HE,
                SALES_PHU_TRACH = x.SALES_PHU_TRACH,
                NGAY_BAT_DAU_PHU_TRACH = x.NGAY_BAT_DAU_PHU_TRACH.ToString(),
                NGAY_KET_THUC_PHU_TRACH = x.NGAY_KET_THUC_PHU_TRACH.ToString(),
                TRANG_THAI = x.TRANG_THAI,
                NGUOI_LIEN_HE = x.NGUOI_LIEN_HE,
                EMAIL_CONG_TY = x.EMAIL_CONG_TY,
                EMAIL_CA_NHAN = x.EMAIL_CA_NHAN,
                SALES_CU = x.SALES_CU,
                SALES_MOI = x.SALES_MOI,
                HO_VA_TEN = x.HO_VA_TEN,
            }).ToList();
            return result;
        }

        // GET: api/Api_SalePhuTrach/5
        [ResponseType(typeof(KH_SALES_PHU_TRACH))]
        public IHttpActionResult GetKH_SALES_PHU_TRACH(int id)
        {
            KH_SALES_PHU_TRACH kH_SALES_PHU_TRACH = db.KH_SALES_PHU_TRACH.Find(id);
            if (kH_SALES_PHU_TRACH == null)
            {
                return NotFound();
            }

            return Ok(kH_SALES_PHU_TRACH);
        }

        // PUT: api/Api_SalePhuTrach/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_SALES_PHU_TRACH(int id, SalesPhuTrach sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sale.ID)
            {
                return BadRequest();
            }
            var nv = db.KH_SALES_PHU_TRACH.Where(x => x.ID == id).FirstOrDefault();

            nv.ID_LIEN_HE = sale.ID_LIEN_HE;
            nv.SALES_PHU_TRACH = sale.SALES_PHU_TRACH;
            if (sale.NGAY_KET_THUC_PHU_TRACH != null)
                nv.NGAY_KET_THUC_PHU_TRACH = xlnt.Xulydatetime(sale.NGAY_KET_THUC_PHU_TRACH);
            if (sale.NGAY_BAT_DAU_PHU_TRACH != null)
                nv.NGAY_BAT_DAU_PHU_TRACH = xlnt.Xulydatetime(sale.NGAY_BAT_DAU_PHU_TRACH);
            nv.TRANG_THAI = sale.TRANG_THAI;
            if (sale.SALES_CU == false && sale.SALES_MOI == false)
            {
                nv.SALES_MOI = true;
                nv.SALES_CU = false;
            }
            else
            {
                nv.SALES_CU = sale.SALES_CU;
                nv.SALES_MOI = sale.SALES_MOI;
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_SALES_PHU_TRACHExists(id))
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
        [HttpPut]
        [Route("api/Api_SalePhuTrach/EditSalePhuTrach/{idlienhe}")]
        public IHttpActionResult EditSalePhuTrach(int id, SalesPhuTrach sale)
        {

            var nv = db.KH_SALES_PHU_TRACH.Where(x => x.ID == id).FirstOrDefault();
            if(nv != null)
            {
                nv.SALES_PHU_TRACH = sale.SALES_PHU_TRACH;
                if (sale.NGAY_KET_THUC_PHU_TRACH != null)
                    nv.NGAY_KET_THUC_PHU_TRACH = xlnt.Xulydatetime(sale.NGAY_KET_THUC_PHU_TRACH);
                nv.TRANG_THAI = sale.TRANG_THAI;
            }
            else
            {
                nv.ID_LIEN_HE = sale.ID_LIEN_HE;
                nv.SALES_PHU_TRACH = sale.SALES_PHU_TRACH;
                nv.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                db.KH_SALES_PHU_TRACH.Add(nv);
                db.SaveChanges();
            }

            
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_SALES_PHU_TRACHExists(id))
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


        // POST: api/Api_SalePhuTrach
        [ResponseType(typeof(KH_SALES_PHU_TRACH))]
        public IHttpActionResult PostKH_SALES_PHU_TRACH(SalesPhuTrach sale)
        {

            KH_SALES_PHU_TRACH nv = new KH_SALES_PHU_TRACH();
            nv.ID_LIEN_HE = sale.ID_LIEN_HE;
            nv.SALES_PHU_TRACH = sale.SALES_PHU_TRACH;
            nv.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
            nv.TRANG_THAI = sale.TRANG_THAI;
            if (sale.SALES_CU == false && sale.SALES_MOI == false)
            {
                nv.SALES_MOI = true;
                nv.SALES_CU = false;
            }
            else
            {
                nv.SALES_CU = sale.SALES_CU;
                nv.SALES_MOI = sale.SALES_MOI;
            }
            db.KH_SALES_PHU_TRACH.Add(nv);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KH_SALES_PHU_TRACHExists(sale.ID_LIEN_HE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nv.ID_LIEN_HE }, nv);
        }

        // DELETE: api/Api_SalePhuTrach/5
        [ResponseType(typeof(KH_SALES_PHU_TRACH))]
        public IHttpActionResult DeleteKH_SALES_PHU_TRACH(int id)
        {
            KH_SALES_PHU_TRACH kH_SALES_PHU_TRACH = db.KH_SALES_PHU_TRACH.Find(id);
            if (kH_SALES_PHU_TRACH == null)
            {
                return NotFound();
            }

            db.KH_SALES_PHU_TRACH.Remove(kH_SALES_PHU_TRACH);
            db.SaveChanges();

            return Ok(kH_SALES_PHU_TRACH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_SALES_PHU_TRACHExists(int id)
        {
            return db.KH_SALES_PHU_TRACH.Count(e => e.ID == id) > 0;
        }
    }
}