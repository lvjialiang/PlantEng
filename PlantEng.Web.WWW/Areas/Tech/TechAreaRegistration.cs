using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Tech
{
    public class TechAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tech";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //单个技术频道更多页面
            context.MapRoute(
                "Tech_Signle_Index_List",
                "Tech/{alias}/list",
                new { action = "ChannelList", controller = "Home" });
            //单个技术频道路由
            context.MapRoute(
                "Tech_Signle_Index",
                "Tech/{alias}",
                new { action = "ChannelIndex", controller = "Home" });

            //技术频道首页默认路由
            context.MapRoute(
                "Tech_default",
                "Tech/{controller}/{action}",
                new { action = "Index",controller="Home"}
            );
        }
    }
}
