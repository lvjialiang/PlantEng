using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using PlantEng.Models;
using PlantEng.Services;
using System.Text.RegularExpressions;
using PlantEng.Core;
using System.Drawing;
using System.Drawing.Imaging;
using PlantEng.Common;
using PlantEng.Login;
using PlantEng.Services.Company;

namespace PlantEng.Web.WWW.Controllers
{
    public class PassportController : Controller
    {
        #region == 注册个人用户 ==
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MemberInfo oldModel, FormCollection fc)
        {
            #region == 验证合法性 ==
            bool errors = false;
            if (string.IsNullOrEmpty(oldModel.UserName))
            {
                ModelState.AddModelError("USERNAME", "用户名不能为空");
                errors = true;
            }
            if (string.IsNullOrEmpty(oldModel.Email))
            {
                ModelState.AddModelError("EMAIL", "Email不能为空");
                errors = true;
            }
            if (string.IsNullOrEmpty(oldModel.Password) || string.IsNullOrEmpty(fc["confirmpassword"]))
            {
                ModelState.AddModelError("PASSWORD", "密码不能为空");
                errors = true;
            }
            if (oldModel.Password != fc["confirmpassword"])
            {
                ModelState.AddModelError("PASSWORD2", "两次密码不一致，请重新输入");
                errors = true;
            }
            if (string.IsNullOrEmpty(oldModel.RealName))
            {
                ModelState.AddModelError("REALNAME", "真实姓名不能为空");
                errors = true;
            }
            //判断用户名是否存在
            if (!string.IsNullOrEmpty(oldModel.UserName))
            {
                //检查用户是否在黑名单中
                if (CheckBlockUser(oldModel.UserName) || MemberService.UserNameExists(oldModel.UserName))
                {
                    ModelState.AddModelError("USERNAMEEXISTS", "用户名已经存在，请选择其他用户名");
                    errors = true;
                }
            }
            //判断Email是否存在
            if (!string.IsNullOrEmpty(oldModel.Email))
            {
                if (MemberService.EmailExists(oldModel.Email))
                {
                    ModelState.AddModelError("EMAILEXISTS", "Email已经存在，请选择其他Email");
                    errors = true;
                }
            }
            #endregion

            if (!errors && ModelState.IsValid)
            {
                //行业
                oldModel.Industry = fc["Industry"];
                oldModel.Province = fc["Province"];
                oldModel.City = fc["City"];
                oldModel.Position = fc["Position"];
                oldModel.MagType = fc["MagType"];
                oldModel.Type = MemberType.Common;      //一般用户
                oldModel.Id = MemberService.Insert(oldModel);

                //登录
                _Login(oldModel);

                Response.Redirect("/registersuccess");
            }

            return View();
        }
        #endregion

        #region == 申请企业用户 ==
        public ActionResult RegisterCompany()
        {
            var catList = CompanyProductCategoryService.GetSystemCategoryList();
            ViewBag.CatList = catList;

            return View();
        }
        [HttpPost]
        public ActionResult RegisterCompany(CompanyInfo oldModel, FormCollection fc)
        {

            var catList = CompanyProductCategoryService.GetSystemCategoryList();
            ViewBag.CatList = catList;

            #region == 验证合法性 ==
            bool errors = false;
            if (string.IsNullOrEmpty(oldModel.UserName))
            {
                ModelState.AddModelError("USERNAME", "用户名不能为空");
                errors = true;
            }
            if (string.IsNullOrEmpty(oldModel.Email))
            {
                ModelState.AddModelError("EMAIL", "Email不能为空");
                errors = true;
            }
            if (string.IsNullOrEmpty(oldModel.Password) || string.IsNullOrEmpty(fc["confirmpassword"]))
            {
                ModelState.AddModelError("PASSWORD", "密码不能为空");
                errors = true;
            }
            if (oldModel.Password != fc["confirmpassword"])
            {
                ModelState.AddModelError("PASSWORD2", "两次密码不一致，请重新输入");
                errors = true;
            }
            if (string.IsNullOrEmpty(oldModel.RealName))
            {
                ModelState.AddModelError("REALNAME", "真实姓名不能为空");
                errors = true;
            }
            //判断用户名是否存在
            if (!string.IsNullOrEmpty(oldModel.UserName))
            {
                if (CheckBlockUser(oldModel.UserName) || MemberService.UserNameExists(oldModel.UserName))
                {
                    ModelState.AddModelError("USERNAMEEXISTS", "用户名已经存在，请选择其他用户名");
                    errors = true;
                }
            }
            //判断Email是否存在
            if (!string.IsNullOrEmpty(oldModel.Email))
            {
                if (MemberService.EmailExists(oldModel.Email))
                {
                    ModelState.AddModelError("EMAILEXISTS", "Email已经存在，请选择其他Email");
                    errors = true;
                }
            }

            //判断公司产品类别是否选择
            var pc = fc["cbCat"];

            
            if (string.IsNullOrEmpty(pc))
            {
                ModelState.AddModelError("PRODUCTCATERROR", "请选择公司产品");
                errors = true;
            }
            else { 
                oldModel.Categories = pc.Split(new char []{ ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt32(i)).ToList();
            }

            #endregion

            if (!errors && ModelState.IsValid)
            {
                //行业
                oldModel.Industry = fc["Industry"];
                oldModel.Province = fc["Province"];
                oldModel.City = fc["City"];
                oldModel.Position = fc["Position"];

                //设置为企业用户类型
                oldModel.Type = MemberType.Enterprise;
                oldModel.UserId = oldModel.Id = MemberService.Insert(oldModel);
                if (oldModel.UserId > 0)
                {
                    //上传LOGO
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase postedFile = files["picfile"];
                    int errorCode = 0;
                    oldModel.CompanyLogo = FileUpload.UploadCompanyLogo(postedFile, out errorCode);

                    //插入企业用户信息
                    oldModel.CompanyId = MemberService.InsertCompany(oldModel);

                    //登录
                    _Login(oldModel);

                    Response.Redirect("/accounts/");
                }
                else
                {
                    ModelState.AddModelError("FINALERROR", "注册失败，请重试。");
                }
            }
            return View();
        }
        #endregion

        #region == 注册成功 ==
        public ActionResult RegisterSuccess()
        {
            if (!PlantEngContext.Current.IsLogin) {
                return Redirect("/login");
            }
            var userInfo = MemberService.Get(PlantEngContext.Current.UserName);
            var bigAvatarUrl = string.Format("{0}/avatar/",PlantEngSites.Current.Image);
            ViewBag.BigAvatar = string.Format("{0}l{1}?t={2}", bigAvatarUrl, userInfo.Avatar, DateTime.Now.Ticks);
            return View(userInfo);
        }
        [HttpPost]
        public ActionResult RegisterSuccess(FormCollection fc) {
            var userInfo = MemberService.Get(PlantEngContext.Current.UserName);
            var bigAvatarUrl = string.Format("{0}/avatar/", PlantEngSites.Current.Image);

            if (fc["type"] == "upload")
            {
                #region == 上传图片 ==

                //首先判断是否是上传图片
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase postedFile = files["picfile"];

                int errorCode = 0;
                //上传头像
                string avatarName = FileUpload.UploadAvatar(postedFile,userInfo.Id,out errorCode);
                switch(errorCode){
                    case 0:
                        {   //更新用户信息
                            MemberService.UpdateAvatar(userInfo.Id, avatarName);
                            //强制刷新
                            Response.Redirect("/registersuccess");
                            Response.End();
                        }
                        break;
                    case 1:
                        ModelState.AddModelError("NOFILE", "没有选择图片");
                        break;
                    case 2:
                        ModelState.AddModelError("EXISTENSIONERROR", "上传格式不正确");
                        break;
                    case 3:
                        ModelState.AddModelError("SIZEERROR", "图片大小不能超过1M");
                        break;
                }
                #endregion
            }
            if (fc["type"] == "update")
            {
                #region == 自动缩放图片 ==
                //说明是选择自动缩放
                try
                {
                    string imgpos = fc["imgpos"];
                    string[] strImgPost = imgpos.Split('_');
                    int x = Convert.ToInt32(strImgPost[0]);         //x轴起点坐标
                    int y = Convert.ToInt32(strImgPost[1]);         //y轴起点坐标
                    int iWidth = Convert.ToInt32(strImgPost[2]);    //x轴终点坐标
                    int iHight = Convert.ToInt32(strImgPost[2]);    //y轴终点坐标

                    int errorCode = 0;
                    //裁剪图片
                    string avatarName = FileUpload.ResizeAvatar(userInfo.Avatar, x, y, iWidth, iHight, out errorCode);
                    switch (errorCode)
                    {
                        case 0:
                            ViewBag.Msg = "头像更新成功，<a href=\"/accounts/\">进入我的管理中心</a>";
                            break;
                        case 1:
                            ModelState.AddModelError("ERROR", "出现未知错误，请重试。");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
            }
            ViewBag.BigAvatar = string.Format("{0}l{1}?t={2}", bigAvatarUrl, userInfo.Avatar, DateTime.Now.Ticks);
            return View(userInfo);
        }
        #endregion

        #region == 登录 ==
        public ActionResult Login()
        {
            if (Request.HttpMethod.Equals("POST"))
            {
                string userName = Request.Form["txtUserName"];
                string password = Request.Form["txtPassword"];
                bool errors = false;
                if (string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("USERNAME", "用户名不能为空");
                    errors = true;
                }
                if (string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("PASSWORD", "密码不能为空");
                    errors = true;
                }
                if (!errors && ModelState.IsValid)
                {
                    var userInfo = MemberService.Get(userName);
                    if (userInfo.Id == 0)
                    {
                        ModelState.AddModelError("USERNOTEXISTS", "用户不存在");
                    }
                    else
                    {
                        //取消密码MD5
                        if (userInfo.Password == password)
                        {
                            //登录
                            _Login(userInfo);

                            string returnUrl = Controleng.Common.CECRequest.GetQueryString("returnurl");
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                Response.Redirect(returnUrl);
                                Response.End();
                            }

                            //返回到首页
                            Response.Redirect("/");
                            Response.End();
                        }
                        else
                        {
                            ModelState.AddModelError("PWDERROR", "密码错误，请重试");
                        }
                    }
                }
            }
            return View();
        }
        #endregion

        [NonAction]
        private void _Login(MemberInfo userInfo) {
            ILoginAdapter login = new LoginAdapter();
            login.WriteLoginCookie(new LoginUserInfo() { 
                Email  = userInfo.Email,
                UserId = userInfo.Id,
                RoleId = (int)userInfo.Type,
                UserName = userInfo.UserName,
                Password = userInfo.Password
            });
        }

        #region == 注销 ==
        public void Logout()
        {
            ILoginAdapter login = new LoginAdapter();
            login.LoginOut();

            Response.Redirect("/");
            Response.End();
        }
        #endregion

        #region == Ajax ==
        public ActionResult UserCheck()
        {
            //Jquery validate 判断的是不存在
            bool notExists = true;
            string action = Controleng.Common.CECRequest.GetQueryString("action").ToLower();
            if (action.Equals("username"))
            {
                string username = Controleng.Common.CECRequest.GetQueryString("username");
                //检查是否在黑名单中
                notExists = !CheckBlockUser(username);
                if (notExists)
                {
                    notExists = !MemberService.UserNameExists(username);
                }
            }
            if (action.Equals("email"))
            {
                string email = Controleng.Common.CECRequest.GetQueryString("email");
                notExists = !MemberService.EmailExists(email);
            }
            return Content((notExists).ToString().ToLower());
        }
        #endregion

        #region == 检查用户名是否在黑名单中 ==
        /// <summary>
        /// 检查用户名是否在黑名单中
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>存在：true,不存在:false</returns>
        private bool CheckBlockUser(string userName)
        {
            bool isExists = false;
            List<string> list = new List<string>();
            list.Add("admin(.*)");
            list.Add("plant(.*)");
            foreach (var item in list)
            {
                if (Regex.IsMatch(userName, item, RegexOptions.IgnoreCase))
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }
        #endregion

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword() {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(FormCollection fc) {
            string userName = fc["username"];
            string email = fc["email"];
            bool error = false;
            if(string.IsNullOrEmpty(userName)){
                error = true;
                ModelState.AddModelError("UserName","用户名不能为空");
            }
            if(string.IsNullOrEmpty(email)){
                error = true;
                ModelState.AddModelError("Email","Email不能为空");
            }
            if(!error && ModelState.IsValid){
                //开始找回
                var userInfo = MemberService.Get(userName);
                if (userInfo.Email == email)
                {
                    //获得密码
                    ViewBag.Password = userInfo.Password;
                }
                else {
                    ModelState.AddModelError("Error", "失败，请重试！");
                }
            }
            return View();
        }
    }
}
