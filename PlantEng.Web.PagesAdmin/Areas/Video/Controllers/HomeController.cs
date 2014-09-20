using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controleng.Common;
using PlantEng.Services.Video;
using PlantEng.Models;
using PlantEng.Services.Category;
using PlantEng.Models.Video;

namespace PlantEng.Web.PagesAdmin.Areas.Video.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Video/Home/
        /// <summary>
        /// 视频列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            int pageIndex = CECRequest.GetQueryInt("page", 1);
            int catId = CECRequest.GetQueryInt("catid", 0);

            //初始化栏目类别
            var catList = VideoCatService.List().ToList();
            ViewBag.CatList = catList;

            var list = VideoService.List(new VideoSearchSetting()
            {
                PageIndex = pageIndex,
                CategoryId = catId,
                ShowDeleted = true
            });

            ViewBag.List = list;
            return View();
        }
        /// <summary>
        /// 添加或编辑视频
        /// </summary>
        /// <returns></returns>
        public ActionResult Add() {
            int id = CECRequest.GetQueryInt("id", 0);
            var model = VideoService.GetById(id);

            //初始化栏目类别
            var catList = VideoCatService.List().ToList();
            ViewBag.CatList = catList;

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(VideoInfo oldModel, FormCollection fc)
        {
            bool isAdd = true;
            if (oldModel.Id > 0)
            {
                isAdd = false;
            }
            
            //判断标题是否为空
            bool errors = false;
            //所选分类
            oldModel.CategoryId = Utils.StrToInt(fc["select_column"], 0);
            if(oldModel.CategoryId == 0){
                ModelState.AddModelError("CATNOTSELECTED", "请选择视频所属分类");
                errors = true;
            }
            if(string.IsNullOrEmpty(oldModel.Title)){
                ModelState.AddModelError("TITLEEMPTY","标题不能为空");
                errors = true;
            }
            

            if(!errors && ModelState.IsValid){
                oldModel = VideoService.Create(oldModel);
                //完成，提示信息
                if (isAdd)
                {
                    ViewBag.Msg = "添加成功！<a href=\"add\">继续？</a><span class=\"ml10\">或</span><a href=\"list\" class=\"ml10\">返回</a>";
                }
                else
                {
                    ViewBag.Msg = "修改成功！<a href=\"add\">添加新视频？</a><span class=\"ml10\">或</span><a href=\"list\" class=\"ml10\">返回</a>";
                }
            }
            //初始化栏目类别
            var catList = VideoCatService.List().ToList();
            ViewBag.CatList = catList;

            return View(oldModel);
        }

    }
}
