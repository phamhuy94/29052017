using System.Web.Mvc;

namespace ERP.Web.Areas.KinhDoanh
{
    public class KinhDoanhAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KinhDoanh";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KinhDoanh_default",
                "KinhDoanh/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}