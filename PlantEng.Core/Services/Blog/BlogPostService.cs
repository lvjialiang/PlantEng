
using PlantEng.Models.Blog;
using PlantEng.Data.Blog;
using PlantEng.Models;
using System.Collections.Generic;

namespace PlantEng.Services.Blog
{
    public static class BlogPostService
    {
        
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BlogPostInfo Update(BlogPostInfo model)
        {
            if (model.Id == 0)
            {
                int id = BlogPostManage.Insert(model);
                model.Id = id;
            }
            else {
                BlogPostManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="postId"></param>
        public static void UpdateViewCount(int postId)
        {
            BlogPostManage.UpdateViewCount(postId);
        }
        public static BlogPostInfo Get(int postId) {
            return BlogPostManage.Get(postId);
        }
        /// <summary>
        /// 获得博客信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static BlogPostInfo Get(int postId,int userId)
        {
            return BlogPostManage.Get(postId,userId);
        }
        /// <summary>
        /// 获得没有分页的博客列表
        /// 默认10条
        /// </summary>
        /// <param name="topCount">默认10条</param>
        /// <returns></returns>
        public static IList<BlogPostInfo> ListWithoutPage(int userId,int topCount = 10) {
            return BlogPostManage.ListWithoutPage(userId,topCount);   
        }
        /// <summary>
        /// 博客列表
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<BlogPostInfo> List(BlogSearchSetting searchSetting)
        {
            return BlogPostManage.List(searchSetting);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int Delete(int postId, int userId) {
            return BlogPostManage.Delete(postId,userId);
        }
    }
}
