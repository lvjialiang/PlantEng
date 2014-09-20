
using PlantEng.Models;
using PlantEng.Models.Advertisment;
using PlantEng.Data.Advertisment;

namespace PlantEng.Services.Advertisment
{
    public static class AdPositionService
    {
        /// <summary>
        /// 广告位列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<AdPositionInfo> List(AdSearchSetting setting) {
            return AdPositionManage.List(setting);
        }
        /// <summary>
        /// 添加或编辑广告位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AdPositionInfo Create(AdPositionInfo model){
            if(model.Id == 0){
                int id=AdPositionManage.Insert(model);
            }else{
                AdPositionManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 获取广告位详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AdPositionInfo Get(int id) {
            return AdPositionManage.Get(id);
        }
    }
}
