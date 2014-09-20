using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Magazine
{
    public class MagazineAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Magazine";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Magazine_default",
                "Magazine/{action}",
                new { action = "Index",controller="Home", id = UrlParameter.Optional }
            );
        }
    }
}
