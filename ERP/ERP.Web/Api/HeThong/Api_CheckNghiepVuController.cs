using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.HeThong
{
    public class Api_CheckNghiepVuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: api/Api_CheckNghiepVu
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Api_CheckNghiepVu/5
        public bool Get(string id)
        {
            bool trangthai;
            var data = db.CN_NGHIEP_VU_NHAN_VIEN.Where(x => x.USERNAME == id).ToList();
            if (data.Count() > 0)
                trangthai = true;
            else
                trangthai = false;

            return trangthai;
        }

        // POST: api/Api_CheckNghiepVu
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Api_CheckNghiepVu/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Api_CheckNghiepVu/5
        public void Delete(int id)
        {
        }
    }
}
