using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.About
{
    public class AboutAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "About";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "About_default",
                "About/{action}",
                new { action = "Index", controller="Home" }
            );
        }
    }
}
