using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PlantEng.Services.Category;
using PlantEng.Models.Category;

namespace PlantEng.Web.PagesAdmin.Areas.Category.Controllers
{
    public class TechController : Controller
    {
        //
        // GET: /Category/Tech/

        public ActionResult List()
        {
            var list = TechService.List();
            ViewBag.List = list;
            int id = Controleng.Common.CECRequest.GetQueryInt("id",0);
            TechInfo techInfo = null;
            if(id>0){
                techInfo = TechService.GetById(id);
            }
            return View(techInfo);
        }
        [HttpPost]
        public ActionResult List(TechInfo model) {
            //检查
            //如果添加，判断别名是否存在
            bool isAdd = true;
            var list = TechService.List();
            if(model.Id>0){
                isAdd = false;
            }
            if (isAdd)
            {
                //判断是否别名存在
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    if (list.Where(m => m.Alias == model.Alias).Count() > 0)
                    {
                        ModelState.AddModelError("ALIASEXISTS", "别名存在，请选择其他别名");
                    }
                }
            }
            else { 
                //编辑，除了自身之外，判断是否存在
                if(!string.IsNullOrEmpty(model.Alias)){
                    if(list.Where(m=> (m.Alias == model.Alias && m.Id != model.Id) ).Count() >0){
                        ModelState.AddModelError("ALIASEXISTS", "别名存在，请选择其他别名");
                    }
                }
            }
            
            if(ModelState.IsValid){
                TechService.Create(model);
                ViewBag.Msg = "保存成功";
            }

            list = TechService.List();
            ViewBag.List = list;

            
            return View();
        }

    }
}
