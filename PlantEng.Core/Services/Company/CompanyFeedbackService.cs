
using PlantEng.Models;
using PlantEng.Models.Company;
using PlantEng.Data.Company;

namespace PlantEng.Services.Company
{
    public static class CompanyFeedbackService
    {
        /// <summary>
        /// 发表反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回ID</returns>
        public static int Insert(CompanyFeedbackInfo model)
        {
            return CompanyFeedbackManage.Insert(model);
        }
        /// <summary>
        /// 获得前台显示列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyFeedbackFrontInfo> FrontList(int companyId, int pageIndex, int pageSize) {
            return CompanyFeedbackManage.FrontList(companyId,pageIndex,pageSize);
        }
        /// <summary>
        /// 根据公司ID获得反馈列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyFeedbackInfo> List(int companyId, int pageIndex, int pageSize)
        {
            return CompanyFeedbackManage.List(companyId,pageIndex,pageSize);
        }
        /// <summary>
        /// 回复反馈
        /// </summary>
        /// <param name="model"></param>
        public static void ReplyFeedback(CompanyFeedbackReplyInfo model)
        {
            CompanyFeedbackManage.ReplyFeedback(model);
        }
        /// <summary>
        /// 删除反馈信息
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <param name="companyId"></param>
        public static void Delete(int feedbackId, int companyId) {
            CompanyFeedbackManage.Delete(feedbackId,companyId);
        }
    }
}
