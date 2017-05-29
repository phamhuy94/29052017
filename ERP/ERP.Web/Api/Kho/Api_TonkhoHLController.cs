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
using System.Data.SqlClient;

namespace ERP.Web.Areas.HopLong.Api.Kho
{
    public class Api_TonKhoHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        [Route("api/Api_TonKhoHL/GetHH_TON_KHO/{id}/{page}")]
        public List<HopLong_DS_TONKHO_Result> GetHH_TON_KHO(string id, int page)
        {
            var query = db.Database.SqlQuery<HopLong_DS_TONKHO_Result>("HopLong_DS_TONKHO @machuan, @trangso", new SqlParameter("machuan", id), new SqlParameter("trangso", page));
            var result = query.ToList();
            return result;
        }

        [Route("api/Api_TonKhoHL/GetHH_Comment/{mahang}")]
        public List<Prod_HH_Comments_Result> GetHH_Comment(string mahang)
        {
            var query = db.Database.SqlQuery<Prod_HH_Comments_Result>("Prod_HH_Comments @mahang", new SqlParameter("mahang", mahang));
            var result = query.ToList();
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("api/Api_TonKhoHL/NewComment")]
        public IHttpActionResult PostNewComment(HH_COMMENTS comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HH_COMMENTS newcomment = new HH_COMMENTS();
            newcomment.MA_HANG = comment.MA_HANG;
            newcomment.NOI_DUNG_COMMENT = comment.NOI_DUNG_COMMENT;
            newcomment.NGAY_COMMENT = DateTime.Today.Date;
            newcomment.NGUOI_COMMENT = comment.NGUOI_COMMENT;
            db.HH_COMMENTS.Add(newcomment);
            db.SaveChanges();

            return Ok(newcomment);
        }


        //Cập nhật ghi chú hàng hóa
        [HttpPost]
        [Route("api/Api_TonKhoHL/CapNhatGhiChu/{mahang}/{ghichu}")]
        public IHttpActionResult CapNhatGhiChu(string mahang, string ghichu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string thongbaocomment;

            var query = db.HHs.Where(x => x.MA_HANG == mahang).FirstOrDefault();
            if(query != null)
            {
                query.GHI_CHU = ghichu;
                thongbaocomment = "Bạn đã cập nhật thành công ghi chú cho mã hàng " + query.MA_CHUAN;
            }
            else
            {
                thongbaocomment = "Không tìm thấy thông tin về mã hàng mà bạn muốn cập nhật ghi chú";
            }
            
            db.SaveChanges();

            return Ok(thongbaocomment);
        }



    }
}