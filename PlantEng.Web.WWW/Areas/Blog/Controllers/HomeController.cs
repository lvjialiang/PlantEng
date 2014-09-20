using System.Web.Mvc;


using Controleng.Common;
using PlantEng.Models;
using PlantEng.Services.Blog;

namespace PlantEng.Web.WWW.Areas.Blog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Blog/
        public ActionResult Index()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            int systemCategoryId = CECRequest.GetQueryInt("scatId",0);
            var list = BlogPostService.List(new BlogSearchSetting() { 
                 PageIndex = pageIndex,
                 SystemCategoryId = systemCategoryId
                 
            });
            ViewBag.List = list;
            return View();
        }

    }
}
