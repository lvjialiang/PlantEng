using System.Web.Mvc;

using PlantEng.Services.Blog;
using PlantEng.Models;
using Controleng.Common;
using PlantEng.Models.Blog;
using PlantEng.Core;
using PlantEng.Mvc.Filters;

namespace PlantEng.Web.WWW.Areas.Accounts.Controllers
{
    /// <summary>
    /// 博客信息
    /// </summary>
    public class BlogController : Controller
    {
        #region == 博客首页 /accounts/blog ==
        /// <summary>
        /// /accounts/blog/
        /// </summary>
        /// <returns></returns>
        [PECAuth]
        public ActionResult Index()
        {
            int pageIndex = CECRequest.GetQueryInt("page", 1);

            var list = BlogPostService.List(new BlogSearchSetting()
            {
                PageIndex = pageIndex,
                UserId = PlantEngContext.Current.UserId
            });
            ViewBag.BlogPostList = list;

            return View();
        }
        #endregion

        #region == 发表或编辑 /accounts/blog/edit?id=123 ==
        /// <summary>
        /// 发表或编辑
        /// </summary>
        /// <returns></returns>
        [PECAuth]
        public ActionResult Edit()
        {
            int postId = CECRequest.GetQueryInt("id", 0);
            var model = BlogPostService.Get(postId, PlantEngContext.Current.UserId);
            return View(model);
        }
        [HttpPost]
        [PECAuth]
        [ValidateInput(false)]
        public ActionResult Edit(BlogPostInfo oldModel, FormCollection fc)
        {
            bool error = false;
            bool isAdd = oldModel.Id == 0;
            int userId = PlantEngContext.Current.UserId;
            string userName = PlantEngContext.Current.UserName;
            if (string.IsNullOrEmpty(oldModel.Title))
            {
                error = true;
                ModelState.AddModelError("TITLEEMPTY", "标题不能为空");
            }
            if (string.IsNullOrEmpty(oldModel.Content))
            {
                error = true;
                ModelState.AddModelError("CONTENTEMPTY", "内容不能为空");
            }
            //系统分类
            oldModel.SystemCategoryId = Utils.StrToInt(fc["ddlSystemCategory"], 0);
            if(oldModel.SystemCategoryId == 0){
                error = true;
                ModelState.AddModelError("SYSTEMCATERROR","请选择系统分类");
            }
            if (!error && ModelState.IsValid)
            {
                oldModel.UserId = userId;
                oldModel.UserName = userName;

                //获得系统分类名称
                oldModel.SystemCategoryName = PlantEng.Services.Category.TechService.GetById(oldModel.SystemCategoryId).Name;


                BlogPostService.Update(oldModel);
                if (isAdd)
                {
                    ViewBag.Msg = "添加成功,继续[<a href=\"/accounts/edit\">添加</a>]或[<a href=\"/accounts/blog/\">返回</a>]";
                }
                else
                {
                    ViewBag.Msg = "更新成功,<a href=\"/accounts/blog/\">返回</a>";
                }
            }
            return View(oldModel);
        }
        #endregion

        #region == 删除 /accounts/blog/delete/123 ==
        /// <summary>
        /// 删除 
        /// </summary>
        [PECAuth]
        public void Delete()
        {
            int postId = CECRequest.GetQueryInt("id", 0);
            int userId = PlantEngContext.Current.UserId;
            BlogPostService.Delete(postId, userId);
            Response.Redirect("/accounts/blog/", true);
        }
        #endregion
    }
}
