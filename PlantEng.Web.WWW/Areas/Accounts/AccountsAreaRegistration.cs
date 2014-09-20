using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Accounts
{
    public class AccountsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Accounts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region == Blog ==
            context.MapRoute(
                "Accounts_Blog_default",
                "Accounts/Blog/{action}",
                new { action = "Index", controller = "Blog" }
            );
            #endregion


            #region == Company ==
            context.MapRoute(
                "Accounts_Company_default",
                "Accounts/Company/{action}",
                new { action = "NewsList", controller = "Company" }
            );
            #endregion



            context.MapRoute(
                "Accounts_default",
                "Accounts/{action}",
                new { action = "Index",controller="Accounts" }
            );
        }
    }
}
