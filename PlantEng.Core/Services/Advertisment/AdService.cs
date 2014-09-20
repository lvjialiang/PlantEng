using PlantEng.Models;
using PlantEng.Models.Advertisment;
using PlantEng.Data.Advertisment;
using System.Collections.Generic;

namespace PlantEng.Services.Advertisment
{
    /// <summary>
    /// 广告物料库
    /// </summary>
    public static class AdService
    {
        /// <summary>
        /// 物料列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<AdInfo> List(AdSearchSetting setting)
        {
            return AdManage.List(setting);
        }
        /// <summary>
        /// 获得没有分页的物料
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static List<AdInfo> ListWithoutPage(int deliveryId)
        {
            return AdManage.ListWithoutPage(deliveryId);
        }
        /// <summary>
        /// 添加或编辑物料信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AdInfo Create(AdInfo model) {
            if (model.Id == 0)
            {
                int id = AdManage.Insert(model);
            }
            else {
                AdManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 获得物料
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public static AdInfo Get(int adId)
        {
            return AdManage.Get(adId);
        }
    }
}
