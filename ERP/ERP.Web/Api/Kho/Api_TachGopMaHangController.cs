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

namespace ERP.Web.Api.Kho
{
    public class Api_TachGopMaHangController : ApiController
    {
       // private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_TachGopMaHang
        public List<Prod_KHO_TachGopMa_Result> Get_DS_TachGop()
        {
            using (var db = new ERP_DATABASEEntities())
            {
                var query = db.Database.SqlQuery<Prod_KHO_TachGopMa_Result>("Prod_KHO_TachGopMa");
                var data = query.ToList();
                return data;
            }
                
        }

        // GET: api/Api_TachGopMaHang/5
        [ResponseType(typeof(KHO_INIT_TACH_GOP_MA))]
        public IHttpActionResult Get_Detail_TachGopMa(int id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                KHO_INIT_TACH_GOP_MA kHO_INIT_TACH_GOP_MA = db.KHO_INIT_TACH_GOP_MA.Find(id);
                if (kHO_INIT_TACH_GOP_MA == null)
                {
                    return NotFound();
                }

                return Ok(kHO_INIT_TACH_GOP_MA);
            }
        }

        // PUT: api/Api_TachGopMaHang/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put_TachGopMa(int id, KHO_INIT_TACH_GOP_MA kHO_INIT_TACH_GOP_MA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHO_INIT_TACH_GOP_MA.ID)
            {
                return BadRequest();
            }
            using (var db = new ERP_DATABASEEntities())
            {

                db.Entry(kHO_INIT_TACH_GOP_MA).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KHO_INIT_TACH_GOP_MAExists(id))
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
        }

        // POST: api/Api_TachGopMaHang
        [ResponseType(typeof(KHO_INIT_TACH_GOP_MA))]
        public IHttpActionResult Post_TachGopMa(KHO_INIT_TACH_GOP_MA kHO_INIT_TACH_GOP_MA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ERP_DATABASEEntities())
            {
                db.KHO_INIT_TACH_GOP_MA.Add(kHO_INIT_TACH_GOP_MA);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = kHO_INIT_TACH_GOP_MA.ID }, kHO_INIT_TACH_GOP_MA);
            }
        }

        // DELETE: api/Api_TachGopMaHang/5
        [ResponseType(typeof(KHO_INIT_TACH_GOP_MA))]
        public IHttpActionResult Delete_TachGopMa(int id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                KHO_INIT_TACH_GOP_MA kHO_INIT_TACH_GOP_MA = db.KHO_INIT_TACH_GOP_MA.Find(id);
                if (kHO_INIT_TACH_GOP_MA == null)
                {
                    return NotFound();
                }

                db.KHO_INIT_TACH_GOP_MA.Remove(kHO_INIT_TACH_GOP_MA);
                db.SaveChanges();

                return Ok(kHO_INIT_TACH_GOP_MA);
            }
        }

        protected override void Dispose(bool disposing)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        private bool KHO_INIT_TACH_GOP_MAExists(int id)
        {
            using (var db = new ERP_DATABASEEntities())
            {
                return db.KHO_INIT_TACH_GOP_MA.Count(e => e.ID == id) > 0;
            }
        }
    }
}