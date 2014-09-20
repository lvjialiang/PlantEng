using System;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace PlantEng.Login
{
    /// <summary>
    /// 登陆相关操作
    /// </summary>
    public class LoginAdapter : ILoginAdapter
    {
        /// <summary>
        /// 获得Cookie名
        /// </summary>
        public string CookieName
        {
            get {
                return System.Configuration.ConfigurationManager.AppSettings["LoginCookieKey"];
            }
        }
        public string DESKey
        {
            get {
                return System.Configuration.ConfigurationManager.AppSettings["DESKey"];
            }
        }
        #region == IsClientLogin ==
        /// <summary>
        /// 验证COOKIE登录
        /// </summary>
        /// <returns></returns>
        public bool IsClientLogin()
        {
            LoginUserInfo userinfo = GetLoginedUserInfo();
            return userinfo.UserId > 0;
        }
        #endregion

        #region == GetLoginedUserId ==
        public int GetLoginedUserId()
        {
            LoginUserInfo userinfo = GetLoginedUserInfo();
            return userinfo.UserId;
        }
        #endregion

        #region == GetLoginedUserInfo ==
        public LoginUserInfo GetLoginedUserInfo()
        {
            LoginUserInfo userInfo = new LoginUserInfo();
            try
            {
                var cookieInfo = _GetCookie();
                if (cookieInfo.Length == 6)
                {
                    userInfo.UserId = Controleng.Common.Utils.StrToInt(cookieInfo[0], 0);
                    userInfo.UserName = cookieInfo[1];
                    userInfo.RoleId = Controleng.Common.Utils.StrToInt(cookieInfo[4], 0);
                    userInfo.Password = cookieInfo[2];
                    userInfo.Email = cookieInfo[3];
                }
            }
            catch
            {
                return userInfo;
            }
            return userInfo;
        }
        #endregion

        #region = Encrypt =
        /// <summary>
        /// 加密 DES
        /// </summary>
        /// <param name="pToEncrypt">需要解密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中
            //原来使用的UTF8编码，我改成Unicode编码了，不行
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量
            //使得输入密码必须输入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        #endregion

        #region = Decrypt =
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">需要解密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }
        #endregion

        #region == 获得Cookie信息 ==
        private string[] _GetCookie()
        {
            string[] s = { };
            if (System.Web.HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                HttpCookie aCookie = System.Web.HttpContext.Current.Request.Cookies[CookieName];
                if (!string.IsNullOrEmpty(aCookie.Value))
                {
                    try
                    {
                        string cookieValue = GetCookieValue(aCookie.Value);
                        return cookieValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    catch
                    {
                        return s;
                    }
                }
            }
            return s;
        }
        #endregion

        public string GetCookieValue(string encryptValue)
        {
            try
            {
                return Decrypt(encryptValue, DESKey);
            }
            catch { }
            return string.Empty;
        }

        #region == 注销 ==
        /// <summary>
        /// 登出
        /// </summary>
        public void LoginOut()
        {
            string domain = System.Configuration.ConfigurationManager.AppSettings["LoginCookieDomain"];
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(CookieName) { Expires = DateTime.Now.AddYears(-1), Domain = domain });

            //清除BBS的Cookie
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("dnt") { Expires = DateTime.Now.AddYears(-1), Domain = domain });
        }
        #endregion

        #region == 登录 ==
        public void WriteLoginCookie(LoginUserInfo userInfo)
        {
            //正确，开始写Cookie                            
            //Cookie格式Id|UserName|Password|Email|Type|DateTime.Now
            string cookieValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                userInfo.UserId,
                userInfo.UserName,
                userInfo.Password,
                userInfo.Email,
                userInfo.RoleId,
                DateTime.Now);
            //过期时间
            int expire = Controleng.Common.Utils.StrToInt
(System.Configuration.ConfigurationManager.AppSettings["LoginCookieExpires"], 60);
            //Domain
            string domain = System.Configuration.ConfigurationManager.AppSettings["LoginCookieDomain"];


            //添加到浏览器中
            //Cookie加密
            string cv = Encrypt(cookieValue,DESKey);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(CookieName, cv) { Expires = DateTime.Now.AddMinutes(expire), Domain = domain });
        }
        #endregion
    }
}
