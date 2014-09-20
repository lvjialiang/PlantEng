using System.Web.Mvc;

namespace PlantEng.Web.PagesAdmin.Areas.Adm
{
    public class AdvertismentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Adm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Adm_default",
                "Adm/{controller}/{action}/{id}",
                new { action = "List",controller="Slot", id = UrlParameter.Optional }
            );
        }
    }
}
