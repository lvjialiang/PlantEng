using System.Web.Mvc;

using PlantEng.Models.Advertisment;
using PlantEng.Models;
using PlantEng.Services.Advertisment;
using Controleng.Common;

namespace PlantEng.Web.PagesAdmin.Areas.Adm.Controllers
{
    public class SlotController : Controller
    {
        #region == List /adm/slot/list ==
        public ActionResult List()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            var list = AdPositionService.List(new AdSearchSetting() { 
                PageIndex = pageIndex
            });
            ViewBag.List = list;
            return View();
        }
        #endregion

        #region == Detail /adm/slot/detai?id=xx ==
        public ActionResult Detail() {
            return View();
        }
        #endregion

        #region == Create /adm/slot/create ==
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdPositionInfo model) {
            bool errors = false;
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("NAME", "广告名称不能为空");
                errors = true;
            }
            if(ModelState.IsValid && !errors){
                AdPositionService.Create(model);
                ViewBag.Msg = "添加成功！<a href=\"/adm/slot/list\">返回</a>";
            }
            return View();
        }
        
        #endregion

        #region == Modify /adm/slot/modify?id=xxx ==
        public ActionResult Modify()
        {
            int id = CECRequest.GetQueryInt("id", 0);
            var model = AdPositionService.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modify(AdPositionInfo model) {
            bool errors = false;
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("NAME", "广告名称不能为空");
                errors = true;
            }
            if (ModelState.IsValid && !errors)
            {
                AdPositionService.Create(model);
                ViewBag.Msg = "修改成功！<a href=\"/adm/slot/list\">返回</a>";
            }
            return View(model);
        }
        #endregion

    }
}
