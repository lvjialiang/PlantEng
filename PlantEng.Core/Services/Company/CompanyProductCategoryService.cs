
using PlantEng.Models.Company;
using PlantEng.Data.Company;
using System.Collections.Generic;

namespace PlantEng.Services.Company
{
    public static class CompanyProductCategoryService
    {
        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CompanyProductCategoryInfo Update(CompanyProductCategoryInfo model)
        {
            if (model.Id == 0)
            {
                //插入
                int id = CompanyProductCategoryManage.Insert(model);
                model.Id = id;
            }
            else {
                CompanyProductCategoryManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 获得系统分类
        /// </summary>
        /// <returns></returns>
        public static List<CompanyProductCategoryInfo> GetSystemCategoryList() {
            return CompanyProductCategoryManage.GetSystemCategoryList();
        }
         /// <summary>
        /// 获得用户自定义分类
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static List<CompanyProductCategoryInfo> GetCategoryList(int companyId) {
            return CompanyProductCategoryManage.GetCategoryList(companyId);
        }
        /// <summary>
        /// 获得分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static CompanyProductCategoryInfo Get(int id, int companyId) {
            return CompanyProductCategoryManage.Get(id,companyId);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static bool Delete(int id, int companyId) {
            return CompanyProductCategoryManage.Delete(id,companyId);
        }
        /// <summary>
        /// 更新产品数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <param name="plus"></param>
        public static void UpdateProductCount(int id, int companyId, bool plus) {
            CompanyProductCategoryManage.UpdateProductCount(id,companyId,plus);
        }
    }
}
