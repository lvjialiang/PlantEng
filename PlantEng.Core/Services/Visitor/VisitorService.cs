using System.Collections.Generic;


using PlantEng.Models.Visitor;
using PlantEng.Data.Visitor;
using PlantEng.Models;

namespace PlantEng.Services.Visitor
{
    public static class VisitorService
    {
        /// <summary>
        /// 添加访客信息
        /// </summary>
        /// <param name="model"></param>
        public static void Insert(VisitorInfo model) {
            VisitorManage.Insert(model);
        }
        /// <summary>
        /// 获得最近访客（没有分页）
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public static List<VisitorInfo> ListWithoutPage(int toUserId = 0, int topCount = 10)
        {
            return VisitorManage.ListWithoutPage(toUserId,topCount);
        }
        /// <summary>
        /// 获得带分页的最近访客
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfList<VisitorInfo> List(int toUserId, int pageIndex, int pageSize)
        {
            return VisitorManage.List(toUserId,pageIndex,pageSize);
        }
    }
}
