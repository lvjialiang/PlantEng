using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controleng.Common;
using PlantEng.Services.Article;
using PlantEng.Models;

namespace PlantEng.Web.WWW.Areas.Resouce.Controllers
{
    
    public class ResouceController : Controller
    {
        //
        // GET: /Resouce/Resouce/

        public ActionResult Index()
        {
            return View();
        }

        /*
         *  下面这个这几个方法可以优化一下，使用同一个路由，这样增加View就不至于再次增加Controller了。
         *  2012-12-11 由于时间太忙，所以先搁置 xingbaifang
         */

        /// <summary>
        /// 资料下载
        /// </summary>
        /// <returns></returns>
        public ActionResult Download() {
            return View("../Download/List");
        }
        /// <summary>
        /// 应用案例
        /// </summary>
        /// <returns></returns>
        public ActionResult Application() {
            return View("../Application/List");
        }
        /// <summary>
        /// 白皮书
        /// </summary>
        /// <returns></returns>
        public ActionResult Whitepaper() {
            return View("../Whitepaper/List");
        }
        /// <summary>
        /// 英文原创
        /// </summary>
        /// <returns></returns>
        public ActionResult Original()
        {
            return View("../Original/List");
        }
        

        [ChildActionOnly]
        public ActionResult _ArticleList(int[] catIds) {
            int pageIndex = CECRequest.GetQueryInt("page", 1);
            var articleList = ArticleService.List(new ArticleSearchSetting()
            {
                ColumnIds = catIds,
                PageIndex = pageIndex,
                IsDeleted = false
            });
            ViewBag.ArticleList = articleList;
            return View();
        }
    }
}
