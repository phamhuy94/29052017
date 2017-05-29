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
    public class Api_MENU_USERController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_MENU_USER
        public List<MENU_USER> GetMENU_USER()
        {
            var vData = db.MENU_USER;
            var result = vData.ToList().Select(x => new MENU_USER()
            {
                MA_PHONG_BAN = x.MA_PHONG_BAN,
                USERNAME = x.USERNAME,
                TRANG_THAI = x.TRANG_THAI,
                MA_MENU = x.MA_MENU,
            }).ToList();
            return result;
        }

        // GET: api/Api_MENU_USER/5
        [ResponseType(typeof(MENU_USER))]
        public IHttpActionResult GetMENU_USER(string id)
        {
            MENU_USER mENU_USER = db.MENU_USER.Find(id);
            if (mENU_USER == null)
            {
                return NotFound();
            }

            return Ok(mENU_USER);
        }

        // PUT: api/Api_MENU_USER/5
        [Route("api/Api_MENU_USER/{maphongban}/{username}/{mamenu}")]
        public IHttpActionResult PutMENU_USER(string maphongban,string username, string mamenu, MENU_USER mENU_USER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (maphongban != mENU_USER.MA_PHONG_BAN || username != mENU_USER.USERNAME)
            {
                return BadRequest();
            }
            var data = db.MENU_USER.Where(x => x.MA_PHONG_BAN == maphongban && x.USERNAME == username && x.MA_MENU == mamenu).FirstOrDefault();
            data.TRANG_THAI = mENU_USER.TRANG_THAI;

            db.Entry(data).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MENU_USERExists(username))
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

        [Route("api/Api_MENU_USER/{username}/{mamenu}")]
        public IHttpActionResult PutMENU_USER(string username, string mamenu, MENU_USER mENU_USER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ( username != mENU_USER.USERNAME)
            {
                return BadRequest();
            }
            var data = db.MENU_USER.Where(x => x.USERNAME == username && x.MA_MENU == mamenu).FirstOrDefault();
            data.TRANG_THAI = mENU_USER.TRANG_THAI;

            db.Entry(data).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MENU_USERExists(username))
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

        // POST: api/Api_MENU_USER
        [ResponseType(typeof(MENU_USER))]
        public IHttpActionResult PostMENU_USER(MENU_USER mENU_USER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MENU_USER.Add(mENU_USER);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MENU_USERExists(mENU_USER.MA_PHONG_BAN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mENU_USER.MA_PHONG_BAN }, mENU_USER);
        }

        // DELETE: api/Api_MENU_USER/5
        [ResponseType(typeof(MENU_USER))]
        public IHttpActionResult DeleteMENU_USER(string id)
        {
            MENU_USER mENU_USER = db.MENU_USER.Find(id);
            if (mENU_USER == null)
            {
                return NotFound();
            }

            db.MENU_USER.Remove(mENU_USER);
            db.SaveChanges();

            return Ok(mENU_USER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MENU_USERExists(string id)
        {
            return db.MENU_USER.Count(e => e.MA_PHONG_BAN == id) > 0;
        }
    }
}