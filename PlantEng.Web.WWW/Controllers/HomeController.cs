using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using PlantEng.Services.Article;
using System.Text.RegularExpressions;
using PlantEng.Services;
using PlantEng.Models;
using PlantEng.Services.Video;
using PlantEng.Services.Company;
using Controleng.Common;

namespace PlantEng.Web.WWW.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public ActionResult Index_V1()
        {

            

            //读取焦点图
            ViewBag.FocusImages = ArticleService.ListWithoutPage(5,new int[]{41});

            //技术头条
            ViewBag.TechHeadLines = ArticleService.ListWithoutPage(3,new int[]{37/*技术前沿*/});

            //产业趋势
            ViewBag.ChanYeQuShi = ArticleService.ListWithoutPage(4,new int[]{5/*产业趋势*/});
            //企业新闻
            ViewBag.EnterpriseNews = ArticleService.ListWithoutPage(4,new int[]{13/*企业新闻*/});
            //新品发布
            ViewBag.NewProducts = ArticleService.ListWithoutPage(4,new int[]{14/*新品发布*/});
            //应用案例
            ViewBag.Case = ArticleService.ListWithoutPage(3,new int[]{39/*应用案例*/});

            //白皮书
            ViewBag.WhitePagers = ArticleService.ListWithoutPage(2,new int[]{20} /*白皮书*/);

            //视频
            ViewBag.Videos = VideoService.ListWithoutPage(3);

            //Webcast
            ViewBag.WebcastRss = WebcastRssService.ListWithoutPage(3);

            //产品库
            ViewBag.Products = CompanyProductService.ListWithoutPage(3,0);

            return View();
        }
        [ActionName("Index")]
        public ActionResult NewIndex() {

            ViewBag.NewsCenter = ArticleService.ListWithoutPage(7, new int[] { 5, 14, 15 });

            //读取焦点图
            ViewBag.FocusImages = ArticleService.ListWithoutPage(5, new int[] { 41 });

            //独家
            ViewBag.DuJia = ArticleService.ListWithoutPageByCondition(GetArticleSQLAndTopOneWithImageForNewIndex(8,15));
            //产业趋势
            ViewBag.ChanYeQuShi = ArticleService.ListWithoutPageByCondition(GetArticleSQLAndTopOneWithImageForNewIndex(8, 5));

            //企业新闻
            ViewBag.QiYeXinWen = ArticleService.ListWithoutPageByCondition(GetArticleSQLAndTopOneWithImageForNewIndex(8, 13));
            //新品速递
            ViewBag.XinPinSuDi = ArticleService.ListWithoutPageByCondition(GetArticleSQLAndTopOneWithImageForNewIndex(8, 14));

            ViewBag.DuiHua = ArticleService.ListWithoutPageByCondition("SELECT TOP(2)* FROM Articles WITH(NOLOCK) WHERE IsDeleted = 0 AND CategoryId  =76 AND ImageUrl <> '' ORDER BY PublishDateTime DESC ");

            //应用案例
            ViewBag.Case = ArticleService.ListWithoutPageByCondition("SELECT TOP(4)* FROM Articles WITH(NOLOCK) WHERE IsDeleted = 0 AND CategoryId  = 39 AND ImageUrl <> '' ORDER BY PublishDateTime DESC ");

            //技术前沿
            ViewBag.JiShuQianYan = ArticleService.ListWithoutPageByCondition(GetArticleSQLAndTopOneWithImageForNewIndex(9, 37));

            //技术之源
            ViewBag.JiShuZhiYuna = ArticleService.ListWithoutPageByCondition(GetArticleSQLAndTopOneWithImageForNewIndex(9, 38));
            //视频
            ViewBag.Videos = VideoService.ListWithoutPage(3);

            //Webcast
            ViewBag.WebcastRss = WebcastRssService.ListWithoutPage(3);

            ViewBag.Products = CompanyProductService.ListWithoutPage(12, 0);

            ViewBag.Companys = MemberService.CompanyList(new MemberSearchSetting()
            {
                MemberType = MemberType.Enterprise,
                PageIndex = 1,
                CompanyStatus = CompanyStatus.Pass,
                PageSize = 12
            });

            return View("NewIndex");
        }
        private string GetArticleSQLAndTopOneWithImageForNewIndex(int topCount,int catId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"SELECT * FROM (
	                            SELECT TOP(1)* FROM dbo.Articles WITH(NOLOCK)
	                            WHERE IsDeleted = 0 AND CategoryId = {1}
	                            AND ImageUrl <> ''
	                            ORDER BY PublishDateTime DESC 
                            ) AS A
                            UNION ALL
                            SELECT * FROM (
	                            SELECT TOP({0})* FROM dbo.Articles WITH(NOLOCK)
	                            WHERE IsDeleted = 0 AND CategoryId = {1}
	                            ORDER BY PublishDateTime DESC 
                            ) AS B",topCount,catId);
            return sb.ToString();
        }

        /// <summary>
        /// 测试页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Test() {
            return View();
        }

        #region == Search ==
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Search() {
            return View();
        }
        #endregion

        #region == Subscribe ==
        /// <summary>
        /// Subscribe订阅
        /// </summary>
        /// <returns></returns>
        public ActionResult Subscribe()
        {

            //eletter列表
            var list = ArticleService.List(new ArticleSearchSetting()
            {
                PageIndex = CECRequest.GetQueryInt("page",1),
                IsDeleted = false,
                ColumnIds = CECRequest.GetQueryInt("catId",0)>0 ? new int[]{ CECRequest.GetQueryInt("catId",0) } : new int[] { 50,51,52,53,54,55,56 }
            });
            ViewBag.List = list;

            return View();
        }
        /// <summary>
        /// Eletter订阅
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EletterSubscribe(FormCollection fc) {
            string email = fc["es_email"];
            string subject = fc["es_subject"];
            if (string.IsNullOrEmpty(subject))
            {
                return Json(new { Success = 0, SubjectEmpty = 1 });
            }
            if(string.IsNullOrEmpty(email)){
                return Json(new { Success = 0, EmailEmpty = 1});
            }
            if (!Regex.IsMatch(email, @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",RegexOptions.IgnoreCase)) {
                return Json(new { Success = 0, EmailError = 1 });
            }
            
            EletterSubscribeService.Insert(new EletterSubscribeInfo { 
                Email = email,
                Subject = subject
            });
            return Json(new { Success = 1 });
        }
        #endregion

        #region == Avatar ==
        /// <summary>
        /// 输出用户头像
        /// </summary>
        /// <returns></returns>
        public ActionResult Avatar() {
            int userId = CECRequest.GetQueryInt("userid",0);
            string avatar = "nophoto.jpg";
            string contentType = "jpeg";
            string rootPath = System.Configuration.ConfigurationManager.AppSettings["AvatarImageServerDictionary"];
            MemberInfo userInfo = MemberService.Get(userId);
            if(userInfo.Id>0){
                avatar = userInfo.Avatar;
            }
            string avatarPath = string.Concat(rootPath,avatar);
            if (System.IO.File.Exists(avatarPath))
            {
                //获得文件扩展名
                string extention = System.IO.Path.GetExtension(avatarPath);

                switch (extention)
                {
                    case ".gif":
                        contentType = "gif";
                        break;
                    case ".jpeg":
                        contentType = "jpeg";
                        break;
                    case ".png":
                        contentType = "png";
                        break;
                    case ".bmp":
                        contentType = "x-ms-bmp";
                        break;
                }
            }
            else {
                avatarPath = string.Concat(rootPath, "nophoto.jpg");
            }
            return File(avatarPath, string.Format("image/{0}", contentType));
        }
        #endregion

        #region == 404 NotFound ==
        public ActionResult NotFound() {
            return View("404");
        }
        #endregion
    }
}
