
using PlantEng.Models;
using PlantEng.Models.Advertisment;
using PlantEng.Data.Advertisment;

namespace PlantEng.Services.Advertisment
{
    public static class AdDeliveryService
    {
        /// <summary>
        /// 广告列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<AdDeliveryInfo> List(AdSearchSetting setting)
        {
            return AdDeliveryManage.List(setting);
        }
        /// <summary>
        /// 添加或编辑广告信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AdDeliveryInfo Create(AdDeliveryInfo model) {
            if (model.Id == 0)
            {
                int id = AdDeliveryManage.Insert(model);
            }
            else {
                AdDeliveryManage.Update(model);
            }
            return model;
        }
        
        /// <summary>
        /// 获得详细信息
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static AdDeliveryInfo GetById(int deliveryId)
        {
            return AdDeliveryManage.Get(deliveryId);
        }
    }
}
