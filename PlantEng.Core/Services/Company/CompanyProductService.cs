
using PlantEng.Models.Company;
using PlantEng.Data.Company;
using PlantEng.Models;
using System.Collections.Generic;

namespace PlantEng.Services.Company
{
    public static class CompanyProductService
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CompanyProductInfo Update(CompanyProductInfo model)
        {
            if (model.Id == 0)
            {
                int id = CompanyProductManage.Insert(model);
                model.Id = id;
            }
            else
            {
                CompanyProductManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 获得产品信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static CompanyProductInfo Get(int id, int companyId)
        {
            return CompanyProductManage.Get(id, companyId);
        }
        public static CompanyProductInfo Get(int id) {
            return CompanyProductManage.Get(id);
        }
        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyProductInfo> List(CompanyProductSearchSetting searchSetting)
        {
            return CompanyProductManage.List(searchSetting);
        }
        /// <summary>
        /// 没有分类的列表
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public static IList<CompanyProductInfo> ListWithoutPage(int topCount,int companyId) {
            return CompanyProductManage.ListWithoutPage(topCount,companyId);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int Delete(int id, int companyId)
        {
            return CompanyProductManage.Delete(id, companyId);
        }
    }
}
