using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Controleng.Common;
using PlantEng.Models;
using PlantEng.Services;

namespace PlantEng.Web.WWW.Areas.Company.Controllers
{
    /// <summary>
    /// 企业库控制器
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /company
        public ActionResult Index()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);

            //已审核的企业信息
            var list = MemberService.CompanyList(new MemberSearchSetting() { 
                 MemberType = MemberType.Enterprise,
                 PageIndex = pageIndex,
                 CompanyStatus = CompanyStatus.Pass
            });
            ViewBag.List = list;

            return View();
        }

    }
}
