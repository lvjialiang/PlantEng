using System.Web.Mvc;
using System.Linq;

using PlantEng.Services.Company;
using PlantEng.Models.Company;
using Controleng.Common;
using PlantEng.Models;
using PlantEng.Services;
using PlantEng.Core;
using PlantEng.Mvc.Filters;
using System;
using System.Web;
using PlantEng.Common;

namespace PlantEng.Web.WWW.Areas.Accounts.Controllers
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CompanyController : Controller
    {
        #region == 产品列表 /accounts/company/productlist ==
        /// <summary>
        /// 产品列表 /accounts/company/productlist
        /// </summary>
        /// <returns></returns>
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ProductList()
        {
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if (CECRequest.GetQueryString("action").ToLower().Equals("delete"))
            {
                int id = CECRequest.GetQueryInt("id", 0);
                //如果是删除
                CompanyProductService.Delete(id, companyInfo.CompanyId);

            }
            var pageIndex = CECRequest.GetQueryInt("page", 1);

            var list = CompanyProductService.List(new CompanyProductSearchSetting
            {
                PageIndex = pageIndex,
                CompanyId = companyInfo.CompanyId
            });
            ViewBag.ProductList = list;
            
            return View();
        }
        #endregion

        #region == 添加或编辑产品 /accounts/company/productedit?id=123 ==
        /// <summary>
        /// 添加或编辑产品 /accounts/company/productedit?id=123
        /// </summary>
        /// <returns></returns>
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ProductEdit()
        {
            int id = CECRequest.GetQueryInt("id", 0);
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            var model = CompanyProductService.Get(id, companyInfo.CompanyId);
            //产品分类
            ViewBag.CategoryList = CompanyProductCategoryService.GetCategoryList(companyInfo.CompanyId);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ProductEdit(CompanyProductInfo oldModel, FormCollection fc)
        {
            bool errors = false;
            bool isAdd = oldModel.Id == 0;
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if (string.IsNullOrEmpty(oldModel.Title))
            {
                errors = true;
                ModelState.AddModelError("TitleEmpty", "标题不能为空");
            }
            if (string.IsNullOrEmpty(oldModel.Content))
            {
                errors = true;
                ModelState.AddModelError("ContentEmpty", "内容不能为空");
            }
            oldModel.SystemCategoryId = Utils.StrToInt(fc["syscat"],0);
            
            if(oldModel.SystemCategoryId == 0){
                errors = true;
                ModelState.AddModelError("SysCatEmpty", "请选择产品系统分类");
            }
            //为了在更新的时候，判断新分类是否还是以前的分类，然后为更新新分类产品数做判断
            int oldCatId = oldModel.CategoryId;
            
            oldModel.CategoryId = Utils.StrToInt(fc["cat"], 0);
            if(oldModel.CategoryId == 0){
                errors = true;
                ModelState.AddModelError("CatEmpty", "请选择自定义分类");
            }
            if (!errors && ModelState.IsValid)
            {
                oldModel.CompanyId = companyInfo.CompanyId;
                
                oldModel = CompanyProductService.Update(oldModel);
                if (isAdd)
                {
                    //如果是添加，则更新产品分类表中的产品数 + 1
                    CompanyProductCategoryService.UpdateProductCount(oldModel.CategoryId, companyInfo.CompanyId, true);
                }
                else { 
                    //如果没有更改分类，则什么都不变
                    //否则则原来的分类的产品数-1，新分类产品数+ 1
                    if (oldCatId == oldModel.CategoryId)
                    {
                        //说明还是在原来的分类上更新
                    }
                    else { 
                        //分类改变了
                        //原来的分类产品数 - 1 ，新分类数 + 1
                        CompanyProductCategoryService.UpdateProductCount(oldCatId, companyInfo.CompanyId, false);
                        //新分类 + 1
                        CompanyProductCategoryService.UpdateProductCount(oldModel.CategoryId, companyInfo.CompanyId, true);
                    }
                }

                
                ViewBag.Msg = "保存成功！";
            }
            //产品分类
            ViewBag.CategoryList = CompanyProductCategoryService.GetCategoryList(companyInfo.CompanyId);
            return View(oldModel);
        }
        #endregion

        #region == 新闻列表 /accounts/company/newslist ==
        /// <summary>
        /// 新闻列表首页 /accounts/company/newslist
        /// </summary>
        /// <returns></returns>
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult NewsList()
        {
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if (CECRequest.GetQueryString("action").ToLower().Equals("delete"))
            {
                int id = CECRequest.GetQueryInt("id", 0);
                //如果是删除
                CompanyNewsService.Delete(id, companyInfo.CompanyId);

            }
            var pageIndex = CECRequest.GetQueryInt("page", 1);

            var list = CompanyNewsService.List(new CompanyNewsSearchSetting
            {
                PageIndex = pageIndex,
                CompanyId = companyInfo.CompanyId
            });
            ViewBag.NewsList = list;
            return View();
        }
        #endregion

        #region == 添加或编辑企业新闻 /accounts/company/newsedit?id=23 ==
        /// <summary>
        /// 添加或编辑企业新闻 /accounts/company/newsedit?id=23
        /// </summary>
        /// <returns></returns>
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult NewsEdit()
        {
            int id = CECRequest.GetQueryInt("id", 0);
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            var model = CompanyNewsService.Get(id, companyInfo.CompanyId);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult NewsEdit(CompanyNewsInfo oldModel, FormCollection fc)
        {
            bool errors = false;
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if (string.IsNullOrEmpty(oldModel.Title))
            {
                errors = true;
                ModelState.AddModelError("TitleEmpty", "标题不能为空");
            }
            if (string.IsNullOrEmpty(oldModel.Content))
            {
                errors = true;
                ModelState.AddModelError("ContentEmpty", "内容不能为空");
            }
            if (!errors && ModelState.IsValid)
            {
                oldModel.CompanyId = companyInfo.CompanyId;

                //添加技术分类
                string requestTechIds = fc["techcat"] == null ? string.Empty : fc["techcat"];
                string[] strTechIds = requestTechIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int[] techIds = strTechIds.Select(i=>Utils.StrToInt(i,0)).ToArray<int>();
                oldModel.TechIds = techIds;

                CompanyNewsService.Update(oldModel);

                ViewBag.Msg = "保存成功！";
            }
            return View(oldModel);
        }
        #endregion

        #region == 技术与案例 /accounts/company/applicationlist ==
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ApplicationList() {
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if (CECRequest.GetQueryString("action").ToLower().Equals("delete"))
            {
                int id = CECRequest.GetQueryInt("id", 0);
                //如果是删除
                CompanyNewsService.Delete(id, companyInfo.CompanyId);

            }
            var pageIndex = CECRequest.GetQueryInt("page", 1);

            var list = CompanyNewsService.List(new CompanyNewsSearchSetting
            {
                PageIndex = pageIndex,
                CompanyId = companyInfo.CompanyId,
                Type = "application" //技术与案例
            });
            ViewBag.NewsList = list;
            return View();
        }
        #endregion

        #region == 添加或编辑技术与案例 /accounts/company/applicationedit?id=123 ==
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ApplicationEdit() {
            int id = CECRequest.GetQueryInt("id", 0);
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            var model = CompanyNewsService.Get(id, companyInfo.CompanyId);
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldModel"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ApplicationEdit(CompanyNewsInfo oldModel, FormCollection fc)
        {
            bool errors = false;
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if (string.IsNullOrEmpty(oldModel.Title))
            {
                errors = true;
                ModelState.AddModelError("TitleEmpty", "标题不能为空");
            }
            if (string.IsNullOrEmpty(oldModel.Content))
            {
                errors = true;
                ModelState.AddModelError("ContentEmpty", "内容不能为空");
            }
            if (!errors && ModelState.IsValid)
            {
                oldModel.CompanyId = companyInfo.CompanyId;
                oldModel.Type = "application";

                //添加技术分类
                string requestTechIds = fc["techcat"] == null ? string.Empty : fc["techcat"];
                string[] strTechIds = requestTechIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int[] techIds = strTechIds.Select(i => Utils.StrToInt(i, 0)).ToArray<int>();
                oldModel.TechIds = techIds;

                CompanyNewsService.Update(oldModel);
                ViewBag.Msg = "保存成功！";
            }
            return View(oldModel);
        }
        #endregion

        #region == 产品分类 /accounts/company/productcategorylist ==
        [ValidateInput(false)]
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ProductCategoryList() {
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            if(CECRequest.GetQueryString("action") == "delete"){
                //删除
                int _id = CECRequest.GetQueryInt("Id", 0);
                bool flag = CompanyProductCategoryService.Delete(_id,companyInfo.CompanyId);
                if(!flag){
                    //暂时没有实现
                    //return JavaScript("<script type=\"text/javascript\">alert('删除失败');</script>");
                }
                Response.Redirect("/accounts/company/productcategorylist");
                Response.End();
            }
            

            int id = CECRequest.GetQueryInt("Id",0);
            var model = CompanyProductCategoryService.Get(id,companyInfo.CompanyId);
            

            //产品分类
            ViewBag.CategoryList = CompanyProductCategoryService.GetCategoryList(companyInfo.CompanyId);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [PECAuth((int)MemberType.Enterprise)]
        public ActionResult ProductCategoryList(CompanyProductCategoryInfo oldModel) {
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            //保存
            bool errors = false;
            if (string.IsNullOrEmpty(oldModel.Name))
            {
                errors = true;
                ModelState.AddModelError("NAMEEMPTY", "名称不能为空");
            }
            if (!errors && ModelState.IsValid)
            {
                //OK
                oldModel.CompanyId = companyInfo.CompanyId;
                CompanyProductCategoryService.Update(oldModel);
                ViewBag.Msg = "保存成功！";
            }
            

            //产品分类
            ViewBag.CategoryList = CompanyProductCategoryService.GetCategoryList(companyInfo.CompanyId);
            return View(oldModel);
        }
        #endregion

        #region == 企业信息 /accounts/company/profile ==
        /// <summary>
        /// 企业信息
        /// </summary>
        /// <returns></returns>        
        [PECAuth]
        public ActionResult Profile() {
            
            var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);

            var catList = CompanyProductCategoryService.GetSystemCategoryList();
            ViewBag.CatList = catList;


            return View(companyInfo);
        }
        [ValidateInput(false)]
        [HttpPost]
        [PECAuth]
        public ActionResult Profile(CompanyInfo oldModel,FormCollection fc) {
            bool errors = false;
            //var companyInfo = MemberService.GetCompanyInfoByUserId(PlantEngContext.Current.UserId);
            //oldModel = companyInfo;

            //POST过来oldModel中CompanyId = 0
            //在这在设置一下

            var catList = CompanyProductCategoryService.GetSystemCategoryList();
            ViewBag.CatList = catList;

            var pc = fc["cbCat"];
            if (string.IsNullOrEmpty(pc))
            {
                errors = true;
                ModelState.AddModelError("PRODUCTCATERROR", "请选择公司产品");
            }
            else {
                oldModel.Categories = pc.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ii => Convert.ToInt32(ii)).ToList();
            }

            #region == 上传 LOGO BANNER ==
            //上传LOGO
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase logoFile = files["logofile"];
            int logoErrorCode = 0;
            string logo = FileUpload.UploadCompanyLogo(logoFile, out logoErrorCode);
            if(!string.IsNullOrEmpty(logo)){
                oldModel.CompanyLogo = logo;
            }
            if (logoErrorCode == 2)
            {
                errors = true;
                ModelState.AddModelError("FILEERROR","LOGO格式错误，请重试");
            }
            if (logoErrorCode == 3)
            {
                errors = true;
                ModelState.AddModelError("FILESIZEERROR","LOGO大小请在3M以内");
            }
            //上传Banner
            HttpPostedFileBase bannerFile = files["bannerfile"];
            int bannerErrorCode = 0;
            string banner = FileUpload.UploadCompanyBanner(bannerFile, out bannerErrorCode);
            if(!string.IsNullOrEmpty(banner)){
                oldModel.CompanyBanner = banner;
            }
            if (bannerErrorCode == 2)
            {
                errors = true;
                ModelState.AddModelError("BANNERFILEERROR", "BANNER格式错误，请重试");
            }
            if (bannerErrorCode == 3)
            {
                errors = true;
                ModelState.AddModelError("BANNERFILESIZEERROR", "BANNER大小请在3M以内");
            }
            #endregion

            if (!errors && ModelState.IsValid)
            {
                
                int i = MemberService.UpdateCompanyInfo(oldModel);
                if (i > 0)
                {
                    ViewBag.Msg = "更新成功";
                }
                else
                {
                    ModelState.AddModelError("ERROR", "更新失败，请重试");
                }
            }
            return View(oldModel);
        }
        #endregion

    }
}
