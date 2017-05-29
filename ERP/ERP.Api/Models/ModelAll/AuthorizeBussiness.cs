
using ERP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Models.BusinessModel
{
    public class AuthorizeBussiness : ActionFilterAttribute
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["USERNAME"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;



            }
            String username = HttpContext.Current.Session["USERNAME"].ToString();
            //lấy tên action
            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" + filterContext.ActionDescriptor.ActionName;
            //lấy thông tin user
            var admin = db.HT_NGUOI_DUNG.Where(a => a.USERNAME == username && a.IS_ADMIN.Value != false).FirstOrDefault();
            //nếu là admin thì mặc nhiên được vào, không cần kiểm tra gì cả
            if (admin != null)
            {
                return;
            }

            //nếu không phải thì sẽ kiểm tra tên các permission được gán cho người dùng
            var listpermission = from p in db.CN_CHI_TIET_NGHIEP_VU
                                 join g in db.CN_NGHIEP_VU_NHAN_VIEN on p.ID equals g.ID_CHI_TIET_NGHIEP_VU
                                 where g.USERNAME == username
                                 select p.TEN_CHI_TIET;
            //kiểm tra các permission có chứa tên action mà người dùng click hay không?
            // nếu không thì nhảy tới trang thông báo
            if (!listpermission.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("/Home/NotificationAuthorize");
                return;
            }
        }
    }
}