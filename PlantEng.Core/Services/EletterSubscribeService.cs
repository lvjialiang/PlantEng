
using PlantEng.Data;
using PlantEng.Models;
using System.Collections.Generic;
using System.Data;

namespace PlantEng.Services
{
    public static class EletterSubscribeService
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(EletterSubscribeInfo model) {
            return EletterSubscribeManage.Insert(model);
        }

        /// <summary>
        /// 获取月份
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetMonthList() {
            return EletterSubscribeManage.GetMonthList();
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DataTable Export(string month) {
            return EletterSubscribeManage.Export(month);
        }
    }
}
