using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlantEng.Models.Article;
using PlantEng.Data.Article;
using PlantEng.Models;
using PlantEng.Models.Category;

namespace PlantEng.Services.Article
{
    public static class ArticleService
    {
        public static ArticleInfo Create(ArticleInfo model) {
            if (model.Id == 0)
            {
                //Insert
                int id = ArticleManage.Insert(model);
                model.Id = id;
            }
            else {
                ArticleManage.Update(model);
            }
            //插入ArticleTags表
            ArticleManage.InsertArticleTagData(model.Id,model.Tags);
            return model;
        }
        public static ArticleInfo GetById(int id) {
            return ArticleManage.GetById(id);
        }
        public static IPageOfList<ArticleInfo> List(ArticleSearchSetting setting) {
            return ArticleManage.List(setting);
        }
        public static void InsertArticle2Category(int articleId, CatType catType, List<int> ids) {
            ArticleManage.InsertArticle2Category(articleId,catType,ids);
        }
        public static List<int> Article2CategoryListByArticleIdAndType(int articleId,CatType catType) {
            return ArticleManage.Article2CategoryListByArticleIdAndType(articleId, catType);
        }
        /// <summary>
        /// 获得不带分页的文章列表
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="catIds"></param>
        /// <returns></returns>
        public static List<ArticleInfo> ListWithoutPage(int topCount, int[] catIds) {
            return ArticleManage.ListWithoutPage(topCount,catIds,new int[]{});
        }
        /// <summary>
        /// 获得不带分页的文章列表
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="catIds"></param>
        /// <param name="techIds"></param>
        /// <returns></returns>
        public static List<ArticleInfo> ListWithoutPage(int topCount,int[] catIds,int[] techIds) {
            return ArticleManage.ListWithoutPage(topCount, catIds,techIds);
        }
        /// <summary>
        /// 根据自定义条件获得文章列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<ArticleInfo> ListWithoutPageByCondition(string condition) {
            return ArticleManage.ListWithoutPageByCondition(condition);
        }
        public static ArticleInfo GetByTimespan(string timespan) {
            return ArticleManage.GetByTimespan(timespan);
        }
        /// 获得相关文章
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetRelatedArticleList(int topCount,int articleId, string tags) {
            return ArticleManage.GetRelatedArticleList(topCount,articleId,tags);
        }
        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="articleId"></param>
        public static void UpdateViewCount(int articleId) {
            ArticleManage.UpdateViewCount(articleId);
        }
    }
}
