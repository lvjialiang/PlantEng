using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Controleng.Common;
using PlantEng.Models;
using PlantEng.Services.Video;

namespace PlantEng.Web.WWW.Areas.Video.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Video/Home/

        public ActionResult Index()
        {
            int catId = CECRequest.GetQueryInt("catId", 0);
            int pageIndex = CECRequest.GetQueryInt("page", 1);
            var list = VideoService.List(new VideoSearchSetting()
            {
                PageIndex = pageIndex,
                CategoryId = catId
            });
            ViewBag.List = list;
            return View();
        }
        /// <summary>
        /// 视频列表 /video/list
        /// </summary>
        /// <returns></returns>
        public ActionResult List() {
            int catId = CECRequest.GetQueryInt("catId", 0);
            int pageIndex = CECRequest.GetQueryInt("page",1);
            var list = VideoService.List(new VideoSearchSetting() { 
                PageIndex = pageIndex,
                CategoryId = catId
            });
            ViewBag.List = list;
            return View();
        }
        /// <summary>
        /// 显示视频 /video/watch?id=xxx
        /// </summary>
        /// <returns></returns>
        public ActionResult Watch() {
            int videoId = CECRequest.GetQueryInt("id",0);

            var model = VideoService.GetById(videoId);
            if(model.Id == 0 || model.IsDeleted){
                return Content("参数错误。");
            }

            //更新浏览次数
            VideoService.UpdateViewCount(videoId);

            return View(model);
        }
    }
}
