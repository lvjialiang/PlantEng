using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Blog_default",
                "Blog",
                new { action = "Index", controller="Home" ,id = UrlParameter.Optional }
            );
        }
    }
}
