using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Mag
{
    public class MagAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mag";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Mag_default",
                "Mag/{action}",
                new { action = "Index", id = UrlParameter.Optional, controller="Home" }
            );
        }
    }
}
