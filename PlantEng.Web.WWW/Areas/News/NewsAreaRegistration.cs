using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.News
{
    public class NewsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "News";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "News_show",
                "Article/Show/{timespan}.html",
                new { action = "Show", controller = "Home" }
            );
            context.MapRoute(
                "News_default",
                "News/{action}",
                new { action = "Index",controller="Home"}
            );
        }
    }
}
