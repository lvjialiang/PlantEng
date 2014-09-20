using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Controleng.Common;
using PlantEng.Models;
using PlantEng.Services.Company;
using PlantEng.Services;

namespace PlantEng.Web.WWW.Areas.Product.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Product/

        #region == 首页 /product/ ==
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            int catId = CECRequest.GetQueryInt("catId",0);
            var list = CompanyProductService.List(new CompanyProductSearchSetting() { 
                 PageIndex = pageIndex,
                 SystemCategoryId = catId
            });
            ViewBag.List = list;

            var catList = CompanyProductCategoryService.GetSystemCategoryList();
            ViewBag.CatList = catList;

            return View();
        }
        #endregion

        #region == 产品详细页 /product/detail?id=xxx ==
        public ActionResult Detail() {
            int productId = CECRequest.GetQueryInt("id",0);

            //根据产品ID获得公司信息
            var productInfo = CompanyProductService.Get(productId);
            if (productInfo.Id == 0)
            {
                return Redirect("/");
            }

            //模板所用公司信息
            var companyInfo = MemberService.GetCompanyInfo(productInfo.CompanyId);
            ViewBag.CompanyInfo = companyInfo;

            return View("ProductDetail",productInfo);
        }
        #endregion

    }
}
