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
    public class Api_NhomNguoiDungNghiepVuController : ApiController
    {
        bool trangthai;

        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NhomNguoiDungNghiepVu
        [Route("api/Api_NhomNguoiDungNghiepVu/{nhomnghiepvu}/{username}")]
        public bool Gettrangthai(string nhomnghiepvu, string username)
        {

            var vData = db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU.Where(x => x.ID_NHOM_NGHIEP_VU == nhomnghiepvu && x.USERNAME == username).ToList();
            if (vData.Count() > 0)
                trangthai = true;
            else
                trangthai = false;
            return trangthai;
        }

        // GET: api/Api_NhomNguoiDungNghiepVu/5
        [ResponseType(typeof(CN_NHOM_NGUOI_DUNG_NGHIEP_VU))]
        public IHttpActionResult GetCN_NHOM_NGUOI_DUNG_NGHIEP_VU(string id)
        {
            CN_NHOM_NGUOI_DUNG_NGHIEP_VU cN_NHOM_NGUOI_DUNG_NGHIEP_VU = db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU.Find(id);
            if (cN_NHOM_NGUOI_DUNG_NGHIEP_VU == null)
            {
                return NotFound();
            }

            return Ok(cN_NHOM_NGUOI_DUNG_NGHIEP_VU);
        }

        // PUT: api/Api_NhomNguoiDungNghiepVu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCN_NHOM_NGUOI_DUNG_NGHIEP_VU(string id, CN_NHOM_NGUOI_DUNG_NGHIEP_VU cN_NHOM_NGUOI_DUNG_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cN_NHOM_NGUOI_DUNG_NGHIEP_VU.USERNAME)
            {
                return BadRequest();
            }

            db.Entry(cN_NHOM_NGUOI_DUNG_NGHIEP_VU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CN_NHOM_NGUOI_DUNG_NGHIEP_VUExists(id))
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

        // POST: api/Api_NhomNguoiDungNghiepVu
        [ResponseType(typeof(CN_NHOM_NGUOI_DUNG_NGHIEP_VU))]
        public IHttpActionResult PostCN_NHOM_NGUOI_DUNG_NGHIEP_VU(CN_NHOM_NGUOI_DUNG_NGHIEP_VU cN_NHOM_NGUOI_DUNG_NGHIEP_VU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU.Add(cN_NHOM_NGUOI_DUNG_NGHIEP_VU);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CN_NHOM_NGUOI_DUNG_NGHIEP_VUExists(cN_NHOM_NGUOI_DUNG_NGHIEP_VU.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cN_NHOM_NGUOI_DUNG_NGHIEP_VU.USERNAME }, cN_NHOM_NGUOI_DUNG_NGHIEP_VU);
        }

        // DELETE: api/Api_NhomNguoiDungNghiepVu/5
        [ResponseType(typeof(CN_NHOM_NGUOI_DUNG_NGHIEP_VU))]
        [Route("api/Api_NhomNguoiDungNghiepVu/{idnhomnghiepvu}/{username}")]
        public IHttpActionResult DeleteCN_CHI_TIET_NHOM_NGHIEP_VU(string idnhomnghiepvu, string username)
        {
            CN_NHOM_NGUOI_DUNG_NGHIEP_VU nhomnghiepvu = db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU.Where(x => x.ID_NHOM_NGHIEP_VU == idnhomnghiepvu && x.USERNAME == username).FirstOrDefault();

            if (nhomnghiepvu == null)
            {
                return NotFound();
            }

            db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU.Remove(nhomnghiepvu);
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

        private bool CN_NHOM_NGUOI_DUNG_NGHIEP_VUExists(string id)
        {
            return db.CN_NHOM_NGUOI_DUNG_NGHIEP_VU.Count(e => e.USERNAME == id) > 0;
        }
    }
}