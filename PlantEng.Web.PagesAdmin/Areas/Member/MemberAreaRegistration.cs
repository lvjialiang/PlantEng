using System.Web.Mvc;

namespace PlantEng.Web.PagesAdmin.Areas.Member
{
    public class MemberAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Member";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Member_Admin_default",
                "Member/admin/{action}",
                new { action = "List", controller = "Admin" }
            );
            context.MapRoute(
                "Member_default",
                "Member/{action}",
                new { action = "List",controller="Home"}
            );
        }
    }
}
