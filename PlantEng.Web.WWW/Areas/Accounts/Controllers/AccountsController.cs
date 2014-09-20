using System.Web.Mvc;

using PlantEng.Models;
using PlantEng.Services;
using System.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using PlantEng.Mvc.Filters;
using PlantEng.Core;
using PlantEng.Common;

namespace PlantEng.Web.WWW.Areas.Accounts.Controllers
{

    public class AccountsController : Controller
    {
        #region == 用户中心首页 ==
        /// <summary>
        /// 用户中心首页 /accounts/
        /// </summary>
        /// <returns></returns>
        [PECAuth]
        public ActionResult Index()
        {
            return Redirect("/accounts/edit");
            //var userInfo = MemberService.Get(PlantEngContext.Current.UserName);
            //return View(userInfo);
        }
        #endregion

        #region == 修改密码 ==
        /// <summary>
        /// 修改密码 /accounts/editpassword
        /// </summary>
        /// <returns></returns>
        [PECAuth]
        public ActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        [PECAuth]
        public ActionResult EditPassword(FormCollection fc)
        {
            string txtOldPwd = fc["txtOldPwd"];
            string txtNewPwd = fc["txtNewPwd"];
            string txtConfirmNewPwd = fc["txtConfirmNewPwd"];
            bool errors = false;
            if (string.IsNullOrEmpty(txtOldPwd))
            {
                errors = true;
                ModelState.AddModelError("OLDPWD", "原密码不能为空");
            }
            if (string.IsNullOrEmpty(txtNewPwd))
            {
                errors = true;
                ModelState.AddModelError("NEWPWD", "新密码不能为空");
            }
            if (txtNewPwd != txtConfirmNewPwd)
            {
                errors = true;
                ModelState.AddModelError("CONFIRMNEWPWD", "两次输入的新密码不一致");
            }
            if (!errors && ModelState.IsValid)
            {
                //判断旧密码是否正确
                var userInfo = MemberService.Get(PlantEngContext.Current.UserName);
                if (userInfo.Password != txtOldPwd)
                {
                    ModelState.AddModelError("OLDPWDERROR", "原密码不正确，请重试");
                }
                else
                {
                    //修改密码
                    MemberService.UpdatePassword(userInfo.Id, txtConfirmNewPwd);
                    ViewBag.Msg = "更新成功！";
                }
            }
            return View();
        }
        #endregion

        #region == 更新头像 ==
        /// <summary>
        /// 更新头像 /accounts/editavatar
        /// </summary>
        /// <returns></returns>
        [PECAuth]
        public ActionResult EditAvatar(FormCollection fc)
        {
            var userInfo = MemberService.Get(PlantEngContext.Current.UserName);
            var bigAvatarUrl = string.Format("{0}/avatar/", PlantEngSites.Current.Image);
            ViewBag.BigAvatar = string.Format("{0}l{1}?t={2}", bigAvatarUrl, userInfo.Avatar, DateTime.Now.Ticks);
            if(Request.HttpMethod.ToUpper() == "POST"){
                if (fc["type"] == "upload")
                {
                    #region == 上传图片 ==

                    //首先判断是否是上传图片
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase postedFile = files["picfile"];

                    int errorCode = 0;
                    //上传头像
                    string avatarName = FileUpload.UploadAvatar(postedFile, userInfo.Id, out errorCode);
                    switch (errorCode)
                    {
                        case 0:
                            {   //更新用户信息
                                MemberService.UpdateAvatar(userInfo.Id, avatarName);
                                //强制刷新
                                Response.Redirect("/accounts/editavatar");
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
                                ViewBag.Msg = "更新成功";
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
            }
            return View(userInfo);
        }
        #endregion

        #region == 编辑基本信息 ==
        /// <summary>
        /// 编辑基本信息 /accounts/edit
        /// </summary>
        /// <returns></returns>
        [PECAuth]
        public ActionResult Edit()
        {
            string userName = PlantEngContext.Current.UserName;
            return View(MemberService.Get(userName));
        }
        [HttpPost]
        [PECAuth]
        public ActionResult Edit(MemberInfo oldModel, FormCollection fc)
        {
            //检查Email是否重复
            bool errors = false;
            if (MemberService.EmailExists(oldModel.Email, oldModel.Id))
            {
                errors = true;
                ModelState.AddModelError("EMAILEXISTS", "Email已存在，请选择其他Email");
            }
            if (!errors && ModelState.IsValid)
            {
                oldModel.Province = fc["Province"];
                oldModel.City = fc["City"];
                oldModel.Industry = fc["Industry"] ?? string.Empty;
                oldModel.MagType = fc["MagType"];


                MemberService.Update(oldModel);

                ViewBag.Msg = "更新成功！";
            }
            return View(oldModel);
        }
        #endregion

        #region == 跳转我的空间 ==
        public ActionResult MySpace() {
            int loginUserId = PlantEngContext.Current.UserId;
            var companyUserInfo = MemberService.GetCompanyInfoByUserId(loginUserId);
            if (companyUserInfo.CompanyId > 0)
            {
                //说明注册的是企业用户
                return Redirect(string.Format("/company/{0}.html", companyUserInfo.CompanyId));
            }
            return Redirect(string.Format("/space/{0}",loginUserId));
        }
        #endregion

        #region == 申请赠阅杂志 ==
        public ActionResult ApplyMag() {
            return View();
        }
        #endregion
    }
}
