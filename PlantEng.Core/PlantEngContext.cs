using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

using Controleng.Common;
using PlantEng.Models;
using PlantEng.Login;

namespace PlantEng.Core
{
    public class PlantEngContext
    {
        public LoginUserInfo LoginUserInfo { get; set; }
        public PlantEngContext() {
            ILoginAdapter login = new LoginAdapter();
            if(login.IsClientLogin()){
                LoginUserInfo = login.GetLoginedUserInfo();
            }
        }
        public static PlantEngContext Current
        {
            get { return new PlantEngContext(); }
        }
        
        public bool IsLogin
        {
            get {
                if (_GetCookie().Length == 6) { return true; }
                return false; }
        }
        public int RoleId
        {
            get {
                if(IsLogin){
                    int roleId = Controleng.Common.Utils.StrToInt(_GetCookie()[4], 0);
                    return roleId;
                }
                return 0 ;
            }
        }
        public string UserName {
            get {
                if (IsLogin) { return _GetCookie()[1]; }
                return string.Empty; }
        }
        public string Email {
            get {
                if (IsLogin) { return _GetCookie()[3]; }
                return string.Empty;
            }
        }
        public int UserId {
            get {
                if (IsLogin) {
                    return Controleng.Common.Utils.StrToInt(_GetCookie()[0],0); }
                return 0;
            }
        }
        /// <summary>
        /// 获得用户空间spaceid
        /// /space/【123】/xxx
        /// </summary>
        public int ClientSpaceId {
            get {
                Regex g = new Regex(@"/space/(\d+)", RegexOptions.IgnoreCase);
                Match m = g.Match(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
                if (m.Success)
                {
                    int spaceId = Utils.StrToInt(m.Groups[1].Value, 0);
                    return spaceId;
                }
                return 0;
            }
        }
        private string[] _GetCookie() {
            string[] s = { };
            string cookieKey = System.Configuration.ConfigurationManager.AppSettings["LoginCookieKey"];
            string desKey  = System.Configuration.ConfigurationManager.AppSettings["DESKey"];
            if (System.Web.HttpContext.Current.Request.Cookies[cookieKey] != null)
            {
                HttpCookie aCookie = System.Web.HttpContext.Current.Request.Cookies[cookieKey];
                if (!string.IsNullOrEmpty(aCookie.Value))
                {
                    try
                    {
                        string cookieValue = Goodspeed.Library.Security.DESCryptography.Decrypt(aCookie.Value, desKey);
                        return cookieValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    }catch{                        
                        return s;
                    }
                }
            }
            return s;
        }
    }
}
