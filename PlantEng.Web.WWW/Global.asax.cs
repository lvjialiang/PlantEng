using System.Web.Mvc;
using System.Web.Routing;
using System;

namespace PlantEng.Web.WWW
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NotFoundAttribute : FilterAttribute, IActionFilter {

        #region IActionFilter Members

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //filterContext.HttpContext.Response.Write("404");
        }


        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        #endregion
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NotFoundAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region == 404 ==
            routes.MapRoute(
                "404_Default",
                "404.html",
                new { controller = "Home",action="NotFound" },
                new string[] { "PlantEng.Web.WWW.Controllers" }
            );
            #endregion

            #region == Eletter ==
            routes.MapRoute(
                "Subscribe_Default",
                "Subscribe",
                new { controller = "Home", action = "Subscribe" },
                new string[] { "PlantEng.Web.WWW.Controllers" }
            );
            #endregion

            routes.MapRoute(
                "Search_Default",
                "Search",
                new { controller = "Home", action = "Search" },
                new string[] { "PlantEng.Web.WWW.Controllers" }
            );
            #region == Search ==

            #endregion

            #region == Ajax ==
            routes.MapRoute(
                "Ajax_Default",
                "_ajax/{action}",
                new {controller = "Ajax" }
            );
            #endregion

            #region == Passport ==
            routes.MapRoute(
                "Passport_Login",
                "login",
                new { action = "Login", controller = "Passport" }
            );
            routes.MapRoute(
                "Passport_Logout",
                "logout",
                new { action = "Logout", controller = "Passport" }
            );
            routes.MapRoute(
                "Passport_Register",
                "register",
                new { action = "Register", controller = "Passport" }
            );
            routes.MapRoute(
                "Passport_RegisterCompany",
                "RegisterCompany",
                new { action = "RegisterCompany", controller = "Passport" }
            );
            routes.MapRoute(
                "Passport_RegisterSuccess",
                "RegisterSuccess",
                new { action = "RegisterSuccess", controller = "Passport" }
            );
            routes.MapRoute(
                "Passport_Ajax",
                "Passport/_ajax/{action}",
                new { action = "Index", controller = "Passport" }
            );
            #endregion

            #region == Avatar /avatar?userid=xxx ==
            routes.MapRoute(
                "Avatar_Default",
                "avatar",
                new { controller = "Home",action = "Avatar" },
                new string[] { "PlantEng.Web.WWW.Controllers" }
            );
            #endregion

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index" }, // Parameter defaults
                new string[] { "PlantEng.Web.WWW.Controllers" } //里边有多个HomeController,防止冲突，在这指定首页的controller
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            MvcHandler.DisableMvcResponseHeader = true;
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}