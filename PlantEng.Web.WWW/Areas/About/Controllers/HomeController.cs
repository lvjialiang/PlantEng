using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.About.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /About/Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact() {
            return View();
        }
        public ActionResult FriendLink() {
            return View();
        }
        public ActionResult Sitemap() {
            return View();
        }

    }
}
