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

namespace ERP.Web.Api.HeThong
{
    public class Api_MausohoadonController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Mausohoadon
        public List<DM_MAU_SO_HOA_DON> GetMAU_SO_HOA_DON()
        {
            var vData = db.DM_MAU_SO_HOA_DON;
            var result = vData.ToList().Select(x => new DM_MAU_SO_HOA_DON()
            {
                MAU_SO = x.MAU_SO,
                TEN_MAU = x.TEN_MAU,
            }).ToList();
            return result;
        }

        // GET: api/Api_Mausohoadon/5
        [ResponseType(typeof(DM_MAU_SO_HOA_DON))]
        public IHttpActionResult GetMAU_SO_HOA_DON(string id)
        {
            DM_MAU_SO_HOA_DON mAU_SO_HOA_DON = db.DM_MAU_SO_HOA_DON.Find(id);
            if (mAU_SO_HOA_DON == null)
            {
                return NotFound();
            }

            return Ok(mAU_SO_HOA_DON);
        }

        // PUT: api/Api_Mausohoadon/5
        [ResponseType(typeof(void))]
        public void PutMAU_SO_HOA_DON(string id, DM_MAU_SO_HOA_DON mAU_SO_HOA_DON)
        {
            var check = db.DM_MAU_SO_HOA_DON.Where(x => x.MAU_SO == id);
            if (check.Count() > 0)
            {
                var resultupdate = check.FirstOrDefault();
                resultupdate.TEN_MAU = mAU_SO_HOA_DON.TEN_MAU;
                db.SaveChanges();
            }
            

        }

        // POST: api/Api_Mausohoadon
        [ResponseType(typeof(DM_MAU_SO_HOA_DON))]
        public IHttpActionResult PostMAU_SO_HOA_DON(DM_MAU_SO_HOA_DON mAU_SO_HOA_DON)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_MAU_SO_HOA_DON.Add(mAU_SO_HOA_DON);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MAU_SO_HOA_DONExists(mAU_SO_HOA_DON.MAU_SO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mAU_SO_HOA_DON.MAU_SO }, mAU_SO_HOA_DON);
        }

        // DELETE: api/Api_Mausohoadon/5
        [ResponseType(typeof(DM_MAU_SO_HOA_DON))]
        public IHttpActionResult DeleteMAU_SO_HOA_DON(string id)
        {
            DM_MAU_SO_HOA_DON mAU_SO_HOA_DON = db.DM_MAU_SO_HOA_DON.Find(id);
            if (mAU_SO_HOA_DON == null)
            {
                return NotFound();
            }

            db.DM_MAU_SO_HOA_DON.Remove(mAU_SO_HOA_DON);
            db.SaveChanges();

            return Ok(mAU_SO_HOA_DON);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MAU_SO_HOA_DONExists(string id)
        {
            return db.DM_MAU_SO_HOA_DON.Count(e => e.MAU_SO == id) > 0;
        }
    }
}