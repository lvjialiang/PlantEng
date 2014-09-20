using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace PlantEng.Web.PagesAdmin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    #region == 判断登录Attribute ==

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PagesadminAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var routeDataTokens = filterContext.RouteData.DataTokens;
            var area = (string)routeDataTokens["area"];
            var httpContext = filterContext.HttpContext;

            if (!string.IsNullOrEmpty(area))
            {
                if (area.Equals("frame", StringComparison.OrdinalIgnoreCase))
                {
                    if (actionName.Equals("login", StringComparison.OrdinalIgnoreCase) || actionName.Equals("logout", StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }
                }
            }
            //判断是否登录
            if (!httpContext.Request.IsAuthenticated)
            {
                httpContext.Response.Redirect(FormsAuthentication.LoginUrl);
            }

            base.OnActionExecuting(filterContext);
        }
    }
    #endregion

    public class MvcApplication : System.Web.HttpApplication
    {
        #region == 解决SWFUpload上传文件过程中总是提示302的解决办法 ==
        void Application_BeginRequest(object sender, EventArgs e)
        {
            /* Fix for the Flash Player Cookie bug in Non-IE browsers.
         * Since Flash Player always sends the IE cookies even in FireFox
         * we have to bypass the cookies by sending the values as part of the POST or GET
         * and overwrite the cookies with the passed in values.
         * 
         * The theory is that at this point (BeginRequest) the cookies have not been read by
         * the Session and Authentication logic and if we update the cookies here we'll get our
         * Session and Authentication restored correctly
         */           

            try
            {
                
                string session_cookie_name = "SESSIONID";
                //设置SESSION

                if (string.IsNullOrEmpty(Controleng.Common.Utils.GetCookie(session_cookie_name)))
                {
                    Controleng.Common.Utils.WriteCookie(session_cookie_name, Guid.NewGuid().ToString());
                }


                string session_param_name = "ASPSESSID";
                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    Controleng.Common.Utils.WriteCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    Controleng.Common.Utils.WriteCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch 
            {
                //Response.StatusCode = 500;
                //Response.Write("Error Initializing Session");
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    Controleng.Common.Utils.WriteCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    Controleng.Common.Utils.WriteCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch
            {
                //Response.StatusCode = 500;
                //Response.Write("Error Initializing Forms Authentication");
            }
        }
        #endregion

        #region == MVC 默认路由配置 ==
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new PagesadminAuthAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Ajax_default", // Route name
                "Ajax/{action}", // URL with parameters
                new { controller = "Ajax", action = "Index" },
               new string[] { "PlantEng.Web.PagesAdmin.Controllers" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        #endregion
    }
}