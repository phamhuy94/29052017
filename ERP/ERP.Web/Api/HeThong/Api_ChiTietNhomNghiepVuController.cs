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
    public class Api_ChiTietNhomNghiepVuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ChiTietNhomNghiepVu
        public IQueryable<CN_CHI_TIET_NHOM_NGHIEP_VU> GetCN_CHI_TIET_NHOM_NGHIEP_VU()
        {
            return db.CN_CHI_TIET_NHOM_NGHIEP_VU;
        }

        // GET: api/Api_ChiTietNhomNghiepVu/5
        [ResponseType(typeof(CN_CHI_TIET_NHOM_NGHIEP_VU))]
        public IHttpActionResult GetCN_CHI_TIET_NHOM_NGHIEP_VU(string id)
        {
            CN_CHI_TIET_NHOM_NGHIEP_VU cN_CHI_TIET_NHOM_NGHIEP_VU = db.CN_CHI_TIET_NHOM_NGHIEP_VU.Find(id);
            if (cN_CHI_TIET_NHOM_NGHIEP_VU == null)
            {
                return NotFound();
            }

            return Ok(cN_CHI_TIET_NHOM_NGHIEP_VU);
        }

        // PUT: api/Api_ChiTietNhomNghiepVu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCN_CHI_TIET_NHOM_NGHIEP_VU(string id, CN_CHI_TIET_NHOM_NGHIEP_VU cN_CHI_TIET_NHOM_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cN_CHI_TIET_NHOM_NGHIEP_VU.ID_NHOM_NGHIEP_VU)
            {
                return BadRequest();
            }

            db.Entry(cN_CHI_TIET_NHOM_NGHIEP_VU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CN_CHI_TIET_NHOM_NGHIEP_VUExists(id))
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

        // POST: api/Api_ChiTietNhomNghiepVu

        [ResponseType(typeof(CN_CHI_TIET_NHOM_NGHIEP_VU))]

        public IHttpActionResult PostCN_CHI_TIET_NHOM_NGHIEP_VU(CN_CHI_TIET_NHOM_NGHIEP_VU cN_CHI_TIET_NHOM_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CN_CHI_TIET_NHOM_NGHIEP_VU.Add(cN_CHI_TIET_NHOM_NGHIEP_VU);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CN_CHI_TIET_NHOM_NGHIEP_VUExists(cN_CHI_TIET_NHOM_NGHIEP_VU.ID_NHOM_NGHIEP_VU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cN_CHI_TIET_NHOM_NGHIEP_VU.ID_NHOM_NGHIEP_VU }, cN_CHI_TIET_NHOM_NGHIEP_VU);
        }

        // DELETE: api/Api_ChiTietNhomNghiepVu/5
        [ResponseType(typeof(CN_CHI_TIET_NHOM_NGHIEP_VU))]
        [Route("api/Api_ChiTietNhomNghiepVu/{id}/{idchitietnghiepvu}")]
        public IHttpActionResult DeleteCN_CHI_TIET_NHOM_NGHIEP_VU(string id, int idchitietnghiepvu)
        {
            CN_CHI_TIET_NHOM_NGHIEP_VU nhomnghiepvu = db.CN_CHI_TIET_NHOM_NGHIEP_VU.Where(x => x.ID_NHOM_NGHIEP_VU == id && x.ID_CHI_TIET_NGHIEP_VU == idchitietnghiepvu).FirstOrDefault();

            if (nhomnghiepvu == null)
            {
                return NotFound();
            }

            db.CN_CHI_TIET_NHOM_NGHIEP_VU.Remove(nhomnghiepvu);
            db.SaveChanges();

            return Ok(nhomnghiepvu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CN_CHI_TIET_NHOM_NGHIEP_VUExists(string id)
        {
            return db.CN_CHI_TIET_NHOM_NGHIEP_VU.Count(e => e.ID_NHOM_NGHIEP_VU == id) > 0;
        }
    }
}