using System.Web.Mvc;

using PlantEng.Core;
using PlantEng.Models;
using PlantEng.Services.Blog;
using Controleng.Common;
using PlantEng.Services;

namespace PlantEng.Web.WWW.Areas.Space.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Space/Blog/
        [ActionName("Index")]
        public ActionResult List()
        {
            int spaceId = PlantEngContext.Current.ClientSpaceId;
            var spaceInfo = MemberService.Get(spaceId);

            int pageIndex = CECRequest.GetQueryInt("page",1);

            var postList = BlogPostService.List(new BlogSearchSetting() { 
                PageIndex = pageIndex,
                UserId = spaceId
            });

            ViewBag.PostList = postList;
            ViewBag.SpaceInfo = spaceInfo;
            return View("List");
        }
        /// <summary>
        /// 博客显示页面 /blog/archive/123
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public ActionResult ArchiveShow(int postId) {
            
            var blogInfo = BlogPostService.Get(postId);
            if (blogInfo.Id == 0) { return Redirect("/"); }

            var spaceInfo = MemberService.Get(blogInfo.UserId);
            //更新浏览数
            BlogPostService.UpdateViewCount(blogInfo.Id);

            //space 模板页专用
            ViewBag.SpaceInfo = spaceInfo;
            return View(blogInfo);
        }

    }
}
