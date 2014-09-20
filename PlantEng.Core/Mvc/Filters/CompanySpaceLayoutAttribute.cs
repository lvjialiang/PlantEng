using System;
using System.Web.Mvc;
using System.Text.RegularExpressions;

using Controleng.Common;
using PlantEng.Services;

namespace PlantEng.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CompanySpaceLayoutAttribute : FilterAttribute, IActionFilter
    {
        #region IActionFilter Members       

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            int companyId = 0;
            Match m = Regex.Match(request.Url.AbsolutePath,@"/company/(\d+)",RegexOptions.IgnoreCase);
            if(m.Success){
                companyId = Utils.StrToInt(m.Groups[1].Value,0);
            }
            var companyInfo = MemberService.GetCompanyInfo(companyId);
            if(companyInfo.CompanyId == 0){
                response.Redirect("/");
                response.End();
            }
            filterContext.Controller.ViewBag.CompanyInfo = companyInfo;

            //产品自定义分类
            //var productCatList = PlantEng.Services.Company.CompanyProductCategoryService.GetCategoryList(companyInfo.CompanyId);
            //filterContext.Controller.ViewBag.CustomerProductCatList = productCatList;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        #endregion
    }
}
