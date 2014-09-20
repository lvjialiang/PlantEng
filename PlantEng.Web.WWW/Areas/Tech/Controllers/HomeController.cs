using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PlantEng.Services.Category;
using PlantEng.Services.Article;
using Controleng.Common;
using PlantEng.Models.Category;

namespace PlantEng.Web.WWW.Areas.Tech.Controllers
{
    /// <summary>
    /// 技术分类控制器
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Tech/Home/

        #region == 技术频道首页 /tech/ ==
        /// <summary>
        /// 技术频道首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获得所有技术分类列表
            ViewBag.TechList = TechService.List();
            return View();
        }

        /// <summary>
        /// 技术频道首页文章列表控件
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult _ArticleListForIndex(int techId)
        {

            //根据技术分类id获得属于该类别下的相关文章列表
            ViewBag.ArticleList = ArticleService.ListWithoutPage(5, new int[] { }, new int[] { techId });
            return View("_ArticleListForIndex");
        }
        #endregion

        #region == 单个技术分类 /tech/{alias} ==
        public ActionResult ChannelIndex(string alias)
        {
            //根据别名获得技术分类详细信息
            var techModel = TechService.GetTechInfoByAlias(alias);
            if (techModel.Id == 0 || techModel.IsDeleted)
            {
                return Content("Argument error.");
            }
            ViewBag.TechInfo = techModel;
            return View();
        }
        [ChildActionOnly]
        public ActionResult _ArticleListForChannelIndex(string title,int catId,TechInfo techInfo) {
            var articleList = ArticleService.ListWithoutPage(3, new int[] { catId }, new int[] { techInfo.Id });

            ViewBag.Title = title;
            ViewBag.CatId = catId;
            ViewBag.TechInfo = techInfo;
            ViewBag.ArticleList = articleList;

            return View();
        }
        #endregion

        #region == 单个技术分类的更多页面 /tech/{alias}/list ==
        public ActionResult ChannelList(string alias)
        {
            //根据别名获得技术分类详细信息
            var techModel = TechService.GetTechInfoByAlias(alias);
            if (techModel.Id == 0 || techModel.IsDeleted)
            {
                return Content("Argument error.");
            }
            //获得栏目ID
            int catId = CECRequest.GetQueryInt("catid", 0);
            var catInfo = ColumnService.GetById(catId);

            int pageIndex = CECRequest.GetQueryInt("page", 1);

            var articleList = ArticleService.List(new Models.ArticleSearchSetting()
            {
                PageIndex = pageIndex,
                ColumnIds = new int[] { catId },
                TechIds = new int[] { techModel.Id },
                IsDeleted = false
            });

            ViewBag.TechInfo = techModel;
            ViewBag.ColumnInfo = catInfo;
            ViewBag.ArticleList = articleList;
            return View();
        }
        #endregion
    }
}
