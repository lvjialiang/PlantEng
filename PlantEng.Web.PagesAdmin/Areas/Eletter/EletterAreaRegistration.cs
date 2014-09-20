using System.Web.Mvc;

namespace PlantEng.Web.PagesAdmin.Areas.Eletter
{
    public class EletterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Eletter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Eletter_default",
                "Eletter/{action}",
                new { action = "Index", controller="Home" }
            );
        }
    }
}
