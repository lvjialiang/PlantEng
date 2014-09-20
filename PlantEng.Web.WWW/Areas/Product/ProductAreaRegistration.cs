using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Product
{
    public class ProductAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Product";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Product_default",
                "Product/{action}",
                new { action = "Index", controller="Home" }
            );
        }
    }
}
