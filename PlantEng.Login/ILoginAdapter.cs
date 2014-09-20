
namespace PlantEng.Login
{
    /// <summary>
    /// 登陆相关操作
    /// </summary>
    public interface ILoginAdapter
    {
        /// <summary>
        /// Cookie名称
        /// 只读
        /// </summary>
        string CookieName { get; }
        /// <summary>
        /// 加密KEY
        /// 只读
        /// </summary>
        string DESKey { get; }
        /// <summary>
        /// 验证COOKIE登录
        /// </summary>
        /// <returns></returns>
        bool IsClientLogin();
        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        int GetLoginedUserId();
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        LoginUserInfo GetLoginedUserInfo();
        /// <summary>
        /// 登出
        /// </summary>
        void LoginOut();

        /// <summary>
        /// 获得Cookie明文
        /// </summary>
        /// <returns></returns>
        string GetCookieValue(string encryptValue);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        void WriteLoginCookie(LoginUserInfo userInfo);
    }
}
