using System.Web.Mvc;

namespace PlantEng.Web.PagesAdmin.Areas.Video
{
    public class VideoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Video";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Video_default",
                "Video/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional,controller="Home" }
            );
        }
    }
}
