using System.Web.Mvc;

namespace PlantEng.Web.PagesAdmin.Areas.Article
{
    public class ArticleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Article";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Article_default",
                "Article/{action}/{id}",
                new { action = "List",controller="Home", id = UrlParameter.Optional }
            );
        }
    }
}
