using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Space
{
    public class SpaceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Space";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region == Blog Archive ==
            context.MapRoute(
                "Space_Blog_Archive",
                "blog/archive/{postId}",
                new { action = "ArchiveShow", controller = "Blog", postId = 0 },
                new { postId = @"\d+" }
            );
            #endregion

            context.MapRoute(
                "Space_default",
                "Space/{spaceid}/{controller}/{action}/",
                new { action = "Index", controller = "Home",spaceid = 0 },
                new { spaceid = @"\d+" }
            );
        }
    }
}
