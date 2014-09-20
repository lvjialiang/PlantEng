using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Controleng.Common;
using PlantEng.Models;
using PlantEng.Services.Advertisment;
using PlantEng.Models.Advertisment;

namespace PlantEng.Web.PagesAdmin.Areas.Adm.Controllers
{
    public class DeliveryController : Controller
    {
        //
        // GET: /Adm/Delivery/

        #region == List /adm/delivery/list ==
        public ActionResult List()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            int slotId = CECRequest.GetQueryInt("slotId",0);
            var list = AdDeliveryService.List(new AdSearchSetting() { 
                AdPositionId = slotId,
                PageIndex = pageIndex
            });
            ViewBag.List = list;
            return View();
        }
        #endregion

        #region == Create /adm/delivery/create?slotId=xxx ==

        public ActionResult Create()
        {
            int slotId = CECRequest.GetQueryInt("slotId",0);
            var adPositionModel = AdPositionService.Get(slotId);
            ViewBag.AdPositionModel = adPositionModel;

            return View(new AdDeliveryInfo());
        }
        [HttpPost]
        public ActionResult Create(AdDeliveryInfo model) {
            Update(model);
            return View(model);
        }
        #endregion

        #region == Modify /adm/delivery/modify?id=xx ==
        public ActionResult Modify() {
            int deliveryId = CECRequest.GetQueryInt("Id", 0);
            var model = AdDeliveryService.GetById(deliveryId);

            var adPositionModel = AdPositionService.Get(model.AdPositionId);
            ViewBag.AdPositionModel = adPositionModel;

            return View("Create",model);   
        }
        [HttpPost]
        public ActionResult Modify(AdDeliveryInfo model) {
            Update(model);

            return View("Create",model);
        }
        #endregion

        private void Update(AdDeliveryInfo model) {
            bool errors = false;
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "广告名称不能为空");
                errors = true;
            }
            if(model.AdPositionId ==0){
                ModelState.AddModelError("AdPositionId", "请选择目标广告位");
                errors = true;
            }
            if (ModelState.IsValid && !errors)
            {
                AdDeliveryService.Create(model);

                ViewBag.Msg = "保存成功！<a href=\"list\">返回</a>";
            }
            var adPositionModel = AdPositionService.Get(model.AdPositionId);
            ViewBag.AdPositionModel = adPositionModel;
        }

    }
}
