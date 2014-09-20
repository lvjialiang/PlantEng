using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantEng.Services.Article;
using System.Text.RegularExpressions;

using PlantEng.Models;
using PlantEng.Services.Category;
using Controleng.Common;

namespace PlantEng.Web.WWW.Areas.News.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /News/Home/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Show() {
            Regex r = new Regex(@"/(\d+).html",RegexOptions.IgnoreCase);
            Match m = r.Match(Request.Url.LocalPath);
            string timespan = string.Empty;
            
            if(m.Success){
                timespan = m.Groups[1].Value; 
            }
            var model = ArticleService.GetByTimespan(timespan);
            ViewBag.CategoryInfo = ColumnService.GetById(model.CategoryId);

            //获得10条相关文章
            ViewBag.RelatedArticleList = ArticleService.GetRelatedArticleList(10,model.Id,model.Tags);

            //更新文章浏览量
            ArticleService.UpdateViewCount(model.Id);

            return View(model);
        }
        public ActionResult List() {
            int catId = CECRequest.GetQueryInt("catid",0);
            int pageIndex = CECRequest.GetQueryInt("page",1);
            ViewBag.CategoryInfo = ColumnService.GetById(catId);
            var articleList = ArticleService.List(new ArticleSearchSetting() { 
                ColumnIds = new int[]{catId},
                PageIndex = pageIndex,
                IsDeleted = false
            });
            ViewBag.ArticleList = articleList;
            return View();
        }
        /// <summary>
        /// 新闻中心首页文章列表用户控件
        /// </summary>
        /// <param name="catIds"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _ArticleListForIndex(int topCount = 10,params int[] catIds) {
            ViewBag.ArticleList = ArticleService.ListWithoutPage(topCount,catIds);
            return View("_ArticleListForIndex");
        }
    }
}
