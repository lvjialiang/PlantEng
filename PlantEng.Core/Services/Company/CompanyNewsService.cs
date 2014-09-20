

using PlantEng.Models.Company;
using PlantEng.Data.Company;
using PlantEng.Models;
using System.Collections.Generic;

namespace PlantEng.Services.Company
{
    public static class CompanyNewsService
    {

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CompanyNewsInfo Update(CompanyNewsInfo model)
        {
            if (model.Id == 0)
            {
                int id = CompanyNewsManage.Insert(model);
                model.Id = id;
            }
            else
            {
                CompanyNewsManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 获得新闻信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static CompanyNewsInfo Get(int id, int companyId)
        {
            return CompanyNewsManage.Get(id, companyId);
        }
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyNewsInfo> List(CompanyNewsSearchSetting searchSetting)
        {
            return CompanyNewsManage.List(searchSetting);
        }
        /// <summary>
        /// 前台页面公司新闻列表，也包含编辑发的新闻
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static IList<CompanyNewsInfo> ListWithoutPageForFront(int topCount, int companyId) {
            return CompanyNewsManage.ListWithoutPageForFront(topCount,companyId);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int Delete(int id, int companyId)
        {
            return CompanyNewsManage.Delete(id, companyId);
        }
    }
}
