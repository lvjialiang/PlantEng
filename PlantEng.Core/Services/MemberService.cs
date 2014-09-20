using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlantEng.Models;
using PlantEng.Data;
using System.Data;

namespace PlantEng.Services
{
    public static class MemberService
    {
        /// <summary>
        /// 添加用户基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(MemberInfo model) {
            //取消密码MD5
            //model.Password = Controleng.Common.Utils.MD5(model.Password);
            return MemberManage.Insert(model);
        }
        /// <summary>
        /// 下载用户基本信息
        /// 2012-12-17
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static DataTable DownloadBaseInfo(int[] ids,DateTime startDate,DateTime endDate) {
            return MemberManage.DownloadBaseInfo(ids,startDate,endDate);
        }
        /// <summary>
        /// 添加公司信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertCompany(CompanyInfo model) {
            //取消密码MD5
            //model.Password = Controleng.Common.Utils.MD5(model.Password);
            return MemberManage.InsertCompany(model);
        }
        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(MemberInfo model) {
            return MemberManage.Update(model);
        }
        /// <summary>
        /// 根据用户ID获取用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static MemberInfo Get(int userId) {
            return MemberManage.Get(userId);
        }
        /// <summary>
        /// 根据用户名获取用户基本信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static MemberInfo Get(string userName) {
            return MemberManage.Get(userName);
        }
        /// <summary>
        /// Email是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailExists(string email) {
            return MemberManage.EmailExists(email,0);
        }
        /// <summary>
        /// Email是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool EmailExists(string email,int userId) {
            return MemberManage.EmailExists(email,userId);
        }
        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool UserNameExists(string userName) {
            return MemberManage.UserNameExists(userName);
        }
        /// <summary>
        /// 公司信息列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyInfo> CompanyList(MemberSearchSetting setting) {
            return MemberManage.CompanyList(setting);
        }
        /// <summary>
        /// 基本信息列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<MemberInfo> BaseInfoList(MemberSearchSetting setting) {
            return MemberManage.BaseInfoList(setting);
        }
        /// <summary>
        /// 更新公司状态
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int SetCompanyStatus(int companyId, int userId, CompanyStatus status) {
            return MemberManage.SetCompanyStatus(companyId,userId,status);
        }
        public static CompanyInfo GetCompanyInfo(int companyId) {
            return MemberManage.GetCompanyInfo(companyId);
        }
        public static CompanyInfo GetBaseCompanyInfo(int companyId) {
            return MemberManage.GetBaseCompanyInfo(companyId);
        }
        public static CompanyInfo GetCompanyInfoByUserId(int userId) {
            return MemberManage.GetCompanyInfoByUserId(userId);
        }
        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateCompanyInfo(CompanyInfo model) {
            return MemberManage.UpdateCompanyInfo(model);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public static int UpdatePassword(int userId, string newPwd) {
            //取消密码MD5
            //newPwd = Controleng.Common.Utils.MD5(newPwd);
            return MemberManage.UpdatePassword(userId,newPwd);
        }
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public static int UpdateAvatar(int userId, string avatar) {
            return MemberManage.UpdateAvatar(userId,avatar);
        }

        #region == 后台管理员操作 ==
        /// <summary>
        /// 管理员验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public static bool AdminMemberValidator(string userName, string userPwd) {
            return MemberManage.AdminMemberValidator(userName,userPwd);
        }
        /// <summary>
        /// 添加后台管理员
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool AddAdminMember(string userName) {
            return MemberManage.AddAdminMember(userName);
        }
        /// <summary>
        /// 删除后台管理员
        /// </summary>
        /// <param name="userName"></param>
        public static void DeleteAdminMember(string userName) {
            MemberManage.DeleteAdminMember(userName);
        }
         /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public static List<string> AdminMemberList() {
            return MemberManage.AdminMemberList();
        }
        /// <summary>
        /// 查询，右LIKE
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<string> AdminSearchList(string userName) {
            return MemberManage.AdminSearchList(userName);
        }
        #endregion
    }
}
