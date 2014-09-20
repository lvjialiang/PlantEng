using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantEng.Common;
using Controleng.Common;

namespace PlantEng.Web.WWW.Controllers
{
    public class AjaxController : Controller
    {
        /// <summary>
        /// 文本框上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImageForInput() {
            int errorCode = 0; //0:没有错误,1:没有选择文件,2:格式错误,3:文件大约3M
            string fileName = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            try
            {
                HttpPostedFileBase postedFile = files[0];

                //ajaxUpload中 0:需要选择图片上传 1:照片格式有误 2:图片大小不能超过3兆
                fileName = FileUpload.UploadImage(postedFile, out errorCode);
                switch (errorCode)
                {
                    case 1:
                        fileName = "0";
                        break;
                    case 2:
                        fileName = "1";
                        break;
                    case 3:
                        fileName = "2";
                        break;
                }
            }
            catch { }
            return Content(fileName);
        }
        /// <summary>
        /// 编辑器中的上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImageForEditor() {
            int errorCode = 0; //0:没有错误,1:没有选择文件,2:格式错误,3:文件大约3M
            string fileName = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            try
            {
                HttpPostedFileBase postedFile = files[0];

                //编辑器中 0:需要选择图片上传 1:照片格式有误 2:图片大小不能超过3兆
                fileName = FileUpload.UploadImage(postedFile, out errorCode);
                switch (errorCode)
                {
                    case 1:
                        fileName = "0";
                        break;
                    case 2:
                        fileName = "1";
                        break;
                    case 3:
                        fileName = "2";
                        break;
                }
            }
            catch { }
            return Content(fileName);
        }
        /// <summary>
        /// 更新视频播放次数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void UpdateVideoPlayCount(FormCollection fc) {
            int videoId = Utils.StrToInt(fc["videoid"],0);
            PlantEng.Services.Video.VideoService.UpdatePlayCount(videoId);
        }
    }
}
