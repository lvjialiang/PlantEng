using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Api.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Api/Home/

        public ActionResult Ping()
        {
            return Content("ok");
        }

    }
}
