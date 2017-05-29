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
    public class Api_DangkypheduyetController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Dangkypheduyet
        public List<XL_DANG_KY_PHE_DUYET> GetXL_DANG_KY_PHE_DUYET()
        {
            var vData = db.XL_DANG_KY_PHE_DUYET;
            var result = vData.ToList().Select(x => new XL_DANG_KY_PHE_DUYET()
            {
                ID = x.ID,
                MA_PHE_DUYET = x.MA_PHE_DUYET,
                NGUOI_PHE_DUYET = x.NGUOI_PHE_DUYET,
                TRUC_THUOC = x.TRUC_THUOC,
                GHI_CHU = x.GHI_CHU

            }).ToList();
            return result;
        }
        
      
        // GET: api/Api_Dangkypheduyet/5
        [ResponseType(typeof(XL_DANG_KY_PHE_DUYET))]
        public IHttpActionResult GetXL_DANG_KY_PHE_DUYET(int id)
        {
            XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET = db.XL_DANG_KY_PHE_DUYET.Find(id);
            if (xL_DANG_KY_PHE_DUYET == null)
            {
                return NotFound();
            }

            return Ok(xL_DANG_KY_PHE_DUYET);
        }

        // PUT: api/Api_Dangkypheduyet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutXL_DANG_KY_PHE_DUYET(int id, XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != xL_DANG_KY_PHE_DUYET.ID)
            {
                return BadRequest();
            }

            db.Entry(xL_DANG_KY_PHE_DUYET).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XL_DANG_KY_PHE_DUYETExists(id))
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

        // POST: api/Api_Dangkypheduyet
        [ResponseType(typeof(XL_DANG_KY_PHE_DUYET))]
        public IHttpActionResult PostXL_DANG_KY_PHE_DUYET(XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.XL_DANG_KY_PHE_DUYET.Add(xL_DANG_KY_PHE_DUYET);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = xL_DANG_KY_PHE_DUYET.ID }, xL_DANG_KY_PHE_DUYET);
        }

        // DELETE: api/Api_Dangkypheduyet/5
        [ResponseType(typeof(XL_DANG_KY_PHE_DUYET))]
        public IHttpActionResult DeleteXL_DANG_KY_PHE_DUYET(int id)
        {
            XL_DANG_KY_PHE_DUYET xL_DANG_KY_PHE_DUYET = db.XL_DANG_KY_PHE_DUYET.Find(id);
            if (xL_DANG_KY_PHE_DUYET == null)
            {
                return NotFound();
            }

            db.XL_DANG_KY_PHE_DUYET.Remove(xL_DANG_KY_PHE_DUYET);
            db.SaveChanges();

            return Ok(xL_DANG_KY_PHE_DUYET);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool XL_DANG_KY_PHE_DUYETExists(int id)
        {
            return db.XL_DANG_KY_PHE_DUYET.Count(e => e.ID == id) > 0;
        }
    }
}