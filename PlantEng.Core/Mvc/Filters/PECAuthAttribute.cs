using System;
using System.Web.Mvc;

using PlantEng.Core;

namespace PlantEng.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PECAuthAttribute : FilterAttribute, IAuthorizationFilter
    {
        private int _roleId = 0;

        public PECAuthAttribute() {
        }
        public PECAuthAttribute(int roleId) {
            _roleId = roleId;
        }
        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string loginUrl = "/login?returnurl=";
            string currentUrl = filterContext.HttpContext.Request.Url.ToString();
            //1,判断登陆            
            if(!PlantEngContext.Current.IsLogin){
                filterContext.HttpContext.Response.Redirect(string.Concat(loginUrl,currentUrl));
                filterContext.HttpContext.Response.End();
            }
            //判断用户类型
            if (_roleId != 0)
            {
                if (PlantEngContext.Current.RoleId != _roleId) {
                    filterContext.HttpContext.Response.Redirect("/accounts/");
                    filterContext.HttpContext.Response.End();
                }
            }
        }

        #endregion
    }
}
