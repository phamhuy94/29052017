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
    public class Api_MenuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_Menu
        public List<MENU> GetMenu()
        {
            var vData = db.MENUs.Where(x => x.MENU_CHA == null && x.MUC_TRUC_THUOC == "TONG_CONG_TY");
            var result = vData.ToList().Select(x => new MENU()
            {
                TEN_MENU = x.TEN_MENU,
                LINK = x.LINK,
                MA_MENU = x.MA_MENU,
                MENU_CHA = x.MENU_CHA
            }).ToList();
            return result;
        }

        // GET: api/Api_Menu/5
        [ResponseType(typeof(MENU))]
        public IHttpActionResult GetMENU(string id)
        {
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return NotFound();
            }

            return Ok(mENU);
        }

        // PUT: api/Api_Menu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMENU(string id, MENU mENU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mENU.MA_MENU)
            {
                return BadRequest();
            }

            db.Entry(mENU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MENUExists(id))
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

        // POST: api/Api_Menu
        [ResponseType(typeof(MENU))]
        public IHttpActionResult PostMENU(MENU mENU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MENUs.Add(mENU);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MENUExists(mENU.MA_MENU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mENU.MA_MENU }, mENU);
        }

        // DELETE: api/Api_Menu/5
        [ResponseType(typeof(MENU))]
        public IHttpActionResult DeleteMENU(string id)
        {
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return NotFound();
            }

            db.MENUs.Remove(mENU);
            db.SaveChanges();

            return Ok(mENU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MENUExists(string id)
        {
            return db.MENUs.Count(e => e.MA_MENU == id) > 0;
        }
    }
}