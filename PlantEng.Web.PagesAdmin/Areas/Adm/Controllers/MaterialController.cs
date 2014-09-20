using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Controleng.Common;
using PlantEng.Models.Advertisment;
using PlantEng.Models;
using PlantEng.Services.Advertisment;

namespace PlantEng.Web.PagesAdmin.Areas.Adm.Controllers
{
    public class MaterialController : Controller
    {
        //
        // GET: /Adm/Material/List

        public ActionResult List()
        {
            var pageIndex = CECRequest.GetQueryInt("page",1);
            var list = AdService.List(new AdSearchSetting() { 
                PageIndex = pageIndex
            });
            ViewBag.List = list;

            return View();
        }

        #region == Create /adm/material/create ==
        public ActionResult Create() {
            return View(new AdInfo());
        }
        [HttpPost]
        public ActionResult Create(AdInfo model) {
            Update(model);
            return View(model);
        }
        #endregion

        #region == 公用更新函数 ==
        private void Update(AdInfo model) { 
            var errors = false;
            if(string.IsNullOrEmpty(model.Name)){
                ModelState.AddModelError("Name","广告物料不能为空");
                errors = true;
            }
            if(string.IsNullOrEmpty(model.ClickUrl)){

                ModelState.AddModelError("ClickUrl","点击链接不能为空");
                errors = true;
            }
            if(ModelState.IsValid && !errors){
                AdService.Create(model);
                ViewBag.Msg = "保存成功!";
            }
        }
        #endregion

        #region == Modify /adm/material/modify?id=xx ==
        public ActionResult Modify() {
            int adId = CECRequest.GetQueryInt("Id",0);
            var model = AdService.Get(adId);
            return View("Create",model);
        }
        [HttpPost]
        public ActionResult Modify(AdInfo model) {
            Update(model);
            return View("Create",model);
        }
        #endregion
    }
}
