using System.Web.Mvc;

namespace PlantEng.Web.PagesAdmin.Areas.Frame
{
    public class FrameAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Frame";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Frame_default",
                "",
                new { controller="Home",action = "Main" }
            );
            context.MapRoute(
                "Frame_index",
                "Frame/{action}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
