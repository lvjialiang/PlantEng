using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Resouce
{
    public class ResouceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Resouce";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Resouce_default",
                "Resouce/{action}",
                new { action = "Index", controller = "Resouce" }
            );
        }
    }
}
