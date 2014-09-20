using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PlantEng.Core;
using PlantEng.Services;
using PlantEng.Services.Blog;

namespace PlantEng.Web.WWW.Areas.Space.Controllers
{
    /// <summary>
    /// 用户空间
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        /// url /space/122
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int spaceId = PlantEngContext.Current.ClientSpaceId;
            var spaceInfo = MemberService.Get(spaceId);

            var blogPostList = BlogPostService.ListWithoutPage(spaceId,10);
            

            ViewBag.SpaceInfo = spaceInfo;
            ViewBag.BlogPostList = blogPostList;

            return View();
        }

    }
}
