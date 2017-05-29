using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Settings.Controllers
{
    [AuthorizeBussiness]
    public class NghiepVuNguoiDungController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        public ActionResult phanquyen(string id)
        {
            //Lấy ra danh sách các quyền trong hệ thống

            //lấy danh sách các controller trong hệ thống
            var listControl = db.CN_NGHIEP_VU.AsEnumerable();
            List<SelectListItem>  items = new List<SelectListItem>();
            foreach (var item in listControl)
            {
                items.Add(new SelectListItem()
                {
                    Text = item.TEN_NGHIEP_VU,
                    Value = item.ID
                });
            }
            ViewBag.items = items;
            //lấy danh sách các quyền đã được cấp
            var listFunction = from g in db.CN_NGHIEP_VU_NHAN_VIEN
                               join p in db.CN_CHI_TIET_NGHIEP_VU on g.ID_CHI_TIET_NGHIEP_VU equals p.ID
                               where g.USERNAME == id //(int)Session["USER_ID"]
                               select new SelectListItem() { Value = p.ID.ToString(), Text = p.MO_TA };
            ViewBag.listFunction = listFunction;
            //lưu id của người dùng đang được cấp quyền ra session
            Session["USER_PERMISSIONS"] = id;  //Session["USER_ID"];
            //Lấy người dùng
            //var USER_PERMISSIONS = db.USERS.Find(Session["USER_ID"]);
            var QUYEN_NGUOI_DUNG = db.HT_NGUOI_DUNG.Find(id);
            //Lưu tên người dùng ra biến
            ViewBag.userpermission = QUYEN_NGUOI_DUNG.USERNAME + "(" + QUYEN_NGUOI_DUNG.HO_VA_TEN + ")";
            return View();
        }

        //Lấy danh sách quyền đang được cấp cho người dùng
        public JsonResult getPermissions(string id, string userTemp)
        {
            //Lấy tất cả các permission của user và của business
            var listgranted = (from G in db.CN_NGHIEP_VU_NHAN_VIEN
                               join P in db.CN_CHI_TIET_NGHIEP_VU on G.ID_CHI_TIET_NGHIEP_VU equals P.ID
                               where G.USERNAME == userTemp && P.ID_NGHIEP_VU == id
                               select new PermissionAction { ID = P.ID, TEN_CHI_TIET = P.TEN_CHI_TIET, MO_TA = P.MO_TA, IS_GRANTED = true }).ToList();
            //Lấy tất cả các permission của controller hiện tại
            var listpermission = from p in db.CN_CHI_TIET_NGHIEP_VU
                                 where p.ID_NGHIEP_VU == id
                                 select new PermissionAction { ID = p.ID, TEN_CHI_TIET = p.TEN_CHI_TIET, MO_TA = p.MO_TA, IS_GRANTED = false };
            //Lấy các id của permission đã được gán ở trên cho người dùng
            var listpermissionid = listgranted.Select(p => p.ID);
            //kiểm tra permission id nào của controller mà chưa có trong listgranted thì đưa vào(is_granted = false)
            foreach (var item in listpermission)
            {
                if (!listpermissionid.Contains(item.ID))
                {
                    listgranted.Add(item);
                }

            }
            return Json(listgranted.OrderBy(x => x.MO_TA), JsonRequestBehavior.AllowGet);
        }

        //cập nhật quyền người dùng
        public string updatePermission(int id, string usertemp)
        {
            string msg = "";
            var grant = db.CN_NGHIEP_VU_NHAN_VIEN.Find(id, usertemp);
            if (grant == null)
            {
                CN_NGHIEP_VU_NHAN_VIEN g = new CN_NGHIEP_VU_NHAN_VIEN() { ID_CHI_TIET_NGHIEP_VU = id, USERNAME = usertemp, MO_TA = "" };
                db.CN_NGHIEP_VU_NHAN_VIEN.Add(g);
                msg = "<div class='alert alert-success'> Cấp quyền thành công </div>";
            }
            else
            {
                db.CN_NGHIEP_VU_NHAN_VIEN.Remove(grant);
                msg = "<div class='alert alert-danger'> Hủy quyền thành công </div>";
            }
            db.SaveChanges();
            return msg;
        }

    }
}