using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantEng.Services;
using System.Web.Security;

namespace PlantEng.Web.PagesAdmin.Areas.Frame.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Frame/Frame/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Main() {
            return View();
        }
        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc) {
            string userName = fc["txtUserName"];
            string userPwd = fc["txtUserPwd"];
            bool errors = false;
            if(string.IsNullOrEmpty(userName)){
                ModelState.AddModelError("UserName","用户名不能为空");
                errors = true;
            }
            if(string.IsNullOrEmpty(userPwd)){
                ModelState.AddModelError("UserPwd","密码不能为空");
                errors = true;
            }
            if(ModelState.IsValid && !errors){
                //判断用户名和密码是否正确

                bool isLogin = MemberService.AdminMemberValidator(userName,userPwd);
                if(isLogin){

                    //写Cookie
                    DateTime cookieExpires = DateTime.Now.Add(FormsAuthentication.Timeout);
                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, cookieExpires, false, "", FormsAuthentication.FormsCookiePath);
                    //加密内容
                    string HashTicket = FormsAuthentication.Encrypt(Ticket);
                    //添加Cookie
                    HttpCookie lcookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                    lcookie.Expires = cookieExpires;
                    lcookie.Domain = FormsAuthentication.CookieDomain;

                    Response.Cookies.Add(lcookie);
                    Response.Cookies[FormsAuthentication.FormsCookieName].Expires = cookieExpires;

                    return Redirect("/");
                }
                ModelState.AddModelError("Error","用户名或密码错误！");
                
            }
            return View();
        }
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

    }
}
