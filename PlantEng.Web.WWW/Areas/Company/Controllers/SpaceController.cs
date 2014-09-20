using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantEng.Mvc.Filters;
using PlantEng.Models;
using PlantEng.Models.Company;
using PlantEng.Services.Company;
using Controleng.Common;
using System.Text;

namespace PlantEng.Web.WWW.Areas.Company.Controllers
{
    [CompanySpaceLayout]
    public class SpaceController : Controller
    {
        //
        // GET: /Company/Space/
        
        public ActionResult Index(int companyId)
        {
            var companyInfo = ViewBag.CompanyInfo;
            //产品
            ViewBag.ProductList = CompanyProductService.ListWithoutPage(5,companyInfo.CompanyId);

            ViewBag.NewsList = CompanyNewsService.ListWithoutPageForFront(10,companyInfo.CompanyId);

            //最新反馈
            var frontFeedbackList = CompanyFeedbackService.FrontList(companyInfo.CompanyId, 1, 10);
            ViewBag.FrontFeedbackList = frontFeedbackList;

            return View();
        }

        #region == 产品显示页 ==
        //public ActionResult ProductDetail(int productId) {
        //    var companyInfo = ViewBag.CompanyInfo;
        //    var productInfo = CompanyProductService.Get(productId,companyInfo.CompanyId);
        //    return View(productInfo);
        //}
        #endregion

        #region == 产品列表 ==
        public ActionResult ProductList(int? pcid) {
            int _pcid = pcid == null ? 0 : Utils.StrToInt(pcid,0);
            var companyInfo = ViewBag.CompanyInfo;
            int pageIndex = CECRequest.GetQueryInt("page",1);
            var list = CompanyProductService.List(new CompanyProductSearchSetting() { 
                PageIndex = pageIndex,
                CompanyId = companyInfo.CompanyId,
                CategoryId = _pcid
            });
            ViewBag.ProductList = list;
            return View();
        }
        #endregion

        #region == 技术案例列表 ==
        public ActionResult ApplicationList() {
            var companyInfo = ViewBag.CompanyInfo;
            int pageIndex = CECRequest.GetQueryInt("page", 1);
            var newsList = CompanyNewsService.List(new CompanyNewsSearchSetting()
            {
                PageIndex = pageIndex,
                CompanyId = companyInfo.CompanyId,
                Type = "application"
            });
            ViewBag.NewsList = newsList;
            return View();
        }
        #endregion

        #region == 最新反馈 ==
        public ActionResult Feedback() {
            var companyInfo = ViewBag.CompanyInfo;
            int pageIndex = CECRequest.GetQueryInt("page",1);
            var frontFeedbackList = CompanyFeedbackService.FrontList(companyInfo.CompanyId,pageIndex,10);
            ViewBag.FrontFeedbackList = frontFeedbackList;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Feedback(FormCollection fc) {
            var companyInfo = ViewBag.CompanyInfo;
            string cbType = fc["cbType"];
            string txtNeed = fc["txtNeed"];
            string txtRealName = fc["txtRealName"];
            string txtCompanyName = fc["txtCompanyName"];
            string txtPhone = fc["txtPhone"];
            string txtEmail = fc["txtEmail"];

            bool errors = false;
            if(string.IsNullOrEmpty(cbType)){
                errors = true;
                ModelState.AddModelError("TYPE","请选择您的需求");
            }
            if(string.IsNullOrEmpty(txtNeed)){
                errors = true;
                ModelState.AddModelError("NEED","请输入您的详细需求");
            }
            if(string.IsNullOrEmpty(txtRealName)){
                errors = true;
                ModelState.AddModelError("REALNAME", "请输入您的姓名");
            }
            if (string.IsNullOrEmpty(txtCompanyName))
            {
                errors = true;
                ModelState.AddModelError("COMPANYNAME", "请输入您的公司");
            }
            if (string.IsNullOrEmpty(txtPhone))
            {
                errors = true;
                ModelState.AddModelError("PHONE", "请输入您的电话");
            }
            if (string.IsNullOrEmpty(txtEmail))
            {
                errors = true;
                ModelState.AddModelError("EMAIL", "请输入您的邮件");
            }
            if(!errors && ModelState.IsValid){
                
                //添加反馈
                CompanyFeedbackService.Insert(new CompanyFeedbackInfo() { 
                    CompanyName = txtCompanyName,
                    RealName = txtRealName,
                    Type = cbType,
                    Content = txtNeed,
                    Email = txtEmail,
                    ForCompanyId = companyInfo.CompanyId,
                    UserId = 0,
                    Phone = txtPhone,
                    IP = CECRequest.GetIP()
                });

                ViewBag.Msg = "Success";
            }

            var frontFeedbackList = CompanyFeedbackService.FrontList(companyInfo.CompanyId, 1, 10);
            ViewBag.FrontFeedbackList = frontFeedbackList;
            return View();
        }
        #endregion

        #region == 新闻列表页 ==
        public ActionResult NewsList() {
            var companyInfo = ViewBag.CompanyInfo;
            int pageIndex = CECRequest.GetQueryInt("page",1);
            var newsList = CompanyNewsService.List(new CompanyNewsSearchSetting() { 
                PageIndex = pageIndex,
                CompanyId = companyInfo.CompanyId
            });
            ViewBag.NewsList = newsList;
            return View();
        }
        #endregion

        #region == 新闻显示页 ==
        public ActionResult NewsDetail() {
            int newsId = CECRequest.GetQueryInt("id",0);
            var companyInfo = ViewBag.CompanyInfo;
            var newsInfo = CompanyNewsService.Get(newsId,companyInfo.CompanyId);
            
            return View(newsInfo);
        }
        #endregion

        #region == 公司介绍 ==
        public ActionResult Introduction() {
            return View();
        }
        #endregion

        #region == 联系方式==
        public ActionResult Contact() {
            return View();
        }
        #endregion



    }
}
