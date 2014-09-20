using System.Collections.Generic;

using PlantEng.Models.Category;
using PlantEng.Data.Category;

namespace PlantEng.Services.Category
{
    public static class TechService
    {
        public static IList<TechInfo> List() {
            return CategoryManage.TechList();
        }
        public static TechInfo Create(TechInfo model) {
            if (model.Id == 0)
            {
                //Add
                int id = CategoryManage.AddTechInfo(model);
                model.Id = id;
            }
            else {
                CategoryManage.UpdateTechInfo(model);
            }
            return model;
        }
        public static TechInfo GetById(int id) {
            return CategoryManage.GetTechInfoById(id);
        }
        /// <summary>
        /// 根据别名获得分类信息
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static TechInfo GetTechInfoByAlias(string alias) {
            return CategoryManage.GetTechInfoByAlias(alias);
        }
    }
}
