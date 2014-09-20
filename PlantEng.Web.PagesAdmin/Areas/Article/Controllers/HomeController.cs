using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Controleng.Common;
using PlantEng.Models.Article;
using PlantEng.Services.Article;
using PlantEng.Services.Category;
using PlantEng.Models.Category;
using PlantEng.Models;
using PlantEng.Services;

namespace PlantEng.Web.PagesAdmin.Areas.Article.Controllers
{
    /// <summary>
    /// 文章默认控制器
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Article/Home/
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            int catId = CECRequest.GetQueryInt("catid",0);

            //初始化栏目类别
            var allColumnList = ColumnService.List().ToList();
            var dropdownList = new List<ColumnInfo>();
            ColumnService.BuildListForTree(dropdownList, allColumnList, 0);
            ViewBag.ColumnDropDownList = dropdownList;

            var articleList = ArticleService.List(new ArticleSearchSetting() { 
                PageIndex = pageIndex,
                ColumnIds = new int[]{catId}
            });
            
            ViewBag.ArticleList = articleList;
            return View();
        }

        #region == 添加或编辑 ==
        public ActionResult Add()
        {
            int id = CECRequest.GetQueryInt("id",0);
            var articleInfo = ArticleService.GetById(id);

            //初始化栏目类别
            var allColumnList = ColumnService.List().ToList();
            var dropdownList = new List<ColumnInfo>();
            ColumnService.BuildListForTree(dropdownList, allColumnList, 0);
            ViewBag.ColumnDropDownList = dropdownList;

            //输出技术分类
            ViewBag.TechList = TechService.List().Where(m=>m.IsDeleted == false);
            //输出行业分类
            ViewBag.IndustryList = IndustryService.List().Where(m=>m.IsDeleted == false);

            //已选择的技术分类和行业分类
            ViewBag.SelectTechList = ArticleService.Article2CategoryListByArticleIdAndType(articleInfo.Id,CatType.Tech);
            ViewBag.SelectIndustryList = ArticleService.Article2CategoryListByArticleIdAndType(articleInfo.Id,CatType.Industry);

            string companyName = string.Empty;
            if(articleInfo.CompanyId>0){
                //对CompanyName进行赋值
                //数据库中只保存CompanyId，没有保存CompanyName,只能在这里处理一下
                 companyName = MemberService.GetBaseCompanyInfo(articleInfo.CompanyId).CompanyName;
            }
            ViewBag.CompanyName = companyName;

            return View(articleInfo);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ArticleInfo oldModel,FormCollection fc) {
            bool isAdd = true;
            if(oldModel.Id >0){
                isAdd = false;
            }
            if(ModelState.IsValid){
                //TODO
                //在这，最好检查一下标题是否重复，目前没做
                oldModel.AddUserName = User.Identity.Name;
                oldModel.LastModifyUserName = User.Identity.Name;
                oldModel.CategoryId = Utils.StrToInt(fc["select_column"],0);

                //改变URL
                oldModel.Url = oldModel.QuickLinkUrl;
                if(string.IsNullOrEmpty(oldModel.Url)){
                    oldModel.Url = string.Format("/article/show/{0}.html", oldModel.TimeSpan);
                }

                oldModel = ArticleService.Create(oldModel);

                //插入到Article2Category表中
                //1,技术分类
                var requestTechListArray = CECRequest.GetFormString("cbtechlist").Split(',');
                List<int> techList = new List<int>();
                foreach(string item in requestTechListArray){
                    int id = Utils.StrToInt(item,0);
                    if(id>0){
                        techList.Add(id);
                    }
                }
                ArticleService.InsertArticle2Category(oldModel.Id, CatType.Tech, techList);
                //2,行业分类
                var requestIndustryList = CECRequest.GetFormString("cbindustrylist").Split(',');
                List<int> industryList = new List<int>();
                foreach(string item in requestIndustryList){
                    int id = Utils.StrToInt(item,0);
                    if(id>0){
                        industryList.Add(id);
                    }
                }
                ArticleService.InsertArticle2Category(oldModel.Id, CatType.Industry, industryList);
                

                //完成，提示信息
                if (isAdd)
                {
                    ViewBag.Msg = "添加成功！<a href=\"add\">继续？</a><span class=\"ml10\">或</span><a href=\"list\" class=\"ml10\">返回</a>";
                }
                else
                {
                    ViewBag.Msg = "修改成功！<a href=\"add\">添加新文章？</a><span class=\"ml10\">或</span><a href=\"list\" class=\"ml10\">返回</a>";
                }
            }

            //初始化栏目类别
            //初始化栏目类别
            var allColumnList = ColumnService.List().ToList();
            var dropdownList = new List<ColumnInfo>();
            ColumnService.BuildListForTree(dropdownList, allColumnList, 0);
            ViewBag.ColumnDropDownList = dropdownList;

            //输出技术分类
            ViewBag.TechList = TechService.List().Where(m => m.IsDeleted == false);
            //输出行业分类
            ViewBag.IndustryList = IndustryService.List().Where(m => m.IsDeleted == false);

            //已选择的技术分类和行业分类
            ViewBag.SelectTechList = ArticleService.Article2CategoryListByArticleIdAndType(oldModel.Id, CatType.Tech);
            ViewBag.SelectIndustryList = ArticleService.Article2CategoryListByArticleIdAndType(oldModel.Id, CatType.Industry);

            string companyName = string.Empty;
            if (oldModel.CompanyId > 0)
            {
                //对CompanyName进行赋值
                //数据库中只保存CompanyId，没有保存CompanyName,只能在这里处理一下
                companyName = MemberService.GetBaseCompanyInfo(oldModel.CompanyId).CompanyName;
            }
            ViewBag.CompanyName = companyName;

            return View(oldModel);
        }
        #endregion
    }
}
