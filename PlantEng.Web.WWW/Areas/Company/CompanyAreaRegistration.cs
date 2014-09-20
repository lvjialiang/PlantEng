using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Company
{
    public class CompanyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Company";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region == 产品自定义分类列表 ==
            context.MapRoute(
                "Company_Space_Product_List_Cat",
                "company/{companyId}/productlist-{pcid}.html",
                new { action = "ProductList", controller = "Space" }
            );
            #endregion

            context.MapRoute(
                "Company_Space_Module_Default",
                "company/{companyId}/{action}.html",
                new { action = "Index", controller = "Space" }
            );

            context.MapRoute(
                 "Company_Space_Default",
                 "company/{companyId}.html",
                 new { action = "Index", controller = "Space" }
             );

            context.MapRoute(
                 "Company_Default",
                 "company/{action}",
                 new { action = "Index", controller = "Home" }
             );
        }
    }
}
