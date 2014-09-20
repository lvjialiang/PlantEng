using System.Web.Mvc;

using PlantEng.Services.Category;
using PlantEng.Models.Category;

namespace PlantEng.Web.PagesAdmin.Areas.Category.Controllers
{
    public class VideoCatController : Controller
    {
        public ActionResult List()
        {
            var list = VideoCatService.List();
            ViewBag.List = list;
            int id = Controleng.Common.CECRequest.GetQueryInt("id", 0);
            VideoCatInfo model = null;
            if (id > 0)
            {
                model = VideoCatService.GetById(id);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult List(VideoCatInfo model)
        {
            var list = VideoCatService.List();
            if (ModelState.IsValid)
            {
                VideoCatService.Create(model);
                ViewBag.Msg = "保存成功";
            }

            list = VideoCatService.List();
            ViewBag.List = list;


            return View();
        }

    }
}
